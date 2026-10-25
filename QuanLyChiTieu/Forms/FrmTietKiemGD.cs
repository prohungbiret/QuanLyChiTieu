using System;
using System.Windows.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    // TV1 (thay TV4) — form Đóng góp / Rút vào mục tiêu tiết kiệm, kế thừa FrmDialogNhap.
    // LoaiGiaoDich = 'DongGop' (tiền RA khỏi ví nguồn) hoặc 'RutTietKiem' (tiền VÀO ví đích)
    // — ngược chiều nhau, ComboBox của ví đổi theo loại, đúng Bảng 2.2e + CHECK 003.
    public class FrmTietKiemGD : FrmDialogNhap
    {
        private readonly bool laRut;
        private readonly ComboBox cboVi = new ComboBox();
        private readonly TextBox txtSoTien = new TextBox();
        private readonly DateTimePicker dtpNgay = new DateTimePicker();
        private readonly TextBox txtGhiChu = new TextBox();

        /// <summary>Sau khi OK: SoTien, ViID (nguồn nếu đóng góp / đích nếu rút), Ngay, GhiChu.</summary>
        public decimal KetQuaSoTien { get; private set; }
        public int KetQuaViID { get; private set; }
        public DateTime KetQuaNgay { get; private set; }
        public string KetQuaGhiChu { get; private set; }

        public FrmTietKiemGD(bool rut, string tenMucTieu)
            : base(rut ? "Rút tiết kiệm — " + tenMucTieu : "Đóng góp — " + tenMucTieu, 470, 320)
        {
            laRut = rut;

            cboVi.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (ViTien v in TietKiemService.LayDsViDangDung()) cboVi.Items.Add(v);
            cboVi.DisplayMember = "TenVi";
            cboVi.ValueMember = "ViTienID";
            if (cboVi.Items.Count > 0) cboVi.SelectedIndex = 0;

            dtpNgay.Format = DateTimePickerFormat.Short;
            dtpNgay.MaxDate = DateTime.Today;
            dtpNgay.Value = DateTime.Today;

            string nhanVi = laRut ? "Ví nhận tiền" : "Ví chi ra";
            string gocVi = laRut
                ? "Rút: tiền VÀO ví đích (CK_GD_TienVao)."
                : "Đóng góp: tiền RA khỏi ví nguồn (CK_GD_TienRa).";

            ThemTruong("Số tiền (đ)", txtSoTien, true);
            ThemTruong(nhanVi, cboVi, true);
            ThemTruong("Ngày", dtpNgay, true);
            ThemTruong("Ghi chú", txtGhiChu);
            ThemGhiChuChuThich(gocVi);
        }

        /// <summary>Dòng chữ diễn giải nhỏ bên dưới (giúp người dùng hiểu chiều tiền).</summary>
        private void ThemGhiChuChuThich(string noiDung)
        {
            var lblChuThich = new Label
            {
                Text = noiDung,
                AutoSize = true,
                ForeColor = Theme.TextSub,
                Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Italic)
            };
            ThemTruong("  ", lblChuThich);
        }

        protected override bool HopLe()
        {
            string loi;
            ViTien vi = cboVi.SelectedItem as ViTien;
            if (vi == null)
            {
                BaoLoi(cboVi, laRut
                    ? "Phải chọn ví nhận tiền. Nếu chưa có ví, hãy thêm ví ở phân hệ Ví tiền."
                    : "Phải chọn ví chi ra. Nếu chưa có ví, hãy thêm ví ở phân hệ Ví tiền.");
                return false;
            }

            decimal soTien;
            if (!ValidationHelper.SoTien(txtSoTien.Text, out soTien, out loi))
            {
                BaoLoi(txtSoTien, loi);
                return false;
            }

            if (dtpNgay.Value.Date > DateTime.Today)
            {
                BaoLoi(dtpNgay, "Ngày không được ở tương lai.");
                return false;
            }

            KetQuaSoTien = soTien;
            KetQuaViID = vi.ViTienID;
            KetQuaNgay = dtpNgay.Value.Date;
            KetQuaGhiChu = txtGhiChu.Text.Trim();
            return true;
        }
    }
}
