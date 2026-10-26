namespace QuanLyChiTieu.Controls
{
    partial class UcVayNo
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

            // pnlTools
            this.pnlTools.BackColor = System.Drawing.Color.FromArgb(244, 247, 251);
            this.pnlTools.Controls.Add(this.lblPhanHe);
            this.pnlTools.Controls.Add(this.btnThem);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTools.Height = 52;
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.pnlTools.WrapContents = false;

            // lblPhanHe
            this.lblPhanHe.AutoSize = true;
            this.lblPhanHe.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblPhanHe.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51);
            this.lblPhanHe.Margin = new System.Windows.Forms.Padding(0, 10, 12, 0);
            this.lblPhanHe.Name = "lblPhanHe";
            this.lblPhanHe.Text = "QUẢN LÝ VAY NỢ";

            // btnThem
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(140, 38);
            this.btnThem.Text = "Thêm khoản";
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

            // UcVayNo
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 247, 251);
            this.Controls.Add(this.pnlDanhSach);
            this.Controls.Add(this.lblTongKet);
            this.Controls.Add(this.pnlTools);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "UcVayNo";
            this.Size = new System.Drawing.Size(1000, 600);

            this.pnlTools.ResumeLayout(false);
            this.pnlTools.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}