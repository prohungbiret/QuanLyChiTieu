using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Controls
{
    partial class UcThongKe
    {
        private System.ComponentModel.IContainer components = null;

        private RadioButton rdoThang;
        private RadioButton rdoKhoangNgay;
        private NumericUpDown nudThang;
        private NumericUpDown nudNam;
        private DateTimePicker dtpTu;
        private DateTimePicker dtpDen;
        private ComboBox cboLoaiTron;
        private Label lblThu;
        private Label lblChi;
        private Label lblLech;
        private Label lblSoGD;
        private UcChart chartCotThu;
        private UcChart chartCotChi;
        private UcChart chartTron;
        private DataGridView dgvDanhMuc;
        private DataGridView dgvChiTiet;
        private Label lblGhiChu;
        private Button btnXem;
        private Button btnXuat;
        private FlowLayoutPanel pnlLoc;
        private TableLayoutPanel pnlKpi;
        private TableLayoutPanel pnlBieuDo;
        private TableLayoutPanel pnlCot;
        private TableLayoutPanel pnlBang;
        private TableLayoutPanel pnlDm;
        private TableLayoutPanel pnlCt;
        private TableLayoutPanel pnlTron;
        private FlowLayoutPanel pnlChonLoai;
        private Panel noiDungTron;
        private Panel theThu;
        private Panel theChi;
        private Panel theLech;
        private Panel theSoGD;
        private Label lblTieuDeThu;
        private Label lblTieuDeChi;
        private Label lblTieuDeLech;
        private Label lblTieuDeSoGD;
        private Label lblSlash;
        private Label lblDen;
        private Label lblTieuDeCotThu;
        private Label lblTieuDeCotChi;
        private Label lblTieuDeTron;
        private Label lblTieuDeDm;
        private Label lblTieuDeCt;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.rdoThang = new System.Windows.Forms.RadioButton();
            this.rdoKhoangNgay = new System.Windows.Forms.RadioButton();
            this.nudThang = new System.Windows.Forms.NumericUpDown();
            this.nudNam = new System.Windows.Forms.NumericUpDown();
            this.dtpTu = new System.Windows.Forms.DateTimePicker();
            this.dtpDen = new System.Windows.Forms.DateTimePicker();
            this.cboLoaiTron = new System.Windows.Forms.ComboBox();
            this.lblThu = new System.Windows.Forms.Label();
            this.lblChi = new System.Windows.Forms.Label();
            this.lblLech = new System.Windows.Forms.Label();
            this.lblSoGD = new System.Windows.Forms.Label();
            this.dgvDanhMuc = new System.Windows.Forms.DataGridView();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnXuat = new System.Windows.Forms.Button();
            this.pnlLoc = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSlash = new System.Windows.Forms.Label();
            this.lblDen = new System.Windows.Forms.Label();
            this.pnlKpi = new System.Windows.Forms.TableLayoutPanel();
            this.theThu = new System.Windows.Forms.Panel();
            this.lblTieuDeThu = new System.Windows.Forms.Label();
            this.theChi = new System.Windows.Forms.Panel();
            this.lblTieuDeChi = new System.Windows.Forms.Label();
            this.theLech = new System.Windows.Forms.Panel();
            this.lblTieuDeLech = new System.Windows.Forms.Label();
            this.theSoGD = new System.Windows.Forms.Panel();
            this.lblTieuDeSoGD = new System.Windows.Forms.Label();
            this.pnlBieuDo = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCot = new System.Windows.Forms.TableLayoutPanel();
            this.panelCotThu = new System.Windows.Forms.Panel();
            this.lblTieuDeCotThu = new System.Windows.Forms.Label();
            this.panelCotChi = new System.Windows.Forms.Panel();
            this.lblTieuDeCotChi = new System.Windows.Forms.Label();
            this.pnlTron = new System.Windows.Forms.TableLayoutPanel();
            this.lblTieuDeTron = new System.Windows.Forms.Label();
            this.noiDungTron = new System.Windows.Forms.Panel();
            this.pnlChonLoai = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlBang = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDm = new System.Windows.Forms.TableLayoutPanel();
            this.lblTieuDeDm = new System.Windows.Forms.Label();
            this.pnlCt = new System.Windows.Forms.TableLayoutPanel();
            this.lblTieuDeCt = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chartCotThu = new QuanLyChiTieu.UI.UcChart();
            this.chartCotChi = new QuanLyChiTieu.UI.UcChart();
            this.chartTron = new QuanLyChiTieu.UI.UcChart();
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlLoc.SuspendLayout();
            this.pnlKpi.SuspendLayout();
            this.theThu.SuspendLayout();
            this.theChi.SuspendLayout();
            this.theLech.SuspendLayout();
            this.theSoGD.SuspendLayout();
            this.pnlBieuDo.SuspendLayout();
            this.pnlCot.SuspendLayout();
            this.panelCotThu.SuspendLayout();
            this.panelCotChi.SuspendLayout();
            this.pnlTron.SuspendLayout();
            this.noiDungTron.SuspendLayout();
            this.pnlChonLoai.SuspendLayout();
            this.pnlBang.SuspendLayout();
            this.pnlDm.SuspendLayout();
            this.pnlCt.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoThang
            // 
            this.rdoThang.AutoSize = true;
            this.rdoThang.Checked = true;
            this.rdoThang.Location = new System.Drawing.Point(0, 18);
            this.rdoThang.Margin = new System.Windows.Forms.Padding(0, 10, 8, 0);
            this.rdoThang.Name = "rdoThang";
            this.rdoThang.Size = new System.Drawing.Size(109, 25);
            this.rdoThang.TabIndex = 0;
            this.rdoThang.TabStop = true;
            this.rdoThang.Text = "Theo tháng";
            this.rdoThang.CheckedChanged += new System.EventHandler(this.rdoThang_CheckedChanged);
            // 
            // rdoKhoangNgay
            // 
            this.rdoKhoangNgay.AutoSize = true;
            this.rdoKhoangNgay.Location = new System.Drawing.Point(315, 18);
            this.rdoKhoangNgay.Margin = new System.Windows.Forms.Padding(16, 10, 8, 0);
            this.rdoKhoangNgay.Name = "rdoKhoangNgay";
            this.rdoKhoangNgay.Size = new System.Drawing.Size(122, 25);
            this.rdoKhoangNgay.TabIndex = 4;
            this.rdoKhoangNgay.Text = "Khoảng ngày";
            this.rdoKhoangNgay.CheckedChanged += new System.EventHandler(this.rdoKhoangNgay_CheckedChanged);
            // 
            // nudThang
            // 
            this.nudThang.Location = new System.Drawing.Point(120, 11);
            this.nudThang.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudThang.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudThang.Name = "nudThang";
            this.nudThang.Size = new System.Drawing.Size(64, 29);
            this.nudThang.TabIndex = 1;
            this.nudThang.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudThang.ValueChanged += new System.EventHandler(this.nudThang_ValueChanged);
            // 
            // nudNam
            // 
            this.nudNam.Location = new System.Drawing.Point(210, 11);
            this.nudNam.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.nudNam.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.nudNam.Name = "nudNam";
            this.nudNam.Size = new System.Drawing.Size(86, 29);
            this.nudNam.TabIndex = 3;
            this.nudNam.Value = new decimal(new int[] {
            2026,
            0,
            0,
            0});
            this.nudNam.ValueChanged += new System.EventHandler(this.nudNam_ValueChanged);
            // 
            // dtpTu
            // 
            this.dtpTu.CustomFormat = "dd/MM/yyyy";
            this.dtpTu.Enabled = false;
            this.dtpTu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTu.Location = new System.Drawing.Point(448, 11);
            this.dtpTu.Name = "dtpTu";
            this.dtpTu.Size = new System.Drawing.Size(120, 29);
            this.dtpTu.TabIndex = 5;
            this.dtpTu.Value = new System.DateTime(2026, 10, 1, 0, 0, 0, 0);
            this.dtpTu.ValueChanged += new System.EventHandler(this.dtpTu_ValueChanged);
            // 
            // dtpDen
            // 
            this.dtpDen.CustomFormat = "dd/MM/yyyy";
            this.dtpDen.Enabled = false;
            this.dtpDen.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDen.Location = new System.Drawing.Point(614, 11);
            this.dtpDen.Name = "dtpDen";
            this.dtpDen.Size = new System.Drawing.Size(120, 29);
            this.dtpDen.TabIndex = 7;
            this.dtpDen.Value = new System.DateTime(2026, 10, 28, 0, 2, 45, 971).Date;
            this.dtpDen.ValueChanged += new System.EventHandler(this.dtpDen_ValueChanged);
            // 
            // cboLoaiTron
            // 
            this.cboLoaiTron.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiTron.Items.AddRange(new object[] {
            "Chi",
            "Thu"});
            this.cboLoaiTron.Location = new System.Drawing.Point(3, 3);
            this.cboLoaiTron.Name = "cboLoaiTron";
            this.cboLoaiTron.Size = new System.Drawing.Size(110, 29);
            this.cboLoaiTron.TabIndex = 0;
            this.cboLoaiTron.SelectedIndexChanged += new System.EventHandler(this.cboLoaiTron_SelectedIndexChanged);
            // 
            // lblThu
            // 
            this.lblThu.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblThu.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblThu.Location = new System.Drawing.Point(14, 28);
            this.lblThu.Name = "lblThu";
            this.lblThu.Size = new System.Drawing.Size(210, 32);
            this.lblThu.TabIndex = 0;
            this.lblThu.Text = "—";
            // 
            // lblChi
            // 
            this.lblChi.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChi.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblChi.Location = new System.Drawing.Point(14, 28);
            this.lblChi.Name = "lblChi";
            this.lblChi.Size = new System.Drawing.Size(210, 32);
            this.lblChi.TabIndex = 0;
            this.lblChi.Text = "—";
            // 
            // lblLech
            // 
            this.lblLech.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLech.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLech.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.lblLech.Location = new System.Drawing.Point(14, 28);
            this.lblLech.Name = "lblLech";
            this.lblLech.Size = new System.Drawing.Size(210, 32);
            this.lblLech.TabIndex = 0;
            this.lblLech.Text = "—";
            // 
            // lblSoGD
            // 
            this.lblSoGD.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSoGD.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSoGD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.lblSoGD.Location = new System.Drawing.Point(14, 28);
            this.lblSoGD.Name = "lblSoGD";
            this.lblSoGD.Size = new System.Drawing.Size(210, 32);
            this.lblSoGD.TabIndex = 0;
            this.lblSoGD.Text = "—";
            // 
            // dgvDanhMuc
            // 
            this.dgvDanhMuc.AllowUserToAddRows = false;
            this.dgvDanhMuc.AllowUserToDeleteRows = false;
            this.dgvDanhMuc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhMuc.BackgroundColor = System.Drawing.Color.White;
            this.dgvDanhMuc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDanhMuc.ColumnHeadersHeight = 29;
            this.dgvDanhMuc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10});
            this.dgvDanhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhMuc.Location = new System.Drawing.Point(13, 37);
            this.dgvDanhMuc.MultiSelect = false;
            this.dgvDanhMuc.Name = "dgvDanhMuc";
            this.dgvDanhMuc.ReadOnly = true;
            this.dgvDanhMuc.RowHeadersVisible = false;
            this.dgvDanhMuc.RowHeadersWidth = 51;
            this.dgvDanhMuc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhMuc.Size = new System.Drawing.Size(382, 136);
            this.dgvDanhMuc.TabIndex = 1;
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTiet.ColumnHeadersHeight = 29;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(13, 37);
            this.dgvChiTiet.MultiSelect = false;
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(542, 136);
            this.dgvChiTiet.TabIndex = 1;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblGhiChu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblGhiChu.Location = new System.Drawing.Point(0, 678);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(1000, 22);
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "Chỉ tính giao dịch loại Thu và Chi ";
            // 
            // btnXem
            // 
            this.btnXem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnXem.FlatAppearance.BorderSize = 0;
            this.btnXem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXem.ForeColor = System.Drawing.Color.White;
            this.btnXem.Location = new System.Drawing.Point(753, 8);
            this.btnXem.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(130, 38);
            this.btnXem.TabIndex = 8;
            this.btnXem.Text = "Xem thống kê";
            this.btnXem.UseVisualStyleBackColor = false;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // btnXuat
            // 
            this.btnXuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.btnXuat.FlatAppearance.BorderSize = 0;
            this.btnXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.btnXuat.Location = new System.Drawing.Point(891, 8);
            this.btnXuat.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(120, 38);
            this.btnXuat.TabIndex = 9;
            this.btnXuat.Text = "Xuất Excel";
            this.btnXuat.UseVisualStyleBackColor = false;
            this.btnXuat.Click += new System.EventHandler(this.btnXuat_Click);
            // 
            // pnlLoc
            // 
            this.pnlLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.pnlLoc.Controls.Add(this.rdoThang);
            this.pnlLoc.Controls.Add(this.nudThang);
            this.pnlLoc.Controls.Add(this.lblSlash);
            this.pnlLoc.Controls.Add(this.nudNam);
            this.pnlLoc.Controls.Add(this.rdoKhoangNgay);
            this.pnlLoc.Controls.Add(this.dtpTu);
            this.pnlLoc.Controls.Add(this.lblDen);
            this.pnlLoc.Controls.Add(this.dtpDen);
            this.pnlLoc.Controls.Add(this.btnXem);
            this.pnlLoc.Controls.Add(this.btnXuat);
            this.pnlLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLoc.Location = new System.Drawing.Point(0, 0);
            this.pnlLoc.Name = "pnlLoc";
            this.pnlLoc.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.pnlLoc.Size = new System.Drawing.Size(1000, 52);
            this.pnlLoc.TabIndex = 4;
            this.pnlLoc.WrapContents = false;
            // 
            // lblSlash
            // 
            this.lblSlash.AutoSize = true;
            this.lblSlash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSlash.Location = new System.Drawing.Point(189, 18);
            this.lblSlash.Margin = new System.Windows.Forms.Padding(2, 10, 2, 0);
            this.lblSlash.Name = "lblSlash";
            this.lblSlash.Size = new System.Drawing.Size(16, 21);
            this.lblSlash.TabIndex = 2;
            this.lblSlash.Text = "/";
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDen.Location = new System.Drawing.Point(573, 18);
            this.lblDen.Margin = new System.Windows.Forms.Padding(2, 10, 2, 0);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(36, 21);
            this.lblDen.TabIndex = 6;
            this.lblDen.Text = "đến";
            // 
            // pnlKpi
            // 
            this.pnlKpi.ColumnCount = 4;
            this.pnlKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpi.Controls.Add(this.theThu, 0, 0);
            this.pnlKpi.Controls.Add(this.theChi, 1, 0);
            this.pnlKpi.Controls.Add(this.theLech, 2, 0);
            this.pnlKpi.Controls.Add(this.theSoGD, 3, 0);
            this.pnlKpi.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpi.Location = new System.Drawing.Point(0, 52);
            this.pnlKpi.Name = "pnlKpi";
            this.pnlKpi.RowCount = 1;
            this.pnlKpi.Size = new System.Drawing.Size(1000, 92);
            this.pnlKpi.TabIndex = 3;
            // 
            // theThu
            // 
            this.theThu.BackColor = System.Drawing.Color.White;
            this.theThu.Controls.Add(this.lblThu);
            this.theThu.Controls.Add(this.lblTieuDeThu);
            this.theThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theThu.Location = new System.Drawing.Point(0, 0);
            this.theThu.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.theThu.Name = "theThu";
            this.theThu.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.theThu.Size = new System.Drawing.Size(238, 100);
            this.theThu.TabIndex = 0;
            // 
            // lblTieuDeThu
            // 
            this.lblTieuDeThu.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeThu.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTieuDeThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTieuDeThu.Location = new System.Drawing.Point(14, 8);
            this.lblTieuDeThu.Name = "lblTieuDeThu";
            this.lblTieuDeThu.Size = new System.Drawing.Size(210, 20);
            this.lblTieuDeThu.TabIndex = 1;
            this.lblTieuDeThu.Text = "Tổng thu";
            // 
            // theChi
            // 
            this.theChi.BackColor = System.Drawing.Color.White;
            this.theChi.Controls.Add(this.lblChi);
            this.theChi.Controls.Add(this.lblTieuDeChi);
            this.theChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theChi.Location = new System.Drawing.Point(250, 0);
            this.theChi.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.theChi.Name = "theChi";
            this.theChi.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.theChi.Size = new System.Drawing.Size(238, 100);
            this.theChi.TabIndex = 1;
            // 
            // lblTieuDeChi
            // 
            this.lblTieuDeChi.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeChi.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTieuDeChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTieuDeChi.Location = new System.Drawing.Point(14, 8);
            this.lblTieuDeChi.Name = "lblTieuDeChi";
            this.lblTieuDeChi.Size = new System.Drawing.Size(210, 20);
            this.lblTieuDeChi.TabIndex = 1;
            this.lblTieuDeChi.Text = "Tổng chi";
            // 
            // theLech
            // 
            this.theLech.BackColor = System.Drawing.Color.White;
            this.theLech.Controls.Add(this.lblLech);
            this.theLech.Controls.Add(this.lblTieuDeLech);
            this.theLech.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theLech.Location = new System.Drawing.Point(500, 0);
            this.theLech.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.theLech.Name = "theLech";
            this.theLech.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.theLech.Size = new System.Drawing.Size(238, 100);
            this.theLech.TabIndex = 2;
            // 
            // lblTieuDeLech
            // 
            this.lblTieuDeLech.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeLech.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTieuDeLech.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTieuDeLech.Location = new System.Drawing.Point(14, 8);
            this.lblTieuDeLech.Name = "lblTieuDeLech";
            this.lblTieuDeLech.Size = new System.Drawing.Size(210, 20);
            this.lblTieuDeLech.TabIndex = 1;
            this.lblTieuDeLech.Text = "Chênh lệch thu − chi";
            // 
            // theSoGD
            // 
            this.theSoGD.BackColor = System.Drawing.Color.White;
            this.theSoGD.Controls.Add(this.lblSoGD);
            this.theSoGD.Controls.Add(this.lblTieuDeSoGD);
            this.theSoGD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theSoGD.Location = new System.Drawing.Point(750, 0);
            this.theSoGD.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.theSoGD.Name = "theSoGD";
            this.theSoGD.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.theSoGD.Size = new System.Drawing.Size(238, 100);
            this.theSoGD.TabIndex = 3;
            // 
            // lblTieuDeSoGD
            // 
            this.lblTieuDeSoGD.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeSoGD.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTieuDeSoGD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTieuDeSoGD.Location = new System.Drawing.Point(14, 8);
            this.lblTieuDeSoGD.Name = "lblTieuDeSoGD";
            this.lblTieuDeSoGD.Size = new System.Drawing.Size(210, 20);
            this.lblTieuDeSoGD.TabIndex = 1;
            this.lblTieuDeSoGD.Text = "Số giao dịch Thu/Chi";
            // 
            // pnlBieuDo
            // 
            this.pnlBieuDo.ColumnCount = 2;
            this.pnlBieuDo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.pnlBieuDo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.pnlBieuDo.Controls.Add(this.pnlCot, 0, 0);
            this.pnlBieuDo.Controls.Add(this.pnlTron, 1, 0);
            this.pnlBieuDo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBieuDo.Location = new System.Drawing.Point(0, 144);
            this.pnlBieuDo.Name = "pnlBieuDo";
            this.pnlBieuDo.RowCount = 1;
            this.pnlBieuDo.Size = new System.Drawing.Size(1000, 340);
            this.pnlBieuDo.TabIndex = 2;
            // 
            // pnlCot
            // 
            this.pnlCot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.pnlCot.ColumnCount = 1;
            this.pnlCot.Controls.Add(this.panelCotThu, 0, 0);
            this.pnlCot.Controls.Add(this.panelCotChi, 0, 1);
            this.pnlCot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCot.Location = new System.Drawing.Point(3, 3);
            this.pnlCot.Name = "pnlCot";
            this.pnlCot.RowCount = 2;
            this.pnlCot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlCot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlCot.Size = new System.Drawing.Size(544, 334);
            this.pnlCot.TabIndex = 0;
            // 
            // panelCotThu
            // 
            this.panelCotThu.Controls.Add(this.chartCotThu);
            this.panelCotThu.Controls.Add(this.lblTieuDeCotThu);
            this.panelCotThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCotThu.Location = new System.Drawing.Point(3, 3);
            this.panelCotThu.Name = "panelCotThu";
            this.panelCotThu.Size = new System.Drawing.Size(538, 161);
            this.panelCotThu.TabIndex = 0;
            // 
            // lblTieuDeCotThu
            // 
            this.lblTieuDeCotThu.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeCotThu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeCotThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.lblTieuDeCotThu.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDeCotThu.Name = "lblTieuDeCotThu";
            this.lblTieuDeCotThu.Size = new System.Drawing.Size(538, 28);
            this.lblTieuDeCotThu.TabIndex = 1;
            this.lblTieuDeCotThu.Text = "Biểu đồ cột — Thu theo tháng (6 tháng gần nhất)";
            // 
            // panelCotChi
            // 
            this.panelCotChi.Controls.Add(this.chartCotChi);
            this.panelCotChi.Controls.Add(this.lblTieuDeCotChi);
            this.panelCotChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCotChi.Location = new System.Drawing.Point(3, 170);
            this.panelCotChi.Name = "panelCotChi";
            this.panelCotChi.Size = new System.Drawing.Size(538, 161);
            this.panelCotChi.TabIndex = 1;
            // 
            // lblTieuDeCotChi
            // 
            this.lblTieuDeCotChi.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeCotChi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeCotChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.lblTieuDeCotChi.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDeCotChi.Name = "lblTieuDeCotChi";
            this.lblTieuDeCotChi.Size = new System.Drawing.Size(538, 28);
            this.lblTieuDeCotChi.TabIndex = 1;
            this.lblTieuDeCotChi.Text = "Biểu đồ cột — Chi theo tháng (6 tháng gần nhất)";
            // 
            // pnlTron
            // 
            this.pnlTron.BackColor = System.Drawing.Color.White;
            this.pnlTron.ColumnCount = 1;
            this.pnlTron.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlTron.Controls.Add(this.lblTieuDeTron, 0, 0);
            this.pnlTron.Controls.Add(this.noiDungTron, 0, 1);
            this.pnlTron.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTron.Location = new System.Drawing.Point(550, 0);
            this.pnlTron.Margin = new System.Windows.Forms.Padding(0, 0, 12, 10);
            this.pnlTron.Name = "pnlTron";
            this.pnlTron.Padding = new System.Windows.Forms.Padding(10, 6, 10, 8);
            this.pnlTron.RowCount = 2;
            this.pnlTron.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.pnlTron.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlTron.Size = new System.Drawing.Size(438, 330);
            this.pnlTron.TabIndex = 1;
            // 
            // lblTieuDeTron
            // 
            this.lblTieuDeTron.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDeTron.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeTron.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.lblTieuDeTron.Location = new System.Drawing.Point(13, 6);
            this.lblTieuDeTron.Name = "lblTieuDeTron";
            this.lblTieuDeTron.Size = new System.Drawing.Size(412, 28);
            this.lblTieuDeTron.TabIndex = 0;
            this.lblTieuDeTron.Text = "Biểu đồ tròn — cơ cấu theo danh mục";
            // 
            // noiDungTron
            // 
            this.noiDungTron.BackColor = System.Drawing.Color.White;
            this.noiDungTron.Controls.Add(this.chartTron);
            this.noiDungTron.Controls.Add(this.pnlChonLoai);
            this.noiDungTron.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noiDungTron.Location = new System.Drawing.Point(13, 37);
            this.noiDungTron.Name = "noiDungTron";
            this.noiDungTron.Size = new System.Drawing.Size(412, 282);
            this.noiDungTron.TabIndex = 1;
            // 
            // pnlChonLoai
            // 
            this.pnlChonLoai.BackColor = System.Drawing.Color.White;
            this.pnlChonLoai.Controls.Add(this.cboLoaiTron);
            this.pnlChonLoai.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChonLoai.Location = new System.Drawing.Point(0, 0);
            this.pnlChonLoai.Name = "pnlChonLoai";
            this.pnlChonLoai.Size = new System.Drawing.Size(412, 36);
            this.pnlChonLoai.TabIndex = 1;
            // 
            // pnlBang
            // 
            this.pnlBang.ColumnCount = 2;
            this.pnlBang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.pnlBang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.pnlBang.Controls.Add(this.pnlDm, 0, 0);
            this.pnlBang.Controls.Add(this.pnlCt, 1, 0);
            this.pnlBang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBang.Location = new System.Drawing.Point(0, 484);
            this.pnlBang.Name = "pnlBang";
            this.pnlBang.RowCount = 1;
            this.pnlBang.Size = new System.Drawing.Size(1000, 194);
            this.pnlBang.TabIndex = 0;
            // 
            // pnlDm
            // 
            this.pnlDm.BackColor = System.Drawing.Color.White;
            this.pnlDm.ColumnCount = 1;
            this.pnlDm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlDm.Controls.Add(this.lblTieuDeDm, 0, 0);
            this.pnlDm.Controls.Add(this.dgvDanhMuc, 0, 1);
            this.pnlDm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDm.Location = new System.Drawing.Point(0, 0);
            this.pnlDm.Margin = new System.Windows.Forms.Padding(0, 0, 12, 10);
            this.pnlDm.Name = "pnlDm";
            this.pnlDm.Padding = new System.Windows.Forms.Padding(10, 6, 10, 8);
            this.pnlDm.RowCount = 2;
            this.pnlDm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.pnlDm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlDm.Size = new System.Drawing.Size(408, 184);
            this.pnlDm.TabIndex = 0;
            // 
            // lblTieuDeDm
            // 
            this.lblTieuDeDm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDeDm.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeDm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.lblTieuDeDm.Location = new System.Drawing.Point(13, 6);
            this.lblTieuDeDm.Name = "lblTieuDeDm";
            this.lblTieuDeDm.Size = new System.Drawing.Size(382, 28);
            this.lblTieuDeDm.TabIndex = 0;
            this.lblTieuDeDm.Text = "Phân bổ theo danh mục trong kỳ";
            // 
            // pnlCt
            // 
            this.pnlCt.BackColor = System.Drawing.Color.White;
            this.pnlCt.ColumnCount = 1;
            this.pnlCt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlCt.Controls.Add(this.lblTieuDeCt, 0, 0);
            this.pnlCt.Controls.Add(this.dgvChiTiet, 0, 1);
            this.pnlCt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCt.Location = new System.Drawing.Point(420, 0);
            this.pnlCt.Margin = new System.Windows.Forms.Padding(0, 0, 12, 10);
            this.pnlCt.Name = "pnlCt";
            this.pnlCt.Padding = new System.Windows.Forms.Padding(10, 6, 10, 8);
            this.pnlCt.RowCount = 2;
            this.pnlCt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.pnlCt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlCt.Size = new System.Drawing.Size(568, 184);
            this.pnlCt.TabIndex = 1;
            // 
            // lblTieuDeCt
            // 
            this.lblTieuDeCt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDeCt.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeCt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(51)))));
            this.lblTieuDeCt.Location = new System.Drawing.Point(13, 6);
            this.lblTieuDeCt.Name = "lblTieuDeCt";
            this.lblTieuDeCt.Size = new System.Drawing.Size(542, 28);
            this.lblTieuDeCt.TabIndex = 0;
            this.lblTieuDeCt.Text = "Chi tiết giao dịch Thu/Chi trong kỳ";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Ngày";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Loại";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Danh mục";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Ví";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Ghi chú";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Số tiền";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Danh mục";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Số GD";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Tổng tiền";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Tỉ trọng";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // chartCotThu
            // 
            this.chartCotThu.BackColor = System.Drawing.Color.White;
            this.chartCotThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartCotThu.Location = new System.Drawing.Point(0, 28);
            this.chartCotThu.Name = "chartCotThu";
            this.chartCotThu.Size = new System.Drawing.Size(538, 133);
            this.chartCotThu.TabIndex = 0;
            // 
            // chartCotChi
            // 
            this.chartCotChi.BackColor = System.Drawing.Color.White;
            this.chartCotChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartCotChi.Location = new System.Drawing.Point(0, 28);
            this.chartCotChi.Name = "chartCotChi";
            this.chartCotChi.Size = new System.Drawing.Size(538, 133);
            this.chartCotChi.TabIndex = 0;
            // 
            // chartTron
            // 
            this.chartTron.BackColor = System.Drawing.Color.White;
            this.chartTron.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartTron.Location = new System.Drawing.Point(0, 36);
            this.chartTron.Name = "chartTron";
            this.chartTron.Size = new System.Drawing.Size(412, 246);
            this.chartTron.TabIndex = 0;
            // 
            // UcThongKe
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.Controls.Add(this.pnlBang);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.pnlBieuDo);
            this.Controls.Add(this.pnlKpi);
            this.Controls.Add(this.pnlLoc);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "UcThongKe";
            this.Size = new System.Drawing.Size(1000, 700);
            this.Tag = "Thống kê";
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlLoc.ResumeLayout(false);
            this.pnlLoc.PerformLayout();
            this.pnlKpi.ResumeLayout(false);
            this.theThu.ResumeLayout(false);
            this.theChi.ResumeLayout(false);
            this.theLech.ResumeLayout(false);
            this.theSoGD.ResumeLayout(false);
            this.pnlBieuDo.ResumeLayout(false);
            this.pnlCot.ResumeLayout(false);
            this.panelCotThu.ResumeLayout(false);
            this.panelCotChi.ResumeLayout(false);
            this.pnlTron.ResumeLayout(false);
            this.noiDungTron.ResumeLayout(false);
            this.pnlChonLoai.ResumeLayout(false);
            this.pnlBang.ResumeLayout(false);
            this.pnlDm.ResumeLayout(false);
            this.pnlCt.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private Panel panelCotThu;
        private Panel panelCotChi;
    }
}
