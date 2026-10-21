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
    // TV3 (Hải) sở hữu — Đổi mật khẩu (FR01 / FR-03 của bản v2, TC-AUTH-06/08).
    // Quy tắc (Tài liệu 01 mục 1.5A, 04 mục 4.9, QD08):
    //  - Bắt buộc xác minh mật khẩu hiện tại trước khi đổi.
    //  - Mật khẩu mới băm bằng AuthService (PBKDF2 100.000 vòng + salt riêng) —
    //    form này KHÔNG tự băm, KHÔNG lưu mật khẩu thuần.
    //  - UPDATE gắn NguoiDungID = CurrentUser — chỉ đổi được mật khẩu của chính mình.
    // Mở từ FrmMain > Tài khoản (sau khi TV1 nối nút) hoặc từ nơi bất kỳ bằng ShowDialog().
    public class FrmChangePassword : Form
    {
        private readonly TextBox txtHienTai = new TextBox();
        private readonly TextBox txtMoi = new TextBox();
        private readonly TextBox txtNhapLai = new TextBox();
        private readonly CheckBox chkHienMatKhau = new CheckBox();
        private readonly Label lblLoi = new Label();

        public FrmChangePassword()
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            BackColor = Theme.White;
            ForeColor = Theme.Text;
            Font = new Font("Segoe UI", 10f);
            Text = "Đổi mật khẩu";
            ClientSize = new Size(440, 330);

            int y = 20;
            Controls.Add(TaoNhan("Mật khẩu hiện tại *", ref y));
            CauHinhO(txtHienTai, ref y);
            Controls.Add(TaoNhan("Mật khẩu mới *", ref y));
            CauHinhO(txtMoi, ref y);
            Controls.Add(TaoNhan("Nhập lại mật khẩu mới *", ref y));
            CauHinhO(txtNhapLai, ref y);

            chkHienMatKhau.Text = "Hiện mật khẩu";
            chkHienMatKhau.AutoSize = true;
            chkHienMatKhau.Location = new Point(24, y);
            chkHienMatKhau.CheckedChanged += (s, e) => ApDungHienMatKhau();
            Controls.Add(chkHienMatKhau);
            y += 30;

            lblLoi.ForeColor = Theme.Chi;
            lblLoi.Location = new Point(24, y);
            lblLoi.Size = new Size(392, 36);
            Controls.Add(lblLoi);
            y += 40;

            var pnlNut = new FlowLayoutPanel();
            pnlNut.Dock = DockStyle.Bottom;
            pnlNut.Height = 56;
            pnlNut.FlowDirection = FlowDirection.RightToLeft;
            pnlNut.Padding = new Padding(16, 8, 16, 8);
            pnlNut.BackColor = Theme.White;

            var btnLuu = new Button();
            btnLuu.Text = "Đổi mật khẩu";
            btnLuu.Size = new Size(130, Theme.CaoNut);
            btnLuu.BackColor = Theme.Primary;
            btnLuu.ForeColor = Theme.White;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += BtnLuu_Click;

            var btnHuy = new Button();
            btnHuy.Text = "Hủy";
            btnHuy.Size = new Size(90, Theme.CaoNut);
            btnHuy.BackColor = Theme.Background;
            btnHuy.ForeColor = Theme.Text;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            pnlNut.Controls.Add(btnHuy);
            pnlNut.Controls.Add(btnLuu);
            Controls.Add(pnlNut);

            AcceptButton = btnLuu;
            CancelButton = btnHuy;
            ApDungHienMatKhau();
        }

        private static Label TaoNhan(string text, ref int y)
        {
            var lbl = new Label();
            lbl.Text = text;
            lbl.AutoSize = true;
            lbl.Location = new Point(24, y);
            y += 22;
            return lbl;
        }

        private void CauHinhO(TextBox txt, ref int y)
        {
            txt.Location = new Point(24, y);
            txt.Size = new Size(392, Theme.CaoControl);
            txt.UseSystemPasswordChar = true;
            Controls.Add(txt);
            y += 44;
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
                // NFR07: không lộ chi tiết kết nối
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

        /// <summary>
        /// Kiểm tra mật khẩu hiện tại bằng AuthService.VerifyPassword, sau đó lưu
        /// hash mới bằng AuthService.HashPassword. Trả false + loi nếu sai mật khẩu hiện tại.
        /// </summary>
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
