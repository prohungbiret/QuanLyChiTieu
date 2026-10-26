using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    public partial class FrmThemDanhMuc : Form
    {
        private readonly int danhMucID;
        public DanhMuc DanhMucKetQua { get; private set; }

        public FrmThemDanhMuc() : this(null) { }

        public FrmThemDanhMuc(DanhMuc sua)
        {
            InitializeComponent();

            danhMucID = sua == null ? 0 : sua.DanhMucID;
            this.Text = sua == null ? "Thêm danh mục" : "Sửa danh mục";
            lblTieuDe.Text = this.Text;

            cboLoai.SelectedIndex = 0;
            txtMoTa.MaxLength = 255;

            if (sua != null)
            {
                txtTen.Text = sua.TenDanhMuc;
                cboLoai.SelectedItem = sua.LoaiDanhMuc;
                txtMoTa.Text = sua.MoTa ?? "";
            }

            btnLuu.BackColor = Theme.Primary;
            btnHuy.BackColor = Theme.Background;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string loi;
            if (!ValidationHelper.BatBuoc(txtTen.Text, "Tên danh mục", out loi))
            {
                MessageBox.Show(loi, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }
            if (cboLoai.SelectedItem == null)
            {
                MessageBox.Show("Phải chọn loại danh mục Thu hoặc Chi.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoai.Focus();
                return;
            }

            DanhMucKetQua = new DanhMuc
            {
                DanhMucID = danhMucID,
                NguoiDungID = CurrentUser.NguoiDungID,
                TenDanhMuc = txtTen.Text.Trim(),
                LoaiDanhMuc = cboLoai.SelectedItem.ToString(),
                MoTa = txtMoTa.Text.Trim(),
                TrangThai = true
            };
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}