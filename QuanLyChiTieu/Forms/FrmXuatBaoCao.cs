using System;
using System.Windows.Forms;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    // TV1 (thay TV4) — hộp thoại chọn kỳ + phạm vi xuất báo cáo Excel (FR phần Báo cáo).
    // Chỉ thu thập tham số; việc đọc số liệu + ghi file do BaoCaoService thực hiện,
    // SaveFileDialog do phía gọi (UcThongKe) mở sau khi DialogResult.OK.
    public class FrmXuatBaoCao : FrmDialogNhap
    {
        private readonly DateTimePicker dtpTu = new DateTimePicker();
        private readonly DateTimePicker dtpDen = new DateTimePicker();
        private readonly System.Windows.Forms.RadioButton rdoThuChi = new System.Windows.Forms.RadioButton();
        private readonly System.Windows.Forms.RadioButton rdoTatCa = new System.Windows.Forms.RadioButton();

        public BaoCaoYeuCau KetQua { get; private set; }

        public FrmXuatBaoCao(DateTime tuMacDinh, DateTime denMacDinh)
            : base("Xuất báo cáo Excel", 460, 300)
        {
            dtpTu.Format = DateTimePickerFormat.Short;
            dtpTu.MaxDate = DateTime.Today;
            dtpTu.Value = tuMacDinh > DateTime.Today ? DateTime.Today : tuMacDinh;

            dtpDen.Format = DateTimePickerFormat.Short;
            dtpDen.MaxDate = DateTime.Today;
            dtpDen.Value = denMacDinh > DateTime.Today ? DateTime.Today : denMacDinh;

            rdoThuChi.Text = "Chỉ thu/chi (mặc định báo cáo — QD02)";
            rdoThuChi.AutoSize = true;
            rdoThuChi.Checked = true;
            rdoThuChi.ForeColor = Theme.TextSub;

            rdoTatCa.Text = "Tất cả loại giao dịch (9 loại — Bảng 2.2e)";
            rdoTatCa.AutoSize = true;
            rdoTatCa.ForeColor = Theme.TextSub;

            var pnlPhamVi = new System.Windows.Forms.Panel { Height = 56 };
            rdoThuChi.Location = new System.Drawing.Point(0, 0);
            rdoTatCa.Location = new System.Drawing.Point(0, 26);
            pnlPhamVi.Controls.Add(rdoThuChi);
            pnlPhamVi.Controls.Add(rdoTatCa);

            ThemTruong("Từ ngày", dtpTu, true);
            ThemTruong("Đến ngày", dtpDen, true);
            ThemTruong("Phạm vi", pnlPhamVi);
        }

        protected override bool HopLe()
        {
            string loi;
            if (!ValidationHelper.KhoangNgay(dtpTu.Value, dtpDen.Value, out loi))
            {
                BaoLoi(dtpDen, loi);
                return false;
            }

            KetQua = new BaoCaoYeuCau
            {
                Tu = dtpTu.Value.Date,
                Den = dtpDen.Value.Date,
                PhamVi = rdoTatCa.Checked ? "TatCa" : "ThuChi"
            };
            return true;
        }
    }
}
