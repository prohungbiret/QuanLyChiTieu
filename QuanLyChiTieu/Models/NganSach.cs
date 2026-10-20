// Model ánh xạ bảng NganSach (mục 2.3) — hạn mức theo (danh mục Chi, tháng)
namespace QuanLyChiTieu.Models
{
    public class NganSach
    {
        public int NganSachID { get; set; }
        public int NguoiDungID { get; set; }
        public int DanhMucID { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public decimal HanMuc { get; set; }
    }
}
