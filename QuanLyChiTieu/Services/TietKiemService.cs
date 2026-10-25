using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyChiTieu.Data;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Services
{
    /// <summary>Một mục tiêu tiết kiệm kèm tiền đã đạt — tiến độ TÍNH ĐỘNG (QD05, không lưu cột):
    /// DaDat = SUM(GiaoDich DongGop) − SUM(GiaoDich RutTietKiem) của đúng mục tiêu.</summary>
    public class MucTieuChiTiet
    {
        public MucTieuTietKiem MucTieu { get; set; }
        public decimal DaDat { get; set; }

        public decimal PhanTram
        {
            get
            {
                return MucTieu.SoTienMucTieu > 0
                    ? DaDat * 100m / MucTieu.SoTienMucTieu : 0m;
            }
        }

        public string NhanTrangThai
        {
            get
            {
                switch (MucTieu.TrangThai)
                {
                    case "HoanThanh": return "Hoàn thành";
                    case "TamDung": return "Tạm dừng";
                    default: return "Đang thực hiện";
                }
            }
        }
    }

    // Phần còn lại của TV4, TV1 đảm nhiệm (không sửa docs — thống nhất của nhóm).
    // Quy tắc nền (schema 002/003 + QD05):
    //  - Tiến độ KHÔNG lưu cột — luôn SUM từ GiaoDich (DongGop = tiền ra ví, RutTietKiem = tiền vào ví).
    //  - CHECK CK_GD_MT_BB / CK_GD_MT_Cam: GD loại DongGop/RutTietKiem PHỎI trỏ MucTieuID,
    //    các loại khác PHẢI để MucTieuID = NULL (mô hình giống hệt KhoanVayNoID với 4 loại vay nợ).
    //  - FK ghép (NguoiDungID, MucTieuID): mọi truy vấn luôn kèm NguoiDungID (L006 sổ tay).
    public static class TietKiemService
    {
        // ---------------- Đọc ----------------

        /// <summary>Toàn bộ mục tiêu của user hiện tại, kèm tiền đã đạt tính động.</summary>
        public static List<MucTieuChiTiet> LayTatCa()
        {
            var ds = new List<MucTieuChiTiet>();
            string sql = @"SELECT MT.MucTieuID, MT.NguoiDungID, MT.TenMucTieu, MT.SoTienMucTieu,
                                  MT.NgayBatDau, MT.NgayKetThuc, MT.TrangThai,
                                  ISNULL(SUM(CASE WHEN G.LoaiGiaoDich = N'DongGop' THEN G.SoTien
                                                  WHEN G.LoaiGiaoDich = N'RutTietKiem' THEN -G.SoTien
                                                  ELSE 0 END), 0) AS DaDat
                           FROM MucTieuTietKiem MT
                           LEFT JOIN GiaoDich G ON G.MucTieuID = MT.MucTieuID
                                               AND G.NguoiDungID = MT.NguoiDungID
                           WHERE MT.NguoiDungID = @NguoiDungID
                           GROUP BY MT.MucTieuID, MT.NguoiDungID, MT.TenMucTieu, MT.SoTienMucTieu,
                                    MT.NgayBatDau, MT.NgayKetThuc, MT.TrangThai
                           ORDER BY MT.NgayBatDau DESC, MT.MucTieuID DESC;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        ds.Add(new MucTieuChiTiet
                        {
                            MucTieu = DocMucTieu(r),
                            DaDat = r.GetDecimal(r.GetOrdinal("DaDat"))
                        });
                    }
                }
            }
            return ds;
        }

        /// <summary>Số tiền đã đóng góp ròng của 1 mục tiêu — dùng trước khi rút (không rút vượt).</summary>
        public static decimal LayDaDat(int mucTieuID)
        {
            string sql = @"SELECT ISNULL(SUM(CASE WHEN LoaiGiaoDich = N'DongGop' THEN SoTien
                                                  WHEN LoaiGiaoDich = N'RutTietKiem' THEN -SoTien
                                                  ELSE 0 END), 0)
                           FROM GiaoDich
                           WHERE NguoiDungID = @NguoiDungID AND MucTieuID = @MucTieuID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                cmd.Parameters.Add("@MucTieuID", SqlDbType.Int).Value = mucTieuID;
                conn.Open();
                object kq = cmd.ExecuteScalar();
                return kq == null || kq == DBNull.Value ? 0m : Convert.ToDecimal(kq);
            }
        }

        // ---------------- Ghi mục tiêu ----------------

        /// <summary>Thêm mục tiêu mới. Trả về ID mới hoặc -1 kèm loi.</summary>
        public static int Them(MucTieuTietKiem mt, out string loi)
        {
            loi = null;
            if (!KiemTraHopLe(mt, out loi)) return -1;

            string sql = @"INSERT INTO MucTieuTietKiem
                               (NguoiDungID, TenMucTieu, SoTienMucTieu, NgayBatDau, NgayKetThuc, TrangThai)
                           VALUES (@NguoiDungID, @TenMucTieu, @SoTienMucTieu, @NgayBatDau, @NgayKetThuc,
                                   N'DangThucHien');
                           SELECT CAST(SCOPE_IDENTITY() AS int);";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                GanThamSo(cmd, mt);
                conn.Open();
                try
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                {
                    loi = "Không thêm được mục tiêu (dữ liệu bị trùng).";
                    return -1;
                }
            }
        }

        /// <summary>Sửa thông tin mục tiêu (tên, số tiền, ngày) thuộc user hiện tại.</summary>
        public static bool Sua(MucTieuTietKiem mt, out string loi)
        {
            loi = null;
            if (!KiemTraHopLe(mt, out loi)) return false;

            string sql = @"UPDATE MucTieuTietKiem
                           SET TenMucTieu = @TenMucTieu, SoTienMucTieu = @SoTienMucTieu,
                               NgayBatDau = @NgayBatDau, NgayKetThuc = @NgayKetThuc
                           WHERE MucTieuID = @MucTieuID AND NguoiDungID = @NguoiDungID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                GanThamSo(cmd, mt);
                cmd.Parameters.Add("@MucTieuID", SqlDbType.Int).Value = mt.MucTieuID;
                conn.Open();
                if (cmd.ExecuteNonQuery() != 1)
                {
                    loi = "Không tìm thấy mục tiêu thuộc tài khoản hiện tại.";
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Xóa mục tiêu thuộc user hiện tại. Chặn khi còn giao dịch đóng góp/rút gắn vào
        /// (CHECK/FK không cho xóa, và xóa sẽ làm lịch sử "mồ côi") — báo người dùng rõ.
        /// </summary>
        public static bool Xoa(int mucTieuID, out string loi)
        {
            loi = null;

            string dem = @"SELECT COUNT(*) FROM GiaoDich
                           WHERE NguoiDungID = @NguoiDungID AND MucTieuID = @MucTieuID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(dem, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                cmd.Parameters.Add("@MucTieuID", SqlDbType.Int).Value = mucTieuID;
                conn.Open();
                if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                {
                    loi = "Mục tiêu còn giao dịch đóng góp/rút gắn vào. Hãy xóa các giao dịch tương ứng trước khi xóa mục tiêu.";
                    return false;
                }
            }

            string sql = @"DELETE FROM MucTieuTietKiem
                           WHERE MucTieuID = @MucTieuID AND NguoiDungID = @NguoiDungID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@MucTieuID", SqlDbType.Int).Value = mucTieuID;
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                if (cmd.ExecuteNonQuery() != 1)
                {
                    loi = "Không tìm thấy mục tiêu thuộc tài khoản hiện tại.";
                    return false;
                }
                return true;
            }
        }

        // ---------------- Đóng góp / Rút (GiaoDich DongGop / RutTietKiem) ----------------

        /// <summary>
        /// Ghi giao dịch đóng góp ('DongGop' — tiền ra từ ví nguồn) hoặc rút
        /// ('RutTietKiem' — tiền vào ví đích) gắn với mục tiêu, trong MỘT transaction
        /// kèm việc đồng bộ lại trạng thái mục tiêu (ĐangThucHien/HoanThanh) theo tiến độ.
        /// Trả về ID giao dịch mới, hoặc -1 kèm loi.
        /// </summary>
        public static int Ghi(string loai, int mucTieuID, decimal soTien,
                              int? viNguonID, int? viDichID, DateTime ngay, string ghiChu, out string loi)
        {
            loi = null;
            if (loai != GiaoDichLoai.DongGop && loai != GiaoDichLoai.RutTietKiem)
            {
                loi = "Loại giao dịch tiết kiệm không hợp lệ.";
                return -1;
            }
            if (soTien <= 0m) { loi = "Số tiền phải lớn hơn 0."; return -1; }
            if (soTien > ValidationHelper.SoTienToiDa) { loi = "Số tiền vượt giới hạn."; return -1; }
            if (ngay.Date > DateTime.Today) { loi = "Ngày không được ở tương lai."; return -1; }

            if (!ValidationHelper.GiaoDichTheoVi(loai, viNguonID, viDichID, out loi)) return -1;

            // Rút không được vượt số tiền đang có trong mục tiêu (quy tắc nghiệp vụ phân hệ).
            decimal daDat = LayDaDat(mucTieuID);
            if (loai == GiaoDichLoai.RutTietKiem && soTien > daDat)
            {
                loi = "Chỉ rút được tối đa số tiền đã đóng góp (" + UIHelper.FormatTien(daDat) + ").";
                return -1;
            }

            string sqlInsert = @"INSERT INTO GiaoDich
                (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, MucTieuID, ViNguonID, ViDichID, GhiChu)
                VALUES (@NguoiDungID, @Loai, @SoTien, @Ngay, @MucTieuID, @ViNguon, @ViDich, @GhiChu);
                SELECT CAST(SCOPE_IDENTITY() AS int);";

            string sqlDongBo = @"UPDATE MT
                SET MT.TrangThai = CASE
                    WHEN Tong.DaDat >= MT.SoTienMucTieu THEN N'HoanThanh'
                    WHEN MT.TrangThai IN (N'DangThucHien', N'HoanThanh') THEN N'DangThucHien'
                    ELSE MT.TrangThai END
                FROM MucTieuTietKiem MT
                LEFT JOIN (SELECT MucTieuID, SUM(CASE WHEN LoaiGiaoDich = N'DongGop' THEN SoTien
                                                     WHEN LoaiGiaoDich = N'RutTietKiem' THEN -SoTien
                                                     ELSE 0 END) AS DaDat
                           FROM GiaoDich
                           WHERE NguoiDungID = @NguoiDungID AND MucTieuID = @MucTieuID
                           GROUP BY MucTieuID) Tong
                       ON Tong.MucTieuID = MT.MucTieuID
                WHERE MT.MucTieuID = @MucTieuID AND MT.NguoiDungID = @NguoiDungID;";

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    int idMoi;
                    using (SqlCommand cmd = new SqlCommand(sqlInsert, conn, tx))
                    {
                        cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                        cmd.Parameters.Add("@Loai", SqlDbType.NVarChar, 20).Value = loai;
                        SqlParameter p = cmd.Parameters.Add("@SoTien", SqlDbType.Decimal);
                        p.Precision = 18; p.Scale = 2; p.Value = soTien;
                        cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay.Date;
                        cmd.Parameters.Add("@MucTieuID", SqlDbType.Int).Value = mucTieuID;
                        cmd.Parameters.Add("@ViNguon", SqlDbType.Int).Value = (object)viNguonID ?? DBNull.Value;
                        cmd.Parameters.Add("@ViDich", SqlDbType.Int).Value = (object)viDichID ?? DBNull.Value;
                        cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 500).Value =
                            string.IsNullOrWhiteSpace(ghiChu) ? (object)DBNull.Value : ghiChu;

                        try
                        {
                            idMoi = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        catch (SqlException ex)
                        {
                            tx.Rollback();
                            loi = LoiSQL(ex, "Không ghi được giao dịch tiết kiệm.");
                            return -1;
                        }
                    }

                    using (SqlCommand cmd2 = new SqlCommand(sqlDongBo, conn, tx))
                    {
                        cmd2.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                        cmd2.Parameters.Add("@MucTieuID", SqlDbType.Int).Value = mucTieuID;
                        cmd2.ExecuteNonQuery();
                    }

                    tx.Commit();
                    return idMoi;
                }
            }
        }

        // ---------------- Hỗ trợ ----------------

        /// <summary>Các ví ĐANG HOẠT ĐỘNG của user hiện tại (dùng cho ComboBox chọn ví).</summary>
        public static List<ViTien> LayDsViDangDung()
        {
            var ds = new List<ViTien>();
            string sql = @"SELECT ViTienID, NguoiDungID, TenVi, LoaiVi, SoDuBanDau, TrangThai
                           FROM ViTien
                           WHERE NguoiDungID = @NguoiDungID AND TrangThai = 1
                           ORDER BY ViTienID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        ds.Add(new ViTien
                        {
                            ViTienID = r.GetInt32(r.GetOrdinal("ViTienID")),
                            NguoiDungID = r.GetInt32(r.GetOrdinal("NguoiDungID")),
                            TenVi = r.GetString(r.GetOrdinal("TenVi")),
                            LoaiVi = r.GetString(r.GetOrdinal("LoaiVi")),
                            SoDuBanDau = r.GetDecimal(r.GetOrdinal("SoDuBanDau")),
                            TrangThai = r.GetBoolean(r.GetOrdinal("TrangThai"))
                        });
                    }
                }
            }
            return ds;
        }

        private static bool KiemTraHopLe(MucTieuTietKiem mt, out string loi)
        {
            loi = null;
            if (mt == null) { loi = "Dữ liệu mục tiêu trống."; return false; }
            if (mt.NgayKetThuc.HasValue && mt.NgayKetThuc.Value < mt.NgayBatDau)
            {
                loi = "Ngày kết thúc không được trước ngày bắt đầu.";
                return false;
            }
            if (mt.NgayBatDau.Date > DateTime.Today)
            {
                loi = "Ngày bắt đầu không được ở tương lai.";
                return false;
            }
            if (mt.SoTienMucTieu <= 0m) { loi = "Số tiền mục tiêu phải lớn hơn 0."; return false; }
            if (mt.SoTienMucTieu > ValidationHelper.SoTienToiDa)
            {
                loi = "Số tiền mục tiêu vượt giới hạn.";
                return false;
            }
            return true;
        }

        private static void GanThamSo(SqlCommand cmd, MucTieuTietKiem mt)
        {
            cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
            cmd.Parameters.Add("@TenMucTieu", SqlDbType.NVarChar, 150).Value = mt.TenMucTieu;
            SqlParameter p = cmd.Parameters.Add("@SoTienMucTieu", SqlDbType.Decimal);
            p.Precision = 18; p.Scale = 2; p.Value = mt.SoTienMucTieu;
            cmd.Parameters.Add("@NgayBatDau", SqlDbType.Date).Value = mt.NgayBatDau.Date;
            cmd.Parameters.Add("@NgayKetThuc", SqlDbType.Date).Value =
                (object)mt.NgayKetThuc ?? DBNull.Value;
        }

        private static MucTieuTietKiem DocMucTieu(SqlDataReader r)
        {
            int iNgayKetThuc = r.GetOrdinal("NgayKetThuc");
            return new MucTieuTietKiem
            {
                MucTieuID = r.GetInt32(r.GetOrdinal("MucTieuID")),
                NguoiDungID = r.GetInt32(r.GetOrdinal("NguoiDungID")),
                TenMucTieu = r.GetString(r.GetOrdinal("TenMucTieu")),
                SoTienMucTieu = r.GetDecimal(r.GetOrdinal("SoTienMucTieu")),
                NgayBatDau = r.GetDateTime(r.GetOrdinal("NgayBatDau")),
                NgayKetThuc = r.IsDBNull(iNgayKetThuc) ? (DateTime?)null : r.GetDateTime(iNgayKetThuc),
                TrangThai = r.GetString(r.GetOrdinal("TrangThai"))
            };
        }

        private static string LoiSQL(SqlException ex, string thongDiepGoc)
        {
            // 547 = vi phạm CHECK/FK
            if (ex.Number == 547) return thongDiepGoc + " Dữ liệu không thỏa ràng buộc (CHECK/FK).";
            return thongDiepGoc;
        }
    }
}
