namespace QuanLyChiTieu.Controls
{
    partial class UcDanhMuc
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.FlowLayoutPanel pnlTools;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnTrangThai;

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoTa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoGD;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;

        private System.Windows.Forms.Label lblTrangThaiBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTools = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblLoai = new System.Windows.Forms.Label();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnTrangThai = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.colTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoGD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTrangThaiBar = new System.Windows.Forms.Label();
            this.pnlTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTools
            // 
            this.pnlTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.pnlTools.Controls.Add(this.lblTimKiem);
            this.pnlTools.Controls.Add(this.txtTimKiem);
            this.pnlTools.Controls.Add(this.lblLoai);
            this.pnlTools.Controls.Add(this.cboLoai);
            this.pnlTools.Controls.Add(this.cboTrangThai);
            this.pnlTools.Controls.Add(this.btnThem);
            this.pnlTools.Controls.Add(this.btnSua);
            this.pnlTools.Controls.Add(this.btnTrangThai);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTools.Location = new System.Drawing.Point(0, 0);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.pnlTools.Size = new System.Drawing.Size(1000, 52);
            this.pnlTools.TabIndex = 2;
            this.pnlTools.WrapContents = false;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTimKiem.Location = new System.Drawing.Point(8, 18);
            this.lblTimKiem.Margin = new System.Windows.Forms.Padding(8, 10, 2, 0);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(77, 21);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(90, 11);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(220, 29);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = true;
            this.lblLoai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLoai.Location = new System.Drawing.Point(321, 18);
            this.lblLoai.Margin = new System.Windows.Forms.Padding(8, 10, 2, 0);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(42, 21);
            this.lblLoai.TabIndex = 2;
            this.lblLoai.Text = "Loại:";
            // 
            // cboLoai
            // 
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.Items.AddRange(new object[] {
            "Tất cả loại",
            "Thu",
            "Chi"});
            this.cboLoai.Location = new System.Drawing.Point(368, 11);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(130, 29);
            this.cboLoai.TabIndex = 3;
            this.cboLoai.SelectedIndexChanged += new System.EventHandler(this.cboLoai_SelectedIndexChanged);
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Tất cả trạng thái",
            "Đang sử dụng",
            "Đã ngừng"});
            this.cboTrangThai.Location = new System.Drawing.Point(504, 11);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(150, 29);
            this.cboTrangThai.TabIndex = 4;
            this.cboTrangThai.SelectedIndexChanged += new System.EventHandler(this.cboTrangThai_SelectedIndexChanged);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(665, 8);
            this.btnThem.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(140, 38);
            this.btnThem.TabIndex = 5;
            this.btnThem.Text = "Thêm danh mục";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.White;
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.btnSua.Location = new System.Drawing.Point(813, 8);
            this.btnSua.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(80, 38);
            this.btnSua.TabIndex = 6;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnTrangThai
            // 
            this.btnTrangThai.BackColor = System.Drawing.Color.White;
            this.btnTrangThai.FlatAppearance.BorderSize = 0;
            this.btnTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.btnTrangThai.Location = new System.Drawing.Point(896, 11);
            this.btnTrangThai.Name = "btnTrangThai";
            this.btnTrangThai.Size = new System.Drawing.Size(130, 38);
            this.btnTrangThai.TabIndex = 7;
            this.btnTrangThai.Text = "Ngừng sử dụng";
            this.btnTrangThai.UseVisualStyleBackColor = false;
            this.btnTrangThai.Click += new System.EventHandler(this.btnTrangThai_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTen,
            this.colLoai,
            this.colMoTa,
            this.colSoGD,
            this.colTrangThai});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 52);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.Size = new System.Drawing.Size(1000, 526);
            this.dgv.TabIndex = 0;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            // 
            // colTen
            // 
            this.colTen.HeaderText = "Tên danh mục";
            this.colTen.MinimumWidth = 6;
            this.colTen.Name = "colTen";
            this.colTen.ReadOnly = true;
            this.colTen.Width = 125;
            // 
            // colLoai
            // 
            this.colLoai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colLoai.HeaderText = "Loại";
            this.colLoai.MinimumWidth = 6;
            this.colLoai.Name = "colLoai";
            this.colLoai.ReadOnly = true;
            this.colLoai.Width = 90;
            // 
            // colMoTa
            // 
            this.colMoTa.HeaderText = "Mô tả";
            this.colMoTa.MinimumWidth = 6;
            this.colMoTa.Name = "colMoTa";
            this.colMoTa.ReadOnly = true;
            this.colMoTa.Width = 125;
            // 
            // colSoGD
            // 
            this.colSoGD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colSoGD.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSoGD.HeaderText = "Số giao dịch";
            this.colSoGD.MinimumWidth = 6;
            this.colSoGD.Name = "colSoGD";
            this.colSoGD.ReadOnly = true;
            this.colSoGD.Width = 110;
            // 
            // colTrangThai
            // 
            this.colTrangThai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.MinimumWidth = 6;
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            this.colTrangThai.Width = 130;
            // 
            // lblTrangThaiBar
            // 
            this.lblTrangThaiBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTrangThaiBar.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTrangThaiBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTrangThaiBar.Location = new System.Drawing.Point(0, 578);
            this.lblTrangThaiBar.Name = "lblTrangThaiBar";
            this.lblTrangThaiBar.Size = new System.Drawing.Size(1000, 22);
            this.lblTrangThaiBar.TabIndex = 1;
            // 
            // UcDanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.lblTrangThaiBar);
            this.Controls.Add(this.pnlTools);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "UcDanhMuc";
            this.Size = new System.Drawing.Size(1000, 600);
            this.pnlTools.ResumeLayout(false);
            this.pnlTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }
    }
}