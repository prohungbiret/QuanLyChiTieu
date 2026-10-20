// Model ánh xạ bảng LichSuTraNo (mục 2.3) — v1 bị thiếu, bổ sung ở v2
// GiaoDichID BẮT BUỘC (QD04); TienGoc + TienLaiPhi = SoTien của giao dịch TraNo liên kết.
namespace QuanLyChiTieu.Models
{
    public class LichSuTraNo
    {
        public int LichSuTraNoID { get; set; }
        public int NguoiDungID { get; set; }
        public int KhoanVayNoID { get; set; }
        public int GiaoDichID { get; set; }
        public System.DateTime NgayThanhToan { get; set; }
        public decimal TienGoc { get; set; }
        public decimal TienLaiPhi { get; set; }
        public string GhiChu { get; set; }
    }
}
