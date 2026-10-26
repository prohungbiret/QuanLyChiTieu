namespace QuanLyChiTieu.Controls
{
    partial class UcTietKiem
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.FlowLayoutPanel pnlTools;
        private System.Windows.Forms.Label lblPhanHe;
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
            this.lblPhanHe = new System.Windows.Forms.Label();
            this.btnThem = new System.Windows.Forms.Button();
            this.lblTongKet = new System.Windows.Forms.Label();
            this.pnlDanhSach = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTools
            // 
            this.pnlTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.pnlTools.Controls.Add(this.lblPhanHe);
            this.pnlTools.Controls.Add(this.btnThem);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTools.Location = new System.Drawing.Point(0, 0);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.pnlTools.Size = new System.Drawing.Size(1000, 52);
            this.pnlTools.TabIndex = 2;
            this.pnlTools.WrapContents = false;
            // 
            // lblPhanHe
            // 
            this.lblPhanHe.AutoSize = true;
            this.lblPhanHe.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblPhanHe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.lblPhanHe.Location = new System.Drawing.Point(0, 18);
            this.lblPhanHe.Margin = new System.Windows.Forms.Padding(0, 10, 12, 0);
            this.lblPhanHe.Name = "lblPhanHe";
            this.lblPhanHe.Size = new System.Drawing.Size(191, 25);
            this.lblPhanHe.TabIndex = 0;
            this.lblPhanHe.Text = "MỤC TIÊU TIẾT KIỆM";
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(219, 8);
            this.btnThem.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(150, 38);
            this.btnThem.TabIndex = 1;
            this.btnThem.Text = "Thêm mục tiêu";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // lblTongKet
            // 
            this.lblTongKet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTongKet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTongKet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTongKet.Location = new System.Drawing.Point(0, 574);
            this.lblTongKet.Name = "lblTongKet";
            this.lblTongKet.Size = new System.Drawing.Size(1000, 26);
            this.lblTongKet.TabIndex = 1;
            this.lblTongKet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDanhSach
            // 
            this.pnlDanhSach.AutoScroll = true;
            this.pnlDanhSach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.pnlDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDanhSach.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlDanhSach.Location = new System.Drawing.Point(0, 52);
            this.pnlDanhSach.Name = "pnlDanhSach";
            this.pnlDanhSach.Size = new System.Drawing.Size(1000, 522);
            this.pnlDanhSach.TabIndex = 0;
            this.pnlDanhSach.WrapContents = false;
            // 
            // UcTietKiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.Controls.Add(this.pnlDanhSach);
            this.Controls.Add(this.lblTongKet);
            this.Controls.Add(this.pnlTools);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "UcTietKiem";
            this.Size = new System.Drawing.Size(1000, 600);
            this.pnlTools.ResumeLayout(false);
            this.pnlTools.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}