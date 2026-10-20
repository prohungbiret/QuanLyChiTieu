using System.Configuration;
using System.Data.SqlClient;

namespace QuanLyChiTieu.Data
{
    // TV1 sở hữu. Mọi service chỉ được tạo kết nối qua đây (mục 4.6, C.1).
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
