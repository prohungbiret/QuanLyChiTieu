USE QuanLyChiTieuDB;
GO
CREATE TABLE NguoiDung (
    NguoiDungID INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50)  NOT NULL,
    MatKhauHash NVARCHAR(255) NOT NULL,
    TenHienThi  NVARCHAR(100) NOT NULL,
    Email       NVARCHAR(255) NULL,
    NgayTao     DATETIME2     NOT NULL CONSTRAINT DF_ND_NgayTao  DEFAULT SYSDATETIME(),
    TrangThai   BIT           NOT NULL CONSTRAINT DF_ND_TrangThai DEFAULT 1
);

CREATE TABLE DanhMuc (
    DanhMucID   INT IDENTITY(1,1) PRIMARY KEY,
    NguoiDungID INT NOT NULL,
    TenDanhMuc  NVARCHAR(100) NOT NULL,
    LoaiDanhMuc NVARCHAR(10)  NOT NULL,      -- CHECK ở 003
    MoTa        NVARCHAR(255) NULL,
    TrangThai   BIT NOT NULL CONSTRAINT DF_DM_TrangThai DEFAULT 1
);

CREATE TABLE ViTien (
    ViTienID    INT IDENTITY(1,1) PRIMARY KEY,
    NguoiDungID INT NOT NULL,
    TenVi       NVARCHAR(100) NOT NULL,
    LoaiVi      NVARCHAR(30)  NOT NULL,      -- TienMat / NganHang / ViDienTu
    SoDuBanDau  DECIMAL(18,2) NOT NULL CONSTRAINT DF_VT_SoDuBanDau DEFAULT 0,
    TrangThai   BIT NOT NULL CONSTRAINT DF_VT_TrangThai DEFAULT 1
);

CREATE TABLE GiaoDich (
    GiaoDichID   INT IDENTITY(1,1) PRIMARY KEY,
    NguoiDungID  INT NOT NULL,
    LoaiGiaoDich NVARCHAR(20)  NOT NULL,     -- CHECK 9 loại ở 003
    SoTien       DECIMAL(18,2) NOT NULL,
    NgayGiaoDich DATE NOT NULL,              -- DATE, không DATETIME (QD09)
    DanhMucID    INT NULL,
    ViNguonID    INT NULL,                   -- tiền ra khỏi ví
    ViDichID     INT NULL,                   -- tiền vào ví
    KhoanVayNoID INT NULL,
    MucTieuID    INT NULL,
    GhiChu       NVARCHAR(500) NULL,
    NgayTao      DATETIME2 NOT NULL CONSTRAINT DF_GD_NgayTao DEFAULT SYSDATETIME()
);

CREATE TABLE NganSach (
    NganSachID  INT IDENTITY(1,1) PRIMARY KEY,
    NguoiDungID INT NOT NULL,
    DanhMucID   INT NOT NULL,                -- phải là danh mục Chi (app-level, Bảng 2.3c)
    Thang       INT NOT NULL,
    Nam         INT NOT NULL,
    HanMuc      DECIMAL(18,2) NOT NULL
);

CREATE TABLE MucTieuTietKiem (
    MucTieuID     INT IDENTITY(1,1) PRIMARY KEY,
    NguoiDungID   INT NOT NULL,
    TenMucTieu    NVARCHAR(150) NOT NULL,
    SoTienMucTieu DECIMAL(18,2) NOT NULL,    -- tiến độ KHÔNG lưu cột (QD05)
    NgayBatDau    DATE NOT NULL,
    NgayKetThuc   DATE NULL,
    TrangThai     NVARCHAR(20) NOT NULL CONSTRAINT DF_MT_TrangThai DEFAULT 'DangThucHien'
);

CREATE TABLE KhoanVayNo (
    KhoanVayNoID     INT IDENTITY(1,1) PRIMARY KEY,
    NguoiDungID      INT NOT NULL,
    LoaiKhoan        NVARCHAR(10)  NOT NULL,  -- Vay / ChoVay
    TenNguoiLienQuan NVARCHAR(150) NOT NULL,
    SoTienGoc        DECIMAL(18,2) NOT NULL,
    NgayPhatSinh     DATE NOT NULL,
    NgayDenHan       DATE NULL,
    LaiSuat          DECIMAL(9,4) NULL,       -- %/năm, lãi đơn; NULL = không lãi
    GhiChu           NVARCHAR(500) NULL,
    TrangThai        NVARCHAR(20) NOT NULL CONSTRAINT DF_KV_TrangThai DEFAULT 'DangXuLy'
);

CREATE TABLE LichSuTraNo (
    LichSuTraNoID INT IDENTITY(1,1) PRIMARY KEY,
    NguoiDungID   INT NOT NULL,
    KhoanVayNoID  INT NOT NULL,
    GiaoDichID    INT NOT NULL,               -- bắt buộc link giao dịch TraNo (QD04)
    NgayThanhToan DATE NOT NULL,
    TienGoc       DECIMAL(18,2) NOT NULL CONSTRAINT DF_LSTN_TienGoc DEFAULT 0,
    TienLaiPhi    DECIMAL(18,2) NOT NULL CONSTRAINT DF_LSTN_TienLai DEFAULT 0,
    GhiChu        NVARCHAR(500) NULL
);
GO