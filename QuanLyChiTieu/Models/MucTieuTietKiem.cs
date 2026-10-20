// Model ánh xạ bảng MucTieuTietKiem (mục 2.3)
// Đã BỎ SoTienDaDat (QD05) — tiến độ luôn tính từ giao dịch DongGop/RutTietKiem.
namespace QuanLyChiTieu.Models
{
    public class MucTieuTietKiem
    {
        public int MucTieuID { get; set; }
        public int NguoiDungID { get; set; }
        public string TenMucTieu { get; set; }
        public decimal SoTienMucTieu { get; set; }
        public System.DateTime NgayBatDau { get; set; }
        public System.DateTime? NgayKetThuc { get; set; }
        public string TrangThai { get; set; }     // DangThucHien | HoanThanh | TamDung
    }
}
