using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    public partial class FrmXuatBaoCao : Form
    {
        public BaoCaoYeuCau KetQua { get; private set; }

        // Constructor 1: Dành cho Designer
        public FrmXuatBaoCao() : this(DateTime.Today.AddDays(-30), DateTime.Today) { }

        // Constructor 2: Chạy thật
        public FrmXuatBaoCao(DateTime tuMacDinh, DateTime denMacDinh)
        {
            InitializeComponent();

            dtpTu.MaxDate = DateTime.Today;
            dtpTu.Value = tuMacDinh > DateTime.Today ? DateTime.Today : tuMacDinh;

            dtpDen.MaxDate = DateTime.Today;
            dtpDen.Value = denMacDinh > DateTime.Today ? DateTime.Today : denMacDinh;

            rdoThuChi.ForeColor = Theme.TextSub;
            rdoTatCa.ForeColor = Theme.TextSub;

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
            if (!ValidationHelper.KhoangNgay(dtpTu.Value, dtpDen.Value, out loi))
            {
                MessageBox.Show(loi, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDen.Focus();
                return;
            }

            KetQua = new BaoCaoYeuCau
            {
                Tu = dtpTu.Value.Date,
                Den = dtpDen.Value.Date,
                PhamVi = rdoTatCa.Checked ? "TatCa" : "ThuChi"
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void rdoTatCa_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}