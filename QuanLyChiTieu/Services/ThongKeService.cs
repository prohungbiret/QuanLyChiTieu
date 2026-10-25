using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyChiTieu.Data;
using QuanLyChiTieu.Models;

namespace QuanLyChiTieu.Services
{
    /// <summary>Tổng thu/chi của một tháng (chỉ loại Thu + Chi).</summary>
    public class ThuChiThang
    {
        public int Nam { get; set; }
        public int Thang { get; set; }
        public decimal TongThu { get; set; }
        public decimal TongChi { get; set; }
        public decimal ChenhLech { get { return TongThu - TongChi; } }
        public string NhanThang { get { return Thang.ToString("00") + "/" + Nam; } }
    }

    /// <summary>Tổng tiền theo một danh mục trong kỳ thống kê.</summary>
    public class ThongKeDanhMuc
    {
        public int DanhMucID { get; set; }
        public string TenDanhMuc { get; set; }
        public string LoaiDanhMuc { get; set; }
        public decimal TongTien { get; set; }
        public int SoGiaoDich { get; set; }
        /// <summary>Tỉ trọng (%) trong tổng của cùng loại Thu/Chi — gán ở tầng gọi khi đã có tổng.</summary>
        public decimal TyTrong { get; set; }
    }

    /// <summary>Tổng quan một kỳ: tổng thu, tổng chi, chênh lệch, số giao dịch Thu/Chi.</summary>
    public class TongQuanKy
    {
        public decimal TongThu { get; set; }
        public decimal TongChi { get; set; }
        public decimal ChenhLech { get { return TongThu - TongChi; } }
        public int SoGiaoDich { get; set; }
    }

    // TV3 (Hải) sở hữu — phân hệ Thống kê & báo cáo (FR07, Tài liệu 06 mục 6.4).
    // Quy tắc cốt lõi (QD02, Bảng 2.2e, Tài liệu 01 mục 1.5 / 2.7):
    //  - Báo cáo thu/chi CHỈ tính hai loại Thu và Chi.
    //    ChuyenTien, NhanVay, TraNo, ChoVay, ThuHoiNo, DongGop, RutTietKiem
    //    KHÔNG bao giờ xuất hiện trong bất kỳ con số nào của service này
    //    (TC-RPT-03, TC-RPT-10, TC-ADV-09).
    //  - Tổng theo tháng lấy từ view chung vwThuChiThang của TV1 (mục 6.7).
    //  - Mọi truy vấn gắn NguoiDungID = CurrentUser, SQL tham số (NFR03, mục 2.8).
    public static class ThongKeService
    {
        // ---------------- Theo tháng ----------------

        /// <summary>Tổng quan của đúng một tháng/năm.</summary>
        public static TongQuanKy LayTongQuanThang(int thang, int nam)
        {
            var kq = new TongQuanKy();
            string sql = @"SELECT ISNULL(TongThu, 0) AS TongThu, ISNULL(TongChi, 0) AS TongChi
                           FROM vwThuChiThang
                           WHERE NguoiDungID = @NguoiDungID AND Thang = @Thang AND Nam = @Nam;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                cmd.Parameters.Add("@Thang", SqlDbType.Int).Value = thang;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = nam;
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        kq.TongThu = r.GetDecimal(0);
                        kq.TongChi = r.GetDecimal(1);
                    }
                }
            }
            kq.SoGiaoDich = DemGiaoDichThuChi(
                new DateTime(nam, thang, 1),
                new DateTime(nam, thang, 1).AddMonths(1).AddDays(-1));
            return kq;
        }

        /// <summary>
        /// Chuỗi thu/chi liên tục soThang tháng kết thúc tại (thang, nam) — cho biểu đồ cột.
        /// Tháng không có giao dịch vẫn xuất hiện với giá trị 0 (TC-RPT-01, TC-RPT-07).
        /// </summary>
        public static List<ThuChiThang> LayThuChiTheoThang(int thang, int nam, int soThang)
        {
            if (soThang < 1) soThang = 1;
            if (soThang > 24) soThang = 24;

            var theoThang = new Dictionary<int, ThuChiThang>();
            string sql = @"SELECT Nam, Thang, TongThu, TongChi FROM vwThuChiThang
                           WHERE NguoiDungID = @NguoiDungID
                             AND (Nam * 100 + Thang) BETWEEN @TuKy AND @DenKy
                           ORDER BY Nam, Thang;";

            DateTime den = new DateTime(nam, thang, 1);
            DateTime tu = den.AddMonths(-(soThang - 1));

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                cmd.Parameters.Add("@TuKy", SqlDbType.Int).Value = tu.Year * 100 + tu.Month;
                cmd.Parameters.Add("@DenKy", SqlDbType.Int).Value = den.Year * 100 + den.Month;
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var t = new ThuChiThang
                        {
                            Nam = r.GetInt32(0),
                            Thang = r.GetInt32(1),
                            TongThu = r.GetDecimal(2),
                            TongChi = r.GetDecimal(3)
                        };
                        theoThang[t.Nam * 100 + t.Thang] = t;
                    }
                }
            }

            var ds = new List<ThuChiThang>();
            for (int i = 0; i < soThang; i++)
            {
                DateTime t = tu.AddMonths(i);
                ThuChiThang co;
                if (!theoThang.TryGetValue(t.Year * 100 + t.Month, out co))
                {
                    co = new ThuChiThang { Nam = t.Year, Thang = t.Month, TongThu = 0m, TongChi = 0m };
                }
                ds.Add(co);
            }
            return ds;
        }

        /// <summary>Phân bổ theo danh mục trong một tháng — tách Thu và Chi (cho biểu đồ tròn).</summary>
        public static List<ThongKeDanhMuc> LayTheoDanhMucThang(int thang, int nam, string loaiDanhMuc)
        {
            DateTime tu = new DateTime(nam, thang, 1);
            DateTime den = tu.AddMonths(1).AddDays(-1);
            return LayTheoDanhMuc(tu, den, loaiDanhMuc);
        }

        // ---------------- Theo khoảng ngày ----------------

        /// <summary>Tổng quan của một khoảng ngày tùy chọn [tuNgay, denNgay].</summary>
        public static TongQuanKy LayTongQuanKhoangNgay(DateTime tuNgay, DateTime denNgay)
        {
            var kq = new TongQuanKy();
            string sql = @"SELECT
                               SUM(CASE WHEN G.LoaiGiaoDich = @LoaiThu THEN G.SoTien ELSE 0 END) AS TongThu,
                               SUM(CASE WHEN G.LoaiGiaoDich = @LoaiChi THEN G.SoTien ELSE 0 END) AS TongChi,
                               COUNT(*) AS SoGiaoDich
                           FROM GiaoDich G
                           WHERE G.NguoiDungID = @NguoiDungID
                             AND G.LoaiGiaoDich IN (@LoaiThu, @LoaiChi)
                             AND G.NgayGiaoDich >= @TuNgay AND G.NgayGiaoDich <= @DenNgay;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                GanThamSoThuChi(cmd, tuNgay, denNgay);
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        kq.TongThu = r.IsDBNull(0) ? 0m : r.GetDecimal(0);
                        kq.TongChi = r.IsDBNull(1) ? 0m : r.GetDecimal(1);
                        kq.SoGiaoDich = r.IsDBNull(2) ? 0 : r.GetInt32(2);
                    }
                }
            }
            return kq;
        }

        /// <summary>
        /// Phân bổ theo danh mục trong khoảng ngày, chỉ một loại ("Thu" hoặc "Chi").
        /// Đã tính sẵn TyTrong (%) theo tổng của loại đó trong kỳ.
        /// </summary>
        public static List<ThongKeDanhMuc> LayTheoDanhMuc(DateTime tuNgay, DateTime denNgay, string loaiDanhMuc)
        {
            var ds = new List<ThongKeDanhMuc>();
            string sql = @"SELECT G.DanhMucID, DM.TenDanhMuc, DM.LoaiDanhMuc,
                                  SUM(G.SoTien) AS TongTien, COUNT(*) AS SoGiaoDich
                           FROM GiaoDich G
                           JOIN DanhMuc DM ON DM.DanhMucID = G.DanhMucID
                                          AND DM.NguoiDungID = G.NguoiDungID
                           WHERE G.NguoiDungID = @NguoiDungID
                             AND G.LoaiGiaoDich = @Loai
                             AND DM.LoaiDanhMuc = @Loai
                             AND G.NgayGiaoDich >= @TuNgay AND G.NgayGiaoDich <= @DenNgay
                           GROUP BY G.DanhMucID, DM.TenDanhMuc, DM.LoaiDanhMuc
                           ORDER BY TongTien DESC;";

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                // Loại giao dịch và loại danh mục luôn khớp nhau (Thu<->Thu, Chi<->Chi, Bảng 2.4.4)
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                cmd.Parameters.Add("@Loai", SqlDbType.NVarChar, 20).Value = loaiDanhMuc;
                cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = tuNgay.Date;
                cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = denNgay.Date;
                conn.Open();
                decimal tong = 0m;
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var tk = new ThongKeDanhMuc
                        {
                            DanhMucID = r.GetInt32(0),
                            TenDanhMuc = r.GetString(1),
                            LoaiDanhMuc = r.GetString(2),
                            TongTien = r.GetDecimal(3),
                            SoGiaoDich = r.GetInt32(4)
                        };
                        tong += tk.TongTien;
                        ds.Add(tk);
                    }
                }
                if (tong > 0m)
                    foreach (ThongKeDanhMuc tk in ds) tk.TyTrong = tk.TongTien * 100m / tong;
            }
            return ds;
        }

        /// <summary>Chi tiết giao dịch Thu/Chi trong kỳ — bảng chi tiết ở màn Thống kê.</summary>
        public static DataTable LayChiTiet(DateTime tuNgay, DateTime denNgay)
        {
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
                             AND G.LoaiGiaoDich IN (@LoaiThu, @LoaiChi)
                             AND G.NgayGiaoDich >= @TuNgay AND G.NgayGiaoDich <= @DenNgay
                           ORDER BY G.NgayGiaoDich DESC, G.NgayTao DESC;";

            var bang = new DataTable();
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                GanThamSoThuChi(cmd, tuNgay, denNgay);
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    bang.Load(r);
                }
            }
            return bang;
        }

        // ---------------- Hỗ trợ ----------------

        private static int DemGiaoDichThuChi(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"SELECT COUNT(*) FROM GiaoDich
                           WHERE NguoiDungID = @NguoiDungID
                             AND LoaiGiaoDich IN (@LoaiThu, @LoaiChi)
                             AND NgayGiaoDich >= @TuNgay AND NgayGiaoDich <= @DenNgay;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                GanThamSoThuChi(cmd, tuNgay, denNgay);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private static void GanThamSoThuChi(SqlCommand cmd, DateTime tuNgay, DateTime denNgay)
        {
            cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
            cmd.Parameters.Add("@LoaiThu", SqlDbType.NVarChar, 20).Value = GiaoDichLoai.Thu;
            cmd.Parameters.Add("@LoaiChi", SqlDbType.NVarChar, 20).Value = GiaoDichLoai.Chi;
            cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = tuNgay.Date;
            cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = denNgay.Date;
        }
    }
}
