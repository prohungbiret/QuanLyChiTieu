namespace QuanLyChiTieu.Forms
{
    partial class FrmDialogNhap
    {
        private System.ComponentModel.IContainer components = null;

        protected System.Windows.Forms.TableLayoutPanel tbl;
        protected System.Windows.Forms.FlowLayoutPanel pnlNut;
        protected System.Windows.Forms.Button btnLuu;
        protected System.Windows.Forms.Button btnHuy;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbl = new System.Windows.Forms.TableLayoutPanel();
            this.pnlNut = new System.Windows.Forms.FlowLayoutPanel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // ============================================================
            // tbl (TableLayoutPanel)
            // ============================================================
            this.tbl.ColumnCount = 2;
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl.Name = "tbl";
            this.tbl.Padding = new System.Windows.Forms.Padding(16, 16, 16, 4); // Theme.GapChuan

            // ============================================================
            // pnlNut (FlowLayoutPanel)
            // ============================================================
            this.pnlNut.BackColor = System.Drawing.Color.White;
            this.pnlNut.Controls.Add(this.btnHuy);
            this.pnlNut.Controls.Add(this.btnLuu);
            this.pnlNut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNut.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlNut.Height = 56;
            this.pnlNut.Name = "pnlNut";
            this.pnlNut.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);

            // ============================================================
            // btnLuu
            // ============================================================
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(37, 99, 235); // Theme.Primary
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 38); // Theme.CaoNut
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.OnLuu);

            // ============================================================
            // btnHuy
            // ============================================================
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(244, 247, 251); // Theme.Background
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51); // Theme.Text
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 38);
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.OnHuy);

            // ============================================================
            // FrmDialogNhap
            // ============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(450, 300);
            this.Controls.Add(this.tbl);
            this.Controls.Add(this.pnlNut);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDialogNhap";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mẫu nhập";

            this.ResumeLayout(false);
        }
    }
}