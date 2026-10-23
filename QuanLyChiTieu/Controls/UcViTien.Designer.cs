namespace QuanLyChiTieu.Controls
{
    partial class UcViTien
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtSoDuHienTai = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSoDuBanDau = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboLoaiVi = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenVi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvViTien = new System.Windows.Forms.DataGridView();
            this.lblDanhSach = new System.Windows.Forms.Label();
            this.grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViTien)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(43)))), ((int)(((byte)(77)))));
            this.lblTieuDe.Location = new System.Drawing.Point(24, 16);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(277, 45);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "QUẢN LÝ VÍ TIỀN";
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoTa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMoTa.Location = new System.Drawing.Point(26, 55);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(538, 25);
            this.lblMoTa.TabIndex = 1;
            this.lblMoTa.Text = "Theo dõi danh sách các nguồn tiền và số dư thực tế theo thời gian";
            // 
            // grpThongTin
            // 
            this.grpThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongTin.Controls.Add(this.btnLamMoi);
            this.grpThongTin.Controls.Add(this.btnXoa);
            this.grpThongTin.Controls.Add(this.btnSua);
            this.grpThongTin.Controls.Add(this.btnThem);
            this.grpThongTin.Controls.Add(this.txtSoDuHienTai);
            this.grpThongTin.Controls.Add(this.label4);
            this.grpThongTin.Controls.Add(this.txtSoDuBanDau);
            this.grpThongTin.Controls.Add(this.label3);
            this.grpThongTin.Controls.Add(this.cboLoaiVi);
            this.grpThongTin.Controls.Add(this.label2);
            this.grpThongTin.Controls.Add(this.txtTenVi);
            this.grpThongTin.Controls.Add(this.label1);
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(43)))), ((int)(((byte)(77)))));
            this.grpThongTin.Location = new System.Drawing.Point(30, 85);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(950, 160);
            this.grpThongTin.TabIndex = 2;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin ví tiền";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnLamMoi.Location = new System.Drawing.Point(405, 105);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(110, 36);
            this.btnLamMoi.TabIndex = 11;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnXoa.Location = new System.Drawing.Point(265, 105);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(130, 36);
            this.btnXoa.TabIndex = 10;
            this.btnXoa.Text = "Ngừng dùng";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSua.Location = new System.Drawing.Point(145, 105);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(110, 36);
            this.btnSua.TabIndex = 9;
            this.btnSua.Text = "Sửa ví";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(20, 105);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(115, 36);
            this.btnThem.TabIndex = 8;
            this.btnThem.Text = "+ Thêm ví";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtSoDuHienTai
            // 
            this.txtSoDuHienTai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.txtSoDuHienTai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoDuHienTai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.txtSoDuHienTai.Location = new System.Drawing.Point(710, 56);
            this.txtSoDuHienTai.Name = "txtSoDuHienTai";
            this.txtSoDuHienTai.ReadOnly = true;
            this.txtSoDuHienTai.Size = new System.Drawing.Size(210, 34);
            this.txtSoDuHienTai.TabIndex = 7;
            this.txtSoDuHienTai.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label4.Location = new System.Drawing.Point(706, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Số dư hiện tại (VNĐ)";
            // 
            // txtSoDuBanDau
            // 
            this.txtSoDuBanDau.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoDuBanDau.Location = new System.Drawing.Point(480, 56);
            this.txtSoDuBanDau.Name = "txtSoDuBanDau";
            this.txtSoDuBanDau.Size = new System.Drawing.Size(210, 34);
            this.txtSoDuBanDau.TabIndex = 5;
            this.txtSoDuBanDau.Text = "0";
            this.txtSoDuBanDau.TextChanged += new System.EventHandler(this.txtSoDuBanDau_TextChanged);
            this.txtSoDuBanDau.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoDuBanDau_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label3.Location = new System.Drawing.Point(476, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Số dư ban đầu (VNĐ)";
            // 
            // cboLoaiVi
            // 
            this.cboLoaiVi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiVi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiVi.FormattingEnabled = true;
            this.cboLoaiVi.Location = new System.Drawing.Point(250, 56);
            this.cboLoaiVi.Name = "cboLoaiVi";
            this.cboLoaiVi.Size = new System.Drawing.Size(210, 36);
            this.cboLoaiVi.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label2.Location = new System.Drawing.Point(246, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Loại ví (*)";
            // 
            // txtTenVi
            // 
            this.txtTenVi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenVi.Location = new System.Drawing.Point(20, 56);
            this.txtTenVi.Name = "txtTenVi";
            this.txtTenVi.Size = new System.Drawing.Size(210, 34);
            this.txtTenVi.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên ví (*)";
            // 
            // dgvViTien
            // 
            this.dgvViTien.AllowUserToAddRows = false;
            this.dgvViTien.AllowUserToDeleteRows = false;
            this.dgvViTien.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvViTien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvViTien.BackgroundColor = System.Drawing.Color.White;
            this.dgvViTien.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvViTien.ColumnHeadersHeight = 36;
            this.dgvViTien.Location = new System.Drawing.Point(30, 285);
            this.dgvViTien.MultiSelect = false;
            this.dgvViTien.Name = "dgvViTien";
            this.dgvViTien.ReadOnly = true;
            this.dgvViTien.RowHeadersVisible = false;
            this.dgvViTien.RowHeadersWidth = 51;
            this.dgvViTien.RowTemplate.Height = 32;
            this.dgvViTien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvViTien.Size = new System.Drawing.Size(950, 420);
            this.dgvViTien.TabIndex = 3;
            this.dgvViTien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvViTien_CellClick);
            // 
            // lblDanhSach
            // 
            this.lblDanhSach.AutoSize = true;
            this.lblDanhSach.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDanhSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(43)))), ((int)(((byte)(77)))));
            this.lblDanhSach.Location = new System.Drawing.Point(26, 255);
            this.lblDanhSach.Name = "lblDanhSach";
            this.lblDanhSach.Size = new System.Drawing.Size(189, 30);
            this.lblDanhSach.TabIndex = 4;
            this.lblDanhSach.Text = "Danh sách ví tiền";
            // 
            // UcViTien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.Controls.Add(this.lblDanhSach);
            this.Controls.Add(this.dgvViTien);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.lblTieuDe);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcViTien";
            this.Size = new System.Drawing.Size(1010, 730);
            this.Load += new System.EventHandler(this.UcViTien_Load);
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViTien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTenVi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboLoaiVi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSoDuBanDau;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSoDuHienTai;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataGridView dgvViTien;
        private System.Windows.Forms.Label lblDanhSach;
    }
}
