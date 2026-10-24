using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace QuanLyChiTieu.Forms
{
    public partial class FrmRegister : Form
    {
        Classes.DatabaseProcess dtBase = new Classes.DatabaseProcess();
        public FrmRegister()
        {
            InitializeComponent();
        }
        private bool KiemTraDinhDangEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email.Trim(), pattern, RegexOptions.IgnoreCase);
        }
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string tendn = txtTenDangNhap.Text.Trim();
            string tenht = txtTenHienThi.Text.Trim();
            string email = txtEmail.Text.Trim();
            string mk = txtMatKhau.Text.Trim();
            string xnmk = txtXacNhanMatKhau.Text.Trim();
            if(tendn == "")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập");
                txtTenDangNhap.Focus();
                return;
            }
            if(tenht == "")
            {
                MessageBox.Show("Vui lòng nhập tên hiển thị");
                txtTenHienThi.Focus();
                return;
            }
            if(email == "")
            {
                MessageBox.Show("Vui lòng nhập email");
                txtEmail.Focus();
                return;
            }
            if(mk == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
                txtMatKhau.Focus();
                return;
            }
            if(xnmk == "")
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu");
                txtXacNhanMatKhau.Focus();
                return;
            }
            if(mk != xnmk)
            {
                MessageBox.Show("Xác nhận mật khẩu không chính xác");
                txtXacNhanMatKhau.Focus();
                return;
            }
            string sqlTDN = "select TenDangNhap from NguoiDung where TenDangNhap = @TenDangNhap";
            DataTable dt1 = dtBase.DataReader(sqlTDN, new SqlParameter[] { new SqlParameter("@TenDangNhap", tendn) });
            if(dt1.Rows.Count > 0 )
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return ;
            }
            if(!KiemTraDinhDangEmail(email))
            {
                MessageBox.Show("Email không đúng định dạng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                txtEmail.SelectAll();
                return;
            }
            string sqlEmail = "select Email from NguoiDung where Email = @Email";
            DataTable dt2 = dtBase.DataReader(sqlEmail, new SqlParameter[] { new SqlParameter("@Email", email) });
            if(dt2.Rows.Count > 0 )
            {
                MessageBox.Show("Email này đã đăng ký tài khoản khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }
            try
            {
                string sqlInsert = "insert into NguoiDung(TenDangNhap,MatKhauHash,TenHienThi,Email) values (@TenDangNhap, @MatKhauHash, @TenHienThi, @Email)";
                SqlParameter[] p = new SqlParameter[]
                {
                    new SqlParameter("@TenDangNhap", tendn),
                    new SqlParameter("@MatKhauHash", Services.AuthService.HashPassword(mk)),
                    new SqlParameter("@TenHienThi", tenht),
                    new SqlParameter("@Email", email)
                };
                dtBase.ChangeData(sqlInsert, p);
                MessageBox.Show("Đăng ký tài khoản thành công! Vui lòng đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btntThoat_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn quay lại giao diện đăng nhập không","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
