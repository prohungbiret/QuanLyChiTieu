using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.UI
{
    // Thanh tiến độ dùng chung (TV1 sở hữu) — đối chiếu mockup budget/savings/debts:
    // bar nền #E9EEF5 + fill tô theo mức: <80% xanh, ≥80% cam (CanhBao), ≥100% đỏ.
    // Màu KHÔNG là dấu hiệu duy nhất — luôn kèm nhãn chữ tình trạng (UI Standard 3.2.1).
    //
    // Cách dùng:
    //   var p = new UcProgress();
    //   p.CapNhat(1850000m, 2000000m);   // đã chi / hạn mức
    // Muốn dùng cho mục tiêu tiết kiệm/vay nợ: CapNhat(tienDo, soTienMucTieu) —
    // truyền theo nghĩa "đã đạt / tổng cần đạt". "Còn X" tự đổi thành "Vượt X".
    public class UcProgress : UserControl
    {
        private decimal daDung;
        private decimal hanMuc;
        private decimal phanTram;

        private readonly Label lblTinhTrang = new Label();
        private readonly Label lblMoTa = new Label();

        public decimal DaDung { get { return daDung; } }
        public decimal HanMuc { get { return hanMuc; } }
        public decimal PhanTram { get { return phanTram; } }

        public UcProgress()
        {
            BackColor = Theme.White;
            Font = new Font("Segoe UI", 8.5f);
            Height = 58;
            DoubleBuffered = true;

            lblTinhTrang.AutoSize = true;
            lblTinhTrang.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            lblTinhTrang.BackColor = Theme.White;
            // neo mép phải trên (vùng bar vẽ chiều cao 12px bắt đầu từ y=6)
            lblTinhTrang.Location = new Point(Width - 120, 10);
            lblTinhTrang.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            lblMoTa.AutoSize = true;
            lblMoTa.ForeColor = Theme.TextSub;
            lblMoTa.Location = new Point(2, 32);

            lblMoTa.Text = "";
            lblTinhTrang.Text = "";

            Controls.Add(lblTinhTrang);
            Controls.Add(lblMoTa);
        }

        /// <summary>Cập nhật tiến độ. hanMuc phải > 0; daDung >= 0.</summary>
        public void CapNhat(decimal daDungMoi, decimal hanMucMoi)
        {
            daDung = daDungMoi < 0 ? 0m : daDungMoi;
            hanMuc = hanMucMoi <= 0 ? 0m : hanMucMoi;
            phanTram = hanMuc > 0 ? daDung * 100m / hanMuc : 0m;

            if (phanTram >= 100m)
            {
                lblTinhTrang.Text = PhanTramText();
            }
            else if (phanTram >= 80m)
            {
                lblTinhTrang.Text = "Cảnh báo 80%";
            }
            else
            {
                lblTinhTrang.Text = "Bình thường";
            }
            lblTinhTrang.ForeColor = MauTrangThai();

            decimal conLai = hanMuc - daDung;
            if (conLai >= 0m)
            {
                lblMoTa.Text = string.Format("Đã dùng {0:0.##}% — còn {1}.",
                    phanTram, UIHelper.FormatTien(conLai));
            }
            else
            {
                lblMoTa.Text = string.Format("Đã dùng {0:0.##}% — vượt {1}.",
                    phanTram, UIHelper.FormatTien(-conLai));
            }

            Invalidate();
        }

        private Color MauTrangThai()
        {
            if (phanTram >= 100m) return Theme.Chi;
            if (phanTram >= 80m) return Theme.CanhBao;
            return Theme.Thu;
        }

        private string PhanTramText()
        {
            if (daDung > hanMuc) return "Vượt hạn mức";
            if (daDung == hanMuc) return "Đạt hạn mức";
            return "Hoàn thành";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bar = new Rectangle(0, 6, Math.Max(0, Width - lblTinhTrang.Width - 8), 12);
            using (SolidBrush nen = new SolidBrush(Color.FromArgb(233, 238, 245))) // #E9EEF5
            using (GraphicsPath path = new GraphicsPath())
            {
                int bo = 6;
                path.AddArc(bar.X, bar.Y, bo, bo, 180, 180);
                path.AddArc(bar.Right - bo, bar.Y, bo, bo, 270, 180);
                path.AddArc(bar.Right - bo, bar.Bottom - bo, bo, bo, 0, 180);
                path.AddArc(bar.X, bar.Bottom - bo, bo, bo, 90, 180);
                path.CloseFigure();
                g.FillPath(nen, path);

                decimal leak = phanTram <= 0m ? 0m : (phanTram > 100m ? 100m : phanTram);
                int rong = (int)((bar.Width) * leak / 100m);
                if (rong > 0)
                {
                    Rectangle dau = new Rectangle(bar.X, bar.Y, Math.Max(rong, bo), bar.Height);
                    using (GraphicsPath path2 = new GraphicsPath())
                    {
                        path2.AddArc(dau.X, dau.Y, bo, bo, 180, 180);
                        path2.AddArc(dau.Right - bo, dau.Y, bo, bo, 270, 180);
                        path2.AddArc(dau.Right - bo, dau.Bottom - bo, bo, bo, 0, 180);
                        path2.AddArc(dau.X, dau.Bottom - bo, bo, bo, 90, 180);
                        path2.CloseFigure();
                        using (SolidBrush mau = new SolidBrush(MauTrangThai()))
                        {
                            g.FillPath(mau, path2);
                        }
                    }
                }
            }
        }
    }
}
