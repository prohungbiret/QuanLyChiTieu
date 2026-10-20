<<<<<<< HEAD
# QuanLyChiTieu — Phần mềm quản lý chi tiêu cá nhân

C# WinForms · .NET Framework 4.8 · ADO.NET (System.Data.SqlClient) · SQL Server LocalDB

## Mô tả
Ứng dụng desktop cá nhân: quản lý tài khoản (PBKDF2), danh mục, ví tiền (số dư tính động),
giao dịch **9 loại** (Thu/Chi/Chuyển tiền/Nhận vay/Trả nợ/Cho vay/Thu hồi nợ/Đóng góp/Rút tiết kiệm),
ngân sách theo tháng với cảnh báo 80%/100%, mục tiêu tiết kiệm, vay nợ, thống kê + xuất Excel/PDF.
Toàn bộ thiết kế theo bộ tài liệu chuẩn v2.0 trong `docs/`.

## Yêu cầu môi trường
1. Visual Studio 2022 (hoặc 2019) — workload **.NET desktop development**
2. .NET Framework 4.8 Developer Pack
3. SQL Server Express **LocalDB** — kiểm tra: `sqllocaldb info`

## Cài đặt & chạy
1. Clone repo, mở `QuanLyChiTieu.slnx` bằng Visual Studio.
2. Tạo database theo thứ tự (SSMS hoặc sqlcmd), dùng thư mục `Database/`:
   - `001_CreateDatabase.sql` → `002_CreateTables.sql` → `003_CreateConstraints.sql` → `004_SeedData.sql`
3. Connection string đã cấu hình sẵn trong `App.config` (LocalDB xác thực Windows);
   muốn dùng máy chủ SQL khác chỉ cần sửa mục `Data Source`.
   ```xml
   <connectionStrings>
     <add name="QuanLyChiTieuConnection"
          connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyChiTieuDB;Integrated Security=True;Connect Timeout=30;"
          providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```
4. Build (F6) rồi Run (F5).

## Tài khoản demo (dữ liệu trong 004)
| Tên đăng nhập | Mật khẩu | Ghi chú |
|---|---|---|
| `truongnhom` | `chitieu123` | Tài khoản demo chính — đủ dữ liệu dashboard per mockup |
| `demo` | `chitieu123` | Dự phòng |
| `thanhvien2` | `sinhvien123` | Dữ liệu tách biệt — kiểm chứng bảo mật giữa tài khoản |

Mật khẩu lưu dạng hash PBKDF2-HMACSHA256 100.000 vòng + salt 16 byte (QD08) — không văn bản thuần.

## Phân hệ hoàn thành (sau giai đoạn nền tảng TV1)
- Tầng nền: Database scripts (001–004), `Data/Database.cs`, `CurrentUser`, `Services/AuthService` (PBKDF2)
- UI foundation: `UI/Theme.cs` (bảng màu UI Standard v1.1), `UI/UIHelper.cs` (định dạng + style DataGridView)
- Màn hình: FrmLogin, FrmMain (sidebar + điều hướng + ShowControl v2), UcDashboard (KPI + giao dịch gần đây)
- Hợp đồng tích hợp mẫu: `UcDangXayDung` + `Forms/FrmMauNhap` (ShowDialog/DialogResult)

Phân hệ còn lại (ví/giao dịch, danh mục/ngân sách/thống kê, tiết kiệm/vay nợ/xuất báo cáo)
chờ TV2/TV3/TV4 theo phân công `docs/06_Task_Assignment.md`.

## Giới hạn đã biết
- "Quên mật khẩu" nằm ngoài phạm vi (hiển thị hướng dẫn liên hệ quản trị).
- FrmRegister / FrmChangePassword thuộc TV2/TV3 — đang là placeholder.
- Biểu đồ của dashboard là phần của phân hệ Thống kê (TV3).

