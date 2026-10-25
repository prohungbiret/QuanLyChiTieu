using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using QuanLyChiTieu.Data;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Services
{
    /// <summary>Data đã chuẩn bị cho xuất báo cáo: bảng hiển thị + tổng kết (QD02:
    /// Tong thu/chi CHỈ tính loại Thu/Chi — các loại khác hiển thị nhưng không tổng).</summary>
    public class BaoCaoDuLieu
    {
        public DataTable Bang;          // đã định dạng string sẵn (ngày dd/MM/yyyy, tiền #,0)
        public decimal TongThu { get; set; }
        public decimal TongChi { get; set; }
        public int SoGiaoDich { get; set; }
        public int SoGiaoDichNgoaiThuChi { get; set; }
    }

    /// <summary>Tham số chọn từ FrmXuatBaoCao.</summary>
    public class BaoCaoYeuCau
    {
        public DateTime Tu;
        public DateTime Den;
        /// <summary>"ThuChi" = chỉ các loại giao dịch thu chi; "TatCa" = đủ 9 loại (Bảng 2.2e).</summary>
        public string PhamVi;
    }

    // TV1 (thay TV4) — xuất báo cáo Excel (FR phần Báo cáo, mức Interop đồng bộ TV2):
    //  - Chỉ dữ liệu của CurrentUser (L006 — luôn kèm NguoiDungID trong SQL).
    //  - TongThu/TongChi chỉ cộng loại Thu/Chi (QD02); các loại khác chỉ liệt kê.
    //  - Hủy COM object sau khi ghi file (đổi máy/xong tránh giữ process Excel).
    public static class BaoCaoService
    {
        // ---------------- Đọc số liệu ----------------

        public static BaoCaoDuLieu LayDuLieu(BaoCaoYeuCau yc)
        {
            bool chiThuChi = yc.PhamVi != "TatCa";
            string locLoai = chiThuChi
                ? @"AND G.LoaiGiaoDich IN (N'Thu', N'Chi')"
                : "";

            string sql = @"SELECT G.NgayGiaoDich, G.LoaiGiaoDich, DM.TenDanhMuc,
                                  G.SoTien, G.GhiChu,
                                  COALESCE(VD.TenVi, VN.TenVi) AS TenVi
                           FROM GiaoDich G
                           LEFT JOIN DanhMuc DM ON DM.DanhMucID = G.DanhMucID
                                               AND DM.NguoiDungID = G.NguoiDungID
                           LEFT JOIN ViTien VN ON VN.ViTienID = G.ViNguonID
                                              AND VN.NguoiDungID = G.NguoiDungID
                           LEFT JOIN ViTien VD ON VD.ViTienID = G.ViDichID
                                              AND VD.NguoiDungID = G.NguoiDungID
                           WHERE G.NguoiDungID = @NguoiDungID
                             " + locLoai + @"
                             AND G.NgayGiaoDich >= @Tu AND G.NgayGiaoDich <= @Den
                           ORDER BY G.NgayGiaoDich ASC, G.NgayTao ASC;";

            var dl = new BaoCaoDuLieu();
            dl.Bang = new DataTable("BaoCao");
            dl.Bang.Columns.Add("Ngày", typeof(string));
            dl.Bang.Columns.Add("Loại giao dịch", typeof(string));
            dl.Bang.Columns.Add("Số tiền (đ)", typeof(string));
            dl.Bang.Columns.Add("Danh mục", typeof(string));
            dl.Bang.Columns.Add("Ví", typeof(string));
            dl.Bang.Columns.Add("Ghi chú", typeof(string));

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                cmd.Parameters.Add("@Tu", SqlDbType.Date).Value = yc.Tu.Date;
                cmd.Parameters.Add("@Den", SqlDbType.Date).Value = yc.Den.Date;
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        string loai = r.GetString(r.GetOrdinal("LoaiGiaoDich"));
                        decimal soTien = r.GetDecimal(r.GetOrdinal("SoTien"));
                        if (loai == GiaoDichLoai.Thu) dl.TongThu += soTien;
                        else if (loai == GiaoDichLoai.Chi) dl.TongChi += soTien;
                        else dl.SoGiaoDichNgoaiThuChi++;

                        dl.SoGiaoDich++;

                        dl.Bang.Rows.Add(
                            UIHelper.FormatNgay(r.GetDateTime(r.GetOrdinal("NgayGiaoDich"))),
                            UIHelper.NhanLoaiGiaoDich(loai),
                            soTien.ToString("#,0"),
                            r["TenDanhMuc"] == DBNull.Value ? "" : r["TenDanhMuc"].ToString(),
                            r["TenVi"] == DBNull.Value ? "" : r["TenVi"].ToString(),
                            r["GhiChu"] == DBNull.Value ? "" : r["GhiChu"].ToString());
                    }
                }
            }
            return dl;
        }

        // ---------------- Xuất Excel (Interop — cùng lib với phân hệ Giao dịch) ----------------

        /// <summary>Ghi file .xlsx. Trả về true khi thành công; thất bại → false + loi.</summary>
        public static bool XuatExcel(BaoCaoDuLieu dl, DateTime tu, DateTime den,
                                     string duongDan, out string loi)
        {
            loi = null;
            Excel.Application exApp = null;
            Excel.Workbook exBook = null;
            Excel.Worksheet exSheet = null;

            try
            {
                exApp = new Excel.Application();
                exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                exSheet = (Excel.Worksheet)exBook.Worksheets[1];
                Excel.Range rng = exSheet.Cells[1, 1];

                // Tiêu đề
                rng.Range["A2:F2"].Merge();
                rng.Range["A2"].Value = "BÁO CÁO GIAO DỊCH CHI TIÊU CÁ NHÂN";
                rng.Range["A2"].Font.Size = 16;
                rng.Range["A2"].Font.Name = "Times New Roman";
                rng.Range["A2"].Font.Bold = true;
                rng.Range["A2"].Font.Color =
                    System.Drawing.ColorTranslator.ToOle(
                        System.Drawing.Color.FromArgb(23, 43, 77));
                rng.Range["A2"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Thông tin xuất
                rng.Range["A3:F3"].Merge();
                rng.Range["A3"].Value = "Người xuất: " + CurrentUser.TenHienThi +
                    " | Kỳ: " + UIHelper.FormatNgay(tu) + " – " + UIHelper.FormatNgay(den) +
                    " | Xuất lúc: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                rng.Range["A3"].Font.Size = 11;
                rng.Range["A3"].Font.Name = "Times New Roman";
                rng.Range["A3"].Font.Italic = true;
                rng.Range["A3"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Hàng tiêu đề cột
                string[] tenCot =
                {
                    "Ngày", "Loại giao dịch", "Số tiền (đ)", "Danh mục", "Ví", "Ghi chú"
                };
                for (int c = 0; c < tenCot.Length; c++)
                    rng.Range[Convert.ToChar('A' + c) + "4"].Value = tenCot[c];
                rng.Range["A4:F4"].Font.Size = 11;
                rng.Range["A4:F4"].Font.Name = "Times New Roman";
                rng.Range["A4:F4"].Font.Bold = true;
                rng.Range["A4:F4"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Nội dung
                int hang = 5;
                foreach (DataRow r in dl.Bang.Rows)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        string giaTri = r[c] == null ? "" : r[c].ToString();
                        if (c == 2) giaTri = "'" + giaTri; // tiền căn phải dạng text, giữ dấu phân vùng
                        rng.Range[Convert.ToChar('A' + c) + hang.ToString()].Value = giaTri;
                    }
                    hang++;
                }

                rng.Range["A4:F" + (hang - 1)].Borders.LineStyle = 1;
                rng.Range["C5:C" + (hang - 1)].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                // Tổng kết (QD02 — chỉ Thu/Chi)
                if (dl.Bang.Rows.Count == 0)
                {
                    rng.Range["B" + hang].Value = "(Không có dữ liệu trong kỳ)";
                }
                else
                {
                    rng.Range["B" + hang].Value = "Tổng thu:";
                    rng.Range["C" + hang].Value = UIHelper.FormatTien(dl.TongThu);
                    rng.Range["B" + hang].Font.Bold = true;
                    rng.Range["C" + hang].Font.Bold = true;

                    rng.Range["B" + (hang + 1)].Value = "Tổng chi:";
                    rng.Range["C" + (hang + 1)].Value = UIHelper.FormatTien(dl.TongChi);
                    rng.Range["B" + (hang + 1)].Font.Bold = true;
                    rng.Range["C" + (hang + 1)].Font.Bold = true;

                    rng.Range["B" + (hang + 2)].Value = "Chênh lệch (thu − chi):";
                    rng.Range["C" + (hang + 2)].Value = UIHelper.FormatTien(dl.TongThu - dl.TongChi);
                    rng.Range["B" + (hang + 2)].Font.Bold = true;
                    rng.Range["C" + (hang + 2)].Font.Bold = true;

                    if (dl.SoGiaoDichNgoaiThuChi > 0)
                    {
                        rng.Range["B" + (hang + 3)].Value =
                            "(Có " + dl.SoGiaoDichNgoaiThuChi + " giao dịch ngoài thu/chi — không tính tổng, QD02)";
                        rng.Range["B" + (hang + 3)].Font.Italic = true;
                    }
                }

                exSheet.Columns.AutoFit();

                if (System.IO.File.Exists(duongDan)) System.IO.File.Delete(duongDan);
                exBook.SaveAs(duongDan);
                return true;
            }
            catch (Exception ex)
            {
                loi = "Không xuất được file Excel. " + ex.Message;
                return false;
            }
            finally
            {
                if (exBook != null) { exBook.Close(false); Marshal.FinalReleaseComObject(exBook); }
                if (exApp != null) { exApp.Quit(); Marshal.FinalReleaseComObject(exApp); }
                if (exSheet != null) Marshal.FinalReleaseComObject(exSheet);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
