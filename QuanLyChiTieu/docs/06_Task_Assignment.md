# TÀI LIỆU 06 — PHÂN CÔNG CÔNG VIỆC VÀ TIÊU CHÍ NGHIỆM THU
*File: docs/06_Task_Assignment.md — bản rút gọn từ bộ tài liệu chuẩn v2.0*

## 6.1 Nguyên tắc
Một phân hệ một người chịu trách nhiệm chính; một file một chủ sở hữu; không cùng sửa một form;
nhóm trưởng điều phối, không ôm nghiệp vụ; **mỗi thành viên sở hữu service phân hệ mình**;
bàn giao code + hướng dẫn chạy + kết quả kiểm thử. Không có mốc ngày — kết thúc khi **đầu ra đạt**.

## 6.2 TV1 — Nhóm trưởng, nền tảng, tích hợp
Project .NET 4.8; LocalDB + connection string; schema đầy đủ CHECK + FK ghép (Đặc tả 02); Database.cs;
CurrentUser; AuthService PBKDF2 (4.9); Theme.cs + UIHelper.cs; FrmMain + sidebar + pnlContent; FrmLogin;
Git; **004_SeedData.sql**; review + tích hợp + kiểm thử toàn hệ thống.
**Không làm thay** CRUD ví/giao dịch/ngân sách/thống kê/tiết kiệm/vay nợ/xuất báo cáo; không làm FrmRegister/FrmChangePassword.

## 6.3 TV2 — Ví tiền, giao dịch, đăng ký  ---Bảo---
`UcViTien, UcGiaoDich, FrmThemViTien, FrmThemGiaoDich, FrmRegister, ViTienService, GiaoDichService`.
CRUD ví; số dư tính động; CRUD giao dịch đủ 9 loại theo Bảng 2.2e; tìm kiếm + lọc; chuyển tiền;
kiểm tra số tiền/ngày (cấm tương lai)/ví/danh mục; số dư đúng sau thêm/sửa/xóa; xác nhận khi xóa.

## 6.4 TV3 — Danh mục, ngân sách, thống kê, đổi mật khẩu ---Hải---
`UcDanhMuc, UcNganSach, UcThongKe, biểu đồ (DataVisualization), FrmChangePassword,
DanhMucService, NganSachService, ThongKeService`.
CRUD + ngừng sử dụng danh mục; ngân sách theo (danh mục Chi, tháng) — chi chỉ tính loại Chi,
**không tính ChuyenTien/TraNo**; cảnh báo 80%/100%/vượt; thống kê tháng/khoảng ngày; biểu đồ cột + tròn.

## 6.5 TV4 — Tiết kiệm, vay nợ, xuất báo cáo ---Hoàng---
`UcTietKiem, UcVayNo, lịch sử thanh toán, xuất Excel/PDF, TietKiemService, VayNoService, BaoCaoService`.
Mục tiêu tiết kiệm tiến độ = SUM(DongGop) − SUM(RutTietKiem); khoản vay qua 4 loại GD;
TraNo + LichSuTraNo trong cùng transaction, tách gốc/lãi (`TienGoc + TienLaiPhi = SoTien`);
xuất ClosedXML + PDFsharp theo bộ lọc, chỉ dữ liệu user hiện tại.

## 6.6 Tiêu chí nghiệm thu chung
Mở được từ FrmMain; build thành công; UI theo Standard v1.1; DB thật; validation; xử lý lỗi;
dữ liệu thuộc user hiện tại (cả app lẫn FK ghép); CRUD + biên đã test; số dư đúng theo công thức 2.6
kể cả GD của phân hệ khác; không phá phân hệ khác; đã commit; có hướng dẫn chạy.

## 6.7 Thành phần dùng chung TV1 đã cung cấp (bắt buộc dùng, không tự viết lại)
| Thành phần | Vị trí | Ai dùng / mục đích |
|---|---|---|
| `vwSoDuVi` | `Database/005_Views.sql` | TV2: số dư ví theo công thức 2.6 — KHÔNG viết lại công thức trong service |
| `vwThuChiThang`, `vwSoTienChiDanhMucThang` | `Database/005_Views.sql` | TV3 (thống kê, ngân sách), TV4 (báo cáo): dữ liệu tổng hợp có sẵn |
| `UcProgress` | `UI/UcProgress.cs` | TV3 (ngân sách), TV4 (tiết kiệm, vay nợ): thanh tiến độ 3 màu + nhãn tình trạng |
| `UcChart` | `UI/UcChart.cs` | TV3 (thống kê): biểu đồ cột + tròn đã style theo Theme — truyền `DiemBieuDo[]` |
| `FrmDialogNhap` | `Forms/FrmDialogNhap.cs` | TV2 (FrmThemViTien/FrmThemGiaoDich), TV4 (FrmTraNo...): base dialog nhập — đúng hợp đồng 5.4, Enter=Lưu/Esc=Hủy |
| `ValidationHelper` | `Services/ValidationHelper.cs` | Tất cả: kiểm số tiền, ngày (cấm tương lai), quan hệ GD-ví, GD-danh mục, TraNo gốc+lãi |
| `recreate-database.ps1` | `Database/` | Cả nhóm: dựng lại DB demo 1 lệnh (drop → 001→005) |
| `build.ps1` | gốc repo | Cả nhóm: build trước khi commit |
