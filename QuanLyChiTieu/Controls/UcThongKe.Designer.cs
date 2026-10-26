namespace QuanLyChiTieu.Controls
{
    partial class UcThongKe
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.FlowLayoutPanel pnlLoc;
        private System.Windows.Forms.RadioButton rdoThang;
        private System.Windows.Forms.NumericUpDown nudThang;
        private System.Windows.Forms.Label lblSlash;
        private System.Windows.Forms.NumericUpDown nudNam;
        private System.Windows.Forms.RadioButton rdoKhoangNgay;
        private System.Windows.Forms.DateTimePicker dtpTu;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.DateTimePicker dtpDen;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnXuat;

        private System.Windows.Forms.TableLayoutPanel pnlKpi;
        private System.Windows.Forms.Panel theThu, theChi, theLech, theSoGD;
        private System.Windows.Forms.Label lblThu, lblChi, lblLech, lblSoGD;
        private System.Windows.Forms.Label lblThuTd, lblChiTd, lblLechTd, lblSoGDTd;

        private System.Windows.Forms.TableLayoutPanel pnlBieuDo;
        private System.Windows.Forms.TableLayoutPanel pnlCot;
        private System.Windows.Forms.TableLayoutPanel pnlTron;
        private System.Windows.Forms.TableLayoutPanel hopThu, hopChi;
        private System.Windows.Forms.Panel noiDungTron;
        private System.Windows.Forms.FlowLayoutPanel pnlChonLoai;
        private System.Windows.Forms.ComboBox cboLoaiTron;

        private System.Windows.Forms.TableLayoutPanel pnlBang;
        private System.Windows.Forms.TableLayoutPanel pnlDm, pnlCt;
        private System.Windows.Forms.DataGridView dgvDanhMuc;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Label lblGhiChu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlLoc = new System.Windows.Forms.FlowLayoutPanel();
            this.rdoThang = new System.Windows.Forms.RadioButton();
            this.nudThang = new System.Windows.Forms.NumericUpDown();
            this.lblSlash = new System.Windows.Forms.Label();
            this.nudNam = new System.Windows.Forms.NumericUpDown();
            this.rdoKhoangNgay = new System.Windows.Forms.RadioButton();
            this.dtpTu = new System.Windows.Forms.DateTimePicker();
            this.lblDen = new System.Windows.Forms.Label();
            this.dtpDen = new System.Windows.Forms.DateTimePicker();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnXuat = new System.Windows.Forms.Button();

            this.pnlKpi = new System.Windows.Forms.TableLayoutPanel();
            this.theThu = new System.Windows.Forms.Panel();
            this.lblThu = new System.Windows.Forms.Label();
            this.lblThuTd = new System.Windows.Forms.Label();
            this.theChi = new System.Windows.Forms.Panel();
            this.lblChi = new System.Windows.Forms.Label();
            this.lblChiTd = new System.Windows.Forms.Label();
            this.theLech = new System.Windows.Forms.Panel();
            this.lblLech = new System.Windows.Forms.Label();
            this.lblLechTd = new System.Windows.Forms.Label();
            this.theSoGD = new System.Windows.Forms.Panel();
            this.lblSoGD = new System.Windows.Forms.Label();
            this.lblSoGDTd = new System.Windows.Forms.Label();

            this.pnlBieuDo = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCot = new System.Windows.Forms.TableLayoutPanel();
            this.hopThu = new System.Windows.Forms.TableLayoutPanel();
            this.hopChi = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTron = new System.Windows.Forms.TableLayoutPanel();
            this.noiDungTron = new System.Windows.Forms.Panel();
            this.pnlChonLoai = new System.Windows.Forms.FlowLayoutPanel();
            this.cboLoaiTron = new System.Windows.Forms.ComboBox();

            this.pnlBang = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDm = new System.Windows.Forms.TableLayoutPanel();
            this.dgvDanhMuc = new System.Windows.Forms.DataGridView();
            this.pnlCt = new System.Windows.Forms.TableLayoutPanel();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.lblGhiChu = new System.Windows.Forms.Label();

            this.pnlLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).BeginInit();
            this.pnlKpi.SuspendLayout();
            this.theThu.SuspendLayout();
            this.theChi.SuspendLayout();
            this.theLech.SuspendLayout();
            this.theSoGD.SuspendLayout();
            this.pnlBieuDo.SuspendLayout();
            this.pnlCot.SuspendLayout();
            this.pnlTron.SuspendLayout();
            this.noiDungTron.SuspendLayout();
            this.pnlChonLoai.SuspendLayout();
            this.pnlBang.SuspendLayout();
            this.pnlDm.SuspendLayout();
            this.pnlCt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();

            // ==========================================
            // pnlLoc
            // ==========================================
            this.pnlLoc.BackColor = System.Drawing.Color.FromArgb(244, 247, 251);
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
            this.pnlLoc.Height = 52;
            this.pnlLoc.Name = "pnlLoc";
            this.pnlLoc.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.pnlLoc.WrapContents = false;

            this.rdoThang.Text = "Theo tháng"; this.rdoThang.AutoSize = true; this.rdoThang.Checked = true;
            this.nudThang.Minimum = 1; this.nudThang.Maximum = 12; this.nudThang.Size = new System.Drawing.Size(64, 30);
            this.lblSlash.AutoSize = true; this.lblSlash.Text = "/"; this.lblSlash.Margin = new System.Windows.Forms.Padding(2, 10, 2, 0);
            this.nudNam.Minimum = 1900; this.nudNam.Maximum = 2100; this.nudNam.Size = new System.Drawing.Size(86, 30);
            this.rdoKhoangNgay.Text = "Khoảng ngày"; this.rdoKhoangNgay.AutoSize = true; this.rdoKhoangNgay.Margin = new System.Windows.Forms.Padding(16, 10, 8, 0);
            this.dtpTu.Format = System.Windows.Forms.DateTimePickerFormat.Custom; this.dtpTu.CustomFormat = "dd/MM/yyyy"; this.dtpTu.Size = new System.Drawing.Size(120, 30); this.dtpTu.Enabled = false;
            this.lblDen.AutoSize = true; this.lblDen.Text = "đến"; this.lblDen.Margin = new System.Windows.Forms.Padding(2, 10, 2, 0);
            this.dtpDen.Format = System.Windows.Forms.DateTimePickerFormat.Custom; this.dtpDen.CustomFormat = "dd/MM/yyyy"; this.dtpDen.Size = new System.Drawing.Size(120, 30); this.dtpDen.Enabled = false;
            this.btnXem.Text = "Xem thống kê"; this.btnXem.Size = new System.Drawing.Size(130, 38); this.btnXem.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnXuat.Text = "Xuất Excel"; this.btnXuat.Size = new System.Drawing.Size(120, 38); this.btnXuat.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);

            // ==========================================
            // pnlKpi (4 thẻ)
            // ==========================================
            this.pnlKpi.ColumnCount = 4;
            this.pnlKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpi.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpi.Height = 92;
            this.pnlKpi.Controls.Add(this.theThu, 0, 0);
            this.pnlKpi.Controls.Add(this.theChi, 1, 0);
            this.pnlKpi.Controls.Add(this.theLech, 2, 0);
            this.pnlKpi.Controls.Add(this.theSoGD, 3, 0);

            // theThu
            this.theThu.BackColor = System.Drawing.Color.White; this.theThu.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8); this.theThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theThu.Controls.Add(this.lblThu); this.theThu.Controls.Add(this.lblThuTd);
            this.lblThu.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold); this.lblThu.Dock = System.Windows.Forms.DockStyle.Top; this.lblThu.Height = 32; this.lblThu.Text = "—";
            this.lblThuTd.Text = "Tổng thu"; this.lblThuTd.Dock = System.Windows.Forms.DockStyle.Top; this.lblThuTd.Height = 20; this.lblThuTd.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);

            // theChi
            this.theChi.BackColor = System.Drawing.Color.White; this.theChi.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8); this.theChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theChi.Controls.Add(this.lblChi); this.theChi.Controls.Add(this.lblChiTd);
            this.lblChi.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold); this.lblChi.Dock = System.Windows.Forms.DockStyle.Top; this.lblChi.Height = 32; this.lblChi.Text = "—";
            this.lblChiTd.Text = "Tổng chi"; this.lblChiTd.Dock = System.Windows.Forms.DockStyle.Top; this.lblChiTd.Height = 20; this.lblChiTd.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);

            // theLech
            this.theLech.BackColor = System.Drawing.Color.White; this.theLech.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8); this.theLech.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theLech.Controls.Add(this.lblLech); this.theLech.Controls.Add(this.lblLechTd);
            this.lblLech.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold); this.lblLech.Dock = System.Windows.Forms.DockStyle.Top; this.lblLech.Height = 32; this.lblLech.Text = "—";
            this.lblLechTd.Text = "Chênh lệch thu − chi"; this.lblLechTd.Dock = System.Windows.Forms.DockStyle.Top; this.lblLechTd.Height = 20; this.lblLechTd.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);

            // theSoGD
            this.theSoGD.BackColor = System.Drawing.Color.White; this.theSoGD.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8); this.theSoGD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theSoGD.Controls.Add(this.lblSoGD); this.theSoGD.Controls.Add(this.lblSoGDTd);
            this.lblSoGD.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold); this.lblSoGD.Dock = System.Windows.Forms.DockStyle.Top; this.lblSoGD.Height = 32; this.lblSoGD.Text = "—";
            this.lblSoGDTd.Text = "Số giao dịch Thu/Chi"; this.lblSoGDTd.Dock = System.Windows.Forms.DockStyle.Top; this.lblSoGDTd.Height = 20; this.lblSoGDTd.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);

            // ==========================================
            // pnlBieuDo
            // ==========================================
            this.pnlBieuDo.ColumnCount = 2;
            this.pnlBieuDo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55f));
            this.pnlBieuDo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45f));
            this.pnlBieuDo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBieuDo.Height = 340;
            this.pnlBieuDo.Controls.Add(this.pnlCot, 0, 0);
            this.pnlBieuDo.Controls.Add(this.pnlTron, 1, 0);

            // pnlCot
            this.pnlCot.ColumnCount = 1; this.pnlCot.RowCount = 2; this.pnlCot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
            this.pnlCot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
            this.pnlCot.Controls.Add(this.hopThu, 0, 0);
            this.pnlCot.Controls.Add(this.hopChi, 0, 1);

            // hopThu / hopChi
            this.hopThu.BackColor = System.Drawing.Color.White; this.hopThu.Dock = System.Windows.Forms.DockStyle.Fill; this.hopThu.Margin = new System.Windows.Forms.Padding(0, 0, 12, 10);
            this.hopChi.BackColor = System.Drawing.Color.White; this.hopChi.Dock = System.Windows.Forms.DockStyle.Fill; this.hopChi.Margin = new System.Windows.Forms.Padding(0, 0, 12, 10);

            // pnlTron
            this.pnlTron.BackColor = System.Drawing.Color.White; this.pnlTron.Dock = System.Windows.Forms.DockStyle.Fill; this.pnlTron.Margin = new System.Windows.Forms.Padding(0, 0, 12, 10);
            this.pnlTron.Controls.Add(this.noiDungTron);
            this.noiDungTron.Dock = System.Windows.Forms.DockStyle.Fill; this.noiDungTron.BackColor = System.Drawing.Color.White;
            this.noiDungTron.Controls.Add(this.pnlChonLoai); // Top
            this.pnlChonLoai.Dock = System.Windows.Forms.DockStyle.Top; this.pnlChonLoai.Height = 36; this.pnlChonLoai.BackColor = System.Drawing.Color.White;
            this.pnlChonLoai.Controls.Add(this.cboLoaiTron);
            this.cboLoaiTron.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cboLoaiTron.Size = new System.Drawing.Size(110, 30);
            this.cboLoaiTron.Items.Add("Chi"); this.cboLoaiTron.Items.Add("Thu"); this.cboLoaiTron.SelectedIndex = 0;

            // ==========================================
            // pnlBang
            // ==========================================
            this.pnlBang.ColumnCount = 2; this.pnlBang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42f)); this.pnlBang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58f));
            this.pnlBang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBang.Controls.Add(this.pnlDm, 0, 0);
            this.pnlBang.Controls.Add(this.pnlCt, 1, 0);

            this.pnlDm.BackColor = System.Drawing.Color.White; this.pnlDm.Dock = System.Windows.Forms.DockStyle.Fill; this.pnlDm.Margin = new System.Windows.Forms.Padding(0, 0, 12, 10);
            this.pnlDm.Controls.Add(this.dgvDanhMuc);
            this.dgvDanhMuc.Dock = System.Windows.Forms.DockStyle.Fill; this.dgvDanhMuc.BackgroundColor = System.Drawing.Color.White;

            this.pnlCt.BackColor = System.Drawing.Color.White; this.pnlCt.Dock = System.Windows.Forms.DockStyle.Fill; this.pnlCt.Margin = new System.Windows.Forms.Padding(0, 0, 12, 10);
            this.pnlCt.Controls.Add(this.dgvChiTiet);
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill; this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;

            // lblGhiChu
            this.lblGhiChu.Dock = System.Windows.Forms.DockStyle.Bottom; this.lblGhiChu.Height = 22; this.lblGhiChu.Text = "Chỉ tính giao dịch loại Thu và Chi...";
            this.lblGhiChu.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);

            // ==========================================
            // UcThongKe
            // ==========================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 247, 251);
            this.Controls.Add(this.pnlBang);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.pnlBieuDo);
            this.Controls.Add(this.pnlKpi);
            this.Controls.Add(this.pnlLoc);
            this.Name = "UcThongKe";
            this.Size = new System.Drawing.Size(1200, 800);

            this.pnlLoc.ResumeLayout(false); this.pnlLoc.PerformLayout();
            this.pnlKpi.ResumeLayout(false);
            this.theThu.ResumeLayout(false);
            this.theChi.ResumeLayout(false);
            this.theLech.ResumeLayout(false);
            this.theSoGD.ResumeLayout(false);
            this.pnlBieuDo.ResumeLayout(false);
            this.pnlCot.ResumeLayout(false);
            this.pnlTron.ResumeLayout(false);
            this.noiDungTron.ResumeLayout(false);
            this.pnlChonLoai.ResumeLayout(false);
            this.pnlBang.ResumeLayout(false);
            this.pnlDm.ResumeLayout(false);
            this.pnlCt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
        }
    }
}