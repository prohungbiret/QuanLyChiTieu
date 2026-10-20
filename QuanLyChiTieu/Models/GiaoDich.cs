// Model ánh xạ bảng GiaoDich — bảng trung tâm, 9 loại (QD02, Bảng 2.2e)
// Quy ước: tiền vào = ViDichID, tiền ra = ViNguonID.
namespace QuanLyChiTieu.Models
{
    public class GiaoDich
    {
        public int GiaoDichID { get; set; }
        public int NguoiDungID { get; set; }
        public string LoaiGiaoDich { get; set; }
        public decimal SoTien { get; set; }
        public System.DateTime NgayGiaoDich { get; set; }
        public int? DanhMucID { get; set; }
        public int? ViNguonID { get; set; }
        public int? ViDichID { get; set; }
        public int? KhoanVayNoID { get; set; }
        public int? MucTieuID { get; set; }
        public string GhiChu { get; set; }
        public System.DateTime NgayTao { get; set; }
    }
}
