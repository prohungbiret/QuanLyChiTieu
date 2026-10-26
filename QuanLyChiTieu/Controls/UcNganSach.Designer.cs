namespace QuanLyChiTieu.Controls
{
    partial class UcNganSach
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.FlowLayoutPanel pnlTools;
        private System.Windows.Forms.Label lblThang;
        private System.Windows.Forms.NumericUpDown nudThang;
        private System.Windows.Forms.Label lblNam;
        private System.Windows.Forms.NumericUpDown nudNam;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label lblTongKet;
        private System.Windows.Forms.FlowLayoutPanel pnlDanhSach;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTools = new System.Windows.Forms.FlowLayoutPanel();
            this.lblThang = new System.Windows.Forms.Label();
            this.nudThang = new System.Windows.Forms.NumericUpDown();
            this.lblNam = new System.Windows.Forms.Label();
            this.nudNam = new System.Windows.Forms.NumericUpDown();
            this.btnThem = new System.Windows.Forms.Button();
            this.lblTongKet = new System.Windows.Forms.Label();
            this.pnlDanhSach = new System.Windows.Forms.FlowLayoutPanel();

            this.pnlTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).BeginInit();
            this.SuspendLayout();

            // pnlTools
            this.pnlTools.BackColor = System.Drawing.Color.FromArgb(244, 247, 251);
            this.pnlTools.Controls.Add(this.lblThang);
            this.pnlTools.Controls.Add(this.nudThang);
            this.pnlTools.Controls.Add(this.lblNam);
            this.pnlTools.Controls.Add(this.nudNam);
            this.pnlTools.Controls.Add(this.btnThem);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTools.Height = 52;
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.pnlTools.WrapContents = false;

            // lblThang
            this.lblThang.AutoSize = true;
            this.lblThang.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblThang.Margin = new System.Windows.Forms.Padding(8, 10, 2, 0);
            this.lblThang.Name = "lblThang";
            this.lblThang.Text = "Tháng:";

            // nudThang
            this.nudThang.Location = new System.Drawing.Point(0, 0);
            this.nudThang.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            this.nudThang.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudThang.Name = "nudThang";
            this.nudThang.Size = new System.Drawing.Size(64, 30);
            this.nudThang.ValueChanged += new System.EventHandler(this.nudThang_ValueChanged);

            // lblNam
            this.lblNam.AutoSize = true;
            this.lblNam.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblNam.Margin = new System.Windows.Forms.Padding(8, 10, 2, 0);
            this.lblNam.Name = "lblNam";
            this.lblNam.Text = "Năm:";

            // nudNam
            this.nudNam.Location = new System.Drawing.Point(0, 0);
            this.nudNam.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            this.nudNam.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            this.nudNam.Name = "nudNam";
            this.nudNam.Size = new System.Drawing.Size(86, 30);
            this.nudNam.ValueChanged += new System.EventHandler(this.nudNam_ValueChanged);

            // btnThem
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(150, 38);
            this.btnThem.Text = "Thêm ngân sách";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            // lblTongKet
            this.lblTongKet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTongKet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTongKet.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTongKet.Height = 26;
            this.lblTongKet.Name = "lblTongKet";
            this.lblTongKet.Text = "";

            // pnlDanhSach
            this.pnlDanhSach.AutoScroll = true;
            this.pnlDanhSach.BackColor = System.Drawing.Color.FromArgb(244, 247, 251);
            this.pnlDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDanhSach.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlDanhSach.Name = "pnlDanhSach";
            this.pnlDanhSach.WrapContents = false;

            // UcNganSach
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 247, 251);
            this.Controls.Add(this.pnlDanhSach);
            this.Controls.Add(this.lblTongKet);
            this.Controls.Add(this.pnlTools);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "UcNganSach";
            this.Size = new System.Drawing.Size(1000, 600);

            this.pnlTools.ResumeLayout(false);
            this.pnlTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).EndInit();
            this.ResumeLayout(false);
        }
    }
}