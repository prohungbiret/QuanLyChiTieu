using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    public partial class FrmTraNo : Form
    {
        private readonly bool laThuHoi;
        private readonly KhoanVayNoChiTiet ct;

        public decimal KetQuaTienGoc { get; private set; }
        public decimal KetQuaTienLai { get; private set; }
        public decimal KetQuaSoTien { get; private set; }
        public int KetQuaViID { get; private set; }
        public DateTime KetQuaNgay { get; private set; }
        public string KetQuaGhiChu { get; private set; }

        // Constructor 1: Dành cho Designer
        public FrmTraNo() : this(new KhoanVayNoChiTiet { Khoan = new KhoanVayNo { TenNguoiLienQuan = "Mẫu" } }, false) { }

        // Constructor 2: Chạy thật
        public FrmTraNo(KhoanVayNoChiTiet chiTiet, bool thuHoiChuKhongTra)
        {
            InitializeComponent();

            laThuHoi = thuHoiChuKhongTra;
            ct = chiTiet;

            // Tùy chỉnh text hiển thị theo loại
            if (laThuHoi)
            {
                this.Text = "Thu hồi nợ — " + chiTiet.Khoan.TenNguoiLienQuan;
                lblTieuDe.Text = "Thu hồi nợ";
                lblGoc.Text = "Số tiền thu hồi (đ) (*)";
                lblVi.Text = "Ví nhận tiền (*)";
                lblLai.Visible = false;
                txtLai.Visible = false;
            }
            else
            {
                this.Text = "Trả nợ — " + chiTiet.Khoan.TenNguoiLienQuan;
                lblTieuDe.Text = "Trả nợ";
                lblGoc.Text = "Tiền gốc (đ) (*)";
                lblVi.Text = "Ví chi ra (*)";
            }

            // Đổ dữ liệu vào ComboBox
            foreach (ViTien v in TietKiemService.LayDsViDangDung()) cboVi.Items.Add(v);
            cboVi.DisplayMember = "TenVi";
            cboVi.ValueMember = "ViTienID";
            if (cboVi.Items.Count > 0) cboVi.SelectedIndex = 0;

            dtpNgay.MaxDate = DateTime.Today;
            dtpNgay.Value = DateTime.Today;

            lblConLai.Text = "Còn nợ: " + UIHelper.FormatTien(ct.ConLai) +
                       " (gốc " + UIHelper.FormatTien(ct.Khoan.SoTienGoc) +
                       (laThuHoi ? "" : " — lãi/phí đã trả: " + UIHelper.FormatTien(ct.TongLaiPhi)) + ")";

            if (!laThuHoi && ct.ConLai > 0m)
                txtGoc.Text = ct.ConLai.ToString("#,0");

            // Áp theme
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
            ViTien vi = cboVi.SelectedItem as ViTien;
            if (vi == null)
            {
                MessageBox.Show(laThuHoi ? "Phải chọn ví nhận tiền thu hồi." : "Phải chọn ví chi ra tiền trả nợ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboVi.Focus();
                return;
            }

            decimal goc;
            if (!ValidationHelper.SoTien(txtGoc.Text, out goc, out loi))
            {
                MessageBox.Show(loi.Replace("Số tiền", laThuHoi ? "Số tiền thu hồi" : "Tiền gốc"), "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGoc.Focus();
                return;
            }

            decimal lai = 0m;
            if (!laThuHoi)
            {
                decimal laiTam;
                if (!decimal.TryParse(string.IsNullOrWhiteSpace(txtLai.Text) ? "0" : txtLai.Text.Trim(),
                        System.Globalization.NumberStyles.Number,
                        System.Globalization.CultureInfo.GetCultureInfo("vi-VN"), out laiTam))
                {
                    MessageBox.Show("Tiền lãi/phí phải là số không âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLai.Focus();
                    return;
                }
                lai = laiTam;
            }

            if (dtpNgay.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Ngày không được ở tương lai.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgay.Focus();
                return;
            }

            KetQuaTienGoc = goc;
            KetQuaTienLai = lai;
            KetQuaSoTien = laThuHoi ? goc : goc + lai;
            KetQuaViID = vi.ViTienID;
            KetQuaNgay = dtpNgay.Value.Date;
            KetQuaGhiChu = txtGhiChu.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}