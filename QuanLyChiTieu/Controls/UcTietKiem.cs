using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Controls
{
    // TV1 (thay TV4) — màn Tiết kiệm: danh sách mục tiêu dạng thẻ + UcProgress dùng chung
    // (đối chiếu mockup savings.html), mỗi thẻ có Đóng góp / Rút / Sửa / Xóa.
    // Tiến độ hiển thị từ MucTieuChiTiet.DaDat (tính động, QD05 — không lưu cột).
    public class UcTietKiem : UserControl
    {
        private readonly FlowLayoutPanel pnlDanhSach = new FlowLayoutPanel();
        private readonly Label lblTongKet = new Label();

        public UcTietKiem()
        {
            Tag = "Tiết kiệm"; // hợp đồng 5.3
            BackColor = Theme.Background;
            Font = new Font("Segoe UI", 9.5f);

            // ---- Hàng công cụ ----
            var pnlTools = new FlowLayoutPanel();
            pnlTools.Dock = DockStyle.Top;
            pnlTools.Height = 52;
            pnlTools.Padding = new Padding(0, 8, 0, 8);
            pnlTools.BackColor = Theme.Background;
            pnlTools.WrapContents = false;

            var lblPhanHe = new Label();
            lblPhanHe.Text = "Mục tiêu tiết kiệm";
            lblPhanHe.AutoSize = true;
            lblPhanHe.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            lblPhanHe.ForeColor = Theme.Text;
            lblPhanHe.Margin = new Padding(0, 10, 12, 0);

            var btnThem = new Button();
            btnThem.Text = "Thêm mục tiêu";
            btnThem.Size = new Size(150, Theme.CaoNut);
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.BackColor = Theme.Primary;
            btnThem.ForeColor = Theme.White;
            btnThem.Margin = new Padding(16, 0, 0, 0);
            btnThem.Click += (s, e) => ThemMoi();

            pnlTools.Controls.Add(lblPhanHe);
            pnlTools.Controls.Add(btnThem);

            // ---- Tổng kết ----
            lblTongKet.Dock = DockStyle.Bottom;
            lblTongKet.Height = 26;
            lblTongKet.Font = new Font("Segoe UI", 9f);
            lblTongKet.ForeColor = Theme.TextSub;

            // ---- Danh sách thẻ mục tiêu (cuộn dọc) ----
            pnlDanhSach.Dock = DockStyle.Fill;
            pnlDanhSach.AutoScroll = true;
            pnlDanhSach.FlowDirection = FlowDirection.TopDown;
            pnlDanhSach.WrapContents = false;
            pnlDanhSach.BackColor = Theme.Background;
            pnlDanhSach.Resize += (s, e) => ChinhRongThe();

            Controls.Add(pnlDanhSach);
            Controls.Add(lblTongKet);
            Controls.Add(pnlTools);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            TaiDanhSach();
        }

        private void ChinhRongThe()
        {
            int rong = pnlDanhSach.ClientSize.Width - 28;
            if (rong < 340) rong = 340;
            foreach (Control c in pnlDanhSach.Controls) c.Width = rong;
        }

        private void TaiDanhSach()
        {
            try
            {
                System.Collections.Generic.List<MucTieuChiTiet> ds = TietKiemService.LayTatCa();

                pnlDanhSach.SuspendLayout();
                pnlDanhSach.Controls.Clear();

                decimal tongMucTieu = 0m, tongDaDat = 0m;
                int soHoanThanh = 0;

                foreach (MucTieuChiTiet ct in ds)
                {
                    tongMucTieu += ct.MucTieu.SoTienMucTieu;
                    tongDaDat += ct.DaDat;
                    if (ct.MucTieu.TrangThai == "HoanThanh") soHoanThanh++;
                    pnlDanhSach.Controls.Add(TaoThe(ct));
                }
                ChinhRongThe();
                pnlDanhSach.ResumeLayout();

                if (ds.Count == 0)
                {
                    lblTongKet.Text = "Chưa có mục tiêu tiết kiệm nào. Hãy thêm mục tiêu đầu tiên.";
                }
                else
                {
                    lblTongKet.Text = "Tổng mục tiêu: " + UIHelper.FormatTien(tongMucTieu) +
                        "  •  Đã đạt: " + UIHelper.FormatTien(tongDaDat) +
                        "  •  Hoàn thành: " + soHoanThanh + "/" + ds.Count;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không tải được danh sách mục tiêu. Hãy kiểm tra cấu hình LocalDB.",
                    "Lỗi truy cập dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private System.Windows.Forms.Control TaoThe(MucTieuChiTiet ct)
        {
            var the = new Panel();
            the.BackColor = Theme.White;
            the.Height = 152;
            the.Width = Math.Max(340, pnlDanhSach.ClientSize.Width - 28);
            the.Margin = new Padding(0, 0, 0, Theme.GapVua);
            the.Padding = new Padding(14, 10, 14, 8);

            var lblTen = new Label();
            lblTen.Text = ct.MucTieu.TenMucTieu +
                (ct.MucTieu.TrangThai == "HoanThanh" ? "  ✓" :
                 ct.MucTieu.TrangThai == "TamDung" ? "  (Tạm dừng)" : "");
            lblTen.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            lblTen.ForeColor = ct.MucTieu.TrangThai == "HoanThanh" ? Theme.Thu : Theme.Text;
            lblTen.AutoSize = true;
            lblTen.Location = new Point(14, 10);

            var lblThanhTich = new Label();
            lblThanhTich.Text = "Đã đạt " + UIHelper.FormatTien(Math.Max(0, ct.DaDat)) +
                " / mục tiêu " + UIHelper.FormatTien(ct.MucTieu.SoTienMucTieu) +
                "  [" + ct.NhanTrangThai + "]";
            lblThanhTich.Font = new Font("Segoe UI", 9f);
            lblThanhTich.ForeColor = Theme.TextSub;
            lblThanhTich.AutoSize = true;
            lblThanhTich.Location = new Point(14, 36);

            var tienDo = new UcProgress();
            tienDo.Location = new Point(14, 58);
            tienDo.Width = Math.Max(200, the.Width - 220);
            tienDo.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            tienDo.CapNhat(Math.Max(0, ct.DaDat), ct.MucTieu.SoTienMucTieu);

            // Hàng nút ở đáy thẻ, căn phải
            var pnlNut = new FlowLayoutPanel();
            pnlNut.FlowDirection = FlowDirection.RightToLeft;
            pnlNut.Location = new Point(14, 96);
            pnlNut.Size = new Size(the.Width - 28, 40);
            pnlNut.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            pnlNut.BackColor = Theme.White;

            var btnDongGop = NutThe("Đóng góp", 96, Theme.Thu, Theme.White);
            btnDongGop.Click += (s, e) => GhiGD(ct, false);
            var btnRut = NutThe("Rút", 96, Theme.Background, Theme.Text);
            btnRut.Click += (s, e) => GhiGD(ct, true);
            var btnSua = NutThe("Sửa", 90, Theme.Background, Theme.Text);
            btnSua.Click += (s, e) => SuaChon(ct);
            var btnXoa = NutThe("Xóa", 90, Theme.Background, Theme.Text);
            btnXoa.Click += (s, e) => XoaChon(ct);

            pnlNut.Controls.Add(btnXoa);
            pnlNut.Controls.Add(btnSua);
            pnlNut.Controls.Add(btnRut);
            pnlNut.Controls.Add(btnDongGop);

            the.Controls.Add(lblTen);
            the.Controls.Add(lblThanhTich);
            the.Controls.Add(tienDo);
            the.Controls.Add(pnlNut);
            return the;
        }

        private static Button NutThe(string text, int rong, Color nen, Color chu)
        {
            var btn = new Button();
            btn.Text = text;
            btn.Size = new Size(rong, 34);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = nen;
            btn.ForeColor = chu;
            btn.Margin = new Padding(6, 0, 0, 0);
            return btn;
        }

        private void GhiGD(MucTieuChiTiet ct, bool laRut)
        {
            using (var frm = new FrmTietKiemGD(laRut, ct.MucTieu.TenMucTieu))
            {
                if (frm.ShowDialog() != DialogResult.OK) return;

                string loai = laRut ? GiaoDichLoai.RutTietKiem : GiaoDichLoai.DongGop;
                int? viNguon = laRut ? (int?)null : frm.KetQuaViID;
                int? viDich = laRut ? (int?)frm.KetQuaViID : null;

                string loi;
                int id = TietKiemService.Ghi(loai, ct.MucTieu.MucTieuID, frm.KetQuaSoTien,
                    viNguon, viDich, frm.KetQuaNgay, frm.KetQuaGhiChu, out loi);
                if (id < 0)
                {
                    MessageBox.Show(loi, laRut ? "Không rút được" : "Không ghi được đóng góp",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TaiDanhSach();
                if (!laRut && ct.PhanTram >= 100m)
                {
                    MessageBox.Show("Mục tiêu \"" + ct.MucTieu.TenMucTieu + "\" đã hoàn thành!",
                        "Xin chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ThemMoi()
        {
            using (var frm = new FrmThemMucTieu())
            {
                if (frm.ShowDialog() != DialogResult.OK) return;
                string loi;
                if (TietKiemService.Them(frm.KetQua, out loi) < 0)
                {
                    MessageBox.Show(loi, "Không thêm được mục tiêu",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TaiDanhSach();
            }
        }

        private void SuaChon(MucTieuChiTiet ct)
        {
            using (var frm = new FrmThemMucTieu(ct.MucTieu))
            {
                if (frm.ShowDialog() != DialogResult.OK) return;
                string loi;
                if (!TietKiemService.Sua(frm.KetQua, out loi))
                {
                    MessageBox.Show(loi, "Không sửa được mục tiêu",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TaiDanhSach();
            }
        }

        private void XoaChon(MucTieuChiTiet ct)
        {
            string thongDiep = "Xóa mục tiêu \"" + ct.MucTieu.TenMucTieu + "\"?\n" +
                "Lịch sử đóng góp/rút gắn với mục tiêu vẫn giữ nguyên ở phân hệ Giao dịch.";
            if (MessageBox.Show(thongDiep, "Xác nhận xóa mục tiêu",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            string loi;
            if (!TietKiemService.Xoa(ct.MucTieu.MucTieuID, out loi))
            {
                MessageBox.Show(loi, "Không xóa được mục tiêu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TaiDanhSach();
        }
    }
}
