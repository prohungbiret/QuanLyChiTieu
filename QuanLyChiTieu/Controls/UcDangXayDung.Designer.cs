namespace QuanLyChiTieu.Controls
{
    partial class UcDangXayDung
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.Label lblKetQua;
        private System.Windows.Forms.Button btnDemoForm;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.lblKetQua = new System.Windows.Forms.Label();
            this.btnDemoForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.lblTieuDe.Location = new System.Drawing.Point(8, 8);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(109, 35);
            this.lblTieuDe.TabIndex = 3;
            this.lblTieuDe.Text = "Phân hệ";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblThongBao
            // 
            this.lblThongBao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblThongBao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblThongBao.Location = new System.Drawing.Point(8, 42);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(660, 46);
            this.lblThongBao.TabIndex = 2;
            this.lblThongBao.Text = "Phân hệ đang được phát triển.";
            // 
            // lblKetQua
            // 
            this.lblKetQua.AutoSize = true;
            this.lblKetQua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblKetQua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblKetQua.Location = new System.Drawing.Point(8, 100);
            this.lblKetQua.Name = "lblKetQua";
            this.lblKetQua.Size = new System.Drawing.Size(0, 23);
            this.lblKetQua.TabIndex = 1;
            // 
            // btnDemoForm
            // 
            this.btnDemoForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnDemoForm.FlatAppearance.BorderSize = 0;
            this.btnDemoForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDemoForm.ForeColor = System.Drawing.Color.White;
            this.btnDemoForm.Location = new System.Drawing.Point(8, 132);
            this.btnDemoForm.Name = "btnDemoForm";
            this.btnDemoForm.Size = new System.Drawing.Size(256, 38);
            this.btnDemoForm.TabIndex = 0;
            this.btnDemoForm.Text = "Thử form nhập mẫu (demo hợp đồng ShowDialog)";
            this.btnDemoForm.UseVisualStyleBackColor = false;
            this.btnDemoForm.Click += new System.EventHandler(this.btnDemoForm_Click);
            // 
            // UcDangXayDung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.Controls.Add(this.btnDemoForm);
            this.Controls.Add(this.lblKetQua);
            this.Controls.Add(this.lblThongBao);
            this.Controls.Add(this.lblTieuDe);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "UcDangXayDung";
            this.Size = new System.Drawing.Size(720, 395);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}