using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    public partial class FrmThemKhoanVay : Form
    {
        private class LoaiKhoanItem
        {
            public bool LaVayCuaToi;
            public readonly string Nhan;
            public LoaiKhoanItem(bool laVayCuaToi, string nhan)
            { LaVayCuaToi = laVayCuaToi; Nhan = nhan; }
            public override string ToString() { return Nhan; }
        }

        private readonly int khoanID;
        public KhoanVayNo KetQua { get; private set; }
        public int KetQuaViID { get; private set; }

        public FrmThemKhoanVay() : this(null) { }

        public FrmThemKhoanVay(KhoanVayNoChiTiet sua)
        {
            InitializeComponent();

            khoanID = sua == null ? 0 : sua.Khoan.KhoanVayNoID;
            this.Text = sua == null ? "Thêm khoản vay" : "Sửa khoản vay";
            lblTieuDe.Text = this.Text;

            cboLoai.Items.Add(new LoaiKhoanItem(true, "Mình nợ (vay tiền của người khác)"));
            cboLoai.Items.Add(new LoaiKhoanItem(false, "Mình cho vay (cho người khác mượn)"));
            cboLoai.SelectedIndex = 0;

            foreach (ViTien v in TietKiemService.LayDsViDangDung()) cboVi.Items.Add(v);
            cboVi.DisplayMember = "TenVi";
            cboVi.ValueMember = "ViTienID";
            if (cboVi.Items.Count > 0) cboVi.SelectedIndex = 0;

            dtpPhatSinh.MaxDate = DateTime.Today;
            dtpPhatSinh.Value = DateTime.Today;

            dtpDenHan.MaxDate = DateTime.Today.AddYears(50);
            dtpDenHan.Value = DateTime.Today;
            dtpDenHan.Enabled = false;

            chkDenHan.ForeColor = Theme.TextSub;

            if (sua != null)
            {
                cboLoai.SelectedIndex = sua.Khoan.LoaiKhoan == "Vay" ? 0 : 1;
                cboLoai.Enabled = false;
                txtNguoiLienQuan.Text = sua.Khoan.TenNguoiLienQuan;
                txtGoc.Text = sua.Khoan.SoTienGoc.ToString("#,0");
                dtpPhatSinh.Value = sua.Khoan.NgayPhatSinh;
                if (sua.Khoan.NgayDenHan.HasValue)
                {
                    chkDenHan.Checked = true;
                    dtpDenHan.Value = sua.Khoan.NgayDenHan.Value;
                }
                if (sua.Khoan.LaiSuat.HasValue)
                    txtLaiSuat.Text = sua.Khoan.LaiSuat.Value.ToString("0.####");
                txtGhiChu.Text = sua.Khoan.GhiChu;

                // Ẩn các trường không cho sửa khi đang sửa khoản vay
                lblVi.Visible = false;
                cboVi.Visible = false;
            }

            btnLuu.BackColor = Theme.Primary;
            btnHuy.BackColor = Theme.Background;
        }

        private void chkDenHan_CheckedChanged(object sender, EventArgs e)
        {
            dtpDenHan.Enabled = chkDenHan.Checked;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string loi;
            if (string.IsNullOrWhiteSpace(txtNguoiLienQuan.Text))
            {
                MessageBox.Show("Tên người liên quan không được bỏ trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNguoiLienQuan.Focus();
                return;
            }

            decimal goc;
            if (!ValidationHelper.SoTien(txtGoc.Text, out goc, out loi))
            {
                MessageBox.Show(loi.Replace("Số tiền", "Tiền gốc"), "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGoc.Focus();
                return;
            }

            decimal? lai = null;
            if (!string.IsNullOrWhiteSpace(txtLaiSuat.Text))
            {
                decimal laiTam;
                if (!decimal.TryParse(txtLaiSuat.Text.Trim(), System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.GetCultureInfo("vi-VN"), out laiTam)
                    || laiTam < 0m || laiTam > 999.9999m)
                {
                    MessageBox.Show("Lãi suất phải là số từ 0 đến 999.9999 (%/năm).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLaiSuat.Focus();
                    return;
                }
                lai = laiTam;
            }

            if (chkDenHan.Checked && dtpDenHan.Enabled && dtpDenHan.Value.Date < dtpPhatSinh.Value.Date)
            {
                MessageBox.Show("Hạn trả không được trước ngày phát sinh.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDenHan.Focus();
                return;
            }

            int viID = 0;
            if (khoanID == 0)
            {
                ViTien vi = cboVi.SelectedItem as ViTien;
                if (vi == null)
                {
                    MessageBox.Show("Phải chọn ví cho giao dịch khởi phát của khoản.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboVi.Focus();
                    return;
                }
                viID = vi.ViTienID;
            }

            var loaiChon = cboLoai.SelectedItem as LoaiKhoanItem;
            bool laVayCuaToi = loaiChon != null ? loaiChon.LaVayCuaToi : true;

            KetQua = new KhoanVayNo
            {
                KhoanVayNoID = khoanID,
                NguoiDungID = CurrentUser.NguoiDungID,
                LoaiKhoan = laVayCuaToi ? "Vay" : "ChoVay",
                TenNguoiLienQuan = txtNguoiLienQuan.Text.Trim(),
                SoTienGoc = goc,
                NgayPhatSinh = dtpPhatSinh.Value.Date,
                NgayDenHan = chkDenHan.Checked && dtpDenHan.Enabled
                    ? (DateTime?)dtpDenHan.Value.Date : null,
                LaiSuat = lai,
                GhiChu = txtGhiChu.Text.Trim()
            };
            KetQuaViID = viID;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}