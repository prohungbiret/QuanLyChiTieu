using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Controls
{
    //  màn Thống kê & báo cáo : theo tháng hoặc khoảng ngày,
    // biểu đồ cột (thu/chi 6 tháng) + biểu đồ tròn (cơ cấu theo danh mục) qua UcChart

    public partial class UcThongKe : UserControl
    {
        private bool dangTai; // tránh vòng lặp sự kiện khi đổi chế độ



        public UcThongKe()
        {
            InitializeComponent();
            UIHelper.StyleDataGridView(dgvDanhMuc);
            UIHelper.StyleDataGridView(dgvChiTiet);
        }

        private void rdoThang_CheckedChanged(object sender, EventArgs e) { DoiCheDo(); }
        private void rdoKhoangNgay_CheckedChanged(object sender, EventArgs e) { DoiCheDo(); }
        private void nudThang_ValueChanged(object sender, EventArgs e) { if (!dangTai) TaiDuLieu(); }
        private void nudNam_ValueChanged(object sender, EventArgs e) { if (!dangTai) TaiDuLieu(); }
        private void dtpTu_ValueChanged(object sender, EventArgs e) { if (!dangTai && rdoKhoangNgay.Checked) TaiDuLieu(); }
        private void dtpDen_ValueChanged(object sender, EventArgs e) { if (!dangTai && rdoKhoangNgay.Checked) TaiDuLieu(); }
        private void cboLoaiTron_SelectedIndexChanged(object sender, EventArgs e) { if (!dangTai) TaiDuLieu(); }
        private void btnXem_Click(object sender, EventArgs e) { TaiDuLieu(); }
        private void btnXuat_Click(object sender, EventArgs e) { MoXuatBaoCao(); }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            dangTai = true;
            if (cboLoaiTron.SelectedIndex < 0 && cboLoaiTron.Items.Count > 0)
                cboLoaiTron.SelectedIndex = 0;
            dangTai = false;

            TaiDuLieu();
        }

        private static Label Nhan(string text)
        {
            var lbl = new Label();
            lbl.Text = text;
            lbl.AutoSize = true;
            lbl.Margin = new Padding(2, 10, 2, 0);
            lbl.ForeColor = Theme.TextSub;
            return lbl;
        }

        private static Panel TaoThe(string tieuDe, Label lblGiaTri, Color mauGiaTri)
        {
            var the = new Panel();
            the.BackColor = Theme.White;
            the.Dock = DockStyle.Fill;
            the.Margin = new Padding(0, 0, 12, 8);
            the.Padding = new Padding(14, 8, 14, 8);

            lblGiaTri.Text = "—";
            lblGiaTri.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            lblGiaTri.ForeColor = mauGiaTri;
            lblGiaTri.Dock = DockStyle.Top;
            lblGiaTri.Height = 32;

            var lblTieuDe = new Label();
            lblTieuDe.Text = tieuDe;
            lblTieuDe.Font = new Font("Segoe UI", 8.5f);
            lblTieuDe.ForeColor = Theme.TextSub;
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Height = 20;

            the.Controls.Add(lblGiaTri);
            the.Controls.Add(lblTieuDe);
            return the;
        }

        private static TableLayoutPanel HopTrang(string tieuDe)
        {
            var hop = new TableLayoutPanel();
            hop.ColumnCount = 1;
            hop.RowCount = 2;
            hop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            hop.RowStyles.Add(new RowStyle(SizeType.Absolute, 28f));
            hop.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            hop.BackColor = Theme.White;
            hop.Dock = DockStyle.Fill;
            hop.Margin = new Padding(0, 0, 12, 10);
            hop.Padding = new Padding(10, 6, 10, 8);

            var lbl = new Label();
            lbl.Text = tieuDe;
            lbl.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lbl.ForeColor = Theme.Text;
            lbl.Dock = DockStyle.Fill;
            hop.Controls.Add(lbl, 0, 0);
            return hop;
        }

        private void CauHinhBangDanhMuc()
        {
            UIHelper.StyleDataGridView(dgvDanhMuc);
            dgvDanhMuc.ReadOnly = true;
            dgvDanhMuc.AllowUserToAddRows = false;
            dgvDanhMuc.AllowUserToDeleteRows = false;
            dgvDanhMuc.RowHeadersVisible = false;
            dgvDanhMuc.Dock = DockStyle.Fill;
            dgvDanhMuc.Columns.Add("colTen", "Danh mục");
            dgvDanhMuc.Columns.Add("colSoGD", "Số GD");
            dgvDanhMuc.Columns.Add("colTong", "Tổng tiền");
            dgvDanhMuc.Columns.Add("colTyTrong", "Tỉ trọng");
            dgvDanhMuc.Columns["colSoGD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvDanhMuc.Columns["colSoGD"].Width = 60;
            dgvDanhMuc.Columns["colSoGD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhMuc.Columns["colTong"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvDanhMuc.Columns["colTong"].Width = 130;
            dgvDanhMuc.Columns["colTong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhMuc.Columns["colTyTrong"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvDanhMuc.Columns["colTyTrong"].Width = 80;
            dgvDanhMuc.Columns["colTyTrong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void CauHinhBangChiTiet()
        {
            UIHelper.StyleDataGridView(dgvChiTiet);
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.AllowUserToDeleteRows = false;
            dgvChiTiet.RowHeadersVisible = false;
            dgvChiTiet.Dock = DockStyle.Fill;
            dgvChiTiet.Columns.Add("colNgay", "Ngày");
            dgvChiTiet.Columns.Add("colLoai", "Loại");
            dgvChiTiet.Columns.Add("colDanhMuc", "Danh mục");
            dgvChiTiet.Columns.Add("colVi", "Ví");
            dgvChiTiet.Columns.Add("colGhiChu", "Ghi chú");
            dgvChiTiet.Columns.Add("colSoTien", "Số tiền");
            dgvChiTiet.Columns["colNgay"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvChiTiet.Columns["colNgay"].Width = 92;
            dgvChiTiet.Columns["colLoai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvChiTiet.Columns["colLoai"].Width = 62;
            dgvChiTiet.Columns["colSoTien"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvChiTiet.Columns["colSoTien"].Width = 130;
            dgvChiTiet.Columns["colSoTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void DoiCheDo()
        {
            bool theoThang = rdoThang.Checked;
            nudThang.Enabled = theoThang;
            nudNam.Enabled = theoThang;
            dtpTu.Enabled = !theoThang;
            dtpDen.Enabled = !theoThang;
            if (!dangTai) TaiDuLieu();
        }

        private void MoXuatBaoCao()
        {
            DateTime tu, den;
            int thang, nam;
            if (!LayKy(out tu, out den, out thang, out nam)) return;

            using (var frm = new FrmXuatBaoCao(tu, den))
            {
                if (frm.ShowDialog() != DialogResult.OK) return;

                BaoCaoDuLieu dl;
                try
                {
                    dl = BaoCaoService.LayDuLieu(frm.KetQua);
                }
                catch (Exception)
                {
                    MessageBox.Show("Không đọc được dữ liệu báo cáo. Hãy kiểm tra cấu hình LocalDB.",
                        "Lỗi truy cập dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string tenMacDinh = $"BaoCao_{frm.KetQua.Tu:yyyyMMdd}_{frm.KetQua.Den:yyyyMMdd}.xlsx";
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Title = "Chọn nơi lưu báo cáo";
                    sfd.Filter = "Excel Book|*.xlsx";
                    sfd.FileName = tenMacDinh;
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    string loi;
                    if (BaoCaoService.XuatExcel(dl, frm.KetQua.Tu, frm.KetQua.Den,
                            sfd.FileName, out loi))
                    {
                        MessageBox.Show("Xuất file Excel thành công!",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(loi, "Không xuất được báo cáo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private bool LayKy(out DateTime tu, out DateTime den, out int thang, out int nam)
        {
            thang = (int)nudThang.Value;
            nam = (int)nudNam.Value;
            if (rdoThang.Checked)
            {
                tu = new DateTime(nam, thang, 1);
                den = tu.AddMonths(1).AddDays(-1);
                return true;
            }
            tu = dtpTu.Value.Date;
            den = dtpDen.Value.Date;
            string loi;
            if (!ValidationHelper.KhoangNgay(tu, den, out loi))
            {
                MessageBox.Show(loi, "Khoảng ngày không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void TaiDuLieu()
        {
            DateTime tu, den;
            int thang, nam;
            if (!LayKy(out tu, out den, out thang, out nam)) return;

            dangTai = true;
            try
            {
                // --- Tổng quan kỳ ---
                TongQuanKy tong = rdoThang.Checked
                    ? ThongKeService.LayTongQuanThang(thang, nam)
                    : ThongKeService.LayTongQuanKhoangNgay(tu, den);
                lblThu.Text = UIHelper.FormatTien(tong.TongThu);
                lblChi.Text = UIHelper.FormatTien(tong.TongChi);
                lblLech.Text = UIHelper.FormatTien(tong.ChenhLech);
                lblLech.ForeColor = tong.ChenhLech >= 0 ? Theme.Thu : Theme.Chi;
                lblSoGD.Text = tong.SoGiaoDich.ToString();

                // --- Biểu đồ cột: luôn là 6 tháng kết thúc tại tháng đang xem ---
                List<ThuChiThang> chuoi = ThongKeService.LayThuChiTheoThang(thang, nam, 6);
                var diemThu = new List<DiemBieuDo>();
                var diemChi = new List<DiemBieuDo>();
                foreach (ThuChiThang t in chuoi)
                {
                    diemThu.Add(new DiemBieuDo { Nhan = t.NhanThang, GiaTri = t.TongThu, Mau = Theme.Thu });
                    diemChi.Add(new DiemBieuDo { Nhan = t.NhanThang, GiaTri = t.TongChi, Mau = Theme.Chi });
                }
                chartCotThu.HienThiCot(diemThu, "Thu");
                chartCotChi.HienThiCot(diemChi, "Chi");

                // --- Biểu đồ tròn + bảng phân bổ theo danh mục (loại đang chọn) ---
                string loai = cboLoaiTron.SelectedItem == null ? "Chi" : cboLoaiTron.SelectedItem.ToString();
                List<ThongKeDanhMuc> theoDM = rdoThang.Checked
                    ? ThongKeService.LayTheoDanhMucThang(thang, nam, loai)
                    : ThongKeService.LayTheoDanhMuc(tu, den, loai);

                var diemTron = new List<DiemBieuDo>();
                foreach (ThongKeDanhMuc tk in theoDM)
                    diemTron.Add(new DiemBieuDo { Nhan = tk.TenDanhMuc, GiaTri = tk.TongTien });
                if (diemTron.Count == 0) chartTron.HienThiTrang();
                else chartTron.HienThiTron(diemTron, loai);

                dgvDanhMuc.Rows.Clear();
                foreach (ThongKeDanhMuc tk in theoDM)
                {
                    int idx = dgvDanhMuc.Rows.Add();
                    DataGridViewRow row = dgvDanhMuc.Rows[idx];
                    row.Cells[0].Value = tk.TenDanhMuc;
                    row.Cells[1].Value = tk.SoGiaoDich;
                    row.Cells[2].Value = UIHelper.FormatTien(tk.TongTien);
                    row.Cells[2].Style.ForeColor = loai == "Thu" ? Theme.Thu : Theme.Chi;
                    row.Cells[2].Style.SelectionForeColor = row.Cells[2].Style.ForeColor;
                    row.Cells[3].Value = tk.TyTrong.ToString("0.##") + "%";
                }

                // --- Bảng chi tiết ---
                DataTable chiTiet = ThongKeService.LayChiTiet(tu, den);
                dgvChiTiet.Rows.Clear();
                foreach (DataRow r in chiTiet.Rows)
                {
                    string loaiGD = r["LoaiGiaoDich"].ToString();
                    int idx = dgvChiTiet.Rows.Add();
                    DataGridViewRow row = dgvChiTiet.Rows[idx];
                    row.Cells[0].Value = UIHelper.FormatNgay(Convert.ToDateTime(r["NgayGiaoDich"]));
                    row.Cells[1].Value = UIHelper.NhanLoaiGiaoDich(loaiGD);
                    row.Cells[1].Style.ForeColor = UIHelper.MauTheoLoai(loaiGD);
                    row.Cells[1].Style.SelectionForeColor = UIHelper.MauTheoLoai(loaiGD);
                    row.Cells[2].Value = r["TenDanhMuc"] == DBNull.Value ? "—" : r["TenDanhMuc"].ToString();
                    row.Cells[3].Value = r["TenVi"] == DBNull.Value ? "—" : r["TenVi"].ToString();
                    row.Cells[4].Value = r["GhiChu"] == DBNull.Value ? "—" : r["GhiChu"].ToString();
                    decimal tien = Convert.ToDecimal(r["SoTien"]);
                    row.Cells[5].Value = UIHelper.FormatTienCoHuong(tien, loaiGD);
                    row.Cells[5].Style.ForeColor = UIHelper.MauTheoLoai(loaiGD);
                    row.Cells[5].Style.SelectionForeColor = UIHelper.MauTheoLoai(loaiGD);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không tải được dữ liệu thống kê. Hãy kiểm tra cấu hình LocalDB.",
                    "Lỗi truy cập dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dangTai = false;
            }
        }
    }
}