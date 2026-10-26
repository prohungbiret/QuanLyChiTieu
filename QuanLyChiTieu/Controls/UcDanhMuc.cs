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
    public partial class UcDanhMuc : UserControl
    {
        public UcDanhMuc()
        {
            InitializeComponent();

            Tag = "Danh mục"; // FrmMain đọc Tag vào header (hợp đồng 5.3)
            BackColor = Theme.Background;
            Font = new Font("Segoe UI", 9.5f);

            // Khôi phục style DataGridView (Designer chỉ set cột, StyleDataGridView set màu/header)
            UIHelper.StyleDataGridView(dgv);
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible = false;

            // Áp theme runtime
            pnlTools.BackColor = Theme.Background;
            lblTimKiem.ForeColor = Theme.TextSub;
            lblLoai.ForeColor = Theme.TextSub;
            txtTimKiem.Size = new Size(220, Theme.CaoControl);
            cboLoai.Size = new Size(130, Theme.CaoControl);
            cboTrangThai.Size = new Size(150, Theme.CaoControl);

            btnThem.BackColor = Theme.Primary;
            btnThem.ForeColor = Theme.White;
            btnThem.Size = new Size(140, Theme.CaoNut);

            btnSua.BackColor = Theme.White;
            btnSua.ForeColor = Theme.Text;
            btnSua.Size = new Size(80, Theme.CaoNut);

            btnTrangThai.BackColor = Theme.White;
            btnTrangThai.ForeColor = Theme.Text;
            btnTrangThai.Size = new Size(130, Theme.CaoNut);

            lblTrangThaiBar.ForeColor = Theme.TextSub;

            // Gán SelectedIndex ban đầu (Designer không tự gán SelectedIndex vì Items được add runtime)
            cboLoai.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            TaiDanhSach();
        }

        // --- Event Handlers ---
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            TaiDanhSach();
        }

        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaiDanhSach();
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaiDanhSach();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ThemMoi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SuaChon();
        }

        private void btnTrangThai_Click(object sender, EventArgs e)
        {
            DoiTrangThai();
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            CapNhatNutTrangThai();
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) SuaChon();
        }

        // --- Logic ---
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