USE QuanLyChiTieuDB;
GO
/* ---------- 1. UNIQUE nền tảng cho FK ghép (2.4.1) ---------- */
ALTER TABLE NguoiDung       ADD CONSTRAINT UQ_ND_TenDN      UNIQUE (TenDangNhap);
ALTER TABLE ViTien          ADD CONSTRAINT UQ_VT_User       UNIQUE (NguoiDungID, ViTienID);
ALTER TABLE DanhMuc         ADD CONSTRAINT UQ_DM_User       UNIQUE (NguoiDungID, DanhMucID);
ALTER TABLE MucTieuTietKiem ADD CONSTRAINT UQ_MT_User       UNIQUE (NguoiDungID, MucTieuID);
ALTER TABLE KhoanVayNo      ADD CONSTRAINT UQ_KV_User       UNIQUE (NguoiDungID, KhoanVayNoID);
/* Bổ sung so tài liệu: FK ghép LichSuTraNo(NguoiDungID, GiaoDichID) -> GiaoDich
   yêu cầu cha phải có UNIQUE ghép này, nếu không SQL Server từ chối tạo FK. */
ALTER TABLE GiaoDich        ADD CONSTRAINT UQ_GD_User       UNIQUE (NguoiDungID, GiaoDichID);
GO
/* ---------- 2. FK thường ---------- */
ALTER TABLE DanhMuc         ADD CONSTRAINT FK_DM_ND FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID);
ALTER TABLE ViTien          ADD CONSTRAINT FK_VT_ND FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID);
ALTER TABLE GiaoDich        ADD CONSTRAINT FK_GD_ND FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID);
ALTER TABLE NganSach        ADD CONSTRAINT FK_NS_ND FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID);
ALTER TABLE MucTieuTietKiem ADD CONSTRAINT FK_MT_ND FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID);
ALTER TABLE KhoanVayNo      ADD CONSTRAINT FK_KV_ND FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID);
ALTER TABLE LichSuTraNo     ADD CONSTRAINT FK_LSTN_ND FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID);
GO
/* ---------- 3. FK ghép — chống dùng chéo user ở mức DB (2.4.2) ---------- */
ALTER TABLE GiaoDich ADD CONSTRAINT FK_GD_ViNguon FOREIGN KEY (NguoiDungID, ViNguonID) REFERENCES ViTien(NguoiDungID, ViTienID);
ALTER TABLE GiaoDich ADD CONSTRAINT FK_GD_ViDich  FOREIGN KEY (NguoiDungID, ViDichID)  REFERENCES ViTien(NguoiDungID, ViTienID);
ALTER TABLE GiaoDich ADD CONSTRAINT FK_GD_DanhMuc FOREIGN KEY (NguoiDungID, DanhMucID) REFERENCES DanhMuc(NguoiDungID, DanhMucID);
ALTER TABLE GiaoDich ADD CONSTRAINT FK_GD_KhoanVayNo FOREIGN KEY (NguoiDungID, KhoanVayNoID) REFERENCES KhoanVayNo(NguoiDungID, KhoanVayNoID);
ALTER TABLE GiaoDich ADD CONSTRAINT FK_GD_MucTieu FOREIGN KEY (NguoiDungID, MucTieuID) REFERENCES MucTieuTietKiem(NguoiDungID, MucTieuID);
ALTER TABLE NganSach ADD CONSTRAINT FK_NS_DanhMuc FOREIGN KEY (NguoiDungID, DanhMucID) REFERENCES DanhMuc(NguoiDungID, DanhMucID);
ALTER TABLE LichSuTraNo ADD CONSTRAINT FK_LSTN_KhoanVayNo FOREIGN KEY (NguoiDungID, KhoanVayNoID) REFERENCES KhoanVayNo(NguoiDungID, KhoanVayNoID);
ALTER TABLE LichSuTraNo ADD CONSTRAINT FK_LSTN_GiaoDich FOREIGN KEY (NguoiDungID, GiaoDichID) REFERENCES GiaoDich(NguoiDungID, GiaoDichID);
GO
/* ---------- 4. CHECK (2.4.3) ---------- */
ALTER TABLE DanhMuc ADD CONSTRAINT CK_DM_Loai CHECK (LoaiDanhMuc IN ('Thu','Chi'));

ALTER TABLE NganSach ADD CONSTRAINT CK_NS_Thang  CHECK (Thang BETWEEN 1 AND 12);
ALTER TABLE NganSach ADD CONSTRAINT CK_NS_Nam    CHECK (Nam BETWEEN 1900 AND 2100);
ALTER TABLE NganSach ADD CONSTRAINT CK_NS_HanMuc CHECK (HanMuc > 0);
ALTER TABLE NganSach ADD CONSTRAINT UQ_NS_Ky     UNIQUE (NguoiDungID, DanhMucID, Thang, Nam);

ALTER TABLE MucTieuTietKiem ADD CONSTRAINT CK_MT_SoTien CHECK (SoTienMucTieu > 0);
ALTER TABLE MucTieuTietKiem ADD CONSTRAINT CK_MT_Ngay CHECK (NgayKetThuc IS NULL OR NgayKetThuc >= NgayBatDau);
ALTER TABLE MucTieuTietKiem ADD CONSTRAINT CK_MT_TrangThai CHECK (TrangThai IN ('DangThucHien','HoanThanh','TamDung'));

ALTER TABLE KhoanVayNo ADD CONSTRAINT CK_KV_Loai  CHECK (LoaiKhoan IN ('Vay','ChoVay'));
ALTER TABLE KhoanVayNo ADD CONSTRAINT CK_KV_Goc   CHECK (SoTienGoc > 0);
ALTER TABLE KhoanVayNo ADD CONSTRAINT CK_KV_Lai   CHECK (LaiSuat IS NULL OR LaiSuat >= 0);
ALTER TABLE KhoanVayNo ADD CONSTRAINT CK_KV_TrangThai CHECK (TrangThai IN ('DangXuLy','HoanThanh'));

ALTER TABLE LichSuTraNo ADD CONSTRAINT CK_LSTN_Tien CHECK (TienGoc >= 0 AND TienLaiPhi >= 0 AND TienGoc + TienLaiPhi > 0);

ALTER TABLE GiaoDich ADD CONSTRAINT CK_GD_Loai     CHECK (LoaiGiaoDich IN ('Thu','Chi','ChuyenTien','NhanVay','TraNo','ChoVay','ThuHoiNo','DongGop','RutTietKiem'));
ALTER TABLE GiaoDich ADD CONSTRAINT CK_GD_SoTien   CHECK (SoTien > 0);
ALTER TABLE GiaoDich ADD CONSTRAINT CK_GD_HaiViKhac CHECK (ViDichID IS NULL OR ViNguonID IS NULL OR ViDichID <> ViNguonID);
ALTER TABLE GiaoDich ADD CONSTRAINT CK_GD_Chuuyen  CHECK (LoaiGiaoDich <> 'ChuyenTien' OR (ViNguonID IS NOT NULL AND ViDichID IS NOT NULL AND DanhMucID IS NULL));
ALTER TABLE GiaoDich ADD CONSTRAINT CK_GD_Thu      CHECK (LoaiGiaoDich <> 'Thu' OR (ViDichID IS NOT NULL AND ViNguonID IS NULL AND DanhMucID IS NOT NULL));
ALTER TABLE GiaoDich ADD CONSTRAINT CK_GD_Chi      CHECK (LoaiGiaoDich <> 'Chi' OR (ViNguonID IS NOT NULL AND ViDichID IS NULL AND DanhMucID IS NOT NULL));
ALTER TABLE GiaoDich ADD CONSTRAINT CK_GD_TienVao  CHECK (LoaiGiaoDich NOT IN ('NhanVay','ThuHoiNo','RutTietKiem') OR (ViDichID IS NOT NULL AND ViNguonID IS NULL));
ALTER TABLE GiaoDich ADD CONSTRAINT CK_GD_TienRa   CHECK (LoaiGiaoDich NOT IN ('TraNo','ChoVay','DongGop') OR (ViNguonID IS NOT NULL AND ViDichID IS NULL));
ALTER TABLE GiaoDich ADD CONSTRAINT CK_GD_KV_BB    CHECK (LoaiGiaoDich NOT IN ('NhanVay','TraNo','ChoVay','ThuHoiNo') OR KhoanVayNoID IS NOT NULL);
ALTER TABLE GiaoDich ADD CONSTRAINT CK_GD_KV_Cam   CHECK (LoaiGiaoDich IN ('NhanVay','TraNo','ChoVay','ThuHoiNo') OR KhoanVayNoID IS NULL);
ALTER TABLE GiaoDich ADD CONSTRAINT CK_GD_MT_BB    CHECK (LoaiGiaoDich NOT IN ('DongGop','RutTietKiem') OR MucTieuID IS NOT NULL);
ALTER TABLE GiaoDich ADD CONSTRAINT CK_GD_MT_Cam   CHECK (LoaiGiaoDich IN ('DongGop','RutTietKiem') OR MucTieuID IS NULL);
ALTER TABLE GiaoDich ADD CONSTRAINT CK_GD_DM_BB    CHECK (LoaiGiaoDich NOT IN ('Thu','Chi') OR DanhMucID IS NOT NULL);
ALTER TABLE GiaoDich ADD CONSTRAINT CK_GD_DM_Cam   CHECK (LoaiGiaoDich IN ('Thu','Chi') OR DanhMucID IS NULL);
GO
/* ---------- 5. Chỉ mục (2.5) ---------- */
CREATE INDEX IX_GD_User_Ngay  ON GiaoDich(NguoiDungID, NgayGiaoDich);
CREATE INDEX IX_GD_User_Loai  ON GiaoDich(NguoiDungID, LoaiGiaoDich);
CREATE INDEX IX_GD_ViNguon    ON GiaoDich(ViNguonID);
CREATE INDEX IX_GD_ViDich     ON GiaoDich(ViDichID);
CREATE INDEX IX_GD_MucTieu    ON GiaoDich(MucTieuID);
CREATE INDEX IX_GD_KhoanVayNo ON GiaoDich(KhoanVayNoID);
GO