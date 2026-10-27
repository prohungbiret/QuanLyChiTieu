namespace QuanLyChiTieu
{
   
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
