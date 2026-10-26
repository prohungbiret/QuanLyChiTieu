using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    public partial class FrmTietKiemGD : Form
    {
        private readonly bool laRut;

        public decimal KetQuaSoTien { get; private set; }
        public int KetQuaViID { get; private set; }
        public DateTime KetQuaNgay { get; private set; }
        public string KetQuaGhiChu { get; private set; }

        public FrmTietKiemGD() : this(false, "Mẫu") { }

        public FrmTietKiemGD(bool rut, string tenMucTieu)
        {
            InitializeComponent();

            laRut = rut;

            this.Text = rut ? "Rút tiết kiệm — " + tenMucTieu : "Đóng góp — " + tenMucTieu;
            lblTieuDe.Text = rut ? "Rút tiết kiệm" : "Đóng góp";

            foreach (ViTien v in TietKiemService.LayDsViDangDung()) cboVi.Items.Add(v);
            cboVi.DisplayMember = "TenVi";
            cboVi.ValueMember = "ViTienID";
            if (cboVi.Items.Count > 0) cboVi.SelectedIndex = 0;

            dtpNgay.MaxDate = DateTime.Today;
            dtpNgay.Value = DateTime.Today;

            lblVi.Text = laRut ? "Ví nhận tiền (*)" : "Ví chi ra (*)";
            lblChuThich.Text = laRut
                ? "Rút: tiền VÀO ví đích (CK_GD_TienVao)."
                : "Đóng góp: tiền RA khỏi ví nguồn (CK_GD_TienRa).";

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
                MessageBox.Show(laRut ? "Phải chọn ví nhận tiền." : "Phải chọn ví chi ra.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboVi.Focus();
                return;
            }

            decimal soTien;
            if (!ValidationHelper.SoTien(txtSoTien.Text, out soTien, out loi))
            {
                MessageBox.Show(loi, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTien.Focus();
                return;
            }

            if (dtpNgay.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Ngày không được ở tương lai.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgay.Focus();
                return;
            }

            KetQuaSoTien = soTien;
            KetQuaViID = vi.ViTienID;
            KetQuaNgay = dtpNgay.Value.Date;
            KetQuaGhiChu = txtGhiChu.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}