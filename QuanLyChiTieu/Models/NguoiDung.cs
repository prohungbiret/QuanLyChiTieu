// Model ánh xạ bảng NguoiDung (Tài liệu 02, mục 2.3)
namespace QuanLyChiTieu.Models
{
    public class NguoiDung
    {
        public int NguoiDungID { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhauHash { get; set; }
        public string TenHienThi { get; set; }
        public string Email { get; set; }
        public System.DateTime NgayTao { get; set; }
        public bool TrangThai { get; set; }
    }
}
