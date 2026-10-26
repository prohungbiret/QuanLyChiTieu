using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    public partial class FrmDialogNhap : Form
    {
        private readonly ErrorProvider err = new ErrorProvider();
        private bool daPhanHoi;   // chống DialogResult đặt 2 lần

        public FrmDialogNhap() : this("Mẫu nhập", 450, 300)
        {
        }

        public FrmDialogNhap(string tieuDe, int rong, int cao)
        {
            InitializeComponent();

            Text = tieuDe;
            ClientSize = new Size(rong, cao);

            AcceptButton = btnLuu;   // Enter = Lưu (đi qua HopLe, an toàn)
            CancelButton = btnHuy;   // Esc = Hủy

            // Áp theme runtime (đề phòng Theme.cs thay đổi)
            BackColor = Theme.White;
            ForeColor = Theme.Text;
            Font = new Font("Segoe UI", 10f);
            pnlNut.BackColor = Theme.White;
            btnLuu.BackColor = Theme.Primary;
            btnLuu.ForeColor = Theme.White;
            btnLuu.Size = new Size(100, Theme.CaoNut);
            btnHuy.BackColor = Theme.Background;
            btnHuy.ForeColor = Theme.Text;
            btnHuy.Size = new Size(90, Theme.CaoNut);
        }

        protected void ThemTruong(string nhan, Control ctl, bool batBuoc = false)
        {
            Label lbl = new Label
            {
                Text = batBuoc ? nhan + " *" : nhan,
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 10, 8, 0)
            };
            ctl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            ctl.Margin = new Padding(0, 10, 0, 10);

            int row = tbl.RowCount;
            while (tbl.RowStyles.Count <= row) tbl.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tbl.RowCount = row + 1;
            tbl.Controls.Add(lbl, 0, row);
            tbl.Controls.Add(ctl, 1, row);
        }

        protected void BaoLoi(Control ctl, string thongDiep)
        {
            err.SetError(ctl, thongDiep);
            ctl.Focus();
        }

        protected void XoaLoi() { err.Clear(); }

        protected virtual bool HopLe() { return true; }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Control dauTien = tbl.Controls.Count > 1 ? tbl.Controls[1] : null;
            if (dauTien != null && dauTien.CanFocus) dauTien.Focus();
        }

        private void OnLuu(object sender, EventArgs e)
        {
            XoaLoi();
            if (!HopLe()) return;         // chỉ OK khi dữ liệu hợp lệ
            if (daPhanHoi) return;
            daPhanHoi = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnHuy(object sender, EventArgs e)
        {
            if (daPhanHoi) return;
            daPhanHoi = true;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen p = new Pen(Theme.Border))
            {
                e.Graphics.DrawLine(p, tbl.Left, pnlNut.Top, ClientSize.Width, pnlNut.Top);
            }
        }
    }
}