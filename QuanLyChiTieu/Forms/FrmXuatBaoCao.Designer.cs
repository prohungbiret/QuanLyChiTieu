namespace QuanLyChiTieu.Forms
{
    partial class FrmXuatBaoCao
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblTu;
        private System.Windows.Forms.DateTimePicker dtpTu;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.DateTimePicker dtpDen;
        private System.Windows.Forms.Label lblPhamVi;
        private System.Windows.Forms.RadioButton rdoThuChi;
        private System.Windows.Forms.RadioButton rdoTatCa;
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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblTu = new System.Windows.Forms.Label();
            this.dtpTu = new System.Windows.Forms.DateTimePicker();
            this.lblDen = new System.Windows.Forms.Label();
            this.dtpDen = new System.Windows.Forms.DateTimePicker();
            this.lblPhamVi = new System.Windows.Forms.Label();
            this.rdoThuChi = new System.Windows.Forms.RadioButton();
            this.rdoTatCa = new System.Windows.Forms.RadioButton();
            this.pnlNut = new System.Windows.Forms.FlowLayoutPanel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.pnlNut.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.lblTieuDe.Location = new System.Drawing.Point(24, 20);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(198, 30);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Xuất báo cáo Excel";
            // 
            // lblTu
            // 
            this.lblTu.AutoSize = true;
            this.lblTu.Location = new System.Drawing.Point(24, 70);
            this.lblTu.Name = "lblTu";
            this.lblTu.Size = new System.Drawing.Size(93, 23);
            this.lblTu.TabIndex = 1;
            this.lblTu.Text = "Từ ngày (*)";
            // 
            // dtpTu
            // 
            this.dtpTu.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTu.Location = new System.Drawing.Point(24, 95);
            this.dtpTu.Name = "dtpTu";
            this.dtpTu.Size = new System.Drawing.Size(150, 30);
            this.dtpTu.TabIndex = 2;
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.Location = new System.Drawing.Point(200, 70);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(105, 23);
            this.lblDen.TabIndex = 3;
            this.lblDen.Text = "Đến ngày (*)";
            // 
            // dtpDen
            // 
            this.dtpDen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDen.Location = new System.Drawing.Point(200, 95);
            this.dtpDen.Name = "dtpDen";
            this.dtpDen.Size = new System.Drawing.Size(150, 30);
            this.dtpDen.TabIndex = 4;
            // 
            // lblPhamVi
            // 
            this.lblPhamVi.AutoSize = true;
            this.lblPhamVi.Location = new System.Drawing.Point(24, 150);
            this.lblPhamVi.Name = "lblPhamVi";
            this.lblPhamVi.Size = new System.Drawing.Size(137, 23);
            this.lblPhamVi.TabIndex = 5;
            this.lblPhamVi.Text = "Phạm vi báo cáo";
            // 
            // rdoThuChi
            // 
            this.rdoThuChi.AutoSize = true;
            this.rdoThuChi.Checked = true;
            this.rdoThuChi.Location = new System.Drawing.Point(24, 175);
            this.rdoThuChi.Name = "rdoThuChi";
            this.rdoThuChi.Size = new System.Drawing.Size(121, 27);
            this.rdoThuChi.TabIndex = 6;
            this.rdoThuChi.TabStop = true;
            this.rdoThuChi.Text = "Chỉ thu/chi ";
            this.rdoThuChi.UseVisualStyleBackColor = true;
            // 
            // rdoTatCa
            // 
            this.rdoTatCa.AutoSize = true;
            this.rdoTatCa.Location = new System.Drawing.Point(24, 205);
            this.rdoTatCa.Name = "rdoTatCa";
            this.rdoTatCa.Size = new System.Drawing.Size(189, 27);
            this.rdoTatCa.TabIndex = 7;
            this.rdoTatCa.Text = "Tất cả loại giao dịch ";
            this.rdoTatCa.UseVisualStyleBackColor = true;
            this.rdoTatCa.CheckedChanged += new System.EventHandler(this.rdoTatCa_CheckedChanged);
            // 
            // pnlNut
            // 
            this.pnlNut.BackColor = System.Drawing.Color.White;
            this.pnlNut.Controls.Add(this.btnHuy);
            this.pnlNut.Controls.Add(this.btnLuu);
            this.pnlNut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNut.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlNut.Location = new System.Drawing.Point(0, 260);
            this.pnlNut.Name = "pnlNut";
            this.pnlNut.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.pnlNut.Size = new System.Drawing.Size(400, 60);
            this.pnlNut.TabIndex = 8;
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.btnHuy.Location = new System.Drawing.Point(275, 13);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 38);
            this.btnHuy.TabIndex = 0;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(159, 13);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(110, 38);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Xuất";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // FrmXuatBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 320);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblTu);
            this.Controls.Add(this.dtpTu);
            this.Controls.Add(this.lblDen);
            this.Controls.Add(this.dtpDen);
            this.Controls.Add(this.lblPhamVi);
            this.Controls.Add(this.rdoThuChi);
            this.Controls.Add(this.rdoTatCa);
            this.Controls.Add(this.pnlNut);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmXuatBaoCao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xuất báo cáo Excel";
            this.pnlNut.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}