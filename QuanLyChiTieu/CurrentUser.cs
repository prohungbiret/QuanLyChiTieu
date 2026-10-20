namespace QuanLyChiTieu
{
    // Ứng dụng desktop nhỏ: class tĩnh là đủ (C.2).
    // KHÔNG thay thế việc kiểm tra quyền sở hữu dữ liệu trong truy vấn SQL (mục 2.8).
    public static class CurrentUser
    {
        public static int NguoiDungID { get; private set; }
        public static string TenHienThi { get; private set; }
        public static bool DaDangNhap { get { return NguoiDungID > 0; } }

        public static void SetUser(int nguoiDungId, string tenHienThi)
        {
            NguoiDungID = nguoiDungId;
            TenHienThi = tenHienThi;
        }

        public static void Clear()
        {
            NguoiDungID = 0;
            TenHienThi = null;
        }
    }
}
