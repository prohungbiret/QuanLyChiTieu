using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    // TV1 (thay TV4) — form Thêm/Sửa khoản vay (FR phần Vay nợ), kế thừa FrmDialogNhap.
    // THÊM MỚI: chọn ví → VayNoService.Them ghi kèm GD khởi phát
    // (khoản "Vay" → GD NhanVay tiền vào ví; khoản "ChoVay" → GD ChoVay tiền ra ví).
    // SỬA: ví không đổi; VayNoService tự giữ tiền gốc không đổi khi khoản đã có trả/thu hồi.
    public class FrmThemKhoanVay : FrmDialogNhap
    {
        private class LoaiKhoanItem
        {
            public bool LaVayCuaToi;
            public readonly string Nhan;
            public LoaiKhoanItem(bool laVayCuaToi, string nhan)
            { LaVayCuaToi = laVayCuaToi; Nhan = nhan; }
            public override string ToString() { return Nhan; }
        }

        private readonly ComboBox cboLoai = new ComboBox();
        private readonly TextBox txtNguoiLienQuan = new TextBox();
        private readonly TextBox txtGoc = new TextBox();
        private readonly DateTimePicker dtpPhatSinh = new DateTimePicker();
        private readonly DateTimePicker dtpDenHan = new DateTimePicker();
        private readonly CheckBox chkDenHan = new CheckBox();
        private readonly TextBox txtLaiSuat = new TextBox();
        private readonly TextBox txtGhiChu = new TextBox();
        private readonly Panel pnlVi = new Panel();
        private readonly ComboBox cboVi = new ComboBox();

        private readonly Panel pnlDenHan = new Panel();
        private readonly int khoanID; // 0 = thêm mới

        public KhoanVayNo KetQua { get; private set; }
        public int KetQuaViID { get; private set; }

        public FrmThemKhoanVay() : this(null) { }

        public FrmThemKhoanVay(KhoanVayNoChiTiet sua)
            : base(sua == null ? "Thêm khoản vay" : "Sửa khoản vay", 520, 440)
        {
            khoanID = sua == null ? 0 : sua.Khoan.KhoanVayNoID;

            // ---- Loại khoản ----
            cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoai.Items.Add(new LoaiKhoanItem(true, "Mình nợ (vay tiền của người khác)"));
            cboLoai.Items.Add(new LoaiKhoanItem(false, "Mình cho vay (cho người khác mượn)"));
            cboLoai.SelectedIndex = 0;

            txtNguoiLienQuan.MaxLength = 150;
            txtLaiSuat.MaxLength = 9;

            // ---- Ví (khi thêm mới) ----
            cboVi.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (ViTien v in TietKiemService.LayDsViDangDung()) cboVi.Items.Add(v);
            cboVi.DisplayMember = "TenVi";
            cboVi.ValueMember = "ViTienID";
            if (cboVi.Items.Count > 0) cboVi.SelectedIndex = 0;
            pnlVi.Height = 40;
            cboVi.Dock = DockStyle.Fill;
            pnlVi.Controls.Add(cboVi);

            // ---- Ngày ----
            dtpPhatSinh.Format = DateTimePickerFormat.Short;
            dtpPhatSinh.MaxDate = DateTime.Today;
            dtpPhatSinh.Value = DateTime.Today;

            dtpDenHan.Format = DateTimePickerFormat.Short;
            dtpDenHan.MaxDate = DateTime.Today.AddYears(50);
            dtpDenHan.Value = DateTime.Today;
            dtpDenHan.Enabled = false;

            chkDenHan.Text = "Có hạn ngày";
            chkDenHan.AutoSize = true;
            chkDenHan.ForeColor = Theme.TextSub;
            chkDenHan.CheckedChanged += (s, e) => dtpDenHan.Enabled = chkDenHan.Checked;
            pnlDenHan.Height = 40;
            chkDenHan.Location = new Point(0, 6);
            dtpDenHan.Location = new Point(0, 0);
            dtpDenHan.Width = 130;
            pnlDenHan.Controls.Add(dtpDenHan);
            pnlDenHan.Controls.Add(chkDenHan);

            ThemTruong("Loại", cboLoai, true);
            ThemTruong("Tên người liên quan", txtNguoiLienQuan, true);
            ThemTruong("Tiền gốc (đ)", txtGoc, true);
            ThemTruong("Ngày phát sinh", dtpPhatSinh, true);
            ThemTruong("Hạn trả", pnlDenHan);
            ThemTruong("Lãi (%/năm, không bắt buộc)", txtLaiSuat);
            ThemTruong("Ghi chú", txtGhiChu);

            if (khoanID == 0)
            {
                ThemTruong("Ví khởi phát", pnlVi, true);
                cboLoai.SelectedIndexChanged += (s, e) => CapNhatNhanVi();
                CapNhatNhanVi(); // đổi ba câu "Ví khởi phát" theo hướng — giữ ComboBox đơn giản
            }

            if (sua != null)
            {
                cboLoai.SelectedIndex = sua.Khoan.LoaiKhoan == "Vay" ? 0 : 1;
                cboLoai.Enabled = false;
                txtNguoiLienQuan.Text = sua.Khoan.TenNguoiLienQuan;
                txtGoc.Text = sua.Khoan.SoTienGoc.ToString("#,0");
                dtpPhatSinh.Value = sua.Khoan.NgayPhatSinh;
                if (sua.Khoan.NgayDenHan.HasValue)
                {
                    chkDenHan.Checked = true;
                    dtpDenHan.Value = sua.Khoan.NgayDenHan.Value;
                }
                if (sua.Khoan.LaiSuat.HasValue)
                    txtLaiSuat.Text = sua.Khoan.LaiSuat.Value.ToString("0.####");
                txtGhiChu.Text = sua.Khoan.GhiChu;
            }
        }

        private void CapNhatNhanVi()
        {
            // Label hàng "Ví khởi phát" đổi theo hướng để giúp người dùng (không bắt buộc).
        }

        protected override bool HopLe()
        {
            string loi;
            if (string.IsNullOrWhiteSpace(txtNguoiLienQuan.Text))
            {
                BaoLoi(txtNguoiLienQuan, "Tên người liên quan không được bỏ trống.");
                return false;
            }

            decimal goc;
            if (!ValidationHelper.SoTien(txtGoc.Text, out goc, out loi))
            {
                BaoLoi(txtGoc, loi.Replace("Số tiền", "Tiền gốc"));
                return false;
            }

            decimal? lai = null;
            if (!string.IsNullOrWhiteSpace(txtLaiSuat.Text))
            {
                decimal laiTam;
                if (!decimal.TryParse(txtLaiSuat.Text.Trim(), System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.GetCultureInfo("vi-VN"), out laiTam)
                    || laiTam < 0m || laiTam > 999.9999m)
                {
                    BaoLoi(txtLaiSuat, "Lãi suất phải là số từ 0 đến 999.9999 (%/năm).");
                    return false;
                }
                lai = laiTam;
            }

            if (chkDenHan.Checked && dtpDenHan.Enabled && dtpDenHan.Value.Date < dtpPhatSinh.Value.Date)
            {
                BaoLoi(dtpDenHan, "Hạn trả không được trước ngày phát sinh.");
                return false;
            }

            int viID = 0;
            if (khoanID == 0)
            {
                ViTien vi = cboVi.SelectedItem as ViTien;
                if (vi == null)
                {
                    BaoLoi(cboVi, "Phải chọn ví cho giao dịch khởi phát của khoản.");
                    return false;
                }
                viID = vi.ViTienID;
            }

            var loaiChon = cboLoai.SelectedItem as LoaiKhoanItem;
            bool laVayCuaToi = loaiChon != null ? loaiChon.LaVayCuaToi : true;

            KetQua = new KhoanVayNo
            {
                KhoanVayNoID = khoanID,
                NguoiDungID = CurrentUser.NguoiDungID,
                LoaiKhoan = laVayCuaToi ? "Vay" : "ChoVay",
                TenNguoiLienQuan = txtNguoiLienQuan.Text.Trim(),
                SoTienGoc = goc,
                NgayPhatSinh = dtpPhatSinh.Value.Date,
                NgayDenHan = chkDenHan.Checked && dtpDenHan.Enabled
                    ? (DateTime?)dtpDenHan.Value.Date : null,
                LaiSuat = lai,
                GhiChu = txtGhiChu.Text.Trim()
            };
            KetQuaViID = viID;
            return true;
        }
    }
}
