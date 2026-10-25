using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Controls
{
    // TV1 (thay TV4) — màn Vay nợ (mockup debts.html): 2 nhóm khoản
    // "Mình nợ" (Vay) và "Mình cho vay" (ChoVay) dạng thẻ + UcProgress,
    // mỗi thẻ có Trả nợ/Thu hồi + Sửa + Xóa. Trả nợ mở FrmTraNo (dialog).
    public class UcVayNo : UserControl
    {
        private readonly FlowLayoutPanel pnlDanhSach = new FlowLayoutPanel();
        private readonly Label lblTongKet = new Label();

        public UcVayNo()
        {
            Tag = "Vay nợ"; // hợp đồng 5.3
            BackColor = Theme.Background;
            Font = new Font("Segoe UI", 9.5f);

            var pnlTools = new FlowLayoutPanel();
            pnlTools.Dock = DockStyle.Top;
            pnlTools.Height = 52;
            pnlTools.Padding = new Padding(0, 8, 0, 8);
            pnlTools.BackColor = Theme.Background;
            pnlTools.WrapContents = false;

            var lblPhanHe = new Label();
            lblPhanHe.Text = "Quản lý vay nợ";
            lblPhanHe.AutoSize = true;
            lblPhanHe.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            lblPhanHe.ForeColor = Theme.Text;
            lblPhanHe.Margin = new Padding(0, 10, 12, 0);

            var btnThem = new Button();
            btnThem.Text = "Thêm khoản";
            btnThem.Size = new Size(140, Theme.CaoNut);
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.BackColor = Theme.Primary;
            btnThem.ForeColor = Theme.White;
            btnThem.Margin = new Padding(16, 0, 0, 0);
            btnThem.Click += (s, e) => ThemMoi();

            pnlTools.Controls.Add(lblPhanHe);
            pnlTools.Controls.Add(btnThem);

            lblTongKet.Dock = DockStyle.Bottom;
            lblTongKet.Height = 26;
            lblTongKet.Font = new Font("Segoe UI", 9f);
            lblTongKet.ForeColor = Theme.TextSub;

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
                List<KhoanVayNoChiTiet> ds = VayNoService.LayTatCa();

                pnlDanhSach.SuspendLayout();
                pnlDanhSach.Controls.Clear();

                decimal tongDangNo = 0m, tongChoVayChuaThu = 0m;
                foreach (KhoanVayNoChiTiet ct in ds)
                {
                    if (!ct.HoanThanhTot)
                    {
                        if (ct.Khoan.LoaiKhoan == "Vay") tongDangNo += ct.ConLai;
                        else tongChoVayChuaThu += ct.ConLai;
                    }
                    // Vẫn hiện cả khoản hoàn thành (✓) — như UcTietKiem, ORDER BY TrangThai
                    // đưa đang xử lý lên trước, hoàn thành xuống dưới.
                    pnlDanhSach.Controls.Add(TaoThe(ct));
                }
                ChinhRongThe();
                pnlDanhSach.ResumeLayout();

                if (ds.Count == 0)
                {
                    lblTongKet.Text = "Chưa có khoản vay nào. Hãy thêm khoản vay đầu tiên.";
                }
                else
                {
                    lblTongKet.Text = "Đang nợ: " + UIHelper.FormatTien(tongDangNo) +
                        "  •  Cho vay chưa thu: " + UIHelper.FormatTien(tongChoVayChuaThu);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không tải được danh sách khoản vay. Hãy kiểm tra cấu hình LocalDB.",
                    "Lỗi truy cập dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Control TaoThe(KhoanVayNoChiTiet ct)
        {
            var the = new Panel();
            the.BackColor = Theme.White;
            the.Height = 148;
            the.Width = Math.Max(340, pnlDanhSach.ClientSize.Width - 28);
            the.Margin = new Padding(0, 0, 0, Theme.GapVua);
            the.Padding = new Padding(14, 10, 14, 8);

            bool laVayCuaToi = ct.Khoan.LoaiKhoan == "Vay";

            var lblTen = new Label();
            lblTen.Text = (laVayCuaToi ? "Nợ " : "Cho ") + ct.Khoan.TenNguoiLienQuan +
                (ct.Khoan.TrangThai == "HoanThanh" ? "  ✓" : "");
            lblTen.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            lblTen.ForeColor = ct.Khoan.TrangThai == "HoanThanh" ? Theme.Thu : Theme.Text;
            lblTen.AutoSize = true;
            lblTen.Location = new Point(14, 10);

            var lblChiTiet = new Label();
            string ngayDenHan = ct.Khoan.NgayDenHan.HasValue
                ? " • Hạn " + UIHelper.FormatNgay(ct.Khoan.NgayDenHan.Value) : "";
            lblChiTiet.Text = (laVayCuaToi
                    ? "Đã trả " + UIHelper.FormatTien(ct.DaTra)
                    : "Đã thu hồi " + UIHelper.FormatTien(ct.DaTra))
                + " / gốc " + UIHelper.FormatTien(ct.Khoan.SoTienGoc) +
                " • Còn " + UIHelper.FormatTien(ct.ConLai) +
                (laVayCuaToi && ct.TongLaiPhi > 0m
                    ? " • Lãi/phí đã trả " + UIHelper.FormatTien(ct.TongLaiPhi) : "") +
                ngayDenHan;
            lblChiTiet.Font = new Font("Segoe UI", 9f);
            lblChiTiet.ForeColor = Theme.TextSub;
            lblChiTiet.AutoSize = true;
            lblChiTiet.Location = new Point(14, 36);

            // Hàng nút ở đáy thẻ, căn phải (add TRƯỚC progress để không bị z-order che)
            var pnlNut = new FlowLayoutPanel();
            pnlNut.FlowDirection = FlowDirection.RightToLeft;
            pnlNut.Location = new Point(14, 94);
            pnlNut.Size = new Size(the.Width - 28, 34);
            pnlNut.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            pnlNut.BackColor = Theme.White;

            var btnGD = NutThe(laVayCuaToi ? "Trả nợ" : "Thu hồi", 100, Theme.Thu, Theme.White);
            btnGD.Click += (s, e) => GhiGD(ct, laVayCuaToi);
            var btnLichSu = NutThe("Lịch sử", 90, Theme.Background, Theme.Text);
            btnLichSu.Click += (s, e) => HienLichSu(ct);
            var btnSua = NutThe("Sửa", 80, Theme.Background, Theme.Text);
            btnSua.Click += (s, e) => SuaChon(ct);
            var btnXoa = NutThe("Xóa", 80, Theme.Background, Theme.Text);
            btnXoa.Click += (s, e) => XoaChon(ct);

            pnlNut.Controls.Add(btnXoa);
            pnlNut.Controls.Add(btnSua);
            pnlNut.Controls.Add(btnLichSu);
            pnlNut.Controls.Add(btnGD);

            // Thanh tiến độ: chỉ bar + tình trạng — đúng ngữ nghĩa từng loại khoản:
            // khoản nợ → "Đã trả", đạt đủ → "Trả đủ"; cho vay → "Đã thu hồi", đủ → "Thu hồi đủ".
            var tienDo = new UcProgress { HienMoTa = false };
            tienDo.Location = new Point(14, 56);
            tienDo.Width = Math.Max(200, the.Width - 28);
            tienDo.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            tienDo.CapNhatTienDo(ct.DaTra, ct.Khoan.SoTienGoc,
                laVayCuaToi ? "Đã trả" : "Đã thu hồi",
                laVayCuaToi ? "Trả đủ" : "Thu hồi đủ");

            the.Controls.Add(lblTen);
            the.Controls.Add(lblChiTiet);
            the.Controls.Add(tienDo);   // add sau pnlNut → nằm dưới, không che nút
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

        private void GhiGD(KhoanVayNoChiTiet ct, bool laVayCuaToi)
        {
            using (var frm = new FrmTraNo(ct, !laVayCuaToi))
            {
                if (frm.ShowDialog() != DialogResult.OK) return;

                string loi;
                int gdID = laVayCuaToi
                    ? VayNoService.TraNo(ct.Khoan.KhoanVayNoID, frm.KetQuaTienGoc, frm.KetQuaTienLai,
                        frm.KetQuaViID, frm.KetQuaNgay, frm.KetQuaGhiChu, out loi)
                    : VayNoService.ThuHoi(ct.Khoan.KhoanVayNoID, frm.KetQuaSoTien,
                        frm.KetQuaViID, frm.KetQuaNgay, frm.KetQuaGhiChu, out loi);

                if (gdID < 0)
                {
                    MessageBox.Show(loi, laVayCuaToi ? "Không trả nợ được" : "Không thu hồi được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TaiDanhSach();
                if (laVayCuaToi && ct.PhanTram >= 100m)
                {
                    MessageBox.Show("Khoản nợ \"" + ct.Khoan.TenNguoiLienQuan + "\" đã trả xong!",
                        "Xin chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void HienLichSu(KhoanVayNoChiTiet ct)
        {
            // Lịch sử trả nợ hiển thị dạng hộp thoại danh sách đơn giản (chỉ khoả khoản "Vay" có).
            List<LichSuTraNo> ds = VayNoService.LayLichSuTraNo(ct.Khoan.KhoanVayNoID);
            if (ds.Count == 0)
            {
                MessageBox.Show("Khoản \"" + ct.Khoan.TenNguoiLienQuan + "\" chưa có lịch sử trả nợ.",
                    "Lịch sử trả nợ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var sb = new System.Text.StringBuilder();
            foreach (LichSuTraNo l in ds)
            {
                sb.AppendLine(UIHelper.FormatNgay(l.NgayThanhToan) +
                    "  •  Gốc: " + UIHelper.FormatTien(l.TienGoc) +
                    "  •  Lãi/phí: " + UIHelper.FormatTien(l.TienLaiPhi) +
                    "  •  Tổng: " + UIHelper.FormatTien(l.TienGoc + l.TienLaiPhi) +
                    (string.IsNullOrEmpty(l.GhiChu) || l.GhiChu == null ? "" : "  — " + l.GhiChu));
            }
            MessageBox.Show(sb.ToString(), "Lịch sử trả nợ — " + ct.Khoan.TenNguoiLienQuan,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ThemMoi()
        {
            using (var frm = new FrmThemKhoanVay())
            {
                if (frm.ShowDialog() != DialogResult.OK) return;
                string loi;
                if (VayNoService.Them(frm.KetQua, frm.KetQuaViID,
                        frm.KetQua.LoaiKhoan == "Vay", out loi) < 0)
                {
                    MessageBox.Show(loi, "Không thêm được khoản",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TaiDanhSach();
            }
        }

        private void SuaChon(KhoanVayNoChiTiet ct)
        {
            using (var frm = new FrmThemKhoanVay(ct))
            {
                if (frm.ShowDialog() != DialogResult.OK) return;
                string loi;
                if (!VayNoService.Sua(frm.KetQua, out loi))
                {
                    MessageBox.Show(loi, "Không sửa được khoản",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TaiDanhSach();
            }
        }

        private void XoaChon(KhoanVayNoChiTiet ct)
        {
            if (MessageBox.Show("Xóa khoản \"" + ct.Khoan.TenNguoiLienQuan + "\"?\n" +
                    "Khoản còn giao dịch gắn vào sẽ bị chặn xóa (báo lỗi chi tiết).",
                "Xác nhận xóa khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            string loi;
            if (!VayNoService.Xoa(ct.Khoan.KhoanVayNoID, out loi))
            {
                MessageBox.Show(loi, "Không xóa được khoản",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TaiDanhSach();
        }
    }
}
