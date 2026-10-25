using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    // TV1 (thay TV4) — form Trả nợ (khoản Vay) / Thu hồi (khoản ChoVay), kế thừa FrmDialogNhap.
    // Trả nợ: tách "Tiền gốc" + "Tiền lãi/phí" (tổng = SoTien GD — CHECK CK_LSTN_Tien),
    // VayNoService.TraNo ghi GD + LichSuTraNo trong MỘT transaction.
    // Thu hồi: một trường "Số tiền" (không có LichSuTraNo theo đặc tả).
    public class FrmTraNo : FrmDialogNhap
    {
        private readonly bool laThuHoi;
        private readonly KhoanVayNoChiTiet ct;
        private readonly ComboBox cboVi = new ComboBox();
        private readonly TextBox txtGoc = new TextBox();
        private readonly TextBox txtLai = new TextBox();
        private readonly DateTimePicker dtpNgay = new DateTimePicker();
        private readonly TextBox txtGhiChu = new TextBox();

        public decimal KetQuaTienGoc { get; private set; }
        public decimal KetQuaTienLai { get; private set; }
        public decimal KetQuaSoTien { get; private set; }
        public int KetQuaViID { get; private set; }
        public DateTime KetQuaNgay { get; private set; }
        public string KetQuaGhiChu { get; private set; }

        /// <summary>laThuHoi = true: khoản ChoVay (thu hồi); false: khoản Vay (trả nợ).</summary>
        public FrmTraNo(KhoanVayNoChiTiet chiTiet, bool thuHoiChuKhongTra)
            : base(thuHoiChuKhongTra
                ? "Thu hồi nợ — " + chiTiet.Khoan.TenNguoiLienQuan
                : "Trả nợ — " + chiTiet.Khoan.TenNguoiLienQuan, 480, 380)
        {
            laThuHoi = thuHoiChuKhongTra;
            ct = chiTiet;

            cboVi.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (ViTien v in TietKiemService.LayDsViDangDung()) cboVi.Items.Add(v);
            cboVi.DisplayMember = "TenVi";
            cboVi.ValueMember = "ViTienID";
            if (cboVi.Items.Count > 0) cboVi.SelectedIndex = 0;

            dtpNgay.Format = DateTimePickerFormat.Short;
            dtpNgay.MaxDate = DateTime.Today;
            dtpNgay.Value = DateTime.Today;

            if (laThuHoi)
            {
                ThemTruong("Số tiền thu hồi (đ)", txtGoc, true);
                ThemTruong("Ví nhận tiền", cboVi, true);
            }
            else
            {
                ThemTruong("Tiền gốc (đ)", txtGoc, true);
                ThemTruong("Tiền lãi/phí (đ)", txtLai, true);
                ThemTruong("Ví chi ra", cboVi, true);
            }
            ThemTruong("Ngày", dtpNgay, true);
            ThemTruong("Ghi chú", txtGhiChu);

            // Gợi ý: còn bao nhiêu
            var lblConLai = new Label
            {
                Text = "Còn nợ: " + UIHelper.FormatTien(ct.ConLai) +
                       " (gốc " + UIHelper.FormatTien(ct.Khoan.SoTienGoc) +
                       (laThuHoi ? "" : " — lãi/phí đã trả: " + UIHelper.FormatTien(ct.TongLaiPhi)) + ")",
                AutoSize = true,
                ForeColor = Theme.TextSub,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Italic)
            };
            ThemTruong("  ", lblConLai);

            // Gợi ý điền trước phần còn lại
            if (!laThuHoi && ct.ConLai > 0m)
                txtGoc.Text = ct.ConLai.ToString("#,0");
        }

        protected override bool HopLe()
        {
            string loi;
            ViTien vi = cboVi.SelectedItem as ViTien;
            if (vi == null)
            {
                BaoLoi(cboVi, laThuHoi
                    ? "Phải chọn ví nhận tiền thu hồi."
                    : "Phải chọn ví chi ra tiền trả nợ.");
                return false;
            }

            decimal goc;
            if (!ValidationHelper.SoTien(txtGoc.Text, out goc, out loi))
            {
                BaoLoi(txtGoc, loi.Replace("Số tiền", laThuHoi ? "Số tiền thu hồi" : "Tiền gốc"));
                return false;
            }

            decimal lai = 0m;
            if (!laThuHoi)
            {
                decimal laiTam;
                if (!decimal.TryParse(
                        string.IsNullOrWhiteSpace(txtLai.Text) ? "0" : txtLai.Text.Trim(),
                        System.Globalization.NumberStyles.Number,
                        System.Globalization.CultureInfo.GetCultureInfo("vi-VN"),
                        out laiTam))
                {
                    BaoLoi(txtLai, "Tiền lãi/phí phải là số không âm.");
                    return false;
                }
                lai = laiTam;
            }

            if (dtpNgay.Value.Date > DateTime.Today)
            {
                BaoLoi(dtpNgay, "Ngày không được ở tương lai.");
                return false;
            }

            KetQuaTienGoc = goc;
            KetQuaTienLai = lai;
            KetQuaSoTien = laThuHoi ? goc : goc + lai;
            KetQuaViID = vi.ViTienID;
            KetQuaNgay = dtpNgay.Value.Date;
            KetQuaGhiChu = txtGhiChu.Text.Trim();
            return true;
        }
    }
}
