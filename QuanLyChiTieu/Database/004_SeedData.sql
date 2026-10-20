/* ============================================================
   004_SeedData.sql — Dữ liệu demo (Tài liệu 08, mục 8.5; TV1 sở hữu)
   Yêu cầu đã đáp ứng:
   - 3 tài khoản, hash PBKDF2 100.000 vòng + salt (QD08):
       truongnhom / chitieu123   (tài khoản demo chính, dữ liệu "đẹp")
       demo       / chitieu123   (tài khoản dự phòng demo)
       thanhvien2 / sinhvien123  (kiểm chứng tách dữ liệu từng tài khoản)
   - 4 ví đủ loại, số dư ban đầu đa dạng, 1 ví đã ngừng.
   - ≥60 giao dịch Thu/Chi trải 2 tháng gần nhất (tháng hiện tại: dữ liệu
     tường minh khớp 3 vùng ngân sách; tháng trước: bộ 40 giao dịch sinh tự động).
   - ≥1 ChuyenTien, ngân sách đủ 3 vùng (<80%, ≥80%, vượt 100%).
   - Mục tiêu tiết kiệm (~70% tiến độ, có Đóng góp + Rút), khoản vay
     NhanVay/TraNo + LichSuTraNo tách gốc–lãi, khoản cho vay hoàn tất.
   Chạy: 001 → 002 → 003 → 004. Chạy lại 004 là an toàn (xóa sạch trước khi seed).
   ============================================================ */
USE QuanLyChiTieuDB;
GO

/* ---------- Dọn dữ liệu cũ theo thứ tự FK (idempotent) ---------- */
DELETE FROM LichSuTraNo;
DELETE FROM GiaoDich;
DELETE FROM NganSach;
DELETE FROM MucTieuTietKiem;
DELETE FROM KhoanVayNo;
DELETE FROM ViTien;
DELETE FROM DanhMuc;
DELETE FROM NguoiDung;
GO

/* Reset identity để ID dự đoán được khi chạy lại */
DBCC CHECKIDENT ('LichSuTraNo',     RESEED, 0);
DBCC CHECKIDENT ('GiaoDich',        RESEED, 0);
DBCC CHECKIDENT ('NganSach',        RESEED, 0);
DBCC CHECKIDENT ('MucTieuTietKiem', RESEED, 0);
DBCC CHECKIDENT ('KhoanVayNo',      RESEED, 0);
DBCC CHECKIDENT ('ViTien',          RESEED, 0);
DBCC CHECKIDENT ('DanhMuc',         RESEED, 0);
DBCC CHECKIDENT ('NguoiDung',       RESEED, 0);
GO

/* ---------- 1. Tài khoản (một batch duy nhất để dùng biến) ---------- */
DECLARE @nhanNhomID int, @demoID int, @thanhvien2ID int;

INSERT INTO NguoiDung (TenDangNhap, MatKhauHash, TenHienThi, Email)
VALUES (N'truongnhom',
        N'PBKDF2.100000.lksrkG8aQ7N7h57tpsx0ug==.hNyX1QuCSyqHqqga328z7TSEdiVObUcYdFRqHBD1hbM=',
        N'Nguyễn Văn A', N'truongnhom@demo.vn');
SET @nhanNhomID = SCOPE_IDENTITY();

INSERT INTO NguoiDung (TenDangNhap, MatKhauHash, TenHienThi, Email)
VALUES (N'demo',
        N'PBKDF2.100000.lksrkG8aQ7N7h57tpsx0ug==.hNyX1QuCSyqHqqga328z7TSEdiVObUcYdFRqHBD1hbM=',
        N'Tài khoản Demo', NULL);
SET @demoID = SCOPE_IDENTITY();

INSERT INTO NguoiDung (TenDangNhap, MatKhauHash, TenHienThi, Email)
VALUES (N'thanhvien2',
        N'PBKDF2.100000.KgvNT1Dq00VWHwXTmaflcQ==.aO78fzeZlVcZNi3HfZCHlafsUK3gLeYBzK3+CzxsVI0=',
        N'Phạm Thị B', NULL);
SET @thanhvien2ID = SCOPE_IDENTITY();

/* ---------- 2. Danh mục (người 1) ---------- */
DECLARE @dmLuong int, @dmThuong int, @dmHoanTien int, @dmThuKhac int;
DECLARE @dmAnUong int, @dmDiChuyen int, @dmNhaO int, @dmGiaiTri int, @dmKhac int;

INSERT INTO DanhMuc (NguoiDungID, TenDanhMuc, LoaiDanhMuc, MoTa) VALUES (@nhanNhomID, N'Lương',     'Thu', NULL); SET @dmLuong    = SCOPE_IDENTITY();
INSERT INTO DanhMuc (NguoiDungID, TenDanhMuc, LoaiDanhMuc, MoTa) VALUES (@nhanNhomID, N'Thưởng',    'Thu', NULL); SET @dmThuong   = SCOPE_IDENTITY();
INSERT INTO DanhMuc (NguoiDungID, TenDanhMuc, LoaiDanhMuc, MoTa) VALUES (@nhanNhomID, N'Hoàn tiền', 'Thu', N'Tiền hoàn trả khi mua hàng (QD10)'); SET @dmHoanTien = SCOPE_IDENTITY();
INSERT INTO DanhMuc (NguoiDungID, TenDanhMuc, LoaiDanhMuc, MoTa) VALUES (@nhanNhomID, N'Thu khác',  'Thu', NULL); SET @dmThuKhac  = SCOPE_IDENTITY();
INSERT INTO DanhMuc (NguoiDungID, TenDanhMuc, LoaiDanhMuc, MoTa) VALUES (@nhanNhomID, N'Ăn uống',   'Chi', NULL); SET @dmAnUong   = SCOPE_IDENTITY();
INSERT INTO DanhMuc (NguoiDungID, TenDanhMuc, LoaiDanhMuc, MoTa) VALUES (@nhanNhomID, N'Di chuyển', 'Chi', NULL); SET @dmDiChuyen = SCOPE_IDENTITY();
INSERT INTO DanhMuc (NguoiDungID, TenDanhMuc, LoaiDanhMuc, MoTa) VALUES (@nhanNhomID, N'Nhà ở',     'Chi', N'Tiền điện nước / thuê nhà'); SET @dmNhaO     = SCOPE_IDENTITY();
INSERT INTO DanhMuc (NguoiDungID, TenDanhMuc, LoaiDanhMuc, MoTa) VALUES (@nhanNhomID, N'Giải trí',  'Chi', NULL); SET @dmGiaiTri  = SCOPE_IDENTITY();
INSERT INTO DanhMuc (NguoiDungID, TenDanhMuc, LoaiDanhMuc, MoTa) VALUES (@nhanNhomID, N'Khác',      'Chi', NULL); SET @dmKhac     = SCOPE_IDENTITY();

/* ---------- 2b. Danh mục (người 3 — kiểm chứng tách dữ liệu) ---------- */
INSERT INTO DanhMuc (NguoiDungID, TenDanhMuc, LoaiDanhMuc) VALUES (@thanhvien2ID, N'Ăn uống', 'Chi');
INSERT INTO DanhMuc (NguoiDungID, TenDanhMuc, LoaiDanhMuc) VALUES (@thanhvien2ID, N'Lương',   'Thu');

/* ---------- 3. Ví tiền (người 1) ---------- */
DECLARE @viTienMat int, @viVCB int, @viMomo int, @viZaloPay int;

INSERT INTO ViTien (NguoiDungID, TenVi, LoaiVi, SoDuBanDau, TrangThai) VALUES (@nhanNhomID, N'Tiền mặt',              'TienMat',   5000000, 1); SET @viTienMat = SCOPE_IDENTITY();
INSERT INTO ViTien (NguoiDungID, TenVi, LoaiVi, SoDuBanDau, TrangThai) VALUES (@nhanNhomID, N'Ngân hàng Vietcombank', 'NganHang', 10000000, 1); SET @viVCB      = SCOPE_IDENTITY();
INSERT INTO ViTien (NguoiDungID, TenVi, LoaiVi, SoDuBanDau, TrangThai) VALUES (@nhanNhomID, N'Momo',                  'ViDienTu', 1000000, 1); SET @viMomo     = SCOPE_IDENTITY();
INSERT INTO ViTien (NguoiDungID, TenVi, LoaiVi, SoDuBanDau, TrangThai) VALUES (@nhanNhomID, N'Ví ZaloPay (cũ)',       'ViDienTu', 2000000, 0); SET @viZaloPay  = SCOPE_IDENTITY(); -- đã ngừng (FR03)

/* ---------- 3b. Ví (người 3) ---------- */
INSERT INTO ViTien (NguoiDungID, TenVi, LoaiVi, SoDuBanDau) VALUES (@thanhvien2ID, N'Tiền mặt TV2', 'TienMat', 2000000);

/* ---------- 4. Giao dịch tháng hiện tại — dữ liệu tường minh ---------- */
DECLARE @themGiaoDich table (GiaoDichID int);

INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, DanhMucID, ViNguonID, ViDichID, GhiChu)
OUTPUT inserted.GiaoDichID INTO @themGiaoDich (GiaoDichID)
VALUES (@nhanNhomID, 'Chi',  250000, DATEADD(DAY, 0,  CAST(GETDATE() AS DATE)), @dmAnUong,   @viTienMat, NULL, N'Ăn trưa ca làm'),
       (@nhanNhomID, 'Chi',  450000, DATEADD(DAY, -1, CAST(GETDATE() AS DATE)), @dmAnUong,   @viTienMat, NULL, N'Ăn tối cùng nhóm'),
       (@nhanNhomID, 'Chi',  400000, DATEADD(DAY, -2, CAST(GETDATE() AS DATE)), @dmAnUong,   @viMomo,    NULL, N'Café + đồ ăn tuần');

INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, DanhMucID, ViNguonID, ViDichID, GhiChu)
VALUES (@nhanNhomID, 'Chi',  350000, DATEADD(DAY, -3, CAST(GETDATE() AS DATE)), @dmAnUong,   @viTienMat, NULL, N'Ăn sáng cuối tuần'),
       (@nhanNhomID, 'Chi',  200000, DATEADD(DAY, -5, CAST(GETDATE() AS DATE)), @dmAnUong,   @viMomo,    NULL, N'Ăn tối đơn giản'),
       (@nhanNhomID, 'Chi',  200000, DATEADD(DAY, -8, CAST(GETDATE() AS DATE)), @dmAnUong,   @viVCB,     NULL, N'Trang trải ăn uống');

INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, DanhMucID, ViNguonID, ViDichID, GhiChu)
VALUES (@nhanNhomID, 'Chi',  120000, DATEADD(DAY, -6,  CAST(GETDATE() AS DATE)), @dmDiChuyen, @viTienMat, NULL, N'Xe ôm đi làm 3 ngày'),
       (@nhanNhomID, 'Chi',  100000, DATEADD(DAY, -9,  CAST(GETDATE() AS DATE)), @dmDiChuyen, @viMomo,    NULL, N'Grab về quê'),
       (@nhanNhomID, 'Chi', 100000, DATEADD(DAY, -12, CAST(GETDATE() AS DATE)), @dmDiChuyen, @viTienMat, NULL, N'Sạc pin xe điện');

INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, DanhMucID, ViNguonID, ViDichID, GhiChu)
VALUES (@nhanNhomID, 'Chi',  860000, DATEADD(DAY, -2,  CAST(GETDATE() AS DATE)), @dmNhaO,    @viVCB,     NULL, N'Tiền điện nước tháng này'),
       (@nhanNhomID, 'Chi',  740000, DATEADD(DAY, -7,  CAST(GETDATE() AS DATE)), @dmNhaO,    @viVCB,     NULL, N'Trả phần thuê nhà'),
       (@nhanNhomID, 'Chi', 1000000, DATEADD(DAY, -14, CAST(GETDATE() AS DATE)), @dmNhaO,    @viVCB,     NULL, N'Trang trí nhà cửa');

INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, DanhMucID, ViNguonID, ViDichID, GhiChu)
VALUES (@nhanNhomID, 'Chi', 400000, DATEADD(DAY, -4,  CAST(GETDATE() AS DATE)), @dmGiaiTri, @viMomo, NULL, N'Xem phim cuối tuần'),
       (@nhanNhomID, 'Chi', 400000, DATEADD(DAY, -11, CAST(GETDATE() AS DATE)), @dmGiaiTri, @viVCB,  NULL, N'Karaoke cùng bạn');

/* Ngân sách vượt hạn mức: Khác hạn mức 200.000 — đã chi 250.000 → 125% */
INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, DanhMucID, ViNguonID, GhiChu)
VALUES (@nhanNhomID, 'Chi', 250000, DATEADD(DAY, -13, CAST(GETDATE() AS DATE)), @dmKhac, @viMomo, N'Mua đồ dùng cá nhân');

/* Thu tháng hiện tại: 12.000.000 + 1.000.000 + 500.000 = 13.500.000 */
INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, DanhMucID, ViDichID, GhiChu)
VALUES (@nhanNhomID, 'Thu', 12000000, DATEADD(DAY, -4,  CAST(GETDATE() AS DATE)), @dmLuong,    @viVCB,      N'Lương tháng này'),
       (@nhanNhomID, 'Thu', 1000000, DATEADD(DAY, -1,  CAST(GETDATE() AS DATE)), @dmThuong,   @viTienMat,  N'Thưởng dự án'),
       (@nhanNhomID, 'Thu', 500000,  DATEADD(DAY, -10, CAST(GETDATE() AS DATE)), @dmThuKhac,  @viTienMat,  N'Bán đồ cũ');

/* Hoàn tiền = Thu loại danh mục "Hoàn tiền" (QD10) */
INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, DanhMucID, ViDichID, GhiChu)
VALUES (@nhanNhomID, 'Thu', 300000, DATEADD(DAY, -15, CAST(GETDATE() AS DATE)), @dmHoanTien, @viTienMat, N'Shop hoàn tiền đơn hàng sai bản');

/* Chuyển tiền giữa 2 ví — KHÔNG tính vào báo cáo thu chi (Bảng 2.2e) */
INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, ViNguonID, ViDichID, GhiChu)
VALUES (@nhanNhomID, 'ChuyenTien', 2000000, DATEADD(DAY, -3, CAST(GETDATE() AS DATE)), @viVCB, @viTienMat, N'Chuyển tiền mặt để chi tiêu');

/* ---------- 5. Vay nợ + tiết kiệm (ví liên quan theo Bảng 2.2e) ---------- */
DECLARE @khoanAnhMinh int, @khoanLan int, @mtLaptop int, @mtKhanCap int, @gdTraNo int;

INSERT INTO KhoanVayNo (NguoiDungID, LoaiKhoan, TenNguoiLienQuan, SoTienGoc, NgayPhatSinh, NgayDenHan, LaiSuat, GhiChu)
VALUES (@nhanNhomID, 'Vay', N'Anh Minh', 10000000, DATEADD(DAY, -2, CAST(GETDATE() AS DATE)), DATEADD(DAY, 90, CAST(GETDATE() AS DATE)), 12.0000, N'Vay xoay vòng, trả góp mỗi kỳ');
SET @khoanAnhMinh = SCOPE_IDENTITY();

INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, KhoanVayNoID, ViDichID, GhiChu)
VALUES (@nhanNhomID, 'NhanVay', 10000000, DATEADD(DAY, -2, CAST(GETDATE() AS DATE)), @khoanAnhMinh, @viVCB, N'Nhận tiền vay');

INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, KhoanVayNoID, ViNguonID, GhiChu)
VALUES (@nhanNhomID, 'TraNo', 3000000, DATEADD(DAY, -1, CAST(GETDATE() AS DATE)), @khoanAnhMinh, @viVCB, N'Trả góp kỳ 1 (gốc 2.000.000 + lãi 1.000.000)');
SET @gdTraNo = SCOPE_IDENTITY();

/* LichSuTraNo bắt buộc có GiaoDichID (QD04) — TienGoc + TienLaiPhi = SoTien = 3.000.000 */
INSERT INTO LichSuTraNo (NguoiDungID, KhoanVayNoID, GiaoDichID, NgayThanhToan, TienGoc, TienLaiPhi, GhiChu)
VALUES (@nhanNhomID, @khoanAnhMinh, @gdTraNo, DATEADD(DAY, -1, CAST(GETDATE() AS DATE)), 2000000, 1000000, N'Kỳ 1: gốc 2 triệu + lãi 1 triệu');

/* Khoản cho vay Lan: cho vay rồi thu hồi đủ → HoanThanh */
INSERT INTO KhoanVayNo (NguoiDungID, LoaiKhoan, TenNguoiLienQuan, SoTienGoc, NgayPhatSinh, LaiSuat, GhiChu)
VALUES (@nhanNhomID, 'ChoVay', N'Chị Lan', 1000000, DATEADD(DAY, -6, CAST(GETDATE() AS DATE)), NULL, N'Cho Lan mượn gấp');
SET @khoanLan = SCOPE_IDENTITY();

INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, KhoanVayNoID, ViNguonID, GhiChu)
VALUES (@nhanNhomID, 'ChoVay', 1000000, DATEADD(DAY, -6, CAST(GETDATE() AS DATE)), @khoanLan, @viTienMat, N'Cho Lan mượn gấp');

INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, KhoanVayNoID, ViDichID, GhiChu)
VALUES (@nhanNhomID, 'ThuHoiNo', 1000000, DATEADD(DAY, -5, CAST(GETDATE() AS DATE)), @khoanLan, @viTienMat, N'Lan trả đủ nợ');

UPDATE KhoanVayNo SET TrangThai = 'HoanThanh' WHERE KhoanVayNoID = @khoanLan;

/* Mục tiêu tiết kiệm: tiến độ = SUM(DongGop) − SUM(RutTietKiem) (QD05) */
INSERT INTO MucTieuTietKiem (NguoiDungID, TenMucTieu, SoTienMucTieu, NgayBatDau, NgayKetThuc, TrangThai)
VALUES (@nhanNhomID, N'Quỹ khẩn cấp', 10000000, DATEADD(DAY, -40, CAST(GETDATE() AS DATE)), DATEADD(DAY, 300, CAST(GETDATE() AS DATE)), 'DangThucHien');
SET @mtKhanCap = SCOPE_IDENTITY();
INSERT INTO MucTieuTietKiem (NguoiDungID, TenMucTieu, SoTienMucTieu, NgayBatDau, TrangThai)
VALUES (@nhanNhomID, N'Mua laptop', 20000000, DATEADD(DAY, -30, CAST(GETDATE() AS DATE)), 'DangThucHien');
SET @mtLaptop = SCOPE_IDENTITY();

/* Đóng góp quỹ khẩn cấp: 7.000.000 / 10.000.000 = 70% tiến độ (mục 8.5) */
INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, MucTieuID, ViNguonID, GhiChu)
VALUES (@nhanNhomID, 'DongGop', 7000000, DATEADD(DAY, -10, CAST(GETDATE() AS DATE)), @mtKhanCap, @viVCB, N'Góp quỹ khẩn cấp');
INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, MucTieuID, ViNguonID, GhiChu)
VALUES (@nhanNhomID, 'DongGop', 7000000, DATEADD(DAY, -20, CAST(GETDATE() AS DATE)), @mtLaptop, @viVCB, N'Góp quỹ tháng trước');

INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, MucTieuID, ViDichID, GhiChu)
VALUES (@nhanNhomID, 'RutTietKiem', 2000000, DATEADD(DAY, -8, CAST(GETDATE() AS DATE)), @mtLaptop, @viTienMat, N'Rút bớt quỹ về ví');

/* ---------- 6. Ngân sách 3 vùng: bình thường / cảnh báo 80% / đạt + vượt 100% ---------- */
INSERT INTO NganSach (NguoiDungID, DanhMucID, Thang, Nam, HanMuc)
VALUES (@nhanNhomID, @dmAnUong,   MONTH(CAST(GETDATE() AS DATE)), YEAR(CAST(GETDATE() AS DATE)), 2000000),  -- 1.850.000/2.000.000 = 92,5% → Cảnh báo 80%
       (@nhanNhomID, @dmDiChuyen, MONTH(CAST(GETDATE() AS DATE)), YEAR(CAST(GETDATE() AS DATE)), 500000),   -- 320.000/500.000     → Bình thường
       (@nhanNhomID, @dmNhaO,     MONTH(CAST(GETDATE() AS DATE)), YEAR(CAST(GETDATE() AS DATE)), 3000000),  -- 2.600.000/3.000.000 → Cảnh báo 80%
       (@nhanNhomID, @dmGiaiTri,  MONTH(CAST(GETDATE() AS DATE)), YEAR(CAST(GETDATE() AS DATE)), 800000),   -- 800.000/800.000     → Đạt hạn mức 100%
       (@nhanNhomID, @dmKhac,     MONTH(CAST(GETDATE() AS DATE)), YEAR(CAST(GETDATE() AS DATE)), 200000),   -- 250.000/200.000     → Vượt 100%
       (@nhanNhomID, @dmAnUong,   MONTH(DATEADD(MONTH, -1, CAST(GETDATE() AS DATE))), YEAR(DATEADD(MONTH, -1, CAST(GETDATE() AS DATE))), 1500000);

/* ---------- 7. Bộ 40 giao dịch tháng trước (tổng ≥ 60 Thu/Chi, 8.5) ---------- */
DECLARE @i int = 1;
DECLARE @dmCa4 NVARCHAR(20);
IF OBJECT_ID('tempdb..#i') IS NOT NULL DROP TABLE #i;
SELECT TOP 40 IDENTITY(int,1,1) AS so, CAST('' AS nvarchar(20)) AS loaiChon INTO #i FROM sys.objects o1 CROSS JOIN sys.objects o2 ORDER BY (SELECT NULL);

UPDATE t SET loaiChon =
    CASE (t.so % 4)
        WHEN 0 THEN N'Ăn uống'
        WHEN 1 THEN N'Di chuyển'
        WHEN 2 THEN N'Nhà ở'
        ELSE        N'Giải trí'
    END
FROM #i t;

INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, DanhMucID, ViNguonID, ViDichID, GhiChu)
SELECT @nhanNhomID,
       CASE WHEN t.so % 7 = 0 THEN 'Thu' ELSE 'Chi' END,
       CASE WHEN t.so % 7 = 0 THEN 400000 ELSE (t.so % 5 + 1) * 100000 + 50000 END,
       DATEADD(DAY, -(t.so + 31), CAST(GETDATE() AS DATE)),
       DM.DanhMucID,
       CASE WHEN t.so % 3 = 0 THEN @viTienMat WHEN t.so % 3 = 1 THEN @viVCB ELSE @viMomo END,
       NULL,
       CASE WHEN t.so % 7 = 0 THEN N'Thu lẻ tháng trước' ELSE N'Chi tiêu hàng ngày (demo)' END
FROM #i t
JOIN DanhMuc DM ON DM.NguoiDungID = @nhanNhomID AND DM.TenDanhMuc = t.loaiChon;

/* ---------- 8. Dữ liệu riêng người 3 (tách biệt với người 1) ---------- */
INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, DanhMucID, ViDichID, GhiChu)
VALUES (@thanhvien2ID, 'Thu', 3000000, DATEADD(DAY, -20, CAST(GETDATE() AS DATE)),
        (SELECT DanhMucID FROM DanhMuc WHERE NguoiDungID = @thanhvien2ID AND TenDanhMuc = N'Lương'),
        (SELECT ViTienID  FROM ViTien  WHERE NguoiDungID = @thanhvien2ID), N'Lương làm thêm');

INSERT INTO GiaoDich (NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, DanhMucID, ViNguonID, GhiChu)
VALUES (@thanhvien2ID, 'Chi', 150000, DATEADD(DAY, -10, CAST(GETDATE() AS DATE)),
        (SELECT DanhMucID FROM DanhMuc WHERE NguoiDungID = @thanhvien2ID AND TenDanhMuc = N'Ăn uống'),
        (SELECT ViTienID  FROM ViTien  WHERE NguoiDungID = @thanhvien2ID), N'Ăn trưa'),
       (@thanhvien2ID, 'Chi', 90000,  DATEADD(DAY, -5,  CAST(GETDATE() AS DATE)),
        (SELECT DanhMucID FROM DanhMuc WHERE NguoiDungID = @thanhvien2ID AND TenDanhMuc = N'Ăn uống'),
        (SELECT ViTienID  FROM ViTien  WHERE NguoiDungID = @thanhvien2ID), N'Cà phê');

GO
/* ---------- Kiểm tra nhanh sau seed (chạy tự do, không bắt buộc) ----------
SELECT NguoiDungID, TenDangNhap, LEFT(MatKhauHash, 20) + '...' AS MatKhau FROM NguoiDung;
SELECT LoaiGiaoDich, COUNT(*) AS SoGD FROM GiaoDich GROUP BY LoaiGiaoDich;
SELECT DM.TenDanhMuc, NS.HanMuc,
       (SELECT SUM(G.SoTien) FROM GiaoDich G
         WHERE G.NguoiDungID = NS.NguoiDungID AND G.DanhMucID = NS.DanhMucID
           AND LoaiGiaoDich = 'Chi' AND YEAR(NgayGiaoDich) = NS.Nam AND MONTH(NgayGiaoDich) = NS.Thang) AS DaChi
FROM NganSach NS JOIN DanhMuc DM ON DM.DanhMucID = NS.DanhMucID;
------------------------------------------------------------ */
