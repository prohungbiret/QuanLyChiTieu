using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    // Base form nhập (TV1 sở hữu) — đồng dạng FrmMauNhap, đúng hợp đồng 5.4:
    // DialogResult.OK CHỈ được đặt khi HopLe() trả về true.
    //
    // Cách dùng (TV2: FrmThemViTien / FrmThemGiaoDich; TV4: FrmTraNo...):
    //   public class FrmThemViTien : FrmDialogNhap
    //   {
    //       public FrmThemViTien() : base("Thêm ví tiền", 520, 300)
    //       {
    //           ThemTruong("Tên ví *", txtTenVi, true);
    //           ThemTruong("Loại ví *", cboLoaiVi, true);
    //           ThemTruong("Số dư ban đầu", txtSoDu);
    //       }
    //       protected override bool HopLe()
    //       {
    //           if (!ValidationHelper.BatBuoc(txtTenVi.Text, "Tên ví", out string loi))
    //           { BaoLoi(txtTenVi, loi); return false; }
    //           return true;
    //       }
    //   }
    // Enter = Lưu, Esc = Hủy (đã cấu hình sẵn; cấm ghi đè AcceptButton/CancelButton).
    public class FrmDialogNhap : Form
    {
        protected readonly TableLayoutPanel tbl = new TableLayoutPanel();
        protected readonly FlowLayoutPanel pnlNut = new FlowLayoutPanel();
        protected readonly Button btnLuu = new Button();
        protected readonly Button btnHuy = new Button();

        private readonly ErrorProvider err = new ErrorProvider();
        private bool daPhanHoi;   // chống DialogResult đặt 2 lần

        public FrmDialogNhap(string tieuDe, int rong, int cao)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            BackColor = Theme.White;
            ForeColor = Theme.Text;
            Font = new Font("Segoe UI", 10f);
            Text = tieuDe;
            ClientSize = new Size(rong, cao);

            // Khu nội dung: 2 cột (nhãn 130px cố định, control co giãn) — TV chỉ Add vào
            // hàng mới bằng ThemTruong, KHÔNG tự đổi ColumnStyles.
            tbl.Dock = DockStyle.Fill;
            tbl.Padding = new Padding(Theme.GapChuan, Theme.GapChuan, Theme.GapChuan, 4);
            tbl.ColumnCount = 2;
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130f));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            // Hàng nút Lưu/Hủy
            pnlNut.Dock = DockStyle.Bottom;
            pnlNut.FlowDirection = FlowDirection.RightToLeft;
            pnlNut.Padding = new Padding(Theme.GapChuan, Theme.GapNho, Theme.GapChuan, Theme.GapNho);
            pnlNut.BackColor = Theme.White;
            pnlNut.Height = 56;

            btnLuu.Name = "btnLuu";
            btnLuu.Text = "Lưu";
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.BackColor = Theme.Primary;
            btnLuu.ForeColor = Theme.White;
            btnLuu.Size = new Size(100, Theme.CaoNut);

            btnHuy.Name = "btnHuy";
            btnHuy.Text = "Hủy";
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.BackColor = Theme.Background;
            btnHuy.ForeColor = Theme.Text;
            btnHuy.Size = new Size(90, Theme.CaoNut);

            btnLuu.Click += OnLuu;
            btnHuy.Click += OnHuy;
            pnlNut.Controls.Add(btnHuy);
            pnlNut.Controls.Add(btnLuu);

            AcceptButton = btnLuu;   // Enter = Lưu (đi qua HopLe, an toàn)
            CancelButton = btnHuy;   // Esc = Hủy

            Controls.Add(tbl);
            Controls.Add(pnlNut);
        }

        /// <summary>Thêm 1 hàng "nhãn + control". batBuoc = true → nhãn thêm dấu * (chuẩn UI Standard 3.1).</summary>
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

        /// <summary>Báo lỗi theo chuẩn UI Standard 4.2.1: viền đỏ + tooltip + focus về ô sai.</summary>
        protected void BaoLoi(Control ctl, string thongDiep)
        {
            err.SetError(ctl, thongDiep);
            ctl.Focus();
        }

        protected void XoaLoi() { err.Clear(); }

        /// <summary>Phân hệ ghi đè: kiểm tra hợp lệ của toàn bộ dữ liệu đang nhập.</summary>
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
            if (!HopLe()) return;         // chỉ OK khi dữ liệu hợp lệ (hợp đồng 5.4)
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
