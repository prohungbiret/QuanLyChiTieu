# BÀN GIAO TV3 — HẢI: Danh mục, Ngân sách, Thống kê, Đổi mật khẩu

*Branch: `feature/category-budget-report` — đối chiếu Tài liệu 06 mục 6.4 và tiêu chí nghiệm thu 6.6.*

## 1. File đã làm (đúng phân quyền 5.2)

| File | Nội dung |
|---|---|
| `Services/DanhMucService.cs` | CRUD danh mục + ngừng sử dụng / kích hoạt lại (soft-delete), đếm giao dịch tham chiếu |
| `Services/NganSachService.cs` | Ngân sách theo (danh mục Chi, tháng); đã chi từ `vwSoTienChiDanhMucThang`; trạng thái Bình thường / Cảnh báo 80% / Đạt / Vượt |
| `Services/ThongKeService.cs` | Tổng quan tháng / khoảng ngày; chuỗi thu–chi theo tháng từ `vwThuChiThang`; phân bổ theo danh mục (kèm tỉ trọng); chi tiết giao dịch Thu/Chi |
| `Controls/UcDanhMuc.cs` | Tìm kiếm + lọc loại/trạng thái, Thêm/Sửa (dialog), Ngừng/Kích hoạt có xác nhận |
| `Controls/UcNganSach.cs` | Thẻ ngân sách theo tháng kèm `UcProgress`, Thêm/Sửa/Xóa, cảnh báo ngay khi chạm ngưỡng |
| `Controls/UcThongKe.cs` | 2 chế độ (tháng / khoảng ngày), 4 thẻ tổng quan, cột Thu + cột Chi 6 tháng, tròn cơ cấu danh mục, 2 bảng — dùng `UcChart` chung |
| `Forms/FrmThemDanhMuc.cs` | Dialog nhập (kế thừa `FrmDialogNhap`, hợp đồng 5.4) |
| `Forms/FrmThemNganSach.cs` | Dialog nhập; chỉ liệt kê danh mục Chi đang sử dụng |
| `Forms/FrmChangePassword.cs` | Đổi mật khẩu: verify mật khẩu hiện tại qua `AuthService.VerifyPassword`, lưu hash mới qua `AuthService.HashPassword` (PBKDF2, QD08) |

File chạm chung (cần TV1 review khi merge):

- `FrmMain.cs`: 3 nút Danh mục / Ngân sách / Thống kê mở UserControl thật thay cho placeholder;
  nút Tài khoản mở `FrmChangePassword` dạng dialog (form này không phải UserControl).
- `QuanLyChiTieu.csproj`: khai báo Compile cho 9 file mới.

## 2. Quy tắc nghiệp vụ đã áp

- **Danh mục:** không xóa cứng; ngừng sử dụng giữ nguyên lịch sử (FR02). Chặn trùng tên trong cùng loại.
  Không cho đổi loại Thu/Chi khi đã có giao dịch hoặc ngân sách (tránh phá quy tắc danh mục–giao dịch, Bảng 2.4.4).
- **Ngân sách:** chỉ danh mục `Chi`; 1 danh mục = 1 hạn mức/tháng (check ở service + `UQ_NS_Ky` ở DB, bắt `SqlException 2627/2601`).
  Đã chi **chỉ tính loại `Chi`** qua view 005 — `ChuyenTien`/`TraNo` không bao giờ được tính. Hạn mức phải > 0.
- **Thống kê:** mọi con số chỉ gồm `Thu` + `Chi` (QD02). Tháng trống vẫn hiện 0 trong chuỗi 6 tháng.
  Khoảng ngày kiểm bằng `ValidationHelper.KhoangNgay` (Từ ≤ Đến, Đến không tương lai).
- **Đổi mật khẩu:** mật khẩu mới ≥ 6 ký tự, khác mật khẩu cũ, nhập lại phải khớp; sai mật khẩu hiện tại → từ chối (TC-AUTH-06).
  Sau đổi, đăng nhập bằng mật khẩu mới được, mật khẩu cũ bị từ chối (TC-AUTH-08).
- Mọi truy vấn gắn `CurrentUser.NguoiDungID`, SQL tham số hóa (NFR03, mục 2.8). Không dùng chéo Service của TV2/TV4 (5.1):
  `NganSachService` đọc trực tiếp bảng/view qua ADO.NET như đặc tả cho phép.

Lưu ý dùng chung: `UcChart.HienThiCot` chỉ vẽ 1 series nên Thống kê dùng **hai biểu đồ cột xếp dọc**
(Thu xanh / Chi đỏ, cùng trục 6 tháng) thay vì tự cấu hình chart bên trong (đúng mục 6.7).

## 3. Cách chạy kiểm tra

1. Dựng DB: `Database\recreate-database.ps1` (001 → 005).
2. Mở solution, đăng nhập `truongnhom / chitieu123`.
3. Sidebar → Danh mục / Ngân sách / Thống kê; Tài khoản → hộp thoại Đổi mật khẩu.

Dữ liệu seed (004) đã có sẵn 3 vùng ngân sách để đối chiếu ngay trong tháng hiện tại:
Ăn uống 92,5% (cảnh báo 80%), Giải trí 100% (đạt), Khác 125% (vượt), Di chuyển 64% (bình thường).

## 4. Kiểm thử đã chạy

- **TC-RPT-04/05/06/09** (ngưỡng): 79% → Bình thường; 80% → Cảnh báo; 100% → Đạt; 110%/125% → Vượt, còn lại âm — đạt.
- **PBKDF2**: hash đúng format `PBKDF2.100000...`; verify đúng/sai; chuỗi hỏng trả false không crash;
  hash seed trong 004 verify đúng với `chitieu123` — đạt.
- **Build**: toàn bộ 47 file `.cs` biên dịch sạch (0 lỗi, 0 cảnh báo) ở LangVersion 7.3 với
  reference assemblies .NET Framework 4.8. Trên Windows chạy thêm `.\build.ps1` trước khi push để
  xác nhận bằng MSBuild của Visual Studio.
- Chưa chạy được trên LocalDB thật trong môi trường bàn giao (Linux, không có SQL Server) —
  cần chạy bộ TC của Tài liệu 07 mục 7.2 (TC-AUTH-06/08) và 7.4 (TC-RPT-01…10) trên máy Windows
  có LocalDB trước buổi demo, đặc biệt kiểm tra view 005 đã được tạo (script 005 chạy sau 004).

## 5. Ngoài phạm vi / phụ thuộc

- Chặn chọn danh mục đã ngừng trong form giao dịch thuộc `GiaoDichService` của TV2 —
  phía TV3 đã có `DanhMucService.LayTheoLoai(loai, chiDangDung: true)` để TV2 dùng khi nạp combo.
- Xuất Excel/PDF từ màn Thống kê thuộc TV4 (`BaoCaoService`); màn Thống kê của TV3 chỉ hiển thị.
