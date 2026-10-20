using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using QuanLyChiTieu.Data;

namespace QuanLyChiTieu.Services
{
    // TV1 sở hữu. Đặc tả: mục 4.9, quyết định QD08. Code chuẩn Phụ lục C.3.
    // Chỉ cung cấp API; FrmRegister (TV2) / FrmChangePassword (TV3) gọi các hàm này, không tự hash.
    public static class AuthService
    {
        private const int VongLap = 100000;
        private const int DoDaiSalt = 16;
        private const int DoDaiHash = 32;

        /// <summary>
        /// Băm mật khẩu: PBKDF2-HMACSHA256, 100.000 vòng, salt 16 byte ngẫu nhiên.
        /// Format lưu: PBKDF2.&lt;iterations&gt;.&lt;saltBase64&gt;.&lt;hashBase64&gt;
        /// </summary>
        public static string HashPassword(string matKhau)
        {
            byte[] salt = new byte[DoDaiSalt];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            byte[] hash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(
                matKhau, salt, VongLap, HashAlgorithmName.SHA256))
            {
                hash = pbkdf2.GetBytes(DoDaiHash);
            }
            return "PBKDF2." + VongLap + "."
                + Convert.ToBase64String(salt) + "."
                + Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Xác minh mật khẩu với chuỗi đã lưu. Chuỗi sai định dạng → coi như mật khẩu sai,
        /// không để exception bật ra (mục 4.9).
        /// </summary>
        public static bool VerifyPassword(string matKhau, string chuoiDaLuu)
        {
            string[] phan = (chuoiDaLuu ?? "").Split('.');
            if (phan.Length != 4 || phan[0] != "PBKDF2") return false;

            int vongLap;
            if (!int.TryParse(phan[1], out vongLap) || vongLap < 1) return false;

            byte[] salt;
            byte[] hashGoc;
            try
            {
                salt = Convert.FromBase64String(phan[2]);
                hashGoc = Convert.FromBase64String(phan[3]);
            }
            catch (FormatException)
            {
                return false;
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                matKhau, salt, vongLap, HashAlgorithmName.SHA256))
            {
                byte[] hashNhap = pbkdf2.GetBytes(DoDaiHash);
                return SoSanhByte(hashGoc, hashNhap); // so theo byte, không so chuỗi
            }
        }

        private static bool SoSanhByte(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length) return false;
            int lech = 0;
            for (int i = 0; i < a.Length; i++) lech |= a[i] ^ b[i];
            return lech == 0;
        }

        /// <summary>
        /// Đăng nhập bằng DB thật. Khi thành công đã gọi CurrentUser.SetUser.
        /// </summary>
        public static bool DangNhap(string tenDangNhap, string matKhau,
            out string thongBaoLoi)
        {
            thongBaoLoi = null;
            string sql = @"SELECT NguoiDungID, TenHienThi, MatKhauHash, TrangThai
                           FROM NguoiDung WHERE TenDangNhap = @TenDangNhap";

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar, 50)
                    .Value = tenDangNhap;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        thongBaoLoi = "Tên đăng nhập hoặc mật khẩu không đúng.";
                        return false;
                    }
                    bool hoatDong = reader.GetBoolean(reader.GetOrdinal("TrangThai"));
                    string hashLuu = reader.GetString(reader.GetOrdinal("MatKhauHash"));
                    if (!hoatDong || !VerifyPassword(matKhau, hashLuu))
                    {
                        thongBaoLoi = "Tên đăng nhập hoặc mật khẩu không đúng.";
                        return false;
                    }
                    CurrentUser.SetUser(reader.GetInt32(reader.GetOrdinal("NguoiDungID")),
                        reader.GetString(reader.GetOrdinal("TenHienThi")));
                    return true;
                }
            }
        }

        /// <summary>Kết thúc phiên: xóa thông tin user hiện tại.</summary>
        public static void DangXuat()
        {
            CurrentUser.Clear();
        }
    }
}
