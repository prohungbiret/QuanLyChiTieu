using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Controls
{
    // TV3 (Hải) sở hữu — màn Thống kê & báo cáo (FR07): theo tháng hoặc khoảng ngày,
    // biểu đồ cột (thu/chi 6 tháng) + biểu đồ tròn (cơ cấu theo danh mục) qua UcChart
    // dùng chung của TV1, bảng phân bổ theo danh mục và bảng chi tiết giao dịch.
    // Mọi con số chỉ gồm loại Thu + Chi (ThongKeService — QD02, TC-RPT-03/10).
    public class UcThongKe : UserControl
    {
        private readonly RadioButton rdoThang = new RadioButton();
        private readonly RadioButton rdoKhoangNgay = new RadioButton();
        private readonly NumericUpDown nudThang = new NumericUpDown();
        private readonly NumericUpDown nudNam = new NumericUpDown();
        private readonly DateTimePicker dtpTu = new DateTimePicker();
        private readonly DateTimePicker dtpDen = new DateTimePicker();
        private readonly ComboBox cboLoaiTron = new ComboBox();

        private readonly Label lblThu = new Label();
        private readonly Label lblChi = new Label();
        private readonly Label lblLech = new Label();
        private readonly Label lblSoGD = new Label();

        private readonly UcChart chartCotThu = new UcChart(UcChart.KieuBieuDo.Cot);
        private readonly UcChart chartCotChi = new UcChart(UcChart.KieuBieuDo.Cot);
        private readonly UcChart chartTron = new UcChart(UcChart.KieuBieuDo.Tron);
        private readonly DataGridView dgvDanhMuc = new DataGridView();
        private readonly DataGridView dgvChiTiet = new DataGridView();
        private readonly Label lblGhiChu = new Label();

        private bool dangTai; // tránh vòng lặp sự kiện khi đổi chế độ

        public UcThongKe()
        {
            Tag = "Thống kê"; // hợp đồng 5.3
            BackColor = Theme.Background;
            Font = new Font("Segoe UI", 9.5f);

            // ---- Hàng lọc ----
            var pnlLoc = new FlowLayoutPanel();
            pnlLoc.Dock = DockStyle.Top;
            pnlLoc.Height = 52;
            pnlLoc.Padding = new Padding(0, 8, 0, 8);
            pnlLoc.BackColor = Theme.Background;
            pnlLoc.WrapContents = false;

            rdoThang.Text = "Theo tháng";
            rdoThang.AutoSize = true;
            rdoThang.Checked = true;
            rdoThang.Margin = new Padding(0, 10, 8, 0);
            rdoThang.CheckedChanged += (s, e) => DoiCheDo();

            nudThang.Minimum = 1;
            nudThang.Maximum = 12;
            nudThang.Value = DateTime.Now.Month;
            nudThang.Size = new Size(64, Theme.CaoControl);
            nudThang.ValueChanged += (s, e) => { if (!dangTai) TaiDuLieu(); };

            nudNam.Minimum = 1900;
            nudNam.Maximum = 2100;
            nudNam.Value = DateTime.Now.Year;
            nudNam.Size = new Size(86, Theme.CaoControl);
            nudNam.ValueChanged += (s, e) => { if (!dangTai) TaiDuLieu(); };

            rdoKhoangNgay.Text = "Khoảng ngày";
            rdoKhoangNgay.AutoSize = true;
            rdoKhoangNgay.Margin = new Padding(16, 10, 8, 0);
            rdoKhoangNgay.CheckedChanged += (s, e) => DoiCheDo();

            dtpTu.Format = DateTimePickerFormat.Custom;
            dtpTu.CustomFormat = "dd/MM/yyyy";
            dtpTu.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTu.Size = new Size(120, Theme.CaoControl);
            dtpTu.Enabled = false;
            dtpTu.ValueChanged += (s, e) => { if (!dangTai && rdoKhoangNgay.Checked) TaiDuLieu(); };

            dtpDen.Format = DateTimePickerFormat.Custom;
            dtpDen.CustomFormat = "dd/MM/yyyy";
            dtpDen.Value = DateTime.Now.Date;
            dtpDen.Size = new Size(120, Theme.CaoControl);
            dtpDen.Enabled = false;
            dtpDen.ValueChanged += (s, e) => { if (!dangTai && rdoKhoangNgay.Checked) TaiDuLieu(); };

            var btnXem = new Button();
            btnXem.Text = "Xem thống kê";
            btnXem.Size = new Size(130, Theme.CaoNut);
            btnXem.FlatStyle = FlatStyle.Flat;
            btnXem.FlatAppearance.BorderSize = 0;
            btnXem.BackColor = Theme.Primary;
            btnXem.ForeColor = Theme.White;
            btnXem.Margin = new Padding(16, 0, 0, 0);
            btnXem.Click += (s, e) => TaiDuLieu();

            pnlLoc.Controls.Add(rdoThang);
            pnlLoc.Controls.Add(nudThang);
            pnlLoc.Controls.Add(Nhan("/"));
            pnlLoc.Controls.Add(nudNam);
            pnlLoc.Controls.Add(rdoKhoangNgay);
            pnlLoc.Controls.Add(dtpTu);
            pnlLoc.Controls.Add(Nhan("đến"));
            pnlLoc.Controls.Add(dtpDen);
            pnlLoc.Controls.Add(btnXem);

            // ---- 4 thẻ tổng quan ----
            var pnlKpi = new TableLayoutPanel();
            pnlKpi.ColumnCount = 4;
            for (int i = 0; i < 4; i++)
                pnlKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            pnlKpi.Dock = DockStyle.Top;
            pnlKpi.Height = 92;
            pnlKpi.Controls.Add(TaoThe("Tổng thu", lblThu, Theme.Thu), 0, 0);
            pnlKpi.Controls.Add(TaoThe("Tổng chi", lblChi, Theme.Chi), 1, 0);
            pnlKpi.Controls.Add(TaoThe("Chênh lệch thu − chi", lblLech, Theme.Text), 2, 0);
            pnlKpi.Controls.Add(TaoThe("Số giao dịch Thu/Chi", lblSoGD, Theme.Text), 3, 0);

            // ---- Khu biểu đồ: cột trái, tròn phải ----
            var pnlBieuDo = new TableLayoutPanel();
            pnlBieuDo.ColumnCount = 2;
            pnlBieuDo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55f));
            pnlBieuDo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45f));
            pnlBieuDo.Dock = DockStyle.Top;
            pnlBieuDo.Height = 340;

            // UcChart dùng chung chỉ vẽ 1 series (6.7) → hai biểu đồ cột xếp dọc:
            // Thu (xanh) ở trên, Chi (đỏ) ở dưới, cùng trục tháng để đối chiếu.
            var pnlCot = new TableLayoutPanel();
            pnlCot.ColumnCount = 1;
            pnlCot.RowCount = 2;
            pnlCot.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            pnlCot.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            pnlCot.Dock = DockStyle.Fill;
            pnlCot.BackColor = Theme.Background;

            var hopThu = HopTrang("Biểu đồ cột — Thu theo tháng (6 tháng gần nhất)");
            chartCotThu.Dock = DockStyle.Fill;
            hopThu.Controls.Add(chartCotThu, 0, 1);
            pnlCot.Controls.Add(hopThu, 0, 0);

            var hopChi = HopTrang("Biểu đồ cột — Chi theo tháng (6 tháng gần nhất)");
            chartCotChi.Dock = DockStyle.Fill;
            hopChi.Controls.Add(chartCotChi, 0, 1);
            pnlCot.Controls.Add(hopChi, 0, 1);

            pnlBieuDo.Controls.Add(pnlCot, 0, 0);

            var pnlTron = HopTrang("Biểu đồ tròn — cơ cấu theo danh mục");
            var pnlChonLoai = new FlowLayoutPanel();
            pnlChonLoai.Dock = DockStyle.Top;
            pnlChonLoai.Height = 36;
            pnlChonLoai.BackColor = Theme.White;
            cboLoaiTron.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiTron.Size = new Size(110, Theme.CaoControl);
            cboLoaiTron.Items.Add("Chi");
            cboLoaiTron.Items.Add("Thu");
            cboLoaiTron.SelectedIndex = 0;
            cboLoaiTron.SelectedIndexChanged += (s, e) => { if (!dangTai) TaiDuLieu(); };
            pnlChonLoai.Controls.Add(cboLoaiTron);
            chartTron.Dock = DockStyle.Fill;
            var noiDungTron = new Panel();
            noiDungTron.Dock = DockStyle.Fill;
            noiDungTron.BackColor = Theme.White;
            noiDungTron.Controls.Add(chartTron);     // Fill thêm trước
            noiDungTron.Controls.Add(pnlChonLoai);   // Top thêm sau (chiếm mép trên)
            pnlTron.Controls.Add(noiDungTron, 0, 1);
            pnlBieuDo.Controls.Add(pnlTron, 1, 0);

            // ---- Hai bảng: phân bổ danh mục (trái) + chi tiết (phải) ----
            var pnlBang = new TableLayoutPanel();
            pnlBang.ColumnCount = 2;
            pnlBang.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42f));
            pnlBang.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58f));
            pnlBang.Dock = DockStyle.Fill;

            var pnlDm = HopTrang("Phân bổ theo danh mục trong kỳ");
            CauHinhBangDanhMuc();
            pnlDm.Controls.Add(dgvDanhMuc, 0, 1);
            pnlBang.Controls.Add(pnlDm, 0, 0);

            var pnlCt = HopTrang("Chi tiết giao dịch Thu/Chi trong kỳ");
            CauHinhBangChiTiet();
            pnlCt.Controls.Add(dgvChiTiet, 0, 1);
            pnlBang.Controls.Add(pnlCt, 1, 0);

            lblGhiChu.Dock = DockStyle.Bottom;
            lblGhiChu.Height = 22;
            lblGhiChu.Font = new Font("Segoe UI", 8.5f);
            lblGhiChu.ForeColor = Theme.TextSub;
            lblGhiChu.Text = "Chỉ tính giao dịch loại Thu và Chi — chuyển tiền, vay nợ, tiết kiệm không vào báo cáo (Bảng 2.2e).";

            Controls.Add(pnlBang);
            Controls.Add(lblGhiChu);
            Controls.Add(pnlBieuDo);
            Controls.Add(pnlKpi);
            Controls.Add(pnlLoc);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
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

        // Hộp trắng: hàng 0 = tiêu đề, hàng 1 = nội dung (TableLayoutPanel —
        // không phụ thuộc thứ tự Add như Dock, tránh lỗi bố cục của Dashboard từng gặp).
        // Nơi gọi đặt nội dung vào ô (0, 1).
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

        // Kỳ đang chọn: chế độ tháng → cả tháng; chế độ khoảng ngày → [dtpTu, dtpDen].
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
