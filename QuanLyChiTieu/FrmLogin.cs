using System;
using System.Windows.Forms;

namespace QuanLyChiTieu
{
    // TV1 sở hữu. Đăng nhập → AuthService → CurrentUser → mở FrmMain (Program.cs điều phối).
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            lblLoi.Text = "";
        }

        private void logoLabel_Click(object sender, EventArgs e)
        {
            // Logo chỉ mang tính hiển thị, không gắn hành vi.
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string ten = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            if (ten.Length == 0)
            {
                lblLoi.Text = "Vui lòng nhập tên đăng nhập.";
                txtTenDangNhap.Focus();
                return;
            }
            if (matKhau.Length == 0)
            {
                lblLoi.Text = "Vui lòng nhập mật khẩu.";
                txtMatKhau.Focus();
                return;
            }

            try
            {
                string loi;
                if (Services.AuthService.DangNhap(ten, matKhau, out loi))
                {
                    DialogResult = DialogResult.OK; // chỉ OK khi đăng nhập thành công
                    Close();
                }
                else
                {
                    lblLoi.Text = loi;
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
            }
            catch (Exception)
            {
                // NFR07: không lộ connection string, hướng dẫn kiểm tra LocalDB
                lblLoi.Text = "Không kết nối được cơ sở dữ liệu. Hãy kiểm tra LocalDB.";
            }
        }

        private void lnkDangKy_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (Forms.FrmRegister frm = new Forms.FrmRegister())
            {
                frm.ShowDialog();
            }
        }

        private void lnkQuenMatKhau_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Ngoài phạm vi phiên bản đầu (mockup login.html).
            MessageBox.Show("Chức năng khôi phục mật khẩu nằm ngoài phạm vi. Vui lòng liên hệ quản trị.", "Quên mật khẩu",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
