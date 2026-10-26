namespace QuanLyChiTieu.Forms
{
    partial class FrmChangePassword
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblHienTai;
        private System.Windows.Forms.TextBox txtHienTai;
        private System.Windows.Forms.Label lblMoi;
        private System.Windows.Forms.TextBox txtMoi;
        private System.Windows.Forms.Label lblNhapLai;
        private System.Windows.Forms.TextBox txtNhapLai;
        private System.Windows.Forms.CheckBox chkHienMatKhau;
        private System.Windows.Forms.Label lblLoi;
        private System.Windows.Forms.FlowLayoutPanel pnlNut;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnLuu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblHienTai = new System.Windows.Forms.Label();
            this.txtHienTai = new System.Windows.Forms.TextBox();
            this.lblMoi = new System.Windows.Forms.Label();
            this.txtMoi = new System.Windows.Forms.TextBox();
            this.lblNhapLai = new System.Windows.Forms.Label();
            this.txtNhapLai = new System.Windows.Forms.TextBox();
            this.chkHienMatKhau = new System.Windows.Forms.CheckBox();
            this.lblLoi = new System.Windows.Forms.Label();
            this.pnlNut = new System.Windows.Forms.FlowLayoutPanel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();

            this.pnlNut.SuspendLayout();
            this.SuspendLayout();

            // lblHienTai
            this.lblHienTai.AutoSize = true;
            this.lblHienTai.Location = new System.Drawing.Point(24, 20);
            this.lblHienTai.Name = "lblHienTai";
            this.lblHienTai.Text = "Mật khẩu hiện tại *";

            // txtHienTai
            this.txtHienTai.Location = new System.Drawing.Point(24, 42);
            this.txtHienTai.Name = "txtHienTai";
            this.txtHienTai.Size = new System.Drawing.Size(392, 36);
            this.txtHienTai.UseSystemPasswordChar = true;

            // lblMoi
            this.lblMoi.AutoSize = true;
            this.lblMoi.Location = new System.Drawing.Point(24, 86);
            this.lblMoi.Name = "lblMoi";
            this.lblMoi.Text = "Mật khẩu mới *";

            // txtMoi
            this.txtMoi.Location = new System.Drawing.Point(24, 108);
            this.txtMoi.Name = "txtMoi";
            this.txtMoi.Size = new System.Drawing.Size(392, 36);
            this.txtMoi.UseSystemPasswordChar = true;

            // lblNhapLai
            this.lblNhapLai.AutoSize = true;
            this.lblNhapLai.Location = new System.Drawing.Point(24, 152);
            this.lblNhapLai.Name = "lblNhapLai";
            this.lblNhapLai.Text = "Nhập lại mật khẩu mới *";

            // txtNhapLai
            this.txtNhapLai.Location = new System.Drawing.Point(24, 174);
            this.txtNhapLai.Name = "txtNhapLai";
            this.txtNhapLai.Size = new System.Drawing.Size(392, 36);
            this.txtNhapLai.UseSystemPasswordChar = true;

            // chkHienMatKhau
            this.chkHienMatKhau.AutoSize = true;
            this.chkHienMatKhau.Location = new System.Drawing.Point(24, 218);
            this.chkHienMatKhau.Name = "chkHienMatKhau";
            this.chkHienMatKhau.Text = "Hiện mật khẩu";
            this.chkHienMatKhau.CheckedChanged += new System.EventHandler(this.chkHienMatKhau_CheckedChanged);

            // lblLoi
            this.lblLoi.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38); // Theme.Chi
            this.lblLoi.Location = new System.Drawing.Point(24, 248);
            this.lblLoi.Name = "lblLoi";
            this.lblLoi.Size = new System.Drawing.Size(392, 36);

            // pnlNut
            this.pnlNut.BackColor = System.Drawing.Color.White;
            this.pnlNut.Controls.Add(this.btnHuy);
            this.pnlNut.Controls.Add(this.btnLuu);
            this.pnlNut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNut.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlNut.Height = 56;
            this.pnlNut.Name = "pnlNut";
            this.pnlNut.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);

            // btnLuu
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(37, 99, 235); // Theme.Primary
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(130, 38);
            this.btnLuu.Text = "Đổi mật khẩu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.BtnLuu_Click);

            // btnHuy
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(244, 247, 251); // Theme.Background
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 38);
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);

            // FrmChangePassword
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(440, 330);
            this.Controls.Add(this.lblHienTai);
            this.Controls.Add(this.txtHienTai);
            this.Controls.Add(this.lblMoi);
            this.Controls.Add(this.txtMoi);
            this.Controls.Add(this.lblNhapLai);
            this.Controls.Add(this.txtNhapLai);
            this.Controls.Add(this.chkHienMatKhau);
            this.Controls.Add(this.lblLoi);
            this.Controls.Add(this.pnlNut);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChangePassword";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đổi mật khẩu";

            this.pnlNut.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}