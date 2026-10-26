using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Forms;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Controls
{
    public partial class UcNganSach : UserControl
    {
        public UcNganSach()
        {
            InitializeComponent();
            Tag = "Ngân sách";
            BackColor = Theme.Background;
            Font = new Font("Segoe UI", 9.5f);

            nudThang.Value = DateTime.Now.Month;
            nudNam.Value = DateTime.Now.Year;

            // Áp theme
            pnlTools.BackColor = Theme.Background;
            lblThang.ForeColor = Theme.TextSub;
            lblNam.ForeColor = Theme.TextSub;
            nudThang.Size = new Size(64, Theme.CaoControl);
            nudNam.Size = new Size(86, Theme.CaoControl);
            btnThem.BackColor = Theme.Primary;
            btnThem.ForeColor = Theme.White;
            btnThem.Size = new Size(150, Theme.CaoNut);
            lblTongKet.ForeColor = Theme.TextSub;
            pnlDanhSach.BackColor = Theme.Background;

            pnlDanhSach.Resize += (s, e) => ChinhRongThe();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            TaiDanhSach();
        }

        private void nudThang_ValueChanged(object sender, EventArgs e)
        {
            TaiDanhSach();
        }

        private void nudNam_ValueChanged(object sender, EventArgs e)
        {
            TaiDanhSach();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ThemMoi();
        }

        private void ChinhRongThe()
        {
            int rong = pnlDanhSach.ClientSize.Width - 28;
            if (rong < 300) rong = 300;
            foreach (Control c in pnlDanhSach.Controls) c.Width = rong;
        }

        private void TaiDanhSach()
        {
            try
            {
                int thang = (int)nudThang.Value;
                int nam = (int)nudNam.Value;
                var ds = NganSachService.LayTheoThang(thang, nam);

                pnlDanhSach.SuspendLayout();
                pnlDanhSach.Controls.Clear();

                decimal tongHanMuc = 0m, tongDaChi = 0m;
                int soCanhBao = 0;

                foreach (NganSachChiTiet ct in ds)
                {
                    tongHanMuc += ct.NganSach.HanMuc;
                    tongDaChi += ct.DaChi;
                    if (ct.PhanTram >= 80m) soCanhBao++;
                    pnlDanhSach.Controls.Add(TaoThe(ct));
                }
                ChinhRongThe();
                pnlDanhSach.ResumeLayout();

                if (ds.Count == 0)
                {
                    lblTongKet.Text = "Tháng " + thang + "/" + nam + " chưa có ngân sách nào. " +
                        "Hãy thêm ngân sách cho một danh mục Chi.";
                }
                else
                {
                    lblTongKet.Text = "Tổng hạn mức: " + UIHelper.FormatTien(tongHanMuc) +
                        "  •  Đã chi: " + UIHelper.FormatTien(tongDaChi) +
                        "  •  Còn lại: " + UIHelper.FormatTien(tongHanMuc - tongDaChi) +
                        (soCanhBao > 0 ? "  •  " + soCanhBao + " ngân sách đang cảnh báo (>= 80%)" : "");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không tải được danh sách ngân sách. Hãy kiểm tra cấu hình LocalDB.",
                    "Lỗi truy cập dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Control TaoThe(NganSachChiTiet ct)
        {
            var the = new Panel();
            the.BackColor = Theme.White;
            the.Height = 118;
            the.Width = Math.Max(300, pnlDanhSach.ClientSize.Width - 28);
            the.Margin = new Padding(0, 0, 0, Theme.GapVua);
            the.Padding = new Padding(14, 10, 14, 8);

            var lblTen = new Label();
            lblTen.Text = ct.TenDanhMuc;
            lblTen.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            lblTen.ForeColor = Theme.Text;
            lblTen.AutoSize = true;
            lblTen.Location = new Point(14, 10);

            var lblSoTien = new Label();
            lblSoTien.Text = "Đã chi " + UIHelper.FormatTien(ct.DaChi) + " / " + UIHelper.FormatTien(ct.NganSach.HanMuc);
            lblSoTien.Font = new Font("Segoe UI", 9f);
            lblSoTien.ForeColor = Theme.TextSub;
            lblSoTien.AutoSize = true;
            lblSoTien.Location = new Point(14, 36);

            var tienDo = new UcProgress();
            tienDo.Location = new Point(14, 58);
            tienDo.Width = Math.Max(200, the.Width - 220);
            tienDo.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            tienDo.CapNhat(ct.DaChi, ct.NganSach.HanMuc);

            var btnSua = NutThe("Sửa", the.Width - 148);
            btnSua.Click += (s, e) => SuaChon(ct);
            var btnXoa = NutThe("Xóa", the.Width - 78);
            btnXoa.Click += (s, e) => XoaChon(ct);

            the.Controls.Add(lblTen);
            the.Controls.Add(lblSoTien);
            the.Controls.Add(tienDo);
            the.Controls.Add(btnSua);
            the.Controls.Add(btnXoa);
            return the;
        }

        private static Button NutThe(string text, int x)
        {
            var btn = new Button();
            btn.Text = text;
            btn.Size = new Size(64, 32);
            btn.Location = new Point(x, 10);
            btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Theme.Background;
            btn.ForeColor = Theme.Text;
            return btn;
        }

        private void ThemMoi()
        {
            using (var frm = new FrmThemNganSach((int)nudThang.Value, (int)nudNam.Value))
            {
                if (frm.ShowDialog() != DialogResult.OK) return;
                string loi;
                if (NganSachService.Them(frm.NganSachKetQua, out loi) < 0)
                {
                    MessageBox.Show(loi, "Không thêm được ngân sách",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                nudThang.Value = frm.NganSachKetQua.Thang;
                nudNam.Value = frm.NganSachKetQua.Nam;
                TaiDanhSach();
                CanhBaoNeuVuot(frm.NganSachKetQua);
            }
        }

        private void SuaChon(NganSachChiTiet ct)
        {
            using (var frm = new FrmThemNganSach(ct.NganSach, (int)nudThang.Value, (int)nudNam.Value))
            {
                if (frm.ShowDialog() != DialogResult.OK) return;
                string loi;
                if (!NganSachService.Sua(frm.NganSachKetQua, out loi))
                {
                    MessageBox.Show(loi, "Không sửa được ngân sách",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                nudThang.Value = frm.NganSachKetQua.Thang;
                nudNam.Value = frm.NganSachKetQua.Nam;
                TaiDanhSach();
                CanhBaoNeuVuot(frm.NganSachKetQua);
            }
        }

        private void XoaChon(NganSachChiTiet ct)
        {
            string thongDiep = "Xóa ngân sách \"" + ct.TenDanhMuc + "\" tháng " +
                ct.NganSach.Thang + "/" + ct.NganSach.Nam + "?\n" +
                "Chỉ xóa bản ghi hạn mức, giao dịch chi vẫn giữ nguyên.";
            if (MessageBox.Show(thongDiep, "Xác nhận xóa ngân sách",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            string loi;
            if (!NganSachService.Xoa(ct.NganSach.NganSachID, out loi))
            {
                MessageBox.Show(loi, "Không xóa được ngân sách",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TaiDanhSach();
        }

        private void CanhBaoNeuVuot(Models.NganSach ns)
        {
            decimal daChi = NganSachService.TinhDaChi(ns.DanhMucID, ns.Thang, ns.Nam);
            if (ns.HanMuc <= 0) return;
            decimal pt = daChi * 100m / ns.HanMuc;
            if (pt >= 100m)
            {
                MessageBox.Show("Ngân sách tháng " + ns.Thang + "/" + ns.Nam + " đã " +
                    (pt > 100m ? "VƯỢT" : "đạt") + " hạn mức: đã chi " + UIHelper.FormatTien(daChi) +
                    " / " + UIHelper.FormatTien(ns.HanMuc) + " (" + pt.ToString("0.##") + "%).",
                    "Cảnh báo ngân sách", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (pt >= 80m)
            {
                MessageBox.Show("Ngân sách tháng " + ns.Thang + "/" + ns.Nam +
                    " đã dùng " + pt.ToString("0.##") + "% hạn mức (đã chi " +
                    UIHelper.FormatTien(daChi) + " / " + UIHelper.FormatTien(ns.HanMuc) + ").",
                    "Cảnh báo ngân sách 80%", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}