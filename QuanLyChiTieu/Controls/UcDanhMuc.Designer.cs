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
            this.components = new System.ComponentModel.Container();
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

            // ============================================================
            // pnlTools — Thanh công cụ (FlowLayoutPanel)
            // ============================================================
            this.pnlTools.BackColor = System.Drawing.Color.FromArgb(244, 247, 251); // Theme.Background
            this.pnlTools.Controls.Add(this.lblTimKiem);
            this.pnlTools.Controls.Add(this.txtTimKiem);
            this.pnlTools.Controls.Add(this.lblLoai);
            this.pnlTools.Controls.Add(this.cboLoai);
            this.pnlTools.Controls.Add(this.cboTrangThai);
            this.pnlTools.Controls.Add(this.btnThem);
            this.pnlTools.Controls.Add(this.btnSua);
            this.pnlTools.Controls.Add(this.btnTrangThai);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTools.Height = 52;
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.pnlTools.WrapContents = false;

            // lblTimKiem
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); // Theme.TextSub
            this.lblTimKiem.Margin = new System.Windows.Forms.Padding(8, 10, 2, 0);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Text = "Tìm kiếm:";

            // txtTimKiem
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(220, 36); // Theme.CaoControl = 36
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);

            // lblLoai
            this.lblLoai.AutoSize = true;
            this.lblLoai.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblLoai.Margin = new System.Windows.Forms.Padding(8, 10, 2, 0);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Text = "Loại:";

            // cboLoai
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.Items.AddRange(new object[] { "Tất cả loại", "Thu", "Chi" });
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(130, 36);
            this.cboLoai.SelectedIndexChanged += new System.EventHandler(this.cboLoai_SelectedIndexChanged);

            // cboTrangThai
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Items.AddRange(new object[] { "Tất cả trạng thái", "Đang sử dụng", "Đã ngừng" });
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(150, 36);
            this.cboTrangThai.SelectedIndexChanged += new System.EventHandler(this.cboTrangThai_SelectedIndexChanged);

            // btnThem
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(37, 99, 235); // Theme.Primary
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(140, 38); // Theme.CaoNut = 38
            this.btnThem.Text = "Thêm danh mục";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            // btnSua
            this.btnSua.BackColor = System.Drawing.Color.White;
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51); // Theme.Text
            this.btnSua.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(80, 38);
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            // btnTrangThai
            this.btnTrangThai.BackColor = System.Drawing.Color.White;
            this.btnTrangThai.FlatAppearance.BorderSize = 0;
            this.btnTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrangThai.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51);
            this.btnTrangThai.Name = "btnTrangThai";
            this.btnTrangThai.Size = new System.Drawing.Size(130, 38);
            this.btnTrangThai.Text = "Ngừng sử dụng";
            this.btnTrangThai.UseVisualStyleBackColor = false;
            this.btnTrangThai.Click += new System.EventHandler(this.btnTrangThai_Click);

            // ============================================================
            // dgv — DataGridView
            // ============================================================
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
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);

            // Cột
            this.colTen.HeaderText = "Tên danh mục";
            this.colTen.Name = "colTen";

            this.colLoai.HeaderText = "Loại";
            this.colLoai.Name = "colLoai";
            this.colLoai.Width = 90;
            this.colLoai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;

            this.colMoTa.HeaderText = "Mô tả";
            this.colMoTa.Name = "colMoTa";

            this.colSoGD.HeaderText = "Số giao dịch";
            this.colSoGD.Name = "colSoGD";
            this.colSoGD.Width = 110;
            this.colSoGD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colSoGD.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.Width = 130;
            this.colTrangThai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;

            // ============================================================
            // lblTrangThaiBar
            // ============================================================
            this.lblTrangThaiBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTrangThaiBar.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTrangThaiBar.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); // Theme.TextSub
            this.lblTrangThaiBar.Height = 22;
            this.lblTrangThaiBar.Name = "lblTrangThaiBar";
            this.lblTrangThaiBar.Text = "";

            // ============================================================
            // UcDanhMuc
            // ============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 247, 251); // Theme.Background
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