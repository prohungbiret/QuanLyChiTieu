using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;

namespace QuanLyChiTieu.Forms
{
    // TV3 (Hải) sở hữu — form Thêm/Sửa ngân sách (FR08), kế thừa FrmDialogNhap (hợp đồng 5.4).
    // Chỉ liệt kê danh mục loại Chi đang sử dụng (Bảng 2.4.4: ngân sách chỉ cho danh mục Chi).
    public class FrmThemNganSach : FrmDialogNhap
    {
        private readonly ComboBox cboDanhMuc = new ComboBox();
        private readonly NumericUpDown nudThang = new NumericUpDown();
        private readonly NumericUpDown nudNam = new NumericUpDown();
        private readonly TextBox txtHanMuc = new TextBox();
        private readonly int nganSachID; // 0 = thêm mới

        /// <summary>Dữ liệu đã nhập (chỉ đọc sau khi ShowDialog trả DialogResult.OK).</summary>
        public NganSach NganSachKetQua { get; private set; }

        public FrmThemNganSach(int thang, int nam) : this(null, thang, nam) { }

        public FrmThemNganSach(NganSach sua, int thangMacDinh, int namMacDinh)
            : base(sua == null ? "Thêm ngân sách" : "Sửa ngân sách", 470, 300)
        {
            nganSachID = sua == null ? 0 : sua.NganSachID;

            cboDanhMuc.DropDownStyle = ComboBoxStyle.DropDownList;
            List<DanhMuc> dsChi = DanhMucService.LayTheoLoai(DanhMucService.LoaiChi, true);
            foreach (DanhMuc dm in dsChi) cboDanhMuc.Items.Add(dm);
            cboDanhMuc.DisplayMember = "TenDanhMuc";
            cboDanhMuc.ValueMember = "DanhMucID";
            if (cboDanhMuc.Items.Count > 0) cboDanhMuc.SelectedIndex = 0;

            nudThang.Minimum = 1;
            nudThang.Maximum = 12;
            nudThang.Value = sua != null ? sua.Thang : thangMacDinh;

            nudNam.Minimum = 1900;
            nudNam.Maximum = 2100;
            nudNam.Value = sua != null ? sua.Nam : namMacDinh;

            ThemTruong("Danh mục Chi", cboDanhMuc, true);
            ThemTruong("Tháng", nudThang, true);
            ThemTruong("Năm", nudNam, true);
            ThemTruong("Hạn mức (đ)", txtHanMuc, true);

            if (sua != null)
            {
                foreach (object item in cboDanhMuc.Items)
                {
                    DanhMuc dm = item as DanhMuc;
                    if (dm != null && dm.DanhMucID == sua.DanhMucID)
                    {
                        cboDanhMuc.SelectedItem = item;
                        break;
                    }
                }
                txtHanMuc.Text = sua.HanMuc.ToString("#,0");
            }
        }

        protected override bool HopLe()
        {
            DanhMuc dm = cboDanhMuc.SelectedItem as DanhMuc;
            if (dm == null)
            {
                BaoLoi(cboDanhMuc, "Phải chọn một danh mục loại Chi đang sử dụng. " +
                    "Nếu chưa có, hãy tạo danh mục Chi trước.");
                return false;
            }

            decimal hanMuc;
            string loi;
            if (!ValidationHelper.SoTien(txtHanMuc.Text, out hanMuc, out loi))
            {
                BaoLoi(txtHanMuc, loi.Replace("Số tiền", "Hạn mức"));
                return false;
            }

            NganSachKetQua = new NganSach
            {
                NganSachID = nganSachID,
                NguoiDungID = CurrentUser.NguoiDungID,
                DanhMucID = dm.DanhMucID,
                Thang = (int)nudThang.Value,
                Nam = (int)nudNam.Value,
                HanMuc = hanMuc
            };
            return true;
        }
    }
}
