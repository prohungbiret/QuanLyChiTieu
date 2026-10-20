// Model ánh xạ bảng KhoanVayNo (mục 2.3)
// LaiSuat quy ước: %/năm (12.0000 = 12%/năm), lãi đơn, NULL khi không lãi.
namespace QuanLyChiTieu.Models
{
    public class KhoanVayNo
    {
        public int KhoanVayNoID { get; set; }
        public int NguoiDungID { get; set; }
        public string LoaiKhoan { get; set; }     // Vay | ChoVay
        public string TenNguoiLienQuan { get; set; }
        public decimal SoTienGoc { get; set; }
        public System.DateTime NgayPhatSinh { get; set; }
        public System.DateTime? NgayDenHan { get; set; }
        public decimal? LaiSuat { get; set; }
        public string GhiChu { get; set; }
        public string TrangThai { get; set; }     // DangXuLy | HoanThanh
    }
}
