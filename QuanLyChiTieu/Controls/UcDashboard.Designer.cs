namespace QuanLyChiTieu.Controls
{
    partial class UcDashboard
    {
        private System.ComponentModel.IContainer components = null;

        // Khai báo các control chính
        private System.Windows.Forms.TableLayoutPanel pnlKpis;
        private System.Windows.Forms.Label lblTieuDeBang;
        private System.Windows.Forms.DataGridView dgvGanDay;
        private System.Windows.Forms.Label lblBangFoot;

        // Khai báo 4 thẻ KPI
        private System.Windows.Forms.Panel pnlKpiThu;
        private System.Windows.Forms.Panel pnlKpiChi;
        private System.Windows.Forms.Panel pnlKpiLech;
        private System.Windows.Forms.Panel pnlKpiSoDu;

        // Khai báo Labels của Thẻ KPI 1 (Thu)
        private System.Windows.Forms.Label lblTieuDeThu;
        private System.Windows.Forms.Label lblMoTaThu;
        private System.Windows.Forms.Label lblGiaTriThu;

        // Khai báo Labels của Thẻ KPI 2 (Chi)
        private System.Windows.Forms.Label lblTieuDeChi;
        private System.Windows.Forms.Label lblMoTaChi;
        private System.Windows.Forms.Label lblGiaTriChi;

        // Khai báo Labels của Thẻ KPI 3 (Lệch)
        private System.Windows.Forms.Label lblTieuDeLech;
        private System.Windows.Forms.Label lblMoTaLech;
        private System.Windows.Forms.Label lblGiaTriLech;

        // Khai báo Labels của Thẻ KPI 4 (Số dư)
        private System.Windows.Forms.Label lblTieuDeSoDu;
        private System.Windows.Forms.Label lblMoTaSoDu;
        private System.Windows.Forms.Label lblGiaTriSoDu;

        // Khai báo các cột của DataGridView
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKhoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoTien;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlKpis = new System.Windows.Forms.TableLayoutPanel();
            this.pnlKpiThu = new System.Windows.Forms.Panel();
            this.pnlKpiChi = new System.Windows.Forms.Panel();
            this.pnlKpiLech = new System.Windows.Forms.Panel();
            this.pnlKpiSoDu = new System.Windows.Forms.Panel();
            this.lblTieuDeBang = new System.Windows.Forms.Label();
            this.dgvGanDay = new System.Windows.Forms.DataGridView();
            this.colNgay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKhoan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblBangFoot = new System.Windows.Forms.Label();

            // Labels thẻ KPI
            this.lblTieuDeThu = new System.Windows.Forms.Label();
            this.lblMoTaThu = new System.Windows.Forms.Label();
            this.lblGiaTriThu = new System.Windows.Forms.Label();
            this.lblTieuDeChi = new System.Windows.Forms.Label();
            this.lblMoTaChi = new System.Windows.Forms.Label();
            this.lblGiaTriChi = new System.Windows.Forms.Label();
            this.lblTieuDeLech = new System.Windows.Forms.Label();
            this.lblMoTaLech = new System.Windows.Forms.Label();
            this.lblGiaTriLech = new System.Windows.Forms.Label();
            this.lblTieuDeSoDu = new System.Windows.Forms.Label();
            this.lblMoTaSoDu = new System.Windows.Forms.Label();
            this.lblGiaTriSoDu = new System.Windows.Forms.Label();

            this.pnlKpis.SuspendLayout();
            this.pnlKpiThu.SuspendLayout();
            this.pnlKpiChi.SuspendLayout();
            this.pnlKpiLech.SuspendLayout();
            this.pnlKpiSoDu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGanDay)).BeginInit();
            this.SuspendLayout();

            // ============================================================
            // pnlKpis — TableLayoutPanel 4 cột chứa 4 thẻ KPI
            // ============================================================
            this.pnlKpis.ColumnCount = 4;
            this.pnlKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpis.Controls.Add(this.pnlKpiThu, 0, 0);
            this.pnlKpis.Controls.Add(this.pnlKpiChi, 1, 0);
            this.pnlKpis.Controls.Add(this.pnlKpiLech, 2, 0);
            this.pnlKpis.Controls.Add(this.pnlKpiSoDu, 3, 0);
            this.pnlKpis.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpis.Height = 118;
            this.pnlKpis.Margin = new System.Windows.Forms.Padding(0, 0, 0, 24); // Theme.GapLon
            this.pnlKpis.Name = "pnlKpis";
            this.pnlKpis.RowCount = 1;
            this.pnlKpis.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            // ============================================================
            // Thẻ KPI 1 — Tổng thu
            // ============================================================
            this.pnlKpiThu.BackColor = System.Drawing.Color.White; // Theme.White
            this.pnlKpiThu.Controls.Add(this.lblGiaTriThu);
            this.pnlKpiThu.Controls.Add(this.lblMoTaThu);
            this.pnlKpiThu.Controls.Add(this.lblTieuDeThu);
            this.pnlKpiThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiThu.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.pnlKpiThu.Name = "pnlKpiThu";
            this.pnlKpiThu.Padding = new System.Windows.Forms.Padding(14, 12, 14, 12);

            this.lblGiaTriThu.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGiaTriThu.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblGiaTriThu.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51); // Theme.Text
            this.lblGiaTriThu.Height = 34;
            this.lblGiaTriThu.Name = "lblGiaTriThu";
            this.lblGiaTriThu.Text = "—";
            this.lblGiaTriThu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblTieuDeThu.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeThu.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTieuDeThu.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); // Theme.TextSub
            this.lblTieuDeThu.Height = 20;
            this.lblTieuDeThu.Name = "lblTieuDeThu";
            this.lblTieuDeThu.Text = "Tổng thu trong kỳ";

            this.lblMoTaThu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMoTaThu.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblMoTaThu.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); // Theme.TextSub
            this.lblMoTaThu.Height = 34;
            this.lblMoTaThu.Name = "lblMoTaThu";
            this.lblMoTaThu.Text = "Tháng hiện tại — chỉ loại Thu";

            // ============================================================
            // Thẻ KPI 2 — Tổng chi
            // ============================================================
            this.pnlKpiChi.BackColor = System.Drawing.Color.White;
            this.pnlKpiChi.Controls.Add(this.lblGiaTriChi);
            this.pnlKpiChi.Controls.Add(this.lblMoTaChi);
            this.pnlKpiChi.Controls.Add(this.lblTieuDeChi);
            this.pnlKpiChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiChi.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.pnlKpiChi.Name = "pnlKpiChi";
            this.pnlKpiChi.Padding = new System.Windows.Forms.Padding(14, 12, 14, 12);

            this.lblGiaTriChi.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGiaTriChi.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblGiaTriChi.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51);
            this.lblGiaTriChi.Height = 34;
            this.lblGiaTriChi.Name = "lblGiaTriChi";
            this.lblGiaTriChi.Text = "—";
            this.lblGiaTriChi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblTieuDeChi.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeChi.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTieuDeChi.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTieuDeChi.Height = 20;
            this.lblTieuDeChi.Name = "lblTieuDeChi";
            this.lblTieuDeChi.Text = "Tổng chi trong kỳ";

            this.lblMoTaChi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMoTaChi.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblMoTaChi.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblMoTaChi.Height = 34;
            this.lblMoTaChi.Name = "lblMoTaChi";
            this.lblMoTaChi.Text = "Tháng hiện tại — chỉ loại Chi";

            // ============================================================
            // Thẻ KPI 3 — Chênh lệch
            // ============================================================
            this.pnlKpiLech.BackColor = System.Drawing.Color.White;
            this.pnlKpiLech.Controls.Add(this.lblGiaTriLech);
            this.pnlKpiLech.Controls.Add(this.lblMoTaLech);
            this.pnlKpiLech.Controls.Add(this.lblTieuDeLech);
            this.pnlKpiLech.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiLech.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.pnlKpiLech.Name = "pnlKpiLech";
            this.pnlKpiLech.Padding = new System.Windows.Forms.Padding(14, 12, 14, 12);

            this.lblGiaTriLech.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGiaTriLech.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblGiaTriLech.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51);
            this.lblGiaTriLech.Height = 34;
            this.lblGiaTriLech.Name = "lblGiaTriLech";
            this.lblGiaTriLech.Text = "—";
            this.lblGiaTriLech.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblTieuDeLech.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeLech.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTieuDeLech.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTieuDeLech.Height = 20;
            this.lblTieuDeLech.Name = "lblTieuDeLech";
            this.lblTieuDeLech.Text = "Chênh lệch thu chi";

            this.lblMoTaLech.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMoTaLech.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblMoTaLech.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblMoTaLech.Height = 34;
            this.lblMoTaLech.Name = "lblMoTaLech";
            this.lblMoTaLech.Text = "Tổng thu − tổng chi";

            // ============================================================
            // Thẻ KPI 4 — Tổng số dư
            // ============================================================
            this.pnlKpiSoDu.BackColor = System.Drawing.Color.White;
            this.pnlKpiSoDu.Controls.Add(this.lblGiaTriSoDu);
            this.pnlKpiSoDu.Controls.Add(this.lblMoTaSoDu);
            this.pnlKpiSoDu.Controls.Add(this.lblTieuDeSoDu);
            this.pnlKpiSoDu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiSoDu.Margin = new System.Windows.Forms.Padding(0);
            this.pnlKpiSoDu.Name = "pnlKpiSoDu";
            this.pnlKpiSoDu.Padding = new System.Windows.Forms.Padding(14, 12, 14, 12);

            this.lblGiaTriSoDu.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGiaTriSoDu.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblGiaTriSoDu.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235); // Theme.Primary
            this.lblGiaTriSoDu.Height = 34;
            this.lblGiaTriSoDu.Name = "lblGiaTriSoDu";
            this.lblGiaTriSoDu.Text = "—";
            this.lblGiaTriSoDu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblTieuDeSoDu.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeSoDu.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTieuDeSoDu.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTieuDeSoDu.Height = 20;
            this.lblTieuDeSoDu.Name = "lblTieuDeSoDu";
            this.lblTieuDeSoDu.Text = "Tổng số dư các ví";

            this.lblMoTaSoDu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMoTaSoDu.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblMoTaSoDu.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblMoTaSoDu.Height = 34;
            this.lblMoTaSoDu.Name = "lblMoTaSoDu";
            this.lblMoTaSoDu.Text = "Các ví đang hoạt động";

            // ============================================================
            // lblTieuDeBang
            // ============================================================
            this.lblTieuDeBang.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeBang.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeBang.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51); // Theme.Text
            this.lblTieuDeBang.Height = 26;
            this.lblTieuDeBang.Name = "lblTieuDeBang";
            this.lblTieuDeBang.Text = "Giao dịch gần đây";
            this.lblTieuDeBang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ============================================================
            // dgvGanDay
            // ============================================================
            this.dgvGanDay.AllowUserToAddRows = false;
            this.dgvGanDay.AllowUserToDeleteRows = false;
            this.dgvGanDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGanDay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNgay,
            this.colLoai,
            this.colKhoan,
            this.colGhiChu,
            this.colVi,
            this.colSoTien});
            this.dgvGanDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGanDay.Name = "dgvGanDay";
            this.dgvGanDay.ReadOnly = true;
            this.dgvGanDay.RowHeadersVisible = false;

            // Cột
            this.colNgay.HeaderText = "Ngày";
            this.colNgay.Name = "colNgay";
            this.colNgay.Width = 96;
            this.colNgay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;

            this.colLoai.HeaderText = "Loại";
            this.colLoai.Name = "colLoai";
            this.colLoai.Width = 110;
            this.colLoai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;

            this.colKhoan.HeaderText = "Danh mục / Khoản";
            this.colKhoan.Name = "colKhoan";

            this.colGhiChu.HeaderText = "Ghi chú";
            this.colGhiChu.Name = "colGhiChu";

            this.colVi.HeaderText = "Ví liên quan";
            this.colVi.Name = "colVi";

            this.colSoTien.HeaderText = "Số tiền";
            this.colSoTien.Name = "colSoTien";
            this.colSoTien.Width = 140;
            this.colSoTien.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colSoTien.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            // ============================================================
            // lblBangFoot
            // ============================================================
            this.lblBangFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBangFoot.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblBangFoot.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); // Theme.TextSub
            this.lblBangFoot.Height = 20;
            this.lblBangFoot.Name = "lblBangFoot";
            this.lblBangFoot.Text = "";
            this.lblBangFoot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ============================================================
            // UcDashboard — Thứ tự Add để Dock chuẩn
            // ============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 247, 251); // Theme.Background
            this.Controls.Add(this.dgvGanDay);
            this.Controls.Add(this.lblBangFoot);
            this.Controls.Add(this.lblTieuDeBang);
            this.Controls.Add(this.pnlKpis);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "UcDashboard";
            this.Size = new System.Drawing.Size(1000, 600);

            this.pnlKpis.ResumeLayout(false);
            this.pnlKpiThu.ResumeLayout(false);
            this.pnlKpiChi.ResumeLayout(false);
            this.pnlKpiLech.ResumeLayout(false);
            this.pnlKpiSoDu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGanDay)).EndInit();
            this.ResumeLayout(false);
        }
    }
}