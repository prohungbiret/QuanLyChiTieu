using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyChiTieu.Data;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Services
{
    /// <summary>Một khoản vay nợ kèm tiến độ:
    /// Khoan "Vay" (mình nợ người khác): DaTra = SUM(LichSuTraNo.TienGoc), ConLai = Goc − DaTra.
    /// Khoan "ChoVay" (mình cho người khác): DaHoi = SUM(GiaoDich ThuHoiNo), ConLai = Goc − DaHoi.
    /// Lãi phát sinh (TienLaiPhi trong LichSuTraNo) KHÔNG giảm công nợ — chỉ gốc làm giảm.</summary>
    public class KhoanVayNoChiTiet
    {
        public KhoanVayNo Khoan { get; set; }
        public decimal DaTra { get; set; }      // gốc đã trả (Vay) hoặc đã thu hồi (ChoVay)
        public decimal TongLaiPhi { get; set; } // tổng lãi/phí đã trả (chỉ khoản Vay có LichSuTraNo)

        public decimal ConLai
        {
            get
            {
                decimal du = Khoan.SoTienGoc - DaTra;
                return du > 0m ? du : 0m;
            }
        }

        public decimal PhanTram
        {
            get { return Khoan.SoTienGoc > 0 ? DaTra * 100m / Khoan.SoTienGoc : 0m; }
        }

        public bool HoanThanhTot { get { return ConLai <= 0m; } }

        public string NhanLoai
        {
            get { return Khoan.LoaiKhoan == "Vay" ? "Đang nợ" : "Cho vay"; }
        }
    }

    // TV1 (thay TV4) — phân hệ Vay nợ mức A (đúng đặc tả 6.5):
    //  - Khoản vay thể hiện qua 4 loại GiaoDich: NhanVay (nhận tiền — vào ví đích),
    //    ChoVay (cho ra — tiền ra ví nguồn), TraNo (trả — tiền ra ví nguồn),
    //    ThuHoiNo (thu hồi — tiền vào ví đích); mỗi GD trỏ KhoanVayNoID (CK_GD_KV_BB/Cam).
    //  - TraNo + LichSuTraNo ghi TRONG MỘT transaction (L009), tách gốc/lãi:
    //    TienGoc + TienLaiPhi = SoTien (CHECK CK_LSTN_Tien + ValidationHelper.TienTraNo).
    //  - FK ghép (NguoiDungID, KhoanVayNoID) — mọi truy vấn kèm NguoiDungID (L006).
    public static class VayNoService
    {
        // ---------------- Đọc ----------------

        public static List<KhoanVayNoChiTiet> LayTatCa()
        {
            var ds = new List<KhoanVayNoChiTiet>();
            string sql = @"SELECT K.KhoanVayNoID, K.NguoiDungID, K.LoaiKhoan, K.TenNguoiLienQuan,
                                  K.SoTienGoc, K.NgayPhatSinh, K.NgayDenHan, K.LaiSuat,
                                  K.GhiChu, K.TrangThai,
                                  ISNULL(T.GocDaTra, 0) AS GocDaTra,
                                  ISNULL(T.LaiDaTra, 0) AS LaiDaTra,
                                  ISNULL(H.DaHoi, 0)    AS DaHoi
                           FROM KhoanVayNo K
                           LEFT JOIN (SELECT KhoanVayNoID,
                                              SUM(TienGoc)    AS GocDaTra,
                                              SUM(TienLaiPhi) AS LaiDaTra
                                      FROM LichSuTraNo
                                      WHERE NguoiDungID = @NguoiDungID
                                      GROUP BY KhoanVayNoID) T
                                  ON T.KhoanVayNoID = K.KhoanVayNoID
                           LEFT JOIN (SELECT KhoanVayNoID, SUM(SoTien) AS DaHoi
                                      FROM GiaoDich
                                      WHERE NguoiDungID = @NguoiDungID
                                        AND LoaiGiaoDich = N'ThuHoiNo'
                                      GROUP BY KhoanVayNoID) H
                                  ON H.KhoanVayNoID = K.KhoanVayNoID
                           WHERE K.NguoiDungID = @NguoiDungID
                           ORDER BY K.TrangThai, K.NgayPhatSinh DESC, K.KhoanVayNoID DESC;";

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        KhoanVayNo k = DocKhoan(r);
                        var ct = new KhoanVayNoChiTiet { Khoan = k };
                        if (k.LoaiKhoan == "Vay")
                        {
                            ct.DaTra = r.GetDecimal(r.GetOrdinal("GocDaTra"));
                            ct.TongLaiPhi = r.GetDecimal(r.GetOrdinal("LaiDaTra"));
                        }
                        else
                        {
                            ct.DaTra = r.GetDecimal(r.GetOrdinal("DaHoi"));
                        }
                        // Đồng bộ trạng thái hiển thị với công nợ thực tế (DB có thể chưa đồng bộ)
                        if (ct.HoanThanhTot) k.TrangThai = "HoanThanh";
                        ds.Add(ct);
                    }
                }
            }
            return ds;
        }

        /// <summary>Lịch sử trả nợ của 1 khoản (bảng LichSuTraNo, kèm thông tin GD).</summary>
        public static List<LichSuTraNo> LayLichSuTraNo(int khoanVayNoID)
        {
            var ds = new List<LichSuTraNo>();
            string sql = @"SELECT L.LichSuTraNoID, L.NguoiDungID, L.KhoanVayNoID, L.GiaoDichID,
                                  L.NgayThanhToan, L.TienGoc, L.TienLaiPhi, L.GhiChu
                           FROM LichSuTraNo L
                           WHERE L.NguoiDungID = @NguoiDungID AND L.KhoanVayNoID = @KhoanVayNoID
                           ORDER BY L.NgayThanhToan DESC, L.LichSuTraNoID DESC;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                cmd.Parameters.Add("@KhoanVayNoID", SqlDbType.Int).Value = khoanVayNoID;
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        ds.Add(new LichSuTraNo
                        {
                            LichSuTraNoID = r.GetInt32(r.GetOrdinal("LichSuTraNoID")),
                            NguoiDungID = r.GetInt32(r.GetOrdinal("NguoiDungID")),
                            KhoanVayNoID = r.GetInt32(r.GetOrdinal("KhoanVayNoID")),
                            GiaoDichID = r.GetInt32(r.GetOrdinal("GiaoDichID")),
                            NgayThanhToan = r.GetDateTime(r.GetOrdinal("NgayThanhToan")),
                            TienGoc = r.GetDecimal(r.GetOrdinal("TienGoc")),
                            TienLaiPhi = r.GetDecimal(r.GetOrdinal("TienLaiPhi")),
                            GhiChu = r.IsDBNull(r.GetOrdinal("GhiChu"))
                                ? null : r.GetString(r.GetOrdinal("GhiChu"))
                        });
                    }
                }
            }
            return ds;
        }

        // ---------------- Ghi khoản ----------------

        /// <summary>
        /// Thêm khoản vay kèm GD khởi phát trong MỘT transaction: loại 'Vay' → GD NhanVay
        /// (tiền vào ví đích); loại 'ChoVay' → GD ChoVay (tiền ra ví nguồn). Chỉ chạy khi THÊM MỚI.
        /// Trả về ID khoản mới, hoặc -1 kèm loi.
        /// </summary>
        public static int Them(KhoanVayNo k, int viID, bool laVayCuaToi, out string loi)
        {
            loi = null;
            k.LoaiKhoan = laVayCuaToi ? "Vay" : "ChoVay";
            if (!KiemTraHopLe(k, out loi)) return -1;
            if (viID <= 0) { loi = "Phải chọn ví cho giao dịch khởi phát của khoản."; return -1; }

            string loaiGD = laVayCuaToi ? GiaoDichLoai.NhanVay : GiaoDichLoai.ChoVay;
            // NhanVay = TienVao (ViDichID), ChoVay = TienRa (ViNguonID) — Bảng 2.2e
            int? viNguon = laVayCuaToi ? (int?)null : viID;
            int? viDich = laVayCuaToi ? (int?)viID : null;
            DateTime ngay = k.NgayPhatSinh.Date;

            string sqlInsertKhoan = @"INSERT INTO KhoanVayNo
                (NguoiDungID, LoaiKhoan, TenNguoiLienQuan, SoTienGoc, NgayPhatSinh,
                 NgayDenHan, LaiSuat, GhiChu, TrangThai)
                VALUES (@NguoiDungID, @LoaiKhoan, @TenNguoiLienQuan, @SoTienGoc, @NgayPhatSinh,
                        @NgayDenHan, @LaiSuat, @GhiChu, N'DangXuLy');
                SELECT CAST(SCOPE_IDENTITY() AS int);";

            string sqlInsertGD = @"INSERT INTO GiaoDich
                (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, KhoanVayNoID, ViNguonID, ViDichID, GhiChu)
                VALUES (@NguoiDungID, @LoaiGD, @SoTien, @Ngay, @KhoanVayNoID, @ViNguon, @ViDich, @GhiChu);";

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    int idKhoan;
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(sqlInsertKhoan, conn, tx))
                        {
                            GanThamSo(cmd, k);
                            object kq = cmd.ExecuteScalar();
                            idKhoan = Convert.ToInt32(kq);
                        }
                        using (SqlCommand cmd = new SqlCommand(sqlInsertGD, conn, tx))
                        {
                            cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                            cmd.Parameters.Add("@LoaiGD", SqlDbType.NVarChar, 20).Value = loaiGD;
                            SqlParameter p = cmd.Parameters.Add("@SoTien", SqlDbType.Decimal);
                            p.Precision = 18; p.Scale = 2; p.Value = k.SoTienGoc;
                            cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay;
                            cmd.Parameters.Add("@KhoanVayNoID", SqlDbType.Int).Value = idKhoan;
                            cmd.Parameters.Add("@ViNguon", SqlDbType.Int).Value = (object)viNguon ?? DBNull.Value;
                            cmd.Parameters.Add("@ViDich", SqlDbType.Int).Value = (object)viDich ?? DBNull.Value;
                            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 500).Value =
                                string.IsNullOrWhiteSpace(k.GhiChu) ? (object)DBNull.Value : k.GhiChu;
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                        return idKhoan;
                    }
                    catch (SqlException ex)
                    {
                        tx.Rollback();
                        loi = LoiSQL(ex, laVayCuaToi
                            ? "Không thêm được khoản vay." : "Không thêm được khoản cho vay.");
                        return -1;
                    }
                }
            }
        }

        /// <summary>Sửa thông tin khoản. Chặn sửa Số tiền gốc sau khi khoản đã có giao dịch
        /// trả nợ/thu hồi (lệch tiến độ) — tất cả câu UPDATE khoá theo (NguoiDungID, ID).</summary>
        public static bool Sua(KhoanVayNo k, out string loi)
        {
            loi = null;
            if (!KiemTraHopLe(k, out loi)) return false;

            string dem = @"SELECT COUNT(*) FROM GiaoDich
                           WHERE NguoiDungID = @NguoiDungID AND KhoanVayNoID = @KhoanVayNoID
                             AND LoaiGiaoDich IN (N'TraNo', N'ThuHoiNo');";
            string sql = @"UPDATE KhoanVayNo
                           SET TenNguoiLienQuan = @TenNguoiLienQuan, SoTienGoc = @SoTienGoc,
                               NgayPhatSinh = @NgayPhatSinh, NgayDenHan = @NgayDenHan,
                               LaiSuat = @LaiSuat, GhiChu = @GhiChu
                           WHERE KhoanVayNoID = @KhoanVayNoID AND NguoiDungID = @NguoiDungID;";

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(dem, conn))
                {
                    cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                    cmd.Parameters.Add("@KhoanVayNoID", SqlDbType.Int).Value = k.KhoanVayNoID;
                    if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                    {
                        // đã có trả nợ/thu hồi → không cho đổi tiền gốc (tiến độ phải nhất quán)
                        // nhưng vẫn cho sửa các thuộc tính khác; thực hiện ở bước 2 bên dưới.
                        k.SoTienGoc = LaySoTienGocHienTai(k.KhoanVayNoID);
                    }
                }
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    GanThamSo(cmd, k);
                    cmd.Parameters.Add("@KhoanVayNoID", SqlDbType.Int).Value = k.KhoanVayNoID;
                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        loi = "Không tìm thấy khoản vay thuộc tài khoản hiện tại.";
                        return false;
                    }
                    return true;
                }
            }
        }

        /// <summary>Xóa khoản. CHẶN khi còn giao dịch trỏ vào (FK ghép) — yêu cầu xóa giao
        /// dịch tương ứng trước, không tự xóa lịch sử tiền của người dùng.</summary>
        public static bool Xoa(int khoanVayNoID, out string loi)
        {
            loi = null;
            string dem = @"SELECT COUNT(*) FROM GiaoDich
                           WHERE NguoiDungID = @NguoiDungID AND KhoanVayNoID = @KhoanVayNoID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(dem, conn))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                cmd.Parameters.Add("@KhoanVayNoID", SqlDbType.Int).Value = khoanVayNoID;
                conn.Open();
                if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                {
                    loi = "Khoản vẫn còn giao dịch gắn vào (nhận/cho/trả/thu hồi). Xóa các giao dịch tương ứng trước khi xóa khoản.";
                    return false;
                }
            }

            string sql = @"DELETE FROM KhoanVayNo
                           WHERE KhoanVayNoID = @KhoanVayNoID AND NguoiDungID = @NguoiDungID;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@KhoanVayNoID", SqlDbType.Int).Value = khoanVayNoID;
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                if (cmd.ExecuteNonQuery() != 1)
                {
                    loi = "Không tìm thấy khoản vay thuộc tài khoản hiện tại.";
                    return false;
                }
                return true;
            }
        }

        // ---------------- Trả nợ / Thu hồi ----------------

        /// <summary>
        /// Trả nợ một khoản 'Vay': tạo GD 'TraNo' (tiền ra ví nguồn) + dòng LichSuTraNo
        /// (tách TienGoc + TienLaiPhi = SoTien) trong MỘT transaction; đồng bộ trạng thái khoản.
        /// Trả về ID GD mới hoặc -1 kèm loi.
        /// </summary>
        public static int TraNo(int khoanVayNoID, decimal tienGoc, decimal tienLaiPhi,
                                int viNguonID, DateTime ngay, string ghiChu, out string loi)
        {
            loi = null;
            if (!ValidationHelper.TienTraNo(tienGoc, tienLaiPhi,
                tienGoc + tienLaiPhi, out loi)) return -1;
            if (!ValidationHelper.TraNoPhaiCoKhoanVay(khoanVayNoID, out loi)) return -1;
            if (viNguonID <= 0) { loi = "Phải chọn ví chi ra tiền trả nợ."; return -1; }
            if (ngay.Date > DateTime.Today) { loi = "Ngày không được ở tương lai."; return -1; }

            decimal soTien = tienGoc + tienLaiPhi;

            string sqlBound = @"SELECT COUNT(*) FROM KhoanVayNo
                                WHERE KhoanVayNoID = @ID AND NguoiDungID = @ND AND LoaiKhoan = N'Vay';";
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                using (SqlCommand c0 = new SqlCommand(sqlBound, conn))
                {
                    c0.Parameters.Add("@ID", SqlDbType.Int).Value = khoanVayNoID;
                    c0.Parameters.Add("@ND", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                    if (Convert.ToInt32(c0.ExecuteScalar()) == 0)
                    {
                        loi = "Khoản không tồn tại hoặc không phải khoản ĐANG NỢ.";
                        return -1;
                    }
                }

                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    int gdID;
                    try
                    {
                        string sqlGD = @"INSERT INTO GiaoDich
                            (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich,
                             KhoanVayNoID, ViNguonID, ViDichID, GhiChu)
                            VALUES (@ND, N'TraNo', @SoTien, @Ngay, @ID, @ViNguon, NULL, @GhiChu);
                            SELECT CAST(SCOPE_IDENTITY() AS int);";
                        using (SqlCommand cmd = new SqlCommand(sqlGD, conn, tx))
                        {
                            cmd.Parameters.Add("@ND", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                            SqlParameter p = cmd.Parameters.Add("@SoTien", SqlDbType.Decimal);
                            p.Precision = 18; p.Scale = 2; p.Value = soTien;
                            cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay.Date;
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = khoanVayNoID;
                            cmd.Parameters.Add("@ViNguon", SqlDbType.Int).Value = viNguonID;
                            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 500).Value =
                                string.IsNullOrWhiteSpace(ghiChu) ? (object)DBNull.Value : ghiChu;
                            gdID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        string sqlLS = @"INSERT INTO LichSuTraNo
                            (NguoiDungID, KhoanVayNoID, GiaoDichID, NgayThanhToan, TienGoc, TienLaiPhi, GhiChu)
                            VALUES (@ND, @ID, @GD, @Ngay, @Goc, @Lai, @GhiChu);";
                        using (SqlCommand cmd = new SqlCommand(sqlLS, conn, tx))
                        {
                            cmd.Parameters.Add("@ND", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = khoanVayNoID;
                            cmd.Parameters.Add("@GD", SqlDbType.Int).Value = gdID;
                            cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay.Date;
                            cmd.Parameters.Add("@Goc", SqlDbType.Decimal).Value = tienGoc;
                            cmd.Parameters.Add("@Lai", SqlDbType.Decimal).Value = tienLaiPhi;
                            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 500).Value =
                                string.IsNullOrWhiteSpace(ghiChu) ? (object)DBNull.Value : ghiChu;
                            cmd.ExecuteNonQuery();
                        }

                        DongBoTrangThai(conn, tx, khoanVayNoID);
                        tx.Commit();
                        return gdID;
                    }
                    catch (SqlException ex)
                    {
                        tx.Rollback();
                        loi = LoiSQL(ex, "Không ghi được trả nợ.");
                        return -1;
                    }
                }
            }
        }

        /// <summary>
        /// Thu hồi một khoản 'ChoVay': tạo GD 'ThuHoiNo' (tiền vào ví đích) trong MỘT
        /// transaction — theo đặc tả KHÔNG tạo LichSuTraNo (chỉ TraNo mới có lịch sử chi tiết).
        /// </summary>
        public static int ThuHoi(int khoanVayNoID, decimal soTien,
                                 int viDichID, DateTime ngay, string ghiChu, out string loi)
        {
            loi = null;
            if (soTien <= 0m) { loi = "Số tiền phải lớn hơn 0."; return -1; }
            if (soTien > ValidationHelper.SoTienToiDa) { loi = "Số tiền vượt giới hạn."; return -1; }
            if (viDichID <= 0) { loi = "Phải chọn ví nhận tiền thu hồi."; return -1; }
            if (ngay.Date > DateTime.Today) { loi = "Ngày không được ở tương lai."; return -1; }

            string sqlBound = @"SELECT COUNT(*) FROM KhoanVayNo
                                WHERE KhoanVayNoID = @ID AND NguoiDungID = @ND AND LoaiKhoan = N'ChoVay';";
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                using (SqlCommand c0 = new SqlCommand(sqlBound, conn))
                {
                    c0.Parameters.Add("@ID", SqlDbType.Int).Value = khoanVayNoID;
                    c0.Parameters.Add("@ND", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                    if (Convert.ToInt32(c0.ExecuteScalar()) == 0)
                    {
                        loi = "Khoản không tồn tại hoặc không phải khoản CHO VAY.";
                        return -1;
                    }
                }

                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    int gdID;
                    try
                    {
                        string sqlGD = @"INSERT INTO GiaoDich
                            (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich,
                             KhoanVayNoID, ViNguonID, ViDichID, GhiChu)
                            VALUES (@ND, N'ThuHoiNo', @SoTien, @Ngay, @ID, NULL, @ViDich, @GhiChu);
                            SELECT CAST(SCOPE_IDENTITY() AS int);";
                        using (SqlCommand cmd = new SqlCommand(sqlGD, conn, tx))
                        {
                            cmd.Parameters.Add("@ND", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                            SqlParameter p = cmd.Parameters.Add("@SoTien", SqlDbType.Decimal);
                            p.Precision = 18; p.Scale = 2; p.Value = soTien;
                            cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay.Date;
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = khoanVayNoID;
                            cmd.Parameters.Add("@ViDich", SqlDbType.Int).Value = viDichID;
                            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 500).Value =
                                string.IsNullOrWhiteSpace(ghiChu) ? (object)DBNull.Value : ghiChu;
                            gdID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        DongBoTrangThai(conn, tx, khoanVayNoID);
                        tx.Commit();
                        return gdID;
                    }
                    catch (SqlException ex)
                    {
                        tx.Rollback();
                        loi = LoiSQL(ex, "Không ghi được thu hồi nợ.");
                        return -1;
                    }
                }
            }
        }

        /// <summary>Cập nhật TrangThai của khoản theo công nợ thực tế (đã trả đủ gốc → HoanThanh).</summary>
        private static void DongBoTrangThai(SqlConnection conn, SqlTransaction tx, int khoanVayNoID)
        {
            string sql = @"UPDATE K
                           SET K.TrangThai = CASE
                               WHEN ISNULL(T.GocDaTra, 0) + ISNULL(H.DaHoi, 0) >= K.SoTienGoc
                                   THEN N'HoanThanh' ELSE N'DangXuLy' END
                           FROM KhoanVayNo K
                           LEFT JOIN (SELECT KhoanVayNoID, SUM(TienGoc) AS GocDaTra
                                      FROM LichSuTraNo
                                      WHERE NguoiDungID = @ND
                                      GROUP BY KhoanVayNoID) T
                                  ON T.KhoanVayNoID = K.KhoanVayNoID
                           LEFT JOIN (SELECT KhoanVayNoID, SUM(SoTien) AS DaHoi
                                      FROM GiaoDich
                                      WHERE NguoiDungID = @ND AND LoaiGiaoDich = N'ThuHoiNo'
                                      GROUP BY KhoanVayNoID) H
                                  ON H.KhoanVayNoID = K.KhoanVayNoID
                           WHERE K.KhoanVayNoID = @ID AND K.NguoiDungID = @ND;";
            using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
            {
                cmd.Parameters.Add("@ND", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = khoanVayNoID;
                cmd.ExecuteNonQuery();
            }
        }

        // ---------------- Hỗ trợ ----------------

        private static decimal LaySoTienGocHienTai(int khoanVayNoID)
        {
            string sql = @"SELECT SoTienGoc FROM KhoanVayNo
                           WHERE KhoanVayNoID = @ID AND NguoiDungID = @ND;";
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = khoanVayNoID;
                cmd.Parameters.Add("@ND", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
                conn.Open();
                object kq = cmd.ExecuteScalar();
                return kq == null || kq == DBNull.Value ? 0m : Convert.ToDecimal(kq);
            }
        }

        private static bool KiemTraHopLe(KhoanVayNo k, out string loi)
        {
            loi = null;
            if (k == null) { loi = "Dữ liệu khoản vay trống."; return false; }
            if (string.IsNullOrWhiteSpace(k.TenNguoiLienQuan))
            { loi = "Tên người liên quan không được bỏ trống."; return false; }
            if (k.TenNguoiLienQuan.Trim().Length > 150)
            { loi = "Tên người liên quan tối đa 150 ký tự."; return false; }
            if (k.SoTienGoc <= 0m) { loi = "Số tiền gốc phải lớn hơn 0."; return false; }
            if (k.SoTienGoc > ValidationHelper.SoTienToiDa)
            { loi = "Số tiền gốc vượt giới hạn."; return false; }
            if (k.LaiSuat.HasValue && k.LaiSuat.Value < 0m)
            { loi = "Lãi suất không được âm."; return false; }
            if (k.NgayDenHan.HasValue && k.NgayDenHan.Value < k.NgayPhatSinh.Date)
            { loi = "Ngày đáo hạn không được trước ngày phát sinh."; return false; }
            if (k.NgayPhatSinh.Date > DateTime.Today)
            { loi = "Ngày phát sinh không được ở tương lai."; return false; }
            return true;
        }

        private static void GanThamSo(SqlCommand cmd, KhoanVayNo k)
        {
            cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int).Value = CurrentUser.NguoiDungID;
            cmd.Parameters.Add("@LoaiKhoan", SqlDbType.NVarChar, 10).Value = k.LoaiKhoan;
            cmd.Parameters.Add("@TenNguoiLienQuan", SqlDbType.NVarChar, 150).Value = k.TenNguoiLienQuan.Trim();
            SqlParameter p = cmd.Parameters.Add("@SoTienGoc", SqlDbType.Decimal);
            p.Precision = 18; p.Scale = 2; p.Value = k.SoTienGoc;
            cmd.Parameters.Add("@NgayPhatSinh", SqlDbType.Date).Value = k.NgayPhatSinh.Date;
            cmd.Parameters.Add("@NgayDenHan", SqlDbType.Date).Value =
                (object)k.NgayDenHan ?? DBNull.Value;
            SqlParameter pl = cmd.Parameters.Add("@LaiSuat", SqlDbType.Decimal);
            pl.Precision = 9; pl.Scale = 4;
            pl.Value = (object)k.LaiSuat ?? DBNull.Value;
            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 500).Value =
                string.IsNullOrWhiteSpace(k.GhiChu) ? (object)DBNull.Value : k.GhiChu;
        }

        private static KhoanVayNo DocKhoan(SqlDataReader r)
        {
            int iDenHan = r.GetOrdinal("NgayDenHan");
            int iLai = r.GetOrdinal("LaiSuat");
            int iGhiChu = r.GetOrdinal("GhiChu");
            return new KhoanVayNo
            {
                KhoanVayNoID = r.GetInt32(r.GetOrdinal("KhoanVayNoID")),
                NguoiDungID = r.GetInt32(r.GetOrdinal("NguoiDungID")),
                LoaiKhoan = r.GetString(r.GetOrdinal("LoaiKhoan")),
                TenNguoiLienQuan = r.GetString(r.GetOrdinal("TenNguoiLienQuan")),
                SoTienGoc = r.GetDecimal(r.GetOrdinal("SoTienGoc")),
                NgayPhatSinh = r.GetDateTime(r.GetOrdinal("NgayPhatSinh")),
                NgayDenHan = r.IsDBNull(iDenHan) ? (DateTime?)null : r.GetDateTime(iDenHan),
                LaiSuat = r.IsDBNull(iLai) ? (decimal?)null : r.GetDecimal(iLai),
                GhiChu = r.IsDBNull(iGhiChu) ? null : r.GetString(iGhiChu),
                TrangThai = r.GetString(r.GetOrdinal("TrangThai"))
            };
        }

        private static string LoiSQL(SqlException ex, string thongDiepGoc)
        {
            if (ex.Number == 547) return thongDiepGoc + " Dữ liệu không thỏa ràng buộc (CHECK/FK).";
            return thongDiepGoc;
        }
    }
}
