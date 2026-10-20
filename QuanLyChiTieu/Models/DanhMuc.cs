// Model ánh xạ bảng DanhMuc (Tài liệu 02, mục 2.3)
namespace QuanLyChiTieu.Models
{
    public class DanhMuc
    {
        public int DanhMucID { get; set; }
        public int NguoiDungID { get; set; }
        public string TenDanhMuc { get; set; }
        public string LoaiDanhMuc { get; set; }   // 'Thu' | 'Chi'
        public string MoTa { get; set; }
        public bool TrangThai { get; set; }       // true = đang dùng (soft-delete)
    }
}
