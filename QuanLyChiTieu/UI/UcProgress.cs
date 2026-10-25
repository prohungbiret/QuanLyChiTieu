using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QuanLyChiTieu.UI
{
    // Thanh tiến độ dùng chung (TV1 sở hữu) — đối chiếu mockup budget/savings/debts:
    // bar nền #E9EEF5 + fill tô theo mức: <80% xanh, ≥80% cam (CanhBao), ≥100% đỏ.
    // Màu KHÔNG là dấu hiệu duy nhất — luôn kèm nhãn chữ tình trạng (UI Standard 3.2.1).
    //
    // Hai cách dùng:
    //   var p = new UcProgress();
    //   p.CapNhat(1850000m, 2000000m);            // Ngân sách: "Đã dùng X% — còn Y"
    //   p.CapNhatTienDo(5000000m, 20000000m, "Đã đạt", "Hoàn thành"); // Tiết kiệm/Vay nợ:
    // chú giải phần trăm + nhãn đạt chỉ tiêu đổi theo ngữ cảnh, màu ≥100% vẫn xanh.
    // HienMoTa = false khi thẻ đã có dòng mô tả riêng (ẩn "Đã dùng X%", gọn cao).
    //
    // Bố cục v2: nhãn tình trạng DỊCH sang gần mép phải ĐO LƯỜNG THẬT (hết đè bar),
    // bar cao 14px hình viên thuốc (hết hiệu ứng "nét vẽ mảnh có núm").
    public class UcProgress : UserControl
    {
        private const int CaoBar = 14;

        private decimal daDung;
        private decimal hanMuc;
        private decimal phanTram;

        private bool laKhuMucTieu;   // true = chế độ mục tiêu (tiết kiệm/vay nợ)
        private string nhanTienDo = "Đã dùng";
        private bool hienMoTa = true;

        private readonly Label lblTinhTrang = new Label();
        private readonly Label lblMoTa = new Label();
        private readonly Font fontTrang = new Font("Segoe UI", 8.5f, FontStyle.Bold);

        public decimal DaDung { get { return daDung; } }
        public decimal HanMuc { get { return hanMuc; } }
        public decimal PhanTram { get { return phanTram; } }

        /// <summary>Suy hao không cần: false → chỉ bar + tình trạng (cao 34px); true (mặc định) → kèm dòng mô tả (cao 50px).</summary>
        public bool HienMoTa
        {
            get { return hienMoTa; }
            set
            {
                hienMoTa = value;
                lblMoTa.Visible = value;
                Height = value ? 50 : 34;
            }
        }

        public UcProgress()
        {
            BackColor = Theme.White;
            Font = new Font("Segoe UI", 8.5f);
            Height = 50;
            DoubleBuffered = true;

            lblTinhTrang.AutoSize = true;
            lblTinhTrang.Font = fontTrang;
            lblTinhTrang.BackColor = Theme.White;
            lblTinhTrang.Text = "";

            lblMoTa.AutoSize = true;
            lblMoTa.ForeColor = Theme.TextSub;
            lblMoTa.Location = new Point(2, 26);
            lblMoTa.Text = "";

            Controls.Add(lblTinhTrang);
            Controls.Add(lblMoTa);
        }

        /// <summary>Ngân sách: hanMuc phải > 0; daDung >= 0. Chữ "Đã dùng X% — còn/vượt Y".</summary>
        public void CapNhat(decimal daDungMoi, decimal hanMucMoi)
        {
            laKhuMucTieu = false;
            nhanTienDo = "Đã dùng";
            CapNhatNoiBo(daDungMoi, hanMucMoi);
        }

        /// <summary>
        /// Chế độ mục tiêu (tiết kiệm/vay nợ): đếm bằng "nhanTienDo" truyền vào
        /// (ví dụ "Đã đạt"/"Đã trả"/"Đã thu hồi"); khi đạt/gần đạt 100% thì nhãn
        /// tình trạng là nhanHoanTat ("Hoàn thành"/"Trả đủ"/"Thu hồi đủ") và màu luôn xanh.
        /// </summary>
        public void CapNhatTienDo(decimal daDungMoi, decimal tong, string nhanTienDoMoi, string nhanHoanTatMoi)
        {
            laKhuMucTieu = true;
            nhanTienDo = string.IsNullOrWhiteSpace(nhanTienDoMoi) ? "Đã đạt" : nhanTienDoMoi;
            nhanHoanTat = string.IsNullOrWhiteSpace(nhanHoanTatMoi) ? "Hoàn thành" : nhanHoanTatMoi;
            CapNhatNoiBo(daDungMoi, tong);
        }

        private string nhanHoanTat = "Hoàn thành";

        private void CapNhatNoiBo(decimal daDungMoi, decimal hanMucMoi)
        {
            daDung = daDungMoi < 0 ? 0m : daDungMoi;
            hanMuc = hanMucMoi <= 0 ? 0m : hanMucMoi;
            phanTram = hanMuc > 0 ? daDung * 100m / hanMuc : 0m;

            // Tình trạng
            if (laKhuMucTieu)
            {
                lblTinhTrang.Text = phanTram >= 100m ? nhanHoanTat : "Bình thường";
            }
            else
            {
                if (phanTram >= 100m)
                    lblTinhTrang.Text = daDung > hanMuc ? "Vượt hạn mức" : "Đạt hạn mức";
                else if (phanTram >= 80m)
                    lblTinhTrang.Text = "Cảnh báo 80%";
                else
                    lblTinhTrang.Text = "Bình thường";
            }
            lblTinhTrang.ForeColor = MauTrangThai();

            // Mô tả
            if (hienMoTa)
            {
                decimal conLai = hanMuc - daDung;
                lblMoTa.Text = conLai >= 0m
                    ? string.Format("{0} {1:0.##}% — còn {2}.",
                        nhanTienDo, phanTram, UIHelper.FormatTien(conLai))
                    : string.Format("{0} {1:0.##}% — vượt {2}.",
                        nhanTienDo, phanTram, UIHelper.FormatTien(-conLai));
            }

            // Nhãn tình trạng: đặt sát mép phải, đo lường thật (hết đè bar)
            DanhViNhan();
            Invalidate();
        }

        private void DanhViNhan()
        {
            int rongNhan = TextRenderer.MeasureText(lblTinhTrang.Text, fontTrang).Width;
            lblTinhTrang.Location = new Point(Math.Max(0, Width - rongNhan - 2), 5);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            DanhViNhan();
        }

        private Color MauTrangThai()
        {
            if (laKhuMucTieu)
            {
                // Mục tiêu: tiến độ hợp lệ đều xanh; đạt chỉ tiêu có nhãn chữ riêng
                return Theme.Thu;
            }
            if (phanTram >= 100m) return Theme.Chi;
            if (phanTram >= 80m) return Theme.CanhBao;
            return Theme.Thu;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Nhãn tình trạng nằm bên PHẢI bar — chừa đúng chỗ đã đo
            int rongNhan = string.IsNullOrEmpty(lblTinhTrang.Text)
                ? 0 : TextRenderer.MeasureText(lblTinhTrang.Text, lblTinhTrang.Font).Width + 4;
            int rongBar = Math.Max(0, Width - rongNhan - 10);
            var bar = new Rectangle(1, 8, rongBar, CaoBar);

            using (SolidBrush nen = new SolidBrush(Color.FromArgb(233, 238, 245))) // #E9EEF5
            {
                VeStadium(g, nen, bar);

                decimal leak = phanTram <= 0m ? 0m : (phanTram > 100m ? 100m : phanTram);
                int rong = (int)((long)bar.Width * leak / 100m);
                if (rong > 0)
                {
                    using (SolidBrush mau = new SolidBrush(MauTrangThai()))
                    {
                        if (rong <= CaoBar)
                        {
                            g.FillEllipse(mau, bar.X, bar.Y, CaoBar, CaoBar);
                        }
                        else
                        {
                            VeStadium(g, mau, new Rectangle(bar.X, bar.Y, rong, CaoBar));
                        }
                    }
                }
            }
        }

        /// <summary>Tô hình viên thuốc (stadium) trơn: 2 nửa vòng tròn đường kính = chiều cao.</summary>
        private static void VeStadium(Graphics g, Brush mau, Rectangle r)
        {
            int d = r.Height;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(r.X, r.Y, d, d, 90, 180);           // nửa trái (dưới → trên)
                path.AddArc(r.Right - d, r.Y, d, d, 270, 180);  // nửa phải (trên → dưới)
                path.CloseFigure();
                g.FillPath(mau, path);
            }
        }
    }
}
