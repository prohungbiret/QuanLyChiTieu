using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    // TV1 (thay TV4) — form Thêm/Sửa mục tiêu tiết kiệm, kế thừa FrmDialogNhap (hợp đồng 5.4).
    // Ngày kết thúc là TÙY CHỌN (schema cho NULL).
    public class FrmThemMucTieu : FrmDialogNhap
    {
        private readonly TextBox txtTen = new TextBox();
        private readonly TextBox txtSoTien = new TextBox();
        private readonly DateTimePicker dtpBatDau = new DateTimePicker();
        private readonly DateTimePicker dtpKetThuc = new DateTimePicker();
        private readonly CheckBox chkKetThuc = new CheckBox();

        private readonly int mucTieuID; // 0 = thêm mới

        /// <summary>Dữ liệu đã nhập (chỉ đọc sau khi ShowDialog trả DialogResult.OK).</summary>
        public MucTieuTietKiem KetQua { get; private set; }

        public FrmThemMucTieu() : this(null) { }

        public FrmThemMucTieu(MucTieuTietKiem sua)
            : base(sua == null ? "Thêm mục tiêu tiết kiệm" : "Sửa mục tiêu tiết kiệm", 480, 340)
        {
            mucTieuID = sua == null ? 0 : sua.MucTieuID;

            dtpBatDau.Format = DateTimePickerFormat.Short;
            dtpBatDau.MaxDate = DateTime.Today;
            dtpBatDau.Value = sua != null ? sua.NgayBatDau : DateTime.Today;

            dtpKetThuc.Format = DateTimePickerFormat.Short;
            dtpKetThuc.MaxDate = DateTime.Today.AddYears(50);
            dtpKetThuc.Value = DateTime.Today;

            chkKetThuc.Text = "Có ngày kết thúc";
            chkKetThuc.AutoSize = true;
            chkKetThuc.ForeColor = Theme.TextSub;
            chkKetThuc.Checked = sua != null && sua.NgayKetThuc.HasValue;
            chkKetThuc.CheckedChanged += (s, e) => dtpKetThuc.Enabled = chkKetThuc.Checked;
            dtpKetThuc.Enabled = chkKetThuc.Checked;
            if (sua != null && sua.NgayKetThuc.HasValue) dtpKetThuc.Value = sua.NgayKetThuc.Value;

            ThemTruong("Tên mục tiêu", txtTen, true);
            ThemTruong("Số tiền mục tiêu (đ)", txtSoTien, true);
            ThemTruong("Ngày bắt đầu", dtpBatDau, true);

            // 1 trường gồm 2 control (checkbox + ngày) — gói chung 1 Panel
            pnlKetThuc.Height = 40;
            chkKetThuc.Location = new Point(0, 6);
            dtpKetThuc.Location = new Point(170, 2);
            dtpKetThuc.Width = 130;
            pnlKetThuc.Controls.Add(chkKetThuc);
            pnlKetThuc.Controls.Add(dtpKetThuc);
            ThemTruong("Ngày kết thúc", pnlKetThuc);

            if (sua != null)
            {
                txtTen.Text = sua.TenMucTieu;
                txtSoTien.Text = sua.SoTienMucTieu.ToString("#,0");
            }
        }

        private readonly Panel pnlKetThuc = new Panel();

        protected override bool HopLe()
        {
            string loi;
            if (!ValidationHelper.BatBuoc(txtTen.Text, "Tên mục tiêu", out loi))
            {
                BaoLoi(txtTen, loi);
                return false;
            }
            if (txtTen.Text.Trim().Length > 150)
            {
                BaoLoi(txtTen, "Tên mục tiêu tối đa 150 ký tự.");
                return false;
            }

            decimal soTien;
            if (!ValidationHelper.SoTien(txtSoTien.Text, out soTien, out loi))
            {
                BaoLoi(txtSoTien, loi.Replace("Số tiền", "Số tiền mục tiêu"));
                return false;
            }

            if (chkKetThuc.Checked && dtpKetThuc.Enabled &&
                dtpKetThuc.Value.Date < dtpBatDau.Value.Date)
            {
                BaoLoi(dtpKetThuc, "Ngày kết thúc không được trước ngày bắt đầu.");
                return false;
            }

            KetQua = new MucTieuTietKiem
            {
                MucTieuID = mucTieuID,
                NguoiDungID = CurrentUser.NguoiDungID,
                TenMucTieu = txtTen.Text.Trim(),
                SoTienMucTieu = soTien,
                NgayBatDau = dtpBatDau.Value.Date,
                NgayKetThuc = chkKetThuc.Checked
                    ? (DateTime?)dtpKetThuc.Value.Date : null,
                TrangThai = "DangThucHien"
            };
            return true;
        }
    }
}
