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
            this.brandPanel = new System.Windows.Forms.Panel();
            this.lblBrand1 = new System.Windows.Forms.Label();
            this.lblBrand2 = new System.Windows.Forms.Label();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblHeaderSub = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.sidebar.SuspendLayout();
            this.navPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.brandPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebar
            // 
            this.sidebar.AutoScroll = true;
            this.sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(43)))), ((int)(((byte)(77)))));
            this.sidebar.Controls.Add(this.navPanel);
            this.sidebar.Controls.Add(this.footerPanel);
            this.sidebar.Controls.Add(this.brandPanel);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Location = new System.Drawing.Point(0, 0);
            this.sidebar.Margin = new System.Windows.Forms.Padding(4);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(290, 975);
            this.sidebar.TabIndex = 2;
            // 
            // navPanel
            // 
            this.navPanel.Controls.Add(this.btnNavTrangChu);
            this.navPanel.Controls.Add(this.btnNavGiaoDich);
            this.navPanel.Controls.Add(this.btnNavViTien);
            this.navPanel.Controls.Add(this.btnNavDanhMuc);
            this.navPanel.Controls.Add(this.btnNavNganSach);
            this.navPanel.Controls.Add(this.btnNavTietKiem);
            this.navPanel.Controls.Add(this.btnNavVayNo);
            this.navPanel.Controls.Add(this.btnNavThongKe);
            this.navPanel.Controls.Add(this.btnNavTaiKhoan);
            this.navPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.navPanel.Location = new System.Drawing.Point(0, 98);
            this.navPanel.Margin = new System.Windows.Forms.Padding(4);
            this.navPanel.Name = "navPanel";
            this.navPanel.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.navPanel.Size = new System.Drawing.Size(290, 772);
            this.navPanel.TabIndex = 0;
            this.navPanel.WrapContents = false;
            // 
            // btnNavTrangChu
            // 
            this.btnNavTrangChu.FlatAppearance.BorderSize = 0;
            this.btnNavTrangChu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavTrangChu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavTrangChu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(211)))), ((int)(((byte)(224)))));
            this.btnNavTrangChu.Location = new System.Drawing.Point(8, 12);
            this.btnNavTrangChu.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnNavTrangChu.Name = "btnNavTrangChu";
            this.btnNavTrangChu.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavTrangChu.Size = new System.Drawing.Size(275, 55);
            this.btnNavTrangChu.TabIndex = 0;
            this.btnNavTrangChu.Text = "▶  Trang chủ";
            this.btnNavTrangChu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavTrangChu.Click += new System.EventHandler(this.btnNavTrangChu_Click);
            // 
            // btnNavGiaoDich
            // 
            this.btnNavGiaoDich.FlatAppearance.BorderSize = 0;
            this.btnNavGiaoDich.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavGiaoDich.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavGiaoDich.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(211)))), ((int)(((byte)(224)))));
            this.btnNavGiaoDich.Location = new System.Drawing.Point(8, 67);
            this.btnNavGiaoDich.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnNavGiaoDich.Name = "btnNavGiaoDich";
            this.btnNavGiaoDich.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavGiaoDich.Size = new System.Drawing.Size(275, 55);
            this.btnNavGiaoDich.TabIndex = 1;
            this.btnNavGiaoDich.Text = "⇅  Giao dịch";
            this.btnNavGiaoDich.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavGiaoDich.Click += new System.EventHandler(this.btnNavGiaoDich_Click);
            // 
            // btnNavViTien
            // 
            this.btnNavViTien.FlatAppearance.BorderSize = 0;
            this.btnNavViTien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavViTien.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavViTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(211)))), ((int)(((byte)(224)))));
            this.btnNavViTien.Location = new System.Drawing.Point(8, 122);
            this.btnNavViTien.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnNavViTien.Name = "btnNavViTien";
            this.btnNavViTien.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavViTien.Size = new System.Drawing.Size(275, 55);
            this.btnNavViTien.TabIndex = 2;
            this.btnNavViTien.Text = "▭  Ví tiền";
            this.btnNavViTien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavViTien.Click += new System.EventHandler(this.btnNavViTien_Click);
            // 
            // btnNavDanhMuc
            // 
            this.btnNavDanhMuc.FlatAppearance.BorderSize = 0;
            this.btnNavDanhMuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavDanhMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(211)))), ((int)(((byte)(224)))));
            this.btnNavDanhMuc.Location = new System.Drawing.Point(8, 177);
            this.btnNavDanhMuc.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnNavDanhMuc.Name = "btnNavDanhMuc";
            this.btnNavDanhMuc.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavDanhMuc.Size = new System.Drawing.Size(275, 55);
            this.btnNavDanhMuc.TabIndex = 3;
            this.btnNavDanhMuc.Text = "☰  Danh mục";
            this.btnNavDanhMuc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavDanhMuc.Click += new System.EventHandler(this.btnNavDanhMuc_Click);
            // 
            // btnNavNganSach
            // 
            this.btnNavNganSach.FlatAppearance.BorderSize = 0;
            this.btnNavNganSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavNganSach.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavNganSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(211)))), ((int)(((byte)(224)))));
            this.btnNavNganSach.Location = new System.Drawing.Point(8, 232);
            this.btnNavNganSach.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnNavNganSach.Name = "btnNavNganSach";
            this.btnNavNganSach.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavNganSach.Size = new System.Drawing.Size(275, 55);
            this.btnNavNganSach.TabIndex = 4;
            this.btnNavNganSach.Text = "◔  Ngân sách";
            this.btnNavNganSach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavNganSach.Click += new System.EventHandler(this.btnNavNganSach_Click);
            // 
            // btnNavTietKiem
            // 
            this.btnNavTietKiem.FlatAppearance.BorderSize = 0;
            this.btnNavTietKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavTietKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavTietKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(211)))), ((int)(((byte)(224)))));
            this.btnNavTietKiem.Location = new System.Drawing.Point(8, 287);
            this.btnNavTietKiem.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnNavTietKiem.Name = "btnNavTietKiem";
            this.btnNavTietKiem.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavTietKiem.Size = new System.Drawing.Size(275, 55);
            this.btnNavTietKiem.TabIndex = 5;
            this.btnNavTietKiem.Text = "◉  Tiết kiệm";
            this.btnNavTietKiem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavTietKiem.Click += new System.EventHandler(this.btnNavTietKiem_Click);
            // 
            // btnNavVayNo
            // 
            this.btnNavVayNo.FlatAppearance.BorderSize = 0;
            this.btnNavVayNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavVayNo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavVayNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(211)))), ((int)(((byte)(224)))));
            this.btnNavVayNo.Location = new System.Drawing.Point(8, 342);
            this.btnNavVayNo.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnNavVayNo.Name = "btnNavVayNo";
            this.btnNavVayNo.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavVayNo.Size = new System.Drawing.Size(275, 55);
            this.btnNavVayNo.TabIndex = 6;
            this.btnNavVayNo.Text = "⇄  Vay nợ";
            this.btnNavVayNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavVayNo.Click += new System.EventHandler(this.btnNavVayNo_Click);
            // 
            // btnNavThongKe
            // 
            this.btnNavThongKe.FlatAppearance.BorderSize = 0;
            this.btnNavThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavThongKe.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavThongKe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(211)))), ((int)(((byte)(224)))));
            this.btnNavThongKe.Location = new System.Drawing.Point(8, 397);
            this.btnNavThongKe.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnNavThongKe.Name = "btnNavThongKe";
            this.btnNavThongKe.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavThongKe.Size = new System.Drawing.Size(275, 55);
            this.btnNavThongKe.TabIndex = 7;
            this.btnNavThongKe.Text = "▤  Thống kê";
            this.btnNavThongKe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavThongKe.Click += new System.EventHandler(this.btnNavThongKe_Click);
            // 
            // btnNavTaiKhoan
            // 
            this.btnNavTaiKhoan.FlatAppearance.BorderSize = 0;
            this.btnNavTaiKhoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(211)))), ((int)(((byte)(224)))));
            this.btnNavTaiKhoan.Location = new System.Drawing.Point(8, 452);
            this.btnNavTaiKhoan.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnNavTaiKhoan.Name = "btnNavTaiKhoan";
            this.btnNavTaiKhoan.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavTaiKhoan.Size = new System.Drawing.Size(275, 55);
            this.btnNavTaiKhoan.TabIndex = 8;
            this.btnNavTaiKhoan.Text = "⚙  Tài khoản";
            this.btnNavTaiKhoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavTaiKhoan.Click += new System.EventHandler(this.btnNavTaiKhoan_Click);
            // 
            // footerPanel
            // 
            this.footerPanel.Controls.Add(this.avatar);
            this.footerPanel.Controls.Add(this.lblUserTen);
            this.footerPanel.Controls.Add(this.lblUserTenDangNhap);
            this.footerPanel.Controls.Add(this.lnkDangXuat);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 870);
            this.footerPanel.Margin = new System.Windows.Forms.Padding(4);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Padding = new System.Windows.Forms.Padding(18, 15, 18, 15);
            this.footerPanel.Size = new System.Drawing.Size(290, 105);
            this.footerPanel.TabIndex = 1;
            // 
            // avatar
            // 
            this.avatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.avatar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.avatar.ForeColor = System.Drawing.Color.White;
            this.avatar.Location = new System.Drawing.Point(18, 18);
            this.avatar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.avatar.Name = "avatar";
            this.avatar.Size = new System.Drawing.Size(42, 42);
            this.avatar.TabIndex = 0;
            this.avatar.Text = "TK";
            this.avatar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUserTen
            // 
            this.lblUserTen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUserTen.ForeColor = System.Drawing.Color.White;
            this.lblUserTen.Location = new System.Drawing.Point(68, 16);
            this.lblUserTen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserTen.Name = "lblUserTen";
            this.lblUserTen.Size = new System.Drawing.Size(200, 21);
            this.lblUserTen.TabIndex = 1;
            this.lblUserTen.Text = "Người dùng";
            // 
            // lblUserTenDangNhap
            // 
            this.lblUserTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblUserTenDangNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(160)))), ((int)(((byte)(184)))));
            this.lblUserTenDangNhap.Location = new System.Drawing.Point(68, 39);
            this.lblUserTenDangNhap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserTenDangNhap.Name = "lblUserTenDangNhap";
            this.lblUserTenDangNhap.Size = new System.Drawing.Size(200, 20);
            this.lblUserTenDangNhap.TabIndex = 2;
            // 
            // lnkDangXuat
            // 
            this.lnkDangXuat.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lnkDangXuat.Location = new System.Drawing.Point(68, 62);
            this.lnkDangXuat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkDangXuat.Name = "lnkDangXuat";
            this.lnkDangXuat.Size = new System.Drawing.Size(200, 22);
            this.lnkDangXuat.TabIndex = 3;
            this.lnkDangXuat.TabStop = true;
            this.lnkDangXuat.Text = "Đăng xuất";
            this.lnkDangXuat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDangXuat_Click);
            // 
            // brandPanel
            // 
            this.brandPanel.Controls.Add(this.lblBrand1);
            this.brandPanel.Controls.Add(this.lblBrand2);
            this.brandPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.brandPanel.Location = new System.Drawing.Point(0, 0);
            this.brandPanel.Margin = new System.Windows.Forms.Padding(4);
            this.brandPanel.Name = "brandPanel";
            this.brandPanel.Size = new System.Drawing.Size(290, 98);
            this.brandPanel.TabIndex = 2;
            // 
            // lblBrand1
            // 
            this.lblBrand1.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblBrand1.ForeColor = System.Drawing.Color.White;
            this.lblBrand1.Location = new System.Drawing.Point(22, 22);
            this.lblBrand1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBrand1.Name = "lblBrand1";
            this.lblBrand1.Size = new System.Drawing.Size(250, 30);
            this.lblBrand1.TabIndex = 0;
            this.lblBrand1.Text = "QUẢN LÝ CHI TIÊU ";
            // 
            // lblBrand2
            // 
            this.lblBrand2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblBrand2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(160)))), ((int)(((byte)(184)))));
            this.lblBrand2.Location = new System.Drawing.Point(32, 53);
            this.lblBrand2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBrand2.Name = "lblBrand2";
            this.lblBrand2.Size = new System.Drawing.Size(250, 22);
            this.lblBrand2.TabIndex = 1;
            this.lblBrand2.Text = "Money Mate";
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.White;
            this.headerPanel.Controls.Add(this.lblHeaderTitle);
            this.headerPanel.Controls.Add(this.lblHeaderSub);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(290, 0);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(4);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1310, 75);
            this.headerPanel.TabIndex = 1;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.lblHeaderTitle.Location = new System.Drawing.Point(30, 10);
            this.lblHeaderTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(875, 32);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Trang chủ";
            // 
            // lblHeaderSub
            // 
            this.lblHeaderSub.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblHeaderSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblHeaderSub.Location = new System.Drawing.Point(30, 45);
            this.lblHeaderSub.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeaderSub.Name = "lblHeaderSub";
            this.lblHeaderSub.Size = new System.Drawing.Size(1125, 21);
            this.lblHeaderSub.TabIndex = 1;
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(290, 75);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(4);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(30);
            this.pnlContent.Size = new System.Drawing.Size(1310, 900);
            this.pnlContent.TabIndex = 0;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1600, 975);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.sidebar);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1276, 801);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý chi tiêu cá nhân";
            this.sidebar.ResumeLayout(false);
            this.navPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.brandPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel sidebar;
        private System.Windows.Forms.Panel brandPanel;
        private System.Windows.Forms.Label lblBrand1;
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
        private System.Windows.Forms.Label lblBrand2;
    }
}
