using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Data;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Controls
{
    
    public partial class UcDashboard : UserControl
    {
        public UcDashboard()
        {
            InitializeComponent();

            Tag = "Trang chủ"; 
            BackColor = Theme.Background;
            Font = new Font("Segoe UI", 9.5f);

            // Áp dụng style DataGridView
            UIHelper.StyleDataGridView(dgvGanDay);

           
            dgvGanDay.ReadOnly = true;
            dgvGanDay.AllowUserToAddRows = false;
            dgvGanDay.AllowUserToDeleteRows = false;
            dgvGanDay.RowHeadersVisible = false;

         
            lblGiaTriSoDu.ForeColor = Theme.Primary;
            lblTieuDeBang.ForeColor = Theme.Text;
            lblBangFoot.ForeColor = Theme.TextSub;

            pnlKpiThu.BackColor = Theme.White;
            pnlKpiChi.BackColor = Theme.White;
            pnlKpiLech.BackColor = Theme.White;
            pnlKpiSoDu.BackColor = Theme.White;

            // Gán text động cho lblMoTa
            string ky = "Tháng " + DateTime.Now.Month + "/" + DateTime.Now.Year;
            lblMoTaThu.Text = ky + " — chỉ loại Thu";
            lblMoTaChi.Text = ky + " — chỉ loại Chi";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // ---- KPI: thu/chi tháng hiện tại ----
                    using (SqlCommand cmdKpi = new SqlCommand(
                        @"SELECT SUM(CASE WHEN LoaiGiaoDich = @LoaiThu THEN SoTien ELSE 0 END) AS TongThu,
                                 SUM(CASE WHEN LoaiGiaoDich = @LoaiChi THEN SoTien ELSE 0 END) AS TongChi
                          FROM GiaoDich
                          WHERE NguoiDungID = @NguoiDungID
                            AND LoaiGiaoDich IN (@LoaiThu, @LoaiChi)
                            AND YEAR(NgayGiaoDich) = @Nam AND MONTH(NgayGiaoDich) = @Thang;", conn))
                    {
                        cmdKpi.Parameters.Add("@LoaiThu", SqlDbType.NVarChar, 20).Value = GiaoDichLoai.Thu;
                        cmdKpi.Parameters.Add("@LoaiChi", SqlDbType.NVarChar, 20).Value = GiaoDichLoai.Chi;
                        cmdKpi.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                        cmdKpi.Parameters.Add("@Nam", SqlDbType.Int).Value = DateTime.Now.Year;
                        cmdKpi.Parameters.Add("@Thang", SqlDbType.Int).Value = DateTime.Now.Month;

                        using (SqlDataReader r = cmdKpi.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                decimal tongThu = r.IsDBNull(0) ? 0m : r.GetDecimal(0);
                                decimal tongChi = r.IsDBNull(1) ? 0m : r.GetDecimal(1);
                                decimal lech = tongThu - tongChi;

                                lblGiaTriThu.Text = UIHelper.FormatTien(tongThu);
                                lblGiaTriThu.ForeColor = Theme.Thu;
                                lblGiaTriChi.Text = UIHelper.FormatTien(tongChi);
                                lblGiaTriChi.ForeColor = Theme.Chi;
                                lblGiaTriLech.Text = UIHelper.FormatTien(lech);
                                lblGiaTriLech.ForeColor = lech >= 0 ? Theme.Thu : Theme.Chi;
                            }
                        }
                    }

                    // ---- Tổng số dư ----
                    decimal soDuAll = 0m;
                    using (SqlCommand cmdSoDu = new SqlCommand(
                        @"SELECT V.ViTienID,
                                 V.SoDuBanDau
                                 + ISNULL((SELECT SUM(G.SoTien) FROM GiaoDich G WHERE G.ViDichID = V.ViTienID), 0)
                                 - ISNULL((SELECT SUM(G.SoTien) FROM GiaoDich G WHERE G.ViNguonID = V.ViTienID), 0) AS SoDu
                          FROM ViTien V
                          WHERE V.NguoiDungID = @NguoiDungID AND V.TrangThai = 1;", conn))
                    {
                        cmdSoDu.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                        int soVi = 0;
                        using (SqlDataReader r = cmdSoDu.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                soVi++;
                                soDuAll += r.GetDecimal(1);
                            }
                            lblGiaTriSoDu.Text = UIHelper.FormatTien(soDuAll);
                        }
                        lblGiaTriSoDu.Tag = soVi;
                        lblMoTaSoDu.Text = soVi + " ví đang hoạt động";
                    }

                    // ---- Giao dịch gần đây ----
                    using (SqlCommand cmdCount = new SqlCommand(
                        @"SELECT COUNT(*) FROM GiaoDich WHERE NguoiDungID = @NguoiDungID;", conn))
                    {
                        cmdCount.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                        int tongSoGD = Convert.ToInt32(cmdCount.ExecuteScalar());
                        lblBangFoot.Text = "Hiển thị " + tongSoGD + " giao dịch";
                    }

                    using (SqlCommand cmdGD = new SqlCommand(
                        @"SELECT 
                              G.NgayGiaoDich, G.LoaiGiaoDich, G.SoTien, G.GhiChu,
                              DM.TenDanhMuc, VN.TenVi AS ViNguon, VD.TenVi AS ViDich,
                              KV.TenNguoiLienQuan, MT.TenMucTieu
                          FROM GiaoDich G
                          LEFT JOIN DanhMuc DM         ON G.DanhMucID     = DM.DanhMucID
                          LEFT JOIN ViTien VN          ON G.ViNguonID     = VN.ViTienID
                          LEFT JOIN ViTien VD          ON G.ViDichID      = VD.ViTienID
                          LEFT JOIN KhoanVayNo KV      ON G.KhoanVayNoID  = KV.KhoanVayNoID
                          LEFT JOIN MucTieuTietKiem MT ON G.MucTieuID     = MT.MucTieuID
                          WHERE G.NguoiDungID = @NguoiDungID
                          ORDER BY G.NgayGiaoDich DESC, G.NgayTao DESC;", conn))
                    {
                        cmdGD.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;

                        dgvGanDay.Rows.Clear();
                        using (SqlDataReader r = cmdGD.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                DateTime ngay = r.GetDateTime(r.GetOrdinal("NgayGiaoDich"));
                                string loai = r.GetString(r.GetOrdinal("LoaiGiaoDich"));
                                decimal soTien = r.GetDecimal(r.GetOrdinal("SoTien"));
                                string danhMuc = DocChuoi(r, "TenDanhMuc");
                                string lienQuan = DocChuoi(r, "TenNguoiLienQuan");
                                string mucTieu = DocChuoi(r, "TenMucTieu");
                                string viNguon = DocChuoi(r, "ViNguon");
                                string viDich = DocChuoi(r, "ViDich");
                                string ghiChu = DocChuoi(r, "GhiChu");

                                string khoanHienThi = "—";
                                if (danhMuc.Length > 0) khoanHienThi = danhMuc;
                                else if (lienQuan.Length > 0) khoanHienThi = "Khoản vay " + lienQuan;
                                else if (mucTieu.Length > 0) khoanHienThi = "Mục tiêu " + mucTieu;

                                string viHienThi;
                                if (loai == GiaoDichLoai.ChuyenTien)
                                    viHienThi = viNguon + " → " + viDich;
                                else if (Array.IndexOf(GiaoDichLoai.TienVao, loai) >= 0)
                                    viHienThi = viDich.Length > 0 ? viDich : "—";
                                else
                                    viHienThi = viNguon.Length > 0 ? viNguon : "—";

                                int idx = dgvGanDay.Rows.Add();
                                DataGridViewRow row = dgvGanDay.Rows[idx];
                                row.Cells[0].Value = UIHelper.FormatNgay(ngay);
                                row.Cells[1].Value = UIHelper.NhanLoaiGiaoDich(loai);
                                row.Cells[1].Style.ForeColor = UIHelper.MauTheoLoai(loai);
                                row.Cells[1].Style.SelectionForeColor = UIHelper.MauTheoLoai(loai);
                                row.Cells[2].Value = khoanHienThi;
                                row.Cells[3].Value = ghiChu.Length > 0 ? ghiChu : "—";
                                row.Cells[4].Value = viHienThi;
                                row.Cells[5].Value = UIHelper.FormatTienCoHuong(soTien, loai);
                                row.Cells[5].Style.ForeColor = UIHelper.MauTheoLoai(loai);
                                row.Cells[5].Style.SelectionForeColor = UIHelper.MauTheoLoai(loai);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không tải được dữ liệu dashboard. Hãy kiểm tra cấu hình LocalDB.",
                    "Lỗi truy cập dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string DocChuoi(SqlDataReader r, string tenCot)
        {
            int i = r.GetOrdinal(tenCot);
            return r.IsDBNull(i) ? "" : r.GetString(i);
        }
    }
}