using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyChiTieu.Data;
using QuanLyChiTieu.Models;

namespace QuanLyChiTieu.Services
{
    // TV3 (Hải) sở hữu — phân hệ Danh mục (FR02, Tài liệu 06 mục 6.4).
    // Quy tắc:
    //  - Mọi truy vấn đều gắn NguoiDungID = CurrentUser (mục 2.8), SQL tham số (NFR03).
    //  - Danh mục KHÔNG xóa cứng: ngừng sử dụng = TrangThai 0 (mục 2.1.4, FR02).
    //    Danh mục đã ngừng vẫn hiện trong lịch sử giao dịch cũ, chỉ không được
    //    chọn cho giao dịch mới (việc chặn chọn ở GiaoDichService của TV2;
    //    service này cung cấp LayTheoLoai(..., chiDangDung: true) để lọc).
    //  - Không đổi LoaiDanhMuc khi danh mục đã có giao dịch / ngân sách:
    //    đổi Thu<->Chi sẽ phá quy tắc GD Thu phải gắn danh mục Thu (Bảng 2.4.4).
    public static class DanhMucService
    {
        public const string LoaiThu = "Thu";
        public const string LoaiChi = "Chi";

        // ---------------- Đọc ----------------

        /// <summary>
        /// Danh sách danh mục của user hiện tại.
        /// loaiDanhMuc: null = cả Thu + Chi; trangThai: null = tất cả;
        /// tuKhoa: lọc theo tên (LIKE, có tham số).
        /// </summary>
        public static List<DanhMuc> LayDanhSach(string loaiDanhMuc, bool? trangThai, string tuKhoa)
        {
            var ds = new List<DanhMuc>();
            if (CurrentUser.NguoiDungID <= 0) return ds;
            string sql = @"SELECT DanhMucID, NguoiDungID, TenDanhMuc, LoaiDanhMuc, MoTa, TrangThai
                           FROM DanhMuc
                           WHERE NguoiDungID = @NguoiDungID
                             AND (@Loai IS NULL OR LoaiDanhMuc = @Loai)
                             AND (@TrangThai IS NULL OR TrangThai = @TrangThai)
                             AND (@TuKhoa IS NULL OR TenDanhMuc LIKE N'%' + @TuKhoa + N'%')
                           ORDER BY LoaiDanhMuc, TenDanhMuc;";

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                cmd.Parameters.Add("@Loai", SqlDbType.NVarChar, 10).Value =
                    string.IsNullOrEmpty(loaiDanhMuc) ? (object)DBNull.Value : loaiDanhMuc;
                cmd.Parameters.Add("@TrangThai", SqlDbType.Bit).Value =
                    trangThai.HasValue ? (object)trangThai.Value : DBNull.Value;
                cmd.Parameters.Add("@TuKhoa", SqlDbType.NVarChar, 100).Value =
                    string.IsNullOrWhiteSpace(tuKhoa) ? (object)DBNull.Value : tuKhoa.Trim();

                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read()) ds.Add(DocDanhMuc(r));
                }
            }
            return ds;
        }

        /// <summary>Danh mục theo loại — dùng cho combo chọn ở giao dịch / ngân sách.</summary>
        public static List<DanhMuc> LayTheoLoai(string loaiDanhMuc, bool chiDangDung)
        {
            return LayDanhSach(loaiDanhMuc, chiDangDung ? (bool?)true : null, null);
        }

        public static DanhMuc LayTheoID(int danhMucID)
        {
            string sql = @"SELECT DanhMucID, NguoiDungID, TenDanhMuc, LoaiDanhMuc, MoTa, TrangThai
                           FROM DanhMuc
                           WHERE DanhMucID = @DanhMucID AND NguoiDungID = @NguoiDungID;";

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@DanhMucID", SqlDbType.Int).Value = danhMucID;
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    return r.Read() ? DocDanhMuc(r) : null;
                }
            }
        }

        /// <summary>Số giao dịch đang tham chiếu danh mục (để cảnh báo khi ngừng / cấm đổi loại).</summary>
        public static int DemGiaoDich(int danhMucID)
        {
            string sql = @"SELECT COUNT(*) FROM GiaoDich
                           WHERE DanhMucID = @DanhMucID AND NguoiDungID = @NguoiDungID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@DanhMucID", SqlDbType.Int).Value = danhMucID;
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // ---------------- Ghi ----------------

        /// <summary>Thêm danh mục. Trả về ID mới (>0) hoặc -1 kèm loi khi bị từ chối.</summary>
        public static int Them(DanhMuc dm, out string loi)
        {
            loi = null;
            if (!KiemTraHopLe(dm, true, out loi)) return -1;

            string sql = @"INSERT INTO DanhMuc (NguoiDungID, TenDanhMuc, LoaiDanhMuc, MoTa, TrangThai)
                           VALUES (@NguoiDungID, @TenDanhMuc, @LoaiDanhMuc, @MoTa, 1);
                           SELECT CAST(SCOPE_IDENTITY() AS int);";

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                GanThamSo(cmd, dm);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>Sửa tên / mô tả / loại. Không sửa được danh mục của user khác (WHERE kèm NguoiDungID).</summary>
        public static bool Sua(DanhMuc dm, out string loi)
        {
            loi = null;
            if (!KiemTraHopLe(dm, false, out loi)) return false;

            DanhMuc cu = LayTheoID(dm.DanhMucID);
            if (cu == null)
            {
                loi = "Không tìm thấy danh mục thuộc tài khoản hiện tại.";
                return false;
            }

            // Đổi loại Thu<->Chi chỉ an toàn khi chưa có giao dịch và chưa có ngân sách.
            if (!string.Equals(cu.LoaiDanhMuc, dm.LoaiDanhMuc, StringComparison.Ordinal))
            {
                if (DemGiaoDich(dm.DanhMucID) > 0)
                {
                    loi = "Danh mục đã có giao dịch nên không thể đổi loại Thu/Chi.";
                    return false;
                }
                if (CoNganSach(dm.DanhMucID))
                {
                    loi = "Danh mục đã có ngân sách nên không thể đổi loại Thu/Chi.";
                    return false;
                }
            }

            string sql = @"UPDATE DanhMuc
                           SET TenDanhMuc = @TenDanhMuc, LoaiDanhMuc = @LoaiDanhMuc, MoTa = @MoTa
                           WHERE DanhMucID = @DanhMucID AND NguoiDungID = @NguoiDungID;";

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                GanThamSo(cmd, dm);
                cmd.Parameters.Add("@DanhMucID", SqlDbType.Int).Value = dm.DanhMucID;
                conn.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>Ngừng sử dụng (soft-delete). Lịch sử giao dịch cũ vẫn giữ nguyên.</summary>
        public static bool NgungSuDung(int danhMucID, out string loi)
        {
            return DoiTrangThai(danhMucID, false, out loi);
        }

        /// <summary>Kích hoạt lại danh mục đã ngừng.</summary>
        public static bool KichHoatLai(int danhMucID, out string loi)
        {
            return DoiTrangThai(danhMucID, true, out loi);
        }

        private static bool DoiTrangThai(int danhMucID, bool trangThaiMoi, out string loi)
        {
            loi = null;
            if (LayTheoID(danhMucID) == null)
            {
                loi = "Không tìm thấy danh mục thuộc tài khoản hiện tại.";
                return false;
            }

            string sql = @"UPDATE DanhMuc SET TrangThai = @TrangThai
                           WHERE DanhMucID = @DanhMucID AND NguoiDungID = @NguoiDungID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = trangThaiMoi;
                cmd.Parameters.Add("@DanhMucID", SqlDbType.Int).Value = danhMucID;
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        // ---------------- Kiểm tra ----------------

        private static bool KiemTraHopLe(DanhMuc dm, bool laThemMoi, out string loi)
        {
            loi = null;
            if (dm == null) { loi = "Dữ liệu danh mục trống."; return false; }

            if (!ValidationHelper.BatBuoc(dm.TenDanhMuc, "Tên danh mục", out loi)) return false;

            if (dm.LoaiDanhMuc != LoaiThu && dm.LoaiDanhMuc != LoaiChi)
            {
                loi = "Loại danh mục phải là Thu hoặc Chi.";
                return false;
            }

            if (dm.MoTa != null && dm.MoTa.Length > 255)
            {
                loi = "Mô tả tối đa 255 ký tự.";
                return false;
            }

            // Trùng tên trong cùng loại của cùng user gây nhầm lẫn khi chọn ở giao dịch.
            // DB không có UNIQUE cho trường hợp này (003) nên kiểm tra ở service.
            string sql = @"SELECT COUNT(*) FROM DanhMuc
                           WHERE NguoiDungID = @NguoiDungID AND LoaiDanhMuc = @LoaiDanhMuc
                             AND TenDanhMuc = @TenDanhMuc
                             AND (@DanhMucID = 0 OR DanhMucID <> @DanhMucID);";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                cmd.Parameters.Add("@LoaiDanhMuc", SqlDbType.NVarChar, 10).Value = dm.LoaiDanhMuc;
                cmd.Parameters.Add("@TenDanhMuc", SqlDbType.NVarChar, 100).Value = dm.TenDanhMuc.Trim();
                cmd.Parameters.Add("@DanhMucID", SqlDbType.Int).Value = laThemMoi ? 0 : dm.DanhMucID;
                conn.Open();
                if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                {
                    loi = "Đã có danh mục \"" + dm.TenDanhMuc.Trim() + "\" thuộc loại " + dm.LoaiDanhMuc + ".";
                    return false;
                }
            }
            return true;
        }

        private static bool CoNganSach(int danhMucID)
        {
            string sql = @"SELECT COUNT(*) FROM NganSach
                           WHERE DanhMucID = @DanhMucID AND NguoiDungID = @NguoiDungID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@DanhMucID", SqlDbType.Int).Value = danhMucID;
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private static void GanThamSo(SqlCommand cmd, DanhMuc dm)
        {
            cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
            cmd.Parameters.Add("@TenDanhMuc", SqlDbType.NVarChar, 100).Value = dm.TenDanhMuc.Trim();
            cmd.Parameters.Add("@LoaiDanhMuc", SqlDbType.NVarChar, 10).Value = dm.LoaiDanhMuc;
            cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar, 255).Value =
                string.IsNullOrWhiteSpace(dm.MoTa) ? (object)DBNull.Value : dm.MoTa.Trim();
        }

        private static DanhMuc DocDanhMuc(SqlDataReader r)
        {
            int iMoTa = r.GetOrdinal("MoTa");
            return new DanhMuc
            {
                DanhMucID = r.GetInt32(r.GetOrdinal("DanhMucID")),
                NguoiDungID = r.GetInt32(r.GetOrdinal("NguoiDungID")),
                TenDanhMuc = r.GetString(r.GetOrdinal("TenDanhMuc")),
                LoaiDanhMuc = r.GetString(r.GetOrdinal("LoaiDanhMuc")),
                MoTa = r.IsDBNull(iMoTa) ? null : r.GetString(iMoTa),
                TrangThai = r.GetBoolean(r.GetOrdinal("TrangThai"))
            };
        }
    }
}
