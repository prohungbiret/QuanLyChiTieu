using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Controls
{
    // TV3 (Hải) sở hữu — màn Danh mục (FR02): CRUD + ngừng sử dụng / kích hoạt lại.
    // Layout theo UI Standard 3.4.1: tiêu đề > tìm kiếm/lọc + nút thao tác > bảng > dòng trạng thái.
    public class UcDanhMuc : UserControl
    {
        private readonly TextBox txtTimKiem = new TextBox();
        private readonly ComboBox cboLoai = new ComboBox();
        private readonly ComboBox cboTrangThai = new ComboBox();
        private readonly DataGridView dgv = new DataGridView();
        private readonly Label lblTrangThaiBar = new Label();
        private readonly Button btnTrangThai = new Button();

        public UcDanhMuc()
        {
            Tag = "Danh mục"; // FrmMain đọc Tag vào header (hợp đồng 5.3)
            BackColor = Theme.Background;
            Font = new Font("Segoe UI", 9.5f);

            // ---- Hàng công cụ ----
            var pnlTools = new FlowLayoutPanel();
            pnlTools.Dock = DockStyle.Top;
            pnlTools.Height = 52;
            pnlTools.Padding = new Padding(0, 8, 0, 8);
            pnlTools.BackColor = Theme.Background;
            pnlTools.WrapContents = false;

            txtTimKiem.Size = new Size(220, Theme.CaoControl);
            txtTimKiem.TextChanged += (s, e) => TaiDanhSach();

            cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoai.Size = new Size(130, Theme.CaoControl);
            cboLoai.Items.Add("Tất cả loại");
            cboLoai.Items.Add("Thu");
            cboLoai.Items.Add("Chi");
            cboLoai.SelectedIndex = 0;
            cboLoai.SelectedIndexChanged += (s, e) => TaiDanhSach();

            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Size = new Size(150, Theme.CaoControl);
            cboTrangThai.Items.Add("Tất cả trạng thái");
            cboTrangThai.Items.Add("Đang sử dụng");
            cboTrangThai.Items.Add("Đã ngừng");
            cboTrangThai.SelectedIndex = 0;
            cboTrangThai.SelectedIndexChanged += (s, e) => TaiDanhSach();

            var btnThem = Nut("Thêm danh mục", true);
            btnThem.Click += (s, e) => ThemMoi();
            var btnSua = Nut("Sửa", false);
            btnSua.Click += (s, e) => SuaChon();
            btnTrangThai.Text = "Ngừng sử dụng";
            btnTrangThai.Size = new Size(130, Theme.CaoNut);
            btnTrangThai.FlatStyle = FlatStyle.Flat;
            btnTrangThai.FlatAppearance.BorderSize = 0;
            btnTrangThai.BackColor = Theme.White;
            btnTrangThai.ForeColor = Theme.Text;
            btnTrangThai.Click += (s, e) => DoiTrangThai();

            pnlTools.Controls.Add(Nhan("Tìm kiếm:"));
            pnlTools.Controls.Add(txtTimKiem);
            pnlTools.Controls.Add(Nhan("Loại:"));
            pnlTools.Controls.Add(cboLoai);
            pnlTools.Controls.Add(cboTrangThai);
            pnlTools.Controls.Add(btnThem);
            pnlTools.Controls.Add(btnSua);
            pnlTools.Controls.Add(btnTrangThai);

            // ---- Bảng ----
            UIHelper.StyleDataGridView(dgv);
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible = false;
            dgv.Dock = DockStyle.Fill;
            dgv.Columns.Add("colTen", "Tên danh mục");
            dgv.Columns.Add("colLoai", "Loại");
            dgv.Columns.Add("colMoTa", "Mô tả");
            dgv.Columns.Add("colSoGD", "Số giao dịch");
            dgv.Columns.Add("colTrangThai", "Trạng thái");
            dgv.Columns["colLoai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns["colLoai"].Width = 90;
            dgv.Columns["colSoGD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns["colSoGD"].Width = 110;
            dgv.Columns["colSoGD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["colTrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns["colTrangThai"].Width = 130;
            dgv.SelectionChanged += (s, e) => CapNhatNutTrangThai();
            dgv.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) SuaChon(); };

            lblTrangThaiBar.Dock = DockStyle.Bottom;
            lblTrangThaiBar.Height = 22;
            lblTrangThaiBar.Font = new Font("Segoe UI", 8.5f);
            lblTrangThaiBar.ForeColor = Theme.TextSub;

            Controls.Add(dgv);
            Controls.Add(lblTrangThaiBar);
            Controls.Add(pnlTools);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            TaiDanhSach();
        }

        private static Label Nhan(string text)
        {
            var lbl = new Label();
            lbl.Text = text;
            lbl.AutoSize = true;
            lbl.Margin = new Padding(8, 10, 2, 0);
            lbl.ForeColor = Theme.TextSub;
            return lbl;
        }

        private static Button Nut(string text, bool chinh)
        {
            var btn = new Button();
            btn.Text = text;
            btn.Size = new Size(chinh ? 140 : 80, Theme.CaoNut);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = chinh ? Theme.Primary : Theme.White;
            btn.ForeColor = chinh ? Theme.White : Theme.Text;
            btn.Margin = new Padding(8, 0, 0, 0);
            return btn;
        }

        private DanhMuc DangChon()
        {
            if (dgv.CurrentRow == null) return null;
            return dgv.CurrentRow.Tag as DanhMuc;
        }

        private void TaiDanhSach()
        {
            try
            {
                string loai = cboLoai.SelectedIndex == 1 ? DanhMucService.LoaiThu
                            : cboLoai.SelectedIndex == 2 ? DanhMucService.LoaiChi
                            : null;
                bool? trangThai = cboTrangThai.SelectedIndex == 1 ? (bool?)true
                                : cboTrangThai.SelectedIndex == 2 ? (bool?)false
                                : null;

                var ds = DanhMucService.LayDanhSach(loai, trangThai, txtTimKiem.Text);
                dgv.Rows.Clear();
                foreach (DanhMuc dm in ds)
                {
                    int idx = dgv.Rows.Add();
                    DataGridViewRow row = dgv.Rows[idx];
                    row.Tag = dm;
                    row.Cells[0].Value = dm.TenDanhMuc;
                    row.Cells[1].Value = dm.LoaiDanhMuc;
                    row.Cells[1].Style.ForeColor = dm.LoaiDanhMuc == DanhMucService.LoaiThu ? Theme.Thu : Theme.Chi;
                    row.Cells[1].Style.SelectionForeColor = row.Cells[1].Style.ForeColor;
                    row.Cells[2].Value = string.IsNullOrEmpty(dm.MoTa) ? "—" : dm.MoTa;
                    row.Cells[3].Value = DanhMucService.DemGiaoDich(dm.DanhMucID);
                    row.Cells[4].Value = dm.TrangThai ? "Đang sử dụng" : "Đã ngừng";
                    row.Cells[4].Style.ForeColor = dm.TrangThai ? Theme.Thu : Theme.TextSub;
                    row.Cells[4].Style.SelectionForeColor = row.Cells[4].Style.ForeColor;
                    if (!dm.TrangThai)
                        row.DefaultCellStyle.ForeColor = Theme.TextSub;
                }
                lblTrangThaiBar.Text = ds.Count == 0
                    ? "Chưa có danh mục nào. Hãy thêm danh mục đầu tiên."
                    : "Tổng số: " + ds.Count + " danh mục";
                CapNhatNutTrangThai();
            }
            catch (Exception)
            {
                MessageBox.Show("Không tải được danh sách danh mục. Hãy kiểm tra cấu hình LocalDB.",
                    "Lỗi truy cập dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatNutTrangThai()
        {
            DanhMuc dm = DangChon();
            btnTrangThai.Text = dm != null && !dm.TrangThai ? "Kích hoạt lại" : "Ngừng sử dụng";
            btnTrangThai.Enabled = dm != null;
        }

        private void ThemMoi()
        {
            using (var frm = new FrmThemDanhMuc())
            {
                if (frm.ShowDialog() != DialogResult.OK) return;
                string loi;
                if (DanhMucService.Them(frm.DanhMucKetQua, out loi) < 0)
                {
                    MessageBox.Show(loi, "Không thêm được danh mục",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TaiDanhSach();
            }
        }

        private void SuaChon()
        {
            DanhMuc dm = DangChon();
            if (dm == null)
            {
                MessageBox.Show("Hãy chọn một danh mục trong bảng.", "Sửa danh mục",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (var frm = new FrmThemDanhMuc(dm))
            {
                if (frm.ShowDialog() != DialogResult.OK) return;
                string loi;
                if (!DanhMucService.Sua(frm.DanhMucKetQua, out loi))
                {
                    MessageBox.Show(loi, "Không sửa được danh mục",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TaiDanhSach();
            }
        }

        private void DoiTrangThai()
        {
            DanhMuc dm = DangChon();
            if (dm == null)
            {
                MessageBox.Show("Hãy chọn một danh mục trong bảng.", "Danh mục",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string loi;
            if (dm.TrangThai)
            {
                int soGD = DanhMucService.DemGiaoDich(dm.DanhMucID);
                string thongDiep = "Ngừng sử dụng danh mục \"" + dm.TenDanhMuc + "\"?\n" +
                    "Danh mục sẽ không còn được chọn cho giao dịch mới" +
                    (soGD > 0 ? ", nhưng " + soGD + " giao dịch cũ vẫn giữ nguyên trong lịch sử." : ".");
                if (MessageBox.Show(thongDiep, "Xác nhận ngừng sử dụng",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                if (!DanhMucService.NgungSuDung(dm.DanhMucID, out loi))
                {
                    MessageBox.Show(loi, "Không ngừng được danh mục",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                if (!DanhMucService.KichHoatLai(dm.DanhMucID, out loi))
                {
                    MessageBox.Show(loi, "Không kích hoạt lại được danh mục",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            TaiDanhSach();
        }
    }
}
