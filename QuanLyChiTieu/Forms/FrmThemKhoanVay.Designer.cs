namespace QuanLyChiTieu.Forms
{
    partial class FrmThemKhoanVay
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.Label lblNguoi;
        private System.Windows.Forms.TextBox txtNguoiLienQuan;
        private System.Windows.Forms.Label lblGoc;
        private System.Windows.Forms.TextBox txtGoc;
        private System.Windows.Forms.Label lblPhatSinh;
        private System.Windows.Forms.DateTimePicker dtpPhatSinh;
        private System.Windows.Forms.Label lblDenHan;
        private System.Windows.Forms.CheckBox chkDenHan;
        private System.Windows.Forms.DateTimePicker dtpDenHan;
        private System.Windows.Forms.Label lblLaiSuat;
        private System.Windows.Forms.TextBox txtLaiSuat;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblVi;
        private System.Windows.Forms.ComboBox cboVi;
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
            this.lblLoai = new System.Windows.Forms.Label();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.lblNguoi = new System.Windows.Forms.Label();
            this.txtNguoiLienQuan = new System.Windows.Forms.TextBox();
            this.lblGoc = new System.Windows.Forms.Label();
            this.txtGoc = new System.Windows.Forms.TextBox();
            this.lblPhatSinh = new System.Windows.Forms.Label();
            this.dtpPhatSinh = new System.Windows.Forms.DateTimePicker();
            this.lblDenHan = new System.Windows.Forms.Label();
            this.chkDenHan = new System.Windows.Forms.CheckBox();
            this.dtpDenHan = new System.Windows.Forms.DateTimePicker();
            this.lblLaiSuat = new System.Windows.Forms.Label();
            this.txtLaiSuat = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblVi = new System.Windows.Forms.Label();
            this.cboVi = new System.Windows.Forms.ComboBox();
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
            this.lblTieuDe.Text = "Khoản vay";

            // lblLoai
            this.lblLoai.AutoSize = true;
            this.lblLoai.Location = new System.Drawing.Point(24, 60);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Text = "Loại (*)";

            // cboLoai
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.Location = new System.Drawing.Point(24, 85);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(400, 30);

            // lblNguoi
            this.lblNguoi.AutoSize = true;
            this.lblNguoi.Location = new System.Drawing.Point(24, 125);
            this.lblNguoi.Name = "lblNguoi";
            this.lblNguoi.Text = "Tên người liên quan (*)";

            // txtNguoiLienQuan
            this.txtNguoiLienQuan.Location = new System.Drawing.Point(24, 150);
            this.txtNguoiLienQuan.Name = "txtNguoiLienQuan";
            this.txtNguoiLienQuan.Size = new System.Drawing.Size(400, 30);
            this.txtNguoiLienQuan.MaxLength = 150;

            // lblGoc
            this.lblGoc.AutoSize = true;
            this.lblGoc.Location = new System.Drawing.Point(24, 190);
            this.lblGoc.Name = "lblGoc";
            this.lblGoc.Text = "Tiền gốc (đ) (*)";

            // txtGoc
            this.txtGoc.Location = new System.Drawing.Point(24, 215);
            this.txtGoc.Name = "txtGoc";
            this.txtGoc.Size = new System.Drawing.Size(400, 30);

            // lblPhatSinh
            this.lblPhatSinh.AutoSize = true;
            this.lblPhatSinh.Location = new System.Drawing.Point(24, 255);
            this.lblPhatSinh.Name = "lblPhatSinh";
            this.lblPhatSinh.Text = "Ngày phát sinh (*)";

            // dtpPhatSinh
            this.dtpPhatSinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPhatSinh.Location = new System.Drawing.Point(24, 280);
            this.dtpPhatSinh.Name = "dtpPhatSinh";
            this.dtpPhatSinh.Size = new System.Drawing.Size(150, 30);

            // lblDenHan
            this.lblDenHan.AutoSize = true;
            this.lblDenHan.Location = new System.Drawing.Point(24, 325);
            this.lblDenHan.Name = "lblDenHan";
            this.lblDenHan.Text = "Hạn trả";

            // chkDenHan
            this.chkDenHan.AutoSize = true;
            this.chkDenHan.Location = new System.Drawing.Point(24, 350);
            this.chkDenHan.Name = "chkDenHan";
            this.chkDenHan.Text = "Có hạn ngày";
            this.chkDenHan.CheckedChanged += new System.EventHandler(this.chkDenHan_CheckedChanged);

            // dtpDenHan
            this.dtpDenHan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenHan.Location = new System.Drawing.Point(160, 348);
            this.dtpDenHan.Name = "dtpDenHan";
            this.dtpDenHan.Size = new System.Drawing.Size(150, 30);

            // lblLaiSuat
            this.lblLaiSuat.AutoSize = true;
            this.lblLaiSuat.Location = new System.Drawing.Point(24, 390);
            this.lblLaiSuat.Name = "lblLaiSuat";
            this.lblLaiSuat.Text = "Lãi (%/năm, không bắt buộc)";

            // txtLaiSuat
            this.txtLaiSuat.Location = new System.Drawing.Point(24, 415);
            this.txtLaiSuat.Name = "txtLaiSuat";
            this.txtLaiSuat.Size = new System.Drawing.Size(150, 30);
            this.txtLaiSuat.MaxLength = 9;

            // lblVi
            this.lblVi.AutoSize = true;
            this.lblVi.Location = new System.Drawing.Point(24, 460);
            this.lblVi.Name = "lblVi";
            this.lblVi.Text = "Ví khởi phát (*)";

            // cboVi
            this.cboVi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVi.Location = new System.Drawing.Point(24, 485);
            this.cboVi.Name = "cboVi";
            this.cboVi.Size = new System.Drawing.Size(400, 30);

            // lblGhiChu
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(24, 525);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Text = "Ghi chú";

            // txtGhiChu
            this.txtGhiChu.Location = new System.Drawing.Point(24, 550);
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

            // FrmThemKhoanVay
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(460, 680);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblLoai);
            this.Controls.Add(this.cboLoai);
            this.Controls.Add(this.lblNguoi);
            this.Controls.Add(this.txtNguoiLienQuan);
            this.Controls.Add(this.lblGoc);
            this.Controls.Add(this.txtGoc);
            this.Controls.Add(this.lblPhatSinh);
            this.Controls.Add(this.dtpPhatSinh);
            this.Controls.Add(this.lblDenHan);
            this.Controls.Add(this.chkDenHan);
            this.Controls.Add(this.dtpDenHan);
            this.Controls.Add(this.lblLaiSuat);
            this.Controls.Add(this.txtLaiSuat);
            this.Controls.Add(this.lblVi);
            this.Controls.Add(this.cboVi);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.pnlNut);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmThemKhoanVay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Khoản vay";

            this.pnlNut.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}