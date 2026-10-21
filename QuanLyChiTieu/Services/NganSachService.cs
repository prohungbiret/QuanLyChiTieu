using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyChiTieu.Data;
using QuanLyChiTieu.Models;

namespace QuanLyChiTieu.Services
{
    /// <summary>Một dòng ngân sách kèm số đã chi và trạng thái cảnh báo (FR08).</summary>
    public class NganSachChiTiet
    {
        public NganSach NganSach { get; set; }
        public string TenDanhMuc { get; set; }
        public decimal DaChi { get; set; }
        public decimal ConLai { get { return NganSach.HanMuc - DaChi; } }
        public decimal PhanTram
        {
            get { return NganSach.HanMuc > 0 ? DaChi * 100m / NganSach.HanMuc : 0m; }
        }
        /// <summary>BinhThuong (&lt;80%) | CanhBao80 (&gt;=80%) | DatHanMuc (=100%) | VuotHanMuc (&gt;100%).</summary>
        public string TrangThai
        {
            get
            {
                if (PhanTram > 100m) return "VuotHanMuc";
                if (PhanTram >= 100m) return "DatHanMuc";
                if (PhanTram >= 80m) return "CanhBao80";
                return "BinhThuong";
            }
        }
        public string NhanTrangThai
        {
            get
            {
                switch (TrangThai)
                {
                    case "VuotHanMuc": return "Vượt hạn mức";
                    case "DatHanMuc": return "Đạt hạn mức";
                    case "CanhBao80": return "Cảnh báo 80%";
                    default: return "Bình thường";
                }
            }
        }
    }

    // TV3 (Hải) sở hữu — phân hệ Ngân sách (FR08, Tài liệu 06 mục 6.4).
    // Quy tắc cốt lõi (Tài liệu 01 mục 1.5F, 02 mục 2.4.4):
    //  - Ngân sách đặt theo (Danh mục loại Chi, Tháng, Năm); UNIQUE ở DB (003).
    //  - Đã chi CHỈ tính giao dịch loại 'Chi' của đúng danh mục + đúng tháng/năm.
    //    ChuyenTien / TraNo / vay nợ / tiết kiệm KHÔNG bao giờ được tính —
    //    dùng view chung vwSoTienChiDanhMucThang của TV1 (mục 6.7), không tự
    //    viết lại công thức ở nơi khác.
    //  - Ngưỡng cảnh báo: >= 80% cảnh báo, >= 100% đạt/vượt hạn mức.
    public static class NganSachService
    {
        // ---------------- Đọc ----------------

        /// <summary>Ngân sách của 1 tháng/năm kèm đã chi (từ view chung) và trạng thái.</summary>
        public static List<NganSachChiTiet> LayTheoThang(int thang, int nam)
        {
            var ds = new List<NganSachChiTiet>();
            string sql = @"SELECT NS.NganSachID, NS.NguoiDungID, NS.DanhMucID, NS.Thang, NS.Nam, NS.HanMuc,
                                  DM.TenDanhMuc,
                                  ISNULL(V.DaChi, 0) AS DaChi
                           FROM NganSach NS
                           JOIN DanhMuc DM ON DM.DanhMucID = NS.DanhMucID
                                          AND DM.NguoiDungID = NS.NguoiDungID
                           LEFT JOIN vwSoTienChiDanhMucThang V
                                  ON V.NguoiDungID = NS.NguoiDungID AND V.DanhMucID = NS.DanhMucID
                                 AND V.Thang = NS.Thang AND V.Nam = NS.Nam
                           WHERE NS.NguoiDungID = @NguoiDungID AND NS.Thang = @Thang AND NS.Nam = @Nam
                           ORDER BY DM.TenDanhMuc;";

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                cmd.Parameters.Add("@Thang", SqlDbType.Int).Value = thang;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = nam;
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        ds.Add(new NganSachChiTiet
                        {
                            NganSach = DocNganSach(r),
                            TenDanhMuc = r.GetString(r.GetOrdinal("TenDanhMuc")),
                            DaChi = r.GetDecimal(r.GetOrdinal("DaChi"))
                        });
                    }
                }
            }
            return ds;
        }

        public static NganSachChiTiet LayTheoID(int nganSachID)
        {
            foreach (NganSachChiTiet ct in LayTatCa())
                if (ct.NganSach.NganSachID == nganSachID) return ct;
            return null;
        }

        /// <summary>Toàn bộ ngân sách của user (mọi kỳ), mới nhất trước.</summary>
        public static List<NganSachChiTiet> LayTatCa()
        {
            var ds = new List<NganSachChiTiet>();
            string sql = @"SELECT NS.NganSachID, NS.NguoiDungID, NS.DanhMucID, NS.Thang, NS.Nam, NS.HanMuc,
                                  DM.TenDanhMuc,
                                  ISNULL(V.DaChi, 0) AS DaChi
                           FROM NganSach NS
                           JOIN DanhMuc DM ON DM.DanhMucID = NS.DanhMucID
                                          AND DM.NguoiDungID = NS.NguoiDungID
                           LEFT JOIN vwSoTienChiDanhMucThang V
                                  ON V.NguoiDungID = NS.NguoiDungID AND V.DanhMucID = NS.DanhMucID
                                 AND V.Thang = NS.Thang AND V.Nam = NS.Nam
                           WHERE NS.NguoiDungID = @NguoiDungID
                           ORDER BY NS.Nam DESC, NS.Thang DESC, DM.TenDanhMuc;";

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        ds.Add(new NganSachChiTiet
                        {
                            NganSach = DocNganSach(r),
                            TenDanhMuc = r.GetString(r.GetOrdinal("TenDanhMuc")),
                            DaChi = r.GetDecimal(r.GetOrdinal("DaChi"))
                        });
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// Đã chi của 1 danh mục trong 1 tháng — CHỈ loại Chi (view 005).
        /// Dashboard / phân hệ khác cần con số này thì gọi qua đây, không tự SUM.
        /// </summary>
        public static decimal TinhDaChi(int danhMucID, int thang, int nam)
        {
            string sql = @"SELECT ISNULL(DaChi, 0) FROM vwSoTienChiDanhMucThang
                           WHERE NguoiDungID = @NguoiDungID AND DanhMucID = @DanhMucID
                             AND Thang = @Thang AND Nam = @Nam;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                cmd.Parameters.Add("@DanhMucID", SqlDbType.Int).Value = danhMucID;
                cmd.Parameters.Add("@Thang", SqlDbType.Int).Value = thang;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = nam;
                conn.Open();
                object kq = cmd.ExecuteScalar();
                return kq == null || kq == DBNull.Value ? 0m : Convert.ToDecimal(kq);
            }
        }

        /// <summary>Danh sách ngân sách của tháng đang >= 80% — dùng cho cảnh báo ở Dashboard.</summary>
        public static List<NganSachChiTiet> LayCanhBao(int thang, int nam)
        {
            var canhBao = new List<NganSachChiTiet>();
            foreach (NganSachChiTiet ct in LayTheoThang(thang, nam))
                if (ct.PhanTram >= 80m) canhBao.Add(ct);
            return canhBao;
        }

        // ---------------- Ghi ----------------

        /// <summary>Thêm ngân sách. Trả về ID mới hoặc -1 kèm loi.</summary>
        public static int Them(NganSach ns, out string loi)
        {
            loi = null;
            if (!KiemTraHopLe(ns, true, out loi)) return -1;

            string sql = @"INSERT INTO NganSach (NguoiDungID, DanhMucID, Thang, Nam, HanMuc)
                           VALUES (@NguoiDungID, @DanhMucID, @Thang, @Nam, @HanMuc);
                           SELECT CAST(SCOPE_IDENTITY() AS int);";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                GanThamSo(cmd, ns);
                conn.Open();
                try
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                {
                    // UQ_NS_Ky (003) — lớp bảo vệ thứ hai sau kiểm tra trùng ở service.
                    loi = "Danh mục này đã có ngân sách trong tháng " + ns.Thang + "/" + ns.Nam + ".";
                    return -1;
                }
            }
        }

        /// <summary>Sửa hạn mức / kỳ / danh mục của ngân sách thuộc user hiện tại.</summary>
        public static bool Sua(NganSach ns, out string loi)
        {
            loi = null;
            if (!KiemTraHopLe(ns, false, out loi)) return false;

            string sql = @"UPDATE NganSach
                           SET DanhMucID = @DanhMucID, Thang = @Thang, Nam = @Nam, HanMuc = @HanMuc
                           WHERE NganSachID = @NganSachID AND NguoiDungID = @NguoiDungID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                GanThamSo(cmd, ns);
                cmd.Parameters.Add("@NganSachID", SqlDbType.Int).Value = ns.NganSachID;
                conn.Open();
                try
                {
                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        loi = "Không tìm thấy ngân sách thuộc tài khoản hiện tại.";
                        return false;
                    }
                    return true;
                }
                catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                {
                    loi = "Danh mục này đã có ngân sách trong tháng " + ns.Thang + "/" + ns.Nam + ".";
                    return false;
                }
            }
        }

        /// <summary>Xóa ngân sách (bản ghi hạn mức — không ảnh hưởng giao dịch). Có xác nhận ở UI.</summary>
        public static bool Xoa(int nganSachID, out string loi)
        {
            loi = null;
            string sql = @"DELETE FROM NganSach
                           WHERE NganSachID = @NganSachID AND NguoiDungID = @NguoiDungID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NganSachID", SqlDbType.Int).Value = nganSachID;
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                if (cmd.ExecuteNonQuery() != 1)
                {
                    loi = "Không tìm thấy ngân sách thuộc tài khoản hiện tại.";
                    return false;
                }
                return true;
            }
        }

        // ---------------- Kiểm tra ----------------

        private static bool KiemTraHopLe(NganSach ns, bool laThemMoi, out string loi)
        {
            loi = null;
            if (ns == null) { loi = "Dữ liệu ngân sách trống."; return false; }

            if (ns.Thang < 1 || ns.Thang > 12) { loi = "Tháng phải từ 1 đến 12."; return false; }
            if (ns.Nam < 1900 || ns.Nam > 2100) { loi = "Năm phải từ 1900 đến 2100."; return false; }
            if (ns.HanMuc <= 0m) { loi = "Hạn mức phải lớn hơn 0."; return false; }
            if (ns.HanMuc > ValidationHelper.SoTienToiDa) { loi = "Hạn mức vượt giới hạn."; return false; }

            // Danh mục phải thuộc user hiện tại VÀ phải là loại Chi (Bảng 2.4.4).
            DanhMuc dm = DanhMucService.LayTheoID(ns.DanhMucID);
            if (dm == null)
            {
                loi = "Danh mục không thuộc tài khoản hiện tại.";
                return false;
            }
            if (dm.LoaiDanhMuc != DanhMucService.LoaiChi)
            {
                loi = "Ngân sách chỉ đặt cho danh mục loại Chi.";
                return false;
            }
            if (!dm.TrangThai)
            {
                // Thêm mới / đổi sang danh mục đã ngừng: chặn.
                // Sửa hạn mức của chính ngân sách cũ (danh mục ngừng SAU khi đã đặt): cho phép.
                bool doiDanhMuc = !laThemMoi && DanhMucCuaNganSach(ns.NganSachID) != ns.DanhMucID;
                if (laThemMoi || doiDanhMuc)
                {
                    loi = "Danh mục đã ngừng sử dụng, không đặt ngân sách mới được.";
                    return false;
                }
            }

            // Một danh mục chỉ có một hạn mức trong một tháng (UQ_NS_Ky ở DB + check ở đây để báo rõ).
            string sql = @"SELECT COUNT(*) FROM NganSach
                           WHERE NguoiDungID = @NguoiDungID AND DanhMucID = @DanhMucID
                             AND Thang = @Thang AND Nam = @Nam
                             AND (@NganSachID = 0 OR NganSachID <> @NganSachID);";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                cmd.Parameters.Add("@DanhMucID", SqlDbType.Int).Value = ns.DanhMucID;
                cmd.Parameters.Add("@Thang", SqlDbType.Int).Value = ns.Thang;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = ns.Nam;
                cmd.Parameters.Add("@NganSachID", SqlDbType.Int).Value = laThemMoi ? 0 : ns.NganSachID;
                conn.Open();
                if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                {
                    loi = "Danh mục \"" + dm.TenDanhMuc + "\" đã có ngân sách trong tháng "
                        + ns.Thang + "/" + ns.Nam + ". Hãy sửa ngân sách hiện có.";
                    return false;
                }
            }
            return true;
        }

        private static int DanhMucCuaNganSach(int nganSachID)
        {
            string sql = @"SELECT DanhMucID FROM NganSach
                           WHERE NganSachID = @NganSachID AND NguoiDungID = @NguoiDungID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NganSachID", SqlDbType.Int).Value = nganSachID;
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                object kq = cmd.ExecuteScalar();
                return kq == null || kq == DBNull.Value ? -1 : Convert.ToInt32(kq);
            }
        }

        private static void GanThamSo(SqlCommand cmd, NganSach ns)
        {
            cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
            cmd.Parameters.Add("@DanhMucID", SqlDbType.Int).Value = ns.DanhMucID;
            cmd.Parameters.Add("@Thang", SqlDbType.Int).Value = ns.Thang;
            cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = ns.Nam;
            SqlParameter p = cmd.Parameters.Add("@HanMuc", SqlDbType.Decimal);
            p.Precision = 18; p.Scale = 2; p.Value = ns.HanMuc;
        }

        private static NganSach DocNganSach(SqlDataReader r)
        {
            return new NganSach
            {
                NganSachID = r.GetInt32(r.GetOrdinal("NganSachID")),
                NguoiDungID = r.GetInt32(r.GetOrdinal("NguoiDungID")),
                DanhMucID = r.GetInt32(r.GetOrdinal("DanhMucID")),
                Thang = r.GetInt32(r.GetOrdinal("Thang")),
                Nam = r.GetInt32(r.GetOrdinal("Nam")),
                HanMuc = r.GetDecimal(r.GetOrdinal("HanMuc"))
            };
        }
    }
}
