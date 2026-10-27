using System.Configuration;
using System.Data.SqlClient;

namespace QuanLyChiTieu.Data
{
    // Mọi service chỉ được tạo kết nối qua đây 
    public static class Database
    {
        private const string TenKetNoi = "QuanLyChiTieuConnection";

        public static SqlConnection GetConnection()
        {
            string chuoi = ConfigurationManager
                .ConnectionStrings[TenKetNoi].ConnectionString;
            return new SqlConnection(chuoi);
        }
    }
}
