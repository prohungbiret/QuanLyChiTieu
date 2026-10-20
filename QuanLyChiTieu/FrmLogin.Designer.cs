namespace QuanLyChiTieu
{
    partial class FrmLogin
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

        // Đối chiếu mockup "login.html": cửa sổ 440×520 FixedDialog,
        // logo ₫ nền Primary, nhãn trên input, lỗi màu #DC2626 dưới ô nhập.
        private void InitializeComponent()
        {
            System.Drawing.Color Navy = System.Drawing.Color.FromArgb(23, 43, 77);
            System.Drawing.Color Primary = System.Drawing.Color.FromArgb(37, 99, 235);
            this.pnlTitlebar = new System.Windows.Forms.Panel();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.logoLabel = new System.Windows.Forms.Label();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblPhu = new System.Windows.Forms.Label();
            this.lblTenDN = new System.Windows.Forms.Label();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.lblLoi = new System.Windows.Forms.Label();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.lnkDangKy = new System.Windows.Forms.LinkLabel();
            this.lnkQuenMatKhau = new System.Windows.Forms.LinkLabel();
            this.pnlTitlebar.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTitlebar
            //
            this.pnlTitlebar.BackColor = Navy;
            this.pnlTitlebar.Controls.Add(this.lblAppTitle);
            this.pnlTitlebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitlebar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitlebar.Name = "pnlTitlebar";
            this.pnlTitlebar.Size = new System.Drawing.Size(440, 48);
            this.pnlTitlebar.TabIndex = 0;
            //
            // lblAppTitle
            //
            this.lblAppTitle.ForeColor = System.Drawing.Color.FromArgb(230, 236, 245);
            this.lblAppTitle.Location = new System.Drawing.Point(18, 0);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(380, 48);
            this.lblAppTitle.TabIndex = 0;
            this.lblAppTitle.Text = "Quản lý chi tiêu cá nhân";
            this.lblAppTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlLogo
            //
            this.pnlLogo.BackColor = Primary;
            this.pnlLogo.Controls.Add(this.logoLabel);
            this.pnlLogo.Location = new System.Drawing.Point(192, 72);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(56, 56);
            this.pnlLogo.TabIndex = 1;
            //
            // logoLabel
            //
            this.logoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoLabel.ForeColor = System.Drawing.Color.White;
            this.logoLabel.Font = new System.Drawing.Font("Segoe UI", 16f, System.Drawing.FontStyle.Bold);
            this.logoLabel.Name = "logoLabel";
            this.logoLabel.Text = "₫";
            this.logoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.logoLabel.Click += new System.EventHandler(this.logoLabel_Click);
            //
            // lblTieuDe
            //
            this.lblTieuDe.AutoSize = false;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12.5f, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(0, 140);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(440, 24);
            this.lblTieuDe.TabIndex = 2;
            this.lblTieuDe.Text = "Đăng nhập";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // lblPhu
            //
            this.lblPhu.AutoSize = false;
            this.lblPhu.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            this.lblPhu.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblPhu.Location = new System.Drawing.Point(0, 166);
            this.lblPhu.Name = "lblPhu";
            this.lblPhu.Size = new System.Drawing.Size(440, 18);
            this.lblPhu.TabIndex = 3;
            this.lblPhu.Text = "Dùng tài khoản đã đăng ký trong hệ thống";
            this.lblPhu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // lblTenDN
            //
            this.lblTenDN.AutoSize = true;
            this.lblTenDN.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblTenDN.Location = new System.Drawing.Point(26, 198);
            this.lblTenDN.Name = "lblTenDN";
            this.lblTenDN.TabIndex = 4;
            this.lblTenDN.Text = "Tên đăng nhập *";
            //
            // txtTenDangNhap
            //
            this.txtTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 10f);
            this.txtTenDangNhap.Location = new System.Drawing.Point(26, 220);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new System.Drawing.Size(388, 26);
            this.txtTenDangNhap.TabIndex = 5;
            //
            // lblMatKhau
            //
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblMatKhau.Location = new System.Drawing.Point(26, 256);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.TabIndex = 6;
            this.lblMatKhau.Text = "Mật khẩu *";
            //
            // txtMatKhau
            //
            this.txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 10f);
            this.txtMatKhau.Location = new System.Drawing.Point(26, 278);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(388, 26);
            this.txtMatKhau.TabIndex = 7;
            this.txtMatKhau.UseSystemPasswordChar = true;
            //
            // lblLoi
            //
            this.lblLoi.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            this.lblLoi.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblLoi.Location = new System.Drawing.Point(26, 310);
            this.lblLoi.Name = "lblLoi";
            this.lblLoi.Size = new System.Drawing.Size(388, 20);
            this.lblLoi.TabIndex = 8;
            //
            // btnDangNhap
            //
            this.btnDangNhap.BackColor = Primary;
            this.btnDangNhap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangNhap.FlatAppearance.BorderSize = 0;
            this.btnDangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangNhap.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            this.btnDangNhap.ForeColor = System.Drawing.Color.White;
            this.btnDangNhap.Location = new System.Drawing.Point(26, 338);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(388, 40);
            this.btnDangNhap.TabIndex = 9;
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.UseVisualStyleBackColor = false;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            //
            // lnkDangKy
            //
            this.lnkDangKy.Location = new System.Drawing.Point(26, 452);
            this.lnkDangKy.Name = "lnkDangKy";
            this.lnkDangKy.Size = new System.Drawing.Size(388, 20);
            this.lnkDangKy.TabIndex = 10;
            this.lnkDangKy.TabStop = true;
            this.lnkDangKy.Text = "Chưa có tài khoản? Đăng ký";
            this.lnkDangKy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnkDangKy.LinkColor = Primary;
            this.lnkDangKy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDangKy_Click);
            //
            // lnkQuenMatKhau
            //
            this.lnkQuenMatKhau.Location = new System.Drawing.Point(26, 430);
            this.lnkQuenMatKhau.Name = "lnkQuenMatKhau";
            this.lnkQuenMatKhau.Size = new System.Drawing.Size(388, 20);
            this.lnkQuenMatKhau.TabIndex = 11;
            this.lnkQuenMatKhau.TabStop = true;
            this.lnkQuenMatKhau.Text = "Quên mật khẩu?";
            this.lnkQuenMatKhau.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnkQuenMatKhau.LinkVisited = false;
            this.lnkQuenMatKhau.LinkColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lnkQuenMatKhau.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkQuenMatKhau_Click);
            //
            // FrmLogin
            //
            this.AcceptButton = this.btnDangNhap;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(440, 520);
            this.Controls.Add(this.pnlTitlebar);
            this.Controls.Add(this.pnlLogo);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblPhu);
            this.Controls.Add(this.lblTenDN);
            this.Controls.Add(this.txtTenDangNhap);
            this.Controls.Add(this.lblMatKhau);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.lblLoi);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.lnkQuenMatKhau);
            this.Controls.Add(this.lnkDangKy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý chi tiêu cá nhân";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.pnlTitlebar.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlTitlebar;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Label logoLabel;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblPhu;
        private System.Windows.Forms.Label lblTenDN;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Label lblLoi;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.LinkLabel lnkDangKy;
        private System.Windows.Forms.LinkLabel lnkQuenMatKhau;
    }
}
