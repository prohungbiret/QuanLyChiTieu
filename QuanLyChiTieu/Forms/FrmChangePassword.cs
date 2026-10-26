using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Data;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    public partial class FrmChangePassword : Form
    {
        public FrmChangePassword()
        {
            InitializeComponent();

            // Áp theme runtime
            BackColor = Theme.White;
            ForeColor = Theme.Text;
            Font = new Font("Segoe UI", 10f);
            pnlNut.BackColor = Theme.White;
            lblLoi.ForeColor = Theme.Chi;
            btnLuu.BackColor = Theme.Primary;
            btnLuu.ForeColor = Theme.White;
            btnLuu.Size = new Size(130, Theme.CaoNut);
            btnHuy.BackColor = Theme.Background;
            btnHuy.ForeColor = Theme.Text;
            btnHuy.Size = new Size(90, Theme.CaoNut);

            AcceptButton = btnLuu;
            CancelButton = btnHuy;
            ApDungHienMatKhau();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            ApDungHienMatKhau();
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ApDungHienMatKhau()
        {
            bool hien = !chkHienMatKhau.Checked;
            txtHienTai.UseSystemPasswordChar = hien;
            txtMoi.UseSystemPasswordChar = hien;
            txtNhapLai.UseSystemPasswordChar = hien;
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            lblLoi.Text = "";

            string hienTai = txtHienTai.Text;
            string moi = txtMoi.Text;
            string nhapLai = txtNhapLai.Text;

            if (hienTai.Length == 0) { BaoLoi(txtHienTai, "Vui lòng nhập mật khẩu hiện tại."); return; }
            if (moi.Length < 6) { BaoLoi(txtMoi, "Mật khẩu mới phải có ít nhất 6 ký tự."); return; }
            if (moi == hienTai) { BaoLoi(txtMoi, "Mật khẩu mới phải khác mật khẩu hiện tại."); return; }
            if (moi != nhapLai) { BaoLoi(txtNhapLai, "Mật khẩu nhập lại không khớp."); return; }

            try
            {
                string loi;
                if (!DoiMatKhau(hienTai, moi, out loi))
                {
                    lblLoi.Text = loi;
                    txtHienTai.Clear();
                    txtHienTai.Focus();
                    return;
                }
            }
            catch (Exception)
            {
                lblLoi.Text = "Không kết nối được cơ sở dữ liệu. Hãy kiểm tra LocalDB.";
                return;
            }

            MessageBox.Show("Đổi mật khẩu thành công. Hãy dùng mật khẩu mới ở lần đăng nhập sau.",
                "Đổi mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BaoLoi(Control ctl, string thongDiep)
        {
            lblLoi.Text = thongDiep;
            ctl.Focus();
        }

        public static bool DoiMatKhau(string matKhauHienTai, string matKhauMoi, out string loi)
        {
            loi = null;
            if (!CurrentUser.DaDangNhap)
            {
                loi = "Chưa đăng nhập.";
                return false;
            }

            string hashDaLuu;
            string sqlDoc = @"SELECT MatKhauHash FROM NguoiDung WHERE NguoiDungID = @NguoiDungID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sqlDoc, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                object kq = cmd.ExecuteScalar();
                hashDaLuu = kq == null || kq == DBNull.Value ? null : kq.ToString();
            }

            if (hashDaLuu == null || !AuthService.VerifyPassword(matKhauHienTai, hashDaLuu))
            {
                loi = "Mật khẩu hiện tại không đúng.";
                return false;
            }

            string hashMoi = AuthService.HashPassword(matKhauMoi);
            string sqlGhi = @"UPDATE NguoiDung SET MatKhauHash = @MatKhauHash
                              WHERE NguoiDungID = @NguoiDungID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sqlGhi, conn))
            {
                cmd.Parameters.Add("@MatKhauHash", SqlDbType.NVarChar, 255).Value = hashMoi;
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                if (cmd.ExecuteNonQuery() != 1)
                {
                    loi = "Không cập nhật được mật khẩu.";
                    return false;
                }
            }
            return true;
        }
    }
}