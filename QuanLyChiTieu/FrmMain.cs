using System;
using System.Linq;
using System.Windows.Forms;
using QuanLyChiTieu.Controls;

namespace QuanLyChiTieu
{
    // TV1 sở hữu. Điều hướng: sidebar 9 mục → ShowControl vào pnlContent (Hợp đồng 5.3).
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!CurrentUser.DaDangNhap)
            {
                Close(); // mở FrmMain mà chưa có phiên → thoát (lỗi luồng)
                return;
            }
            CapNhatUserChip();
            btnNavTrangChu.PerformClick(); // mục mặc định: Trang chủ
        }

        private void CapNhatUserChip()
        {
            lblUserTen.Text = CurrentUser.TenHienThi;
            lblUserTenDangNhap.Text = "QuanLyChiTieu";
            string ten = (CurrentUser.TenHienThi ?? "?").Trim();
            string[] cacTu = ten.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (cacTu.Length >= 2)
            {
                avatar.Text = cacTu[0].Substring(0, 1).ToUpper()
                    + cacTu[cacTu.Length - 1].Substring(0, 1).ToUpper();
            }
            else if (ten.Length > 0)
            {
                avatar.Text = ten.Substring(0, 1).ToUpper();
            }
        }

        // ---- Hợp đồng 5.3: ShowControl chuẩn v2 (có Dispose control cũ) ----
        private void ShowControl(System.Windows.Forms.UserControl control)
        {
            var oldControls = pnlContent.Controls.OfType<UserControl>().ToList();
            pnlContent.Controls.Clear();
            foreach (var old in oldControls)
            {
                old.Dispose();
            }
            control.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(control);

            lblHeaderTitle.Text = (control.Tag as string) ?? "Trang chủ";
            lblHeaderSub.Text = "Quản lý chi tiêu cá nhân — " + CurrentUser.TenHienThi;
        }

        private void MucDangChon(Button muc)
        {
            foreach (var b in navPanel.Controls.OfType<Button>())
            {
                b.BackColor = System.Drawing.Color.Transparent;
                b.ForeColor = System.Drawing.Color.FromArgb(201, 211, 224);
            }
            muc.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            muc.ForeColor = System.Drawing.Color.White;
        }

        // Điều hướng: tạo mới UserControl mỗi lần (dữ liệu luôn tươi, Hợp đồng 5.3)
        private void OpenDangXayDung(Button muc, string tenPhanHe, string nguoiPhuTrach)
        {
            MucDangChon(muc);
            ShowControl(new UcDangXayDung(tenPhanHe, nguoiPhuTrach));
        }

        private void btnNavTrangChu_Click(object sender, EventArgs e)
        {
            MucDangChon(btnNavTrangChu);
            ShowControl(new UcDashboard());
        }

        private void btnNavGiaoDich_Click(object sender, EventArgs e)
        {
            MucDangChon(btnNavGiaoDich);
            ShowControl(new Controls.UcGiaoDich());
        }

        private void btnNavViTien_Click(object sender, EventArgs e)
        {
            MucDangChon(btnNavViTien);
            ShowControl(new Controls.UcViTien());
        }

        private void btnNavDanhMuc_Click(object sender, EventArgs e)
        {
            OpenDangXayDung(btnNavDanhMuc, "Danh mục", "TV3");
        }

        private void btnNavNganSach_Click(object sender, EventArgs e)
        {
            OpenDangXayDung(btnNavNganSach, "Ngân sách", "TV3");
        }

        private void btnNavTietKiem_Click(object sender, EventArgs e)
        {
            OpenDangXayDung(btnNavTietKiem, "Tiết kiệm", "TV4");
        }

        private void btnNavVayNo_Click(object sender, EventArgs e)
        {
            OpenDangXayDung(btnNavVayNo, "Vay nợ", "TV4");
        }

        private void btnNavThongKe_Click(object sender, EventArgs e)
        {
            OpenDangXayDung(btnNavThongKe, "Thống kê + biểu đồ", "TV3");
        }

        private void btnNavTaiKhoan_Click(object sender, EventArgs e)
        {
            OpenDangXayDung(btnNavTaiKhoan, "Tài khoản / Đổi mật khẩu", "TV3 (FrmChangePassword)");
        }

        private void lnkDangXuat_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn đăng xuất?", "Đăng xuất",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Services.AuthService.DangXuat();
                Close(); // về FrmLogin ở lần chạy kế tiếp (Program.cs)
            }
        }
    }
}
