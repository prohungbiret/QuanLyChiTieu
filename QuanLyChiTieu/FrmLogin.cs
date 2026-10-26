using System;
using System.Drawing; // Cần thêm dòng này để dùng Point
using System.Windows.Forms;

namespace QuanLyChiTieu
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            lblLoi.Text = "";

            // Tự động căn giữa Logo theo chiều ngang của Form
            // Trục Y giữ cố định (72px) để không đè lên các control bên dưới
            int x = (this.ClientSize.Width - picLogo.Width) / 2;
            int y = 72;
            picLogo.Location = new Point(x, y);
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
            MessageBox.Show("Chức năng khôi phục mật khẩu nằm ngoài phạm vi. Vui lòng liên hệ quản trị.", "Quên mật khẩu",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}