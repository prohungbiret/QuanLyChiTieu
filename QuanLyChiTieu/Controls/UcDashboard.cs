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
    // UserControl mẫu theo khung C.7 + mockup dashboard: 4 thẻ KPI + giao dịch gần đây.
    // Mức thỏa thuận: KPI + bảng (biểu đồ cột/tròn thuộc TV3 — Thống kê).
    // Toàn bộ từ DB thật theo CurrentUser (FR06), không dùng số minh họa.
    public class UcDashboard : UserControl
    {
        private readonly Label lblGiaTriThu = new Label();
        private readonly Label lblGiaTriChi = new Label();
        private readonly Label lblGiaTriLech = new Label();
        private readonly Label lblGiaTriSoDu = new Label();
        private readonly Label lblTongSoDuSub = new Label();
        private readonly DataGridView dgvGanDay = new DataGridView();
        private readonly Label lblBangFoot = new Label();

        public UcDashboard()
        {
            Tag = "Trang chủ"; // Hợp đồng 5.3: FrmMain đọc Tag vào header title
            BackColor = Theme.Background;
            Font = new Font("Segoe UI", 9.5f);

            // ---- Khu vực 4 thẻ KPI: TableLayoutPanel 4 cột (mockup: .kpis) ----
            var pnlKpis = new TableLayoutPanel();
            pnlKpis.ColumnCount = 4;
            pnlKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            pnlKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            pnlKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            pnlKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            pnlKpis.Dock = DockStyle.Top;
            pnlKpis.Height = 118;
            pnlKpis.Margin = new Padding(0, 0, 0, Theme.GapLon);

            string ky = "Tháng " + DateTime.Now.Month + "/" + DateTime.Now.Year;
            pnlKpis.Controls.Add(TaoTheKpi("Tổng thu trong kỳ", ky + " — chỉ loại Thu", lblGiaTriThu), 0, 0);
            pnlKpis.Controls.Add(TaoTheKpi("Tổng chi trong kỳ", ky + " — chỉ loại Chi", lblGiaTriChi), 1, 0);
            pnlKpis.Controls.Add(TaoTheKpi("Chênh lệch thu chi", "Tổng thu − tổng chi", lblGiaTriLech), 2, 0);
            pnlKpis.Controls.Add(TaoTheKpi("Tổng số dư các ví", "Các ví đang hoạt động", lblGiaTriSoDu), 3, 0);
            lblGiaTriSoDu.ForeColor = Theme.Primary;

            // ---- Tiêu đề bảng ----
            var lblTieuDeBang = new Label();
            lblTieuDeBang.Text = "Giao dịch gần đây";
            lblTieuDeBang.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            lblTieuDeBang.ForeColor = Theme.Text;
            lblTieuDeBang.Dock = DockStyle.Top;
            lblTieuDeBang.Height = 26;

            // ---- Bảng giao dịch gần đây (StyleDataGridView dùng chung) ----
            UIHelper.StyleDataGridView(dgvGanDay);
            dgvGanDay.ReadOnly = true;
            dgvGanDay.AllowUserToAddRows = false;
            dgvGanDay.AllowUserToDeleteRows = false;
            dgvGanDay.RowHeadersVisible = false;
            dgvGanDay.Dock = DockStyle.Fill;
            dgvGanDay.Columns.Add("colNgay", "Ngày");
            dgvGanDay.Columns.Add("colLoai", "Loại");
            dgvGanDay.Columns.Add("colKhoan", "Danh mục / Khoản");
            dgvGanDay.Columns.Add("colGhiChu", "Ghi chú");
            dgvGanDay.Columns.Add("colVi", "Ví liên quan");
            dgvGanDay.Columns.Add("colSoTien", "Số tiền");
            dgvGanDay.Columns["colNgay"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvGanDay.Columns["colNgay"].Width = 96;
            dgvGanDay.Columns["colLoai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvGanDay.Columns["colLoai"].Width = 110;
            dgvGanDay.Columns["colSoTien"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvGanDay.Columns["colSoTien"].Width = 140;
            dgvGanDay.Columns["colSoTien"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            // ---- Foot bảng (số bản ghi — UI Standard 3.5) ----
            lblBangFoot.Text = "";
            lblBangFoot.Font = new Font("Segoe UI", 8.5f);
            lblBangFoot.ForeColor = Theme.TextSub;
            lblBangFoot.Dock = DockStyle.Bottom;
            lblBangFoot.Height = 20;

            // Thứ tự add: Fill trước, rồi Bottom, rồi Top
            // (docking bố trí đảo z-order: phần tử add sau chiếm mép trước)
            Controls.Add(dgvGanDay);
            Controls.Add(lblBangFoot);
            Controls.Add(lblTieuDeBang);
            Controls.Add(pnlKpis);
        }

        // Tạo 1 thẻ KPI: nền trắng, tiêu đề nhỏ xám, giá trị lớn đậm, mô tả xám.
        private static Panel TaoTheKpi(string tieuDe, string moTa, Label lblGiaTri)
        {
            var the = new Panel();
            the.BackColor = Theme.White;
            the.Dock = DockStyle.Fill;
            the.Padding = new Padding(14, 12, 14, 12);
            the.Margin = new Padding(0, 0, 12, 0);

            lblGiaTri.Text = "—";
            lblGiaTri.Font = new Font("Segoe UI", 15f, FontStyle.Bold);
            lblGiaTri.ForeColor = Theme.Text;
            lblGiaTri.Dock = DockStyle.Top;
            lblGiaTri.Height = 34;

            var lblTieuDe = new Label();
            lblTieuDe.Text = tieuDe;
            lblTieuDe.Font = new Font("Segoe UI", 8.5f);
            lblTieuDe.ForeColor = Theme.TextSub;
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Height = 20;

            var lblMoTa = new Label();
            lblMoTa.Text = moTa;
            lblMoTa.Font = new Font("Segoe UI", 8f);
            lblMoTa.ForeColor = Theme.TextSub;
            lblMoTa.Dock = DockStyle.Bottom;
            lblMoTa.Height = 34;

            the.Controls.Add(lblGiaTri);
            the.Controls.Add(lblMoTa);
            the.Controls.Add(lblTieuDe);
            return the;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // ---- KPI: thu/chi tháng hiện tại (chỉ Thu/Chi theo QD02) ----
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

                    // ---- Tổng số dư: công thức 2.6, chỉ ví đang hoạt động (QD03) ----
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
                            lblTongSoDuSub.Visible = false; // placeholder (không dùng thêm)
                        }
                        lblGiaTriSoDu.Tag = soVi;
                        lblTongSoDuSub.Text = soVi + " ví đang hoạt động";
                    }

                    // ---- Giao dịch gần đây: sort NgayGiaoDich DESC rồi NgayTao DESC (QD09) ----
                    using (SqlCommand cmdCount = new SqlCommand(
                        @"SELECT COUNT(*) FROM GiaoDich WHERE NguoiDungID = @NguoiDungID;", conn))
                    {
                        cmdCount.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                        int tongSoGD = Convert.ToInt32(cmdCount.ExecuteScalar());
                        lblBangFoot.Text = "Hiển thị 9 giao dịch gần nhất trong " + tongSoGD + " giao dịch";
                    }

                    using (SqlCommand cmdGD = new SqlCommand(
                        @"SELECT TOP 9
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
                // NFR07: thông báo phù hợp, không lộ connection string (C.7)
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
