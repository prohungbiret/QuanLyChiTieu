using System.Windows.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;

namespace QuanLyChiTieu.Forms
{
    // TV3 (Hải) sở hữu — form Thêm/Sửa danh mục (FR02), kế thừa FrmDialogNhap (hợp đồng 5.4):
    // DialogResult.OK chỉ khi HopLe() đúng; UcDanhMuc đọc DanhMucKetQua rồi gọi DanhMucService.
    public class FrmThemDanhMuc : FrmDialogNhap
    {
        private readonly TextBox txtTen = new TextBox();
        private readonly ComboBox cboLoai = new ComboBox();
        private readonly TextBox txtMoTa = new TextBox();
        private readonly int danhMucID; // 0 = thêm mới

        /// <summary>Dữ liệu đã nhập (chỉ đọc sau khi ShowDialog trả DialogResult.OK).</summary>
        public DanhMuc DanhMucKetQua { get; private set; }

        public FrmThemDanhMuc() : this(null) { }

        public FrmThemDanhMuc(DanhMuc sua) : base(sua == null ? "Thêm danh mục" : "Sửa danh mục", 460, 250)
        {
            danhMucID = sua == null ? 0 : sua.DanhMucID;

            cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoai.Items.Add("Thu");
            cboLoai.Items.Add("Chi");
            cboLoai.SelectedIndex = 0;

            txtMoTa.MaxLength = 255;

            ThemTruong("Tên danh mục", txtTen, true);
            ThemTruong("Loại danh mục", cboLoai, true);
            ThemTruong("Mô tả", txtMoTa);

            if (sua != null)
            {
                txtTen.Text = sua.TenDanhMuc;
                cboLoai.SelectedItem = sua.LoaiDanhMuc;
                txtMoTa.Text = sua.MoTa ?? "";
            }
        }

        protected override bool HopLe()
        {
            string loi;
            if (!ValidationHelper.BatBuoc(txtTen.Text, "Tên danh mục", out loi))
            {
                BaoLoi(txtTen, loi);
                return false;
            }
            if (cboLoai.SelectedItem == null)
            {
                BaoLoi(cboLoai, "Phải chọn loại danh mục Thu hoặc Chi.");
                return false;
            }

            DanhMucKetQua = new DanhMuc
            {
                DanhMucID = danhMucID,
                NguoiDungID = CurrentUser.NguoiDungID,
                TenDanhMuc = txtTen.Text.Trim(),
                LoaiDanhMuc = cboLoai.SelectedItem.ToString(),
                MoTa = txtMoTa.Text.Trim(),
                TrangThai = true
            };
            return true;
        }
    }
}
