namespace QuanLyChiTieu.Forms
{
    partial class FrmThemNganSach
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblDanhMuc;
        private System.Windows.Forms.ComboBox cboDanhMuc;
        private System.Windows.Forms.Label lblThang;
        private System.Windows.Forms.NumericUpDown nudThang;
        private System.Windows.Forms.Label lblNam;
        private System.Windows.Forms.NumericUpDown nudNam;
        private System.Windows.Forms.Label lblHanMuc;
        private System.Windows.Forms.TextBox txtHanMuc;
        private System.Windows.Forms.FlowLayoutPanel pnlNut;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnLuu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblDanhMuc = new System.Windows.Forms.Label();
            this.cboDanhMuc = new System.Windows.Forms.ComboBox();
            this.lblThang = new System.Windows.Forms.Label();
            this.nudThang = new System.Windows.Forms.NumericUpDown();
            this.lblNam = new System.Windows.Forms.Label();
            this.nudNam = new System.Windows.Forms.NumericUpDown();
            this.lblHanMuc = new System.Windows.Forms.Label();
            this.txtHanMuc = new System.Windows.Forms.TextBox();
            this.pnlNut = new System.Windows.Forms.FlowLayoutPanel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).BeginInit();
            this.pnlNut.SuspendLayout();
            this.SuspendLayout();

            // lblTieuDe
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51);
            this.lblTieuDe.Location = new System.Drawing.Point(24, 20);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Text = "Ngân sách";

            // lblDanhMuc
            this.lblDanhMuc.AutoSize = true;
            this.lblDanhMuc.Location = new System.Drawing.Point(24, 70);
            this.lblDanhMuc.Name = "lblDanhMuc";
            this.lblDanhMuc.Text = "Danh mục Chi (*)";

            // cboDanhMuc
            this.cboDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDanhMuc.Location = new System.Drawing.Point(24, 95);
            this.cboDanhMuc.Name = "cboDanhMuc";
            this.cboDanhMuc.Size = new System.Drawing.Size(400, 30);

            // lblThang
            this.lblThang.AutoSize = true;
            this.lblThang.Location = new System.Drawing.Point(24, 140);
            this.lblThang.Name = "lblThang";
            this.lblThang.Text = "Tháng (*)";

            // nudThang
            this.nudThang.Location = new System.Drawing.Point(24, 165);
            this.nudThang.Name = "nudThang";
            this.nudThang.Size = new System.Drawing.Size(100, 30);
            this.nudThang.Minimum = 1;
            this.nudThang.Maximum = 12;

            // lblNam
            this.lblNam.AutoSize = true;
            this.lblNam.Location = new System.Drawing.Point(160, 140);
            this.lblNam.Name = "lblNam";
            this.lblNam.Text = "Năm (*)";

            // nudNam
            this.nudNam.Location = new System.Drawing.Point(160, 165);
            this.nudNam.Name = "nudNam";
            this.nudNam.Size = new System.Drawing.Size(100, 30);
            this.nudNam.Minimum = 1900;
            this.nudNam.Maximum = 2100;

            // lblHanMuc
            this.lblHanMuc.AutoSize = true;
            this.lblHanMuc.Location = new System.Drawing.Point(24, 210);
            this.lblHanMuc.Name = "lblHanMuc";
            this.lblHanMuc.Text = "Hạn mức (đ) (*)";

            // txtHanMuc
            this.txtHanMuc.Location = new System.Drawing.Point(24, 235);
            this.txtHanMuc.Name = "txtHanMuc";
            this.txtHanMuc.Size = new System.Drawing.Size(400, 30);

            // pnlNut
            this.pnlNut.BackColor = System.Drawing.Color.White;
            this.pnlNut.Controls.Add(this.btnHuy);
            this.pnlNut.Controls.Add(this.btnLuu);
            this.pnlNut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNut.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlNut.Height = 60;
            this.pnlNut.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);

            // btnLuu
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(110, 38);
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);

            // btnHuy
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(244, 247, 251);
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 38);
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);

            // FrmThemNganSach
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(470, 340);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblDanhMuc);
            this.Controls.Add(this.cboDanhMuc);
            this.Controls.Add(this.lblThang);
            this.Controls.Add(this.nudThang);
            this.Controls.Add(this.lblNam);
            this.Controls.Add(this.nudNam);
            this.Controls.Add(this.lblHanMuc);
            this.Controls.Add(this.txtHanMuc);
            this.Controls.Add(this.pnlNut);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmThemNganSach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ngân sách";

            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).EndInit();
            this.pnlNut.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}