/* ============================================================
   005_Views.sql — TV1 sở hữu (công thức dùng chung, mục 2.6 của Tài liệu 02)
   3 view dùng chung cho mọi phân hệ — KHÔNG tự viết lại công thức
   2.6 / tổng hợp thu chi trong service riêng:
     dbo.vwSoDuVi                 : số dư mỗi ví theo công thức 2.6 (tính động)
     dbo.vwThuChiThang            : tổng thu/chi mỗi tháng — CHỈ loại Thu + Chi (QD02)
     dbo.vwSoTienChiDanhMucThang  : đã chi theo danh mục trong 1 tháng (ngân sách)
   Có thể tự chạy lại (idempotent — CREATE OR ALTER).
   ============================================================ */
USE QuanLyChiTieuDB;
GO

/* ---------- 1. Số dư mỗi ví (công thức 2.6) ----------
   Số dư = SoDuBanDau + tổng tiền vào (ViDichID) − tổng tiền ra (ViNguonID).
   Trả về cả ví đã ngừng (TrangThai = 0) để lịch sử vẫn hiện số dư cuối (FR03). */
CREATE OR ALTER VIEW dbo.vwSoDuVi
AS
SELECT
    V.ViTienID,
    V.NguoiDungID,
    V.TenVi,
    V.LoaiVi,
    V.TrangThai,
    V.SoDuBanDau,
    V.SoDuBanDau
      + ISNULL((SELECT SUM(G.SoTien) FROM GiaoDich G WHERE G.ViDichID = V.ViTienID AND G.NguoiDungID = V.NguoiDungID), 0)
      - ISNULL((SELECT SUM(G.SoTien) FROM GiaoDich G WHERE G.ViNguonID = V.ViTienID AND G.NguoiDungID = V.NguoiDungID), 0) AS SoDuHienTai
FROM ViTien V;
GO

/* ---------- 2. Tổng thu / chi mỗi tháng (chỉ Thu + Chi — Bảng 2.2e) ----------
   Chuyển tiền, vay nợ, đóng góp KHÔNG xuất hiện ở view này. dùng cho
   Dashboard KPI + biểu đồ cột thu/chi 6 tháng của phân hệ Thống kê. */
CREATE OR ALTER VIEW dbo.vwThuChiThang
AS
SELECT
    G.NguoiDungID,
    YEAR(G.NgayGiaoDich)  AS Nam,
    MONTH(G.NgayGiaoDich) AS Thang,
    SUM(CASE WHEN G.LoaiGiaoDich = 'Thu' THEN G.SoTien ELSE 0 END) AS TongThu,
    SUM(CASE WHEN G.LoaiGiaoDich = 'Chi' THEN G.SoTien ELSE 0 END) AS TongChi
FROM GiaoDich G
WHERE G.LoaiGiaoDich IN ('Thu', 'Chi')
GROUP BY G.NguoiDungID, YEAR(G.NgayGiaoDich), MONTH(G.NgayGiaoDich);
GO

/* ---------- 3. Đã chi theo danh mục mỗi tháng (phân hệ Ngân sách) ----------
   Đã chi chỉ cộng giao dịch loại Chi thuộc danh mục đang đặt hạn mức trong kỳ.
   Chuyển tiền / trả nợ / đóng góp không bao giờ xuất hiện (Tài liệu 1.5F). */
CREATE OR ALTER VIEW dbo.vwSoTienChiDanhMucThang
AS
SELECT
    G.NguoiDungID,
    G.DanhMucID,
    DM.TenDanhMuc,
    DM.LoaiDanhMuc,
    YEAR(G.NgayGiaoDich)  AS Nam,
    MONTH(G.NgayGiaoDich) AS Thang,
    SUM(G.SoTien)         AS DaChi
FROM GiaoDich G
JOIN DanhMuc DM ON DM.DanhMucID = G.DanhMucID AND DM.NguoiDungID = G.NguoiDungID
WHERE G.LoaiGiaoDich = 'Chi'
GROUP BY G.NguoiDungID, G.DanhMucID, DM.TenDanhMuc, DM.LoaiDanhMuc,
         YEAR(G.NgayGiaoDich), MONTH(G.NgayGiaoDich);
GO

/* ---------- Kiểm tra nhanh sau khi tạo view ----------
SELECT TOP 10 NguoiDungID, TenVi, TrangThai, SoDuBanDau, SoDuHienTai
FROM dbo.vwSoDuVi ORDER BY NguoiDungID, ViTienID;

SELECT NguoiDungID, Nam, Thang, TongThu, TongChi
FROM dbo.vwThuChiThang ORDER BY Nam, Thang;

SELECT TOP 10 TenDanhMuc, Nam, Thang, DaChi
FROM dbo.vwSoTienChiDanhMucThang
WHERE NguoiDungID = 1 ORDER BY Nam DESC, Thang DESC, DaChi DESC;
------------------------------------------------------------- */
