// TV tạo chuẩn chung — hằng số loại giao dịch dùng cho TOÀN dự án (mục 4.5, QD02).
// Cấm đặt chuỗi loại giao dịch rải rác trong code; luôn tham chiếu lớp này.
namespace QuanLyChiTieu.Models
{
    public static class GiaoDichLoai
    {
        public const string Thu = "Thu";
        public const string Chi = "Chi";
        public const string ChuyenTien = "ChuyenTien";
        public const string NhanVay = "NhanVay";
        public const string TraNo = "TraNo";
        public const string ChoVay = "ChoVay";
        public const string ThuHoiNo = "ThuHoiNo";
        public const string DongGop = "DongGop";
        public const string RutTietKiem = "RutTietKiem";

        // Chỉ hai loại này được tính vào báo cáo thu chi (QD02, Bảng 2.2e)
        public static readonly string[] TinhVaoBaoCao = { Thu, Chi };

        // Các loại tiền VÀO ví (ViDichID) / tiền RA khỏi ví (ViNguonID) — Bảng 2.2e
        public static readonly string[] TienVao = { Thu, NhanVay, ThuHoiNo, RutTietKiem };
        public static readonly string[] TienRa = { Chi, TraNo, ChoVay, DongGop };
    }
}
