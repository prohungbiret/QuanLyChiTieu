using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    public partial class FrmThemMucTieu : Form
    {
        private readonly int mucTieuID;
        public MucTieuTietKiem KetQua { get; private set; }

        // Constructor 1: Thêm mới
        public FrmThemMucTieu() : this(null) { }

        // Constructor 2: Sửa
        public FrmThemMucTieu(MucTieuTietKiem sua)
        {
            InitializeComponent();

            mucTieuID = sua == null ? 0 : sua.MucTieuID;
            this.Text = sua == null ? "Thêm mục tiêu tiết kiệm" : "Sửa mục tiêu tiết kiệm";
            lblTieuDe.Text = this.Text;

            // Cấu hình ngày tháng
            dtpBatDau.MaxDate = DateTime.Today;
            dtpBatDau.Value = sua != null ? sua.NgayBatDau : DateTime.Today;

            dtpKetThuc.MaxDate = DateTime.Today.AddYears(50);
            dtpKetThuc.Value = DateTime.Today;

            chkKetThuc.ForeColor = Theme.TextSub;
            chkKetThuc.Checked = sua != null && sua.NgayKetThuc.HasValue;
            dtpKetThuc.Enabled = chkKetThuc.Checked;
            if (sua != null && sua.NgayKetThuc.HasValue) dtpKetThuc.Value = sua.NgayKetThuc.Value;

            // Điền dữ liệu nếu là sửa
            if (sua != null)
            {
                txtTen.Text = sua.TenMucTieu;
                txtSoTien.Text = sua.SoTienMucTieu.ToString("#,0");
            }

            // Áp theme
            btnLuu.BackColor = Theme.Primary;
            btnHuy.BackColor = Theme.Background;
        }

        private void chkKetThuc_CheckedChanged(object sender, EventArgs e)
        {
            dtpKetThuc.Enabled = chkKetThuc.Checked;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string loi;
            if (!ValidationHelper.BatBuoc(txtTen.Text, "Tên mục tiêu", out loi))
            {
                MessageBox.Show(loi, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }
            if (txtTen.Text.Trim().Length > 150)
            {
                MessageBox.Show("Tên mục tiêu tối đa 150 ký tự.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            decimal soTien;
            if (!ValidationHelper.SoTien(txtSoTien.Text, out soTien, out loi))
            {
                MessageBox.Show(loi.Replace("Số tiền", "Số tiền mục tiêu"), "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTien.Focus();
                return;
            }

            if (chkKetThuc.Checked && dtpKetThuc.Enabled && dtpKetThuc.Value.Date < dtpBatDau.Value.Date)
            {
                MessageBox.Show("Ngày kết thúc không được trước ngày bắt đầu.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpKetThuc.Focus();
                return;
            }

            KetQua = new MucTieuTietKiem
            {
                MucTieuID = mucTieuID,
                NguoiDungID = CurrentUser.NguoiDungID,
                TenMucTieu = txtTen.Text.Trim(),
                SoTienMucTieu = soTien,
                NgayBatDau = dtpBatDau.Value.Date,
                NgayKetThuc = chkKetThuc.Checked ? (DateTime?)dtpKetThuc.Value.Date : null,
                TrangThai = "DangThucHien"
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}