# TÀI LIỆU 09 — SỔ TAY LỖI THƯỜNG GẶP VÀ CẢNH BÁO
*File: docs/09_SoTayLoi.md — TV1 sở hữu; mọi thành viên GHI THÊM khi gặp lỗi mới (sau khi tự xử lý được).*

Mục đích: lỗi đã gặp 1 lần trong nhóm là lỗi KHÔNG được lặp lại. Khi gặp sự cố:
1. **Tra trước** trong sổ tay này (Ctrl+F theo thông báo lỗi).
2. Tự xử lý xong → **ghi thêm 1 mục** + commit cùng phần fix.
3. Lỗi không có ở đây và không tự xử lý được → hỏi nhóm kèm screenshot + chép nguyên văn thông báo lỗi.

## Hình thức mỗi mục
| Trường | Ví dụ |
|---|---|
| Mã | L006 |
| Triệu chứng | Thông báo lỗi nguyên văn / hiện tượng nhìn thấy |
| Nguyên nhân gốc | Lỗi thứ cấp thật, không phải mô tả chung chung |
| Cách xử lý | Các bước cụ thể người sau làm theo được |
| Quy tắc chặn tái diễn | Quy tắc nhóm rút ra (nếu có) |

---

## Lỗi đã gặp — nhóm TV1 (nền tảng)

### L001 — Mất `<Compile Include>` trong `.csproj` sau khi mở VS
- **Triệu chứng:** `error CS0246: The type or namespace name 'UI'/'Data'/'Models' does not exist...`
- **Nguyên nhân:** Visual Studio serialize lại file `.csproj` (dạng không-SDK) khi mở/giữ solution và làm rơi nhóm `<Compile Include>` tự thêm; đôi khi mất luôn `<Reference Include="System.Configuration">`.
- **Cách xử lý:** xem diff để tìm phần mất (`git diff QuanLyChiTieu/QuanLyChiTieu.csproj`); khôi phục từ commit gần nhất hoặc thêm lại bằng tay; build lại.
- **Quy tắc chặn:** ai thêm file .cs phải tự thêm `<Compile Include>` vào `.csproj` trong cùng commit. Sau mỗi lần pull/merge hoặc mở VS, kiểm `git status` xem `.csproj` có bị sửa ngoài kế hoạch.

### L002 — VS Designer lỗi "Method 'ConfigNavButton' not found"
- **Triệu chứng:** mở Form/UserControl trong VS Designer thấy `Method 'System.Windows.Forms.Form.X' not found` (lặp theo số lần gọi hàm).
- **Nguyên nhân:** trong `InitializeComponent()` có lệnh gọi hàm custom — bộ parser CodeDom của Designer chỉ deserialize được: `new`, gán thuộc tính, `Controls.Add`, `+=` event, `AddRange`, `Add`.
- **Cách xử lý:** bỏ hàm custom khỏi `InitializeComponent`; gán thuộc tính trực tiếp từng control (Designer vẫn serialize lại được), hoặc chạy cấu hình runtime sau `InitializeComponent` (mất preview ở Designer).
- **Quy tắc chặn:** tuyệt đối không gọi hàm custom nào bên trong `InitializeComponent()`.

### L003 — `DBCC CHECKIDENT RESEED 0` khiến ID bắt đầu từ 0
- **Triệu chứng:** dòng đầu bảng có ID = 0 (phải là 1). `CurrentUser.DaDangNhap` đối chiếu `NguoiDungID > 0` → phiên đăng nhập bị coi là chưa đăng nhập.
- **Nguyên nhân:** `RESEED, 0` trên bảng rỗng làm INSERT đầu tiên dùng đúng `new_reseed_value` (0) thay vì 0 + 1.
- **Cách xử lý:** trong `004_SeedData.sql` mọi RESEED dùng giá trị 1; hoặc dựng lại DB hoàn toàn (bảng mới, identity tự bắt đầu 1).
- **Quy tắc chặn:** không dùng ID 0 làm cờ trạng thái "có/không" ở code lẫn schema — logic dạng `id > 0` luôn phải tương thích ID >= 1.

### L004 — Chạy lại `002_CreateTables.sql` / `003_CreateConstraints.sql` bị lỗi
- **Triệu chứng:** `There is already an object named ... in the database`.
- **Nguyên nhân:** script dựng bảng/constraint không idempotent — thiết kế vốn cho chạy lần đầu.
- **Cách xử lý:** dùng **`Database/recreate-database.ps1`** (drop DB rồi chạy 001→005). Không tự chạy 002/003 lần 2 trên DB đang có.
- **Quy tắc chặn:** chỉ rebuild DB bằng script TV1 cho phép; muốn DB riêng để thử nghiệm thì tạo `QuanLyChiTieuDB_<tenTV>` riêng, không đụng DB demo chung.

### L005 — Cảnh báo dữ liệu demo: ví Momo của `truongnhom` đang âm
- **Triệu chứng:** `vwSoDuVi` — Momo `SoDuHienTai` âm.
- **Trạng thái:** CHƯA quyết — có thể là tình huống demo chủ ý (cho thấy số dư có thể âm) hay là dữ liệu lệch.
- **Cần làm:** nhóm thống nhất trước ngày demo; nếu muốn dương thì chỉnh `SoDuBanDau` Momo hoặc bớt giao dịch Chi vào Momo trong `004_SeedData.sql`, rồi chạy `recreate-database.ps1`.
- **Quy tắc chặn:** sửa dữ liệu demo chỉ qua file seed + chạy đầy đủ script — không UPDATE tay trong DB lúc đang demo.

---

## Cảnh báo trước — chưa gặp nhưng nhiều khả năng gặp

### L006 — Dữ liệu "nhảy" sang tài khoản khác (lỗi FK ghép)
- **Triệu chứng:** CRUD thấy dữ liệu của tài khoản khác.
- **Nguyên nhân:** câu SELECT/UPDATE/DELETE thiếu `NguoiDungID = @id` (quên điều kiện ghép ở tầng query).
- **Quy tắc chặn:** mọi truy vấn phân hệ PHẢI tham số hóa `NguoiDungID` lấy từ `CurrentUser.NguoiDungID` — khớp cả chiều FK ghép ở schema (Đặc tả 02).

### L007 — File SQL/.cs tiếng Việt báo lỗi cú pháp vô lý khi chạy sqlcmd
- **Triệu chứng:** sqlcmd ném lỗi sai cú pháp tại dòng comment tiếng Việt.
- **Nguyên nhân:** file lưu không BOM UTF-8 — sqlcmd đọc nhầm byte tiếng Việt.
- **Quy tắc chặn:** file SQL có tiếng Việt phải lưu **UTF-8 with BOM** ("Unicode (UTF-8 with signature)" trong Visual Studio); chạy sqlcmd với `-f 65001` (đã có sẵn trong `recreate-database.ps1`).

### L008 — Nhớt bộ nhớ / control cũ còn sống khi chuyển trang
- **Triệu chứng:** đổi trang nhanh nhiều lần, RAM tăng; control cũ còn bắt event.
- **Nguyên nhân:** không Dispose control cũ khi thay control trong `pnlContent`.
- **Quy tắc chặn:** mọi phân hệ gắn trang qua `FrmMain.ShowControl` v2 (tự Dispose control cũ) — không tự `Controls.Add` vào FrmMain.

### L009 — TraNo thiếu dòng LichSuTraNo trong cùng transaction
- **Triệu chứng:** có GiaoDich loại TraNo nhưng không có LichSuTraNo tương ứng → tiến độ trả nợ lệch.
- **Quy tắc chặn:** TV4 ghi TraNo luôn trong **một transaction** gồm GiaoDich + LichSuTraNo, với `SoTien = TienGoc + TienLaiPhi` (khớp CHECK schema + `ValidationHelper.TienTraNo`).

### L010 — Merge xung đột `.csproj` (xung đột tần suất cao nhất dự án)
- **Triệu chứng:** `git pull` báo conflict `QuanLyChiTieu/QuanLyChiTieu.csproj`.
- **Cách xử lý:** giữ CẢ HAI nhóm `<Compile Include>` của hai nhánh; giữ phần `<Reference>` nguyên vẹn (tham chiếu nhánh của TV1). Sau khi giải xong chạy `build.ps1` — phải đạt trước khi commit merge.
- **Quy tắc chặn:** conflict `.csproj` bắt buộc TV1 duyệt trước khi merge.
