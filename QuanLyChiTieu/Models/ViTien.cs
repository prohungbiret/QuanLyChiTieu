// Model ánh xạ bảng ViTien (Tài liệu 02, mục 2.3)
// KHÔNG có cột số dư hiện tại — số dư luôn tính động theo công thức 2.6 (QD03).
namespace QuanLyChiTieu.Models
{
    public class ViTien
    {
        public int ViTienID { get; set; }
        public int NguoiDungID { get; set; }
        public string TenVi { get; set; }
        public string LoaiVi { get; set; }        // TienMat | NganHang | ViDienTu
        public decimal SoDuBanDau { get; set; }
        public bool TrangThai { get; set; }       // true = đang hoạt động
    }
}
