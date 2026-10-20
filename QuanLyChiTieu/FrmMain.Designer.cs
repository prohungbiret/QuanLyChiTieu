namespace QuanLyChiTieu
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // Đối chiếu mockup "dashboard.html" (khung shell): sidebar Navy 232px Dock Left,
        // header 60px Dock Top, pnlContent Dock Fill nền #F4F7FB, user chip cuối sidebar.
        private void InitializeComponent()
        {
            this.sidebar = new System.Windows.Forms.Panel();
            this.brandPanel = new System.Windows.Forms.Panel();
            this.lblBrand1 = new System.Windows.Forms.Label();
            this.lblBrand2 = new System.Windows.Forms.Label();
            this.navPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNavTrangChu = new System.Windows.Forms.Button();
            this.btnNavGiaoDich = new System.Windows.Forms.Button();
            this.btnNavViTien = new System.Windows.Forms.Button();
            this.btnNavDanhMuc = new System.Windows.Forms.Button();
            this.btnNavNganSach = new System.Windows.Forms.Button();
            this.btnNavTietKiem = new System.Windows.Forms.Button();
            this.btnNavVayNo = new System.Windows.Forms.Button();
            this.btnNavThongKe = new System.Windows.Forms.Button();
            this.btnNavTaiKhoan = new System.Windows.Forms.Button();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.avatar = new System.Windows.Forms.Label();
            this.lblUserTen = new System.Windows.Forms.Label();
            this.lblUserTenDangNhap = new System.Windows.Forms.Label();
            this.lnkDangXuat = new System.Windows.Forms.LinkLabel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblHeaderSub = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.sidebar.SuspendLayout();
            this.brandPanel.SuspendLayout();
            this.navPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // sidebar
            //
            this.sidebar.AutoScroll = true;
            this.sidebar.BackColor = System.Drawing.Color.FromArgb(23, 43, 77);
            this.sidebar.Controls.Add(this.navPanel);
            this.sidebar.Controls.Add(this.footerPanel);
            this.sidebar.Controls.Add(this.brandPanel);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(232, 780);
            //
            // brandPanel
            //
            this.brandPanel.Controls.Add(this.lblBrand1);
            this.brandPanel.Controls.Add(this.lblBrand2);
            this.brandPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.brandPanel.Name = "brandPanel";
            this.brandPanel.Size = new System.Drawing.Size(232, 78);
            //
            // lblBrand1
            //
            this.lblBrand1.Font = new System.Drawing.Font("Segoe UI", 11.5f, System.Drawing.FontStyle.Bold);
            this.lblBrand1.ForeColor = System.Drawing.Color.White;
            this.lblBrand1.Location = new System.Drawing.Point(18, 18);
            this.lblBrand1.Name = "lblBrand1";
            this.lblBrand1.Size = new System.Drawing.Size(200, 24);
            this.lblBrand1.Text = "QuanLyChiTieu";
            //
            // lblBrand2
            //
            this.lblBrand2.Font = new System.Drawing.Font("Segoe UI", 8f);
            this.lblBrand2.ForeColor = System.Drawing.Color.FromArgb(143, 160, 184);
            this.lblBrand2.Location = new System.Drawing.Point(18, 44);
            this.lblBrand2.Name = "lblBrand2";
            this.lblBrand2.Size = new System.Drawing.Size(200, 18);
            this.lblBrand2.Text = "Quản lý chi tiêu cá nhân";
            //
            // navPanel — FlowLayoutPanel TopDown: mục hiển thị đúng thứ tự thêm
            //
            this.navPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.navPanel.WrapContents = false;
            this.navPanel.Name = "navPanel";
            this.navPanel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.navPanel.Controls.Add(this.btnNavTrangChu);
            this.navPanel.Controls.Add(this.btnNavGiaoDich);
            this.navPanel.Controls.Add(this.btnNavViTien);
            this.navPanel.Controls.Add(this.btnNavDanhMuc);
            this.navPanel.Controls.Add(this.btnNavNganSach);
            this.navPanel.Controls.Add(this.btnNavTietKiem);
            this.navPanel.Controls.Add(this.btnNavVayNo);
            this.navPanel.Controls.Add(this.btnNavThongKe);
            this.navPanel.Controls.Add(this.btnNavTaiKhoan);
            //
            // 9 nút nav — gán thuộc tính trực tiếp (designer-safe, không gọi hàm custom)
            //
            this.btnNavTrangChu.FlatAppearance.BorderSize = 0;
            this.btnNavTrangChu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavTrangChu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavTrangChu.ForeColor = System.Drawing.Color.FromArgb(201, 211, 224);
            this.btnNavTrangChu.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnNavTrangChu.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnNavTrangChu.Size = new System.Drawing.Size(220, 44);
            this.btnNavTrangChu.Text = "▶  Trang chủ";
            this.btnNavTrangChu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavGiaoDich.FlatAppearance.BorderSize = 0;
            this.btnNavGiaoDich.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavGiaoDich.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavGiaoDich.ForeColor = System.Drawing.Color.FromArgb(201, 211, 224);
            this.btnNavGiaoDich.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnNavGiaoDich.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnNavGiaoDich.Size = new System.Drawing.Size(220, 44);
            this.btnNavGiaoDich.Text = "⇅  Giao dịch";
            this.btnNavGiaoDich.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavViTien.FlatAppearance.BorderSize = 0;
            this.btnNavViTien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavViTien.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavViTien.ForeColor = System.Drawing.Color.FromArgb(201, 211, 224);
            this.btnNavViTien.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnNavViTien.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnNavViTien.Size = new System.Drawing.Size(220, 44);
            this.btnNavViTien.Text = "▭  Ví tiền";
            this.btnNavViTien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavDanhMuc.FlatAppearance.BorderSize = 0;
            this.btnNavDanhMuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavDanhMuc.ForeColor = System.Drawing.Color.FromArgb(201, 211, 224);
            this.btnNavDanhMuc.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnNavDanhMuc.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnNavDanhMuc.Size = new System.Drawing.Size(220, 44);
            this.btnNavDanhMuc.Text = "☰  Danh mục";
            this.btnNavDanhMuc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavNganSach.FlatAppearance.BorderSize = 0;
            this.btnNavNganSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavNganSach.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavNganSach.ForeColor = System.Drawing.Color.FromArgb(201, 211, 224);
            this.btnNavNganSach.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnNavNganSach.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnNavNganSach.Size = new System.Drawing.Size(220, 44);
            this.btnNavNganSach.Text = "◔  Ngân sách";
            this.btnNavNganSach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavTietKiem.FlatAppearance.BorderSize = 0;
            this.btnNavTietKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavTietKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavTietKiem.ForeColor = System.Drawing.Color.FromArgb(201, 211, 224);
            this.btnNavTietKiem.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnNavTietKiem.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnNavTietKiem.Size = new System.Drawing.Size(220, 44);
            this.btnNavTietKiem.Text = "◉  Tiết kiệm";
            this.btnNavTietKiem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavVayNo.FlatAppearance.BorderSize = 0;
            this.btnNavVayNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavVayNo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavVayNo.ForeColor = System.Drawing.Color.FromArgb(201, 211, 224);
            this.btnNavVayNo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnNavVayNo.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnNavVayNo.Size = new System.Drawing.Size(220, 44);
            this.btnNavVayNo.Text = "⇄  Vay nợ";
            this.btnNavVayNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavThongKe.FlatAppearance.BorderSize = 0;
            this.btnNavThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavThongKe.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavThongKe.ForeColor = System.Drawing.Color.FromArgb(201, 211, 224);
            this.btnNavThongKe.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnNavThongKe.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnNavThongKe.Size = new System.Drawing.Size(220, 44);
            this.btnNavThongKe.Text = "▤  Thống kê";
            this.btnNavThongKe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavTaiKhoan.FlatAppearance.BorderSize = 0;
            this.btnNavTaiKhoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(201, 211, 224);
            this.btnNavTaiKhoan.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnNavTaiKhoan.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnNavTaiKhoan.Size = new System.Drawing.Size(220, 44);
            this.btnNavTaiKhoan.Text = "⚙  Tài khoản";
            this.btnNavTaiKhoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavTrangChu.Click += new System.EventHandler(this.btnNavTrangChu_Click);
            this.btnNavGiaoDich.Click += new System.EventHandler(this.btnNavGiaoDich_Click);
            this.btnNavViTien.Click += new System.EventHandler(this.btnNavViTien_Click);
            this.btnNavDanhMuc.Click += new System.EventHandler(this.btnNavDanhMuc_Click);
            this.btnNavNganSach.Click += new System.EventHandler(this.btnNavNganSach_Click);
            this.btnNavTietKiem.Click += new System.EventHandler(this.btnNavTietKiem_Click);
            this.btnNavVayNo.Click += new System.EventHandler(this.btnNavVayNo_Click);
            this.btnNavThongKe.Click += new System.EventHandler(this.btnNavThongKe_Click);
            this.btnNavTaiKhoan.Click += new System.EventHandler(this.btnNavTaiKhoan_Click);
            //
            // footerPanel
            //
            this.footerPanel.Controls.Add(this.avatar);
            this.footerPanel.Controls.Add(this.lblUserTen);
            this.footerPanel.Controls.Add(this.lblUserTenDangNhap);
            this.footerPanel.Controls.Add(this.lnkDangXuat);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Padding = new System.Windows.Forms.Padding(14, 12, 14, 12);
            this.footerPanel.Size = new System.Drawing.Size(232, 84);
            //
            // avatar
            //
            this.avatar.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.avatar.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.avatar.ForeColor = System.Drawing.Color.White;
            this.avatar.Location = new System.Drawing.Point(14, 14);
            this.avatar.Name = "avatar";
            this.avatar.Size = new System.Drawing.Size(34, 34);
            this.avatar.Text = "TK";
            this.avatar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblUserTen
            //
            this.lblUserTen.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblUserTen.ForeColor = System.Drawing.Color.White;
            this.lblUserTen.Location = new System.Drawing.Point(54, 13);
            this.lblUserTen.Name = "lblUserTen";
            this.lblUserTen.Size = new System.Drawing.Size(160, 17);
            this.lblUserTen.Text = "Người dùng";
            //
            // lblUserTenDangNhap
            //
            this.lblUserTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 7.5f);
            this.lblUserTenDangNhap.ForeColor = System.Drawing.Color.FromArgb(143, 160, 184);
            this.lblUserTenDangNhap.Location = new System.Drawing.Point(54, 31);
            this.lblUserTenDangNhap.Name = "lblUserTenDangNhap";
            this.lblUserTenDangNhap.Size = new System.Drawing.Size(160, 16);
            this.lblUserTenDangNhap.Text = "";
            //
            // lnkDangXuat
            //
            this.lnkDangXuat.LinkColor = System.Drawing.Color.FromArgb(157, 192, 255);
            this.lnkDangXuat.Location = new System.Drawing.Point(54, 50);
            this.lnkDangXuat.Name = "lnkDangXuat";
            this.lnkDangXuat.Size = new System.Drawing.Size(160, 18);
            this.lnkDangXuat.TabStop = true;
            this.lnkDangXuat.Text = "Đăng xuất";
            this.lnkDangXuat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDangXuat_Click);
            //
            // headerPanel
            //
            this.headerPanel.BackColor = System.Drawing.Color.White;
            this.headerPanel.Controls.Add(this.lblHeaderTitle);
            this.headerPanel.Controls.Add(this.lblHeaderSub);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1048, 60);
            //
            // lblHeaderTitle
            //
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12.5f, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51);
            this.lblHeaderTitle.Location = new System.Drawing.Point(24, 8);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(700, 26);
            this.lblHeaderTitle.Text = "Trang chủ";
            //
            // lblHeaderSub
            //
            this.lblHeaderSub.Font = new System.Drawing.Font("Segoe UI", 7.5f);
            this.lblHeaderSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblHeaderSub.Location = new System.Drawing.Point(24, 36);
            this.lblHeaderSub.Name = "lblHeaderSub";
            this.lblHeaderSub.Size = new System.Drawing.Size(900, 17);
            this.lblHeaderSub.Text = "";
            //
            // pnlContent
            //
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(244, 247, 251);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(24, 24, 24, 24);
            //
            // FrmMain
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1280, 780);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.sidebar);
            this.MinimumSize = new System.Drawing.Size(1024, 650);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý chi tiêu cá nhân";
            this.sidebar.ResumeLayout(false);
            this.brandPanel.ResumeLayout(false);
            this.navPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel sidebar;
        private System.Windows.Forms.Panel brandPanel;
        private System.Windows.Forms.Label lblBrand1;
        private System.Windows.Forms.Label lblBrand2;
        private System.Windows.Forms.FlowLayoutPanel navPanel;
        private System.Windows.Forms.Button btnNavTrangChu;
        private System.Windows.Forms.Button btnNavGiaoDich;
        private System.Windows.Forms.Button btnNavViTien;
        private System.Windows.Forms.Button btnNavDanhMuc;
        private System.Windows.Forms.Button btnNavNganSach;
        private System.Windows.Forms.Button btnNavTietKiem;
        private System.Windows.Forms.Button btnNavVayNo;
        private System.Windows.Forms.Button btnNavThongKe;
        private System.Windows.Forms.Button btnNavTaiKhoan;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Label avatar;
        private System.Windows.Forms.Label lblUserTen;
        private System.Windows.Forms.Label lblUserTenDangNhap;
        private System.Windows.Forms.LinkLabel lnkDangXuat;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSub;
        private System.Windows.Forms.Panel pnlContent;
    }
}
