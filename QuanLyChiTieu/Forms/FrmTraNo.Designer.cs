namespace QuanLyChiTieu.Forms
{
    partial class FrmTraNo
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblConLai;

        private System.Windows.Forms.Label lblGoc;
        private System.Windows.Forms.TextBox txtGoc;
        private System.Windows.Forms.Label lblLai;
        private System.Windows.Forms.TextBox txtLai;
        private System.Windows.Forms.Label lblVi;
        private System.Windows.Forms.ComboBox cboVi;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;

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
            this.lblConLai = new System.Windows.Forms.Label();
            this.lblGoc = new System.Windows.Forms.Label();
            this.txtGoc = new System.Windows.Forms.TextBox();
            this.lblLai = new System.Windows.Forms.Label();
            this.txtLai = new System.Windows.Forms.TextBox();
            this.lblVi = new System.Windows.Forms.Label();
            this.cboVi = new System.Windows.Forms.ComboBox();
            this.lblNgay = new System.Windows.Forms.Label();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.pnlNut = new System.Windows.Forms.FlowLayoutPanel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();

            this.pnlNut.SuspendLayout();
            this.SuspendLayout();

            // lblTieuDe
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51);
            this.lblTieuDe.Location = new System.Drawing.Point(24, 20);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Text = "Trả nợ";

            // lblConLai
            this.lblConLai.AutoSize = true;
            this.lblConLai.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblConLai.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblConLai.Location = new System.Drawing.Point(24, 55);
            this.lblConLai.Name = "lblConLai";
            this.lblConLai.Text = "Còn nợ: ...";

            // lblGoc
            this.lblGoc.AutoSize = true;
            this.lblGoc.Location = new System.Drawing.Point(24, 90);
            this.lblGoc.Name = "lblGoc";
            this.lblGoc.Text = "Tiền gốc (đ) (*)";

            // txtGoc
            this.txtGoc.Location = new System.Drawing.Point(24, 115);
            this.txtGoc.Name = "txtGoc";
            this.txtGoc.Size = new System.Drawing.Size(400, 30);

            // lblLai
            this.lblLai.AutoSize = true;
            this.lblLai.Location = new System.Drawing.Point(24, 160);
            this.lblLai.Name = "lblLai";
            this.lblLai.Text = "Tiền lãi/phí (đ)";

            // txtLai
            this.txtLai.Location = new System.Drawing.Point(24, 185);
            this.txtLai.Name = "txtLai";
            this.txtLai.Size = new System.Drawing.Size(400, 30);
            this.txtLai.Text = "0";

            // lblVi
            this.lblVi.AutoSize = true;
            this.lblVi.Location = new System.Drawing.Point(24, 230);
            this.lblVi.Name = "lblVi";
            this.lblVi.Text = "Ví chi ra (*)";

            // cboVi
            this.cboVi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVi.Location = new System.Drawing.Point(24, 255);
            this.cboVi.Name = "cboVi";
            this.cboVi.Size = new System.Drawing.Size(400, 30);

            // lblNgay
            this.lblNgay.AutoSize = true;
            this.lblNgay.Location = new System.Drawing.Point(24, 300);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Text = "Ngày (*)";

            // dtpNgay
            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgay.Location = new System.Drawing.Point(24, 325);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(150, 30);

            // lblGhiChu
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(24, 370);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Text = "Ghi chú";

            // txtGhiChu
            this.txtGhiChu.Location = new System.Drawing.Point(24, 395);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(400, 30);

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

            // FrmTraNo
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(460, 500);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblConLai);
            this.Controls.Add(this.lblGoc);
            this.Controls.Add(this.txtGoc);
            this.Controls.Add(this.lblLai);
            this.Controls.Add(this.txtLai);
            this.Controls.Add(this.lblVi);
            this.Controls.Add(this.cboVi);
            this.Controls.Add(this.lblNgay);
            this.Controls.Add(this.dtpNgay);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.pnlNut);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTraNo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trả nợ";

            this.pnlNut.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}