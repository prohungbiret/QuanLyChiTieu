# TÀI LIỆU 02 — THIẾT KẾ CƠ SỞ DỮ LIỆU

**File:** `docs/02_Database_Design.md`

## 2.1. Nguyên tắc thiết kế

1. Mỗi bảng nghiệp vụ có `NguoiDungID` để phân vùng dữ liệu; người dùng chỉ được truy cập dữ liệu của mình.
2. Dữ liệu tiền dùng `DECIMAL(18,2)`; khóa chính dùng `INT IDENTITY`.
3. Sử dụng khóa ngoại ghép `(NguoiDungID, ID)` để chống tham chiếu chéo giữa các người dùng.
4. Không xóa lịch sử chỉ để làm sạch giao diện. Danh mục và ví sử dụng trạng thái để ngừng hoạt động; giao dịch chỉ bị xóa cứng khi người dùng xác nhận thao tác nghiệp vụ.
5. Những thao tác ghi nhiều bảng liên quan phải sử dụng transaction và rollback khi thất bại.
6. Không lưu cột số dư hiện tại. Số dư luôn được tính từ số dư ban đầu và lịch sử giao dịch.
7. Repository chỉ có một bộ script database chính thức.

## 2.2. Danh sách bảng

Database gồm 8 bảng:

1. `NguoiDung`
2. `DanhMuc`
3. `ViTien`
4. `GiaoDich`
5. `NganSach`
6. `MucTieuTietKiem`
7. `KhoanVayNo`
8. `LichSuTraNo`

Các thay đổi quan trọng so với v1:

- `GiaoDich` hỗ trợ 9 loại giao dịch, bổ sung các khóa ngoại nghiệp vụ và cột `NgayTao`.
- `LichSuTraNo` có `NguoiDungID` và `GiaoDichID` bắt buộc.
- `MucTieuTietKiem` bỏ `SoTienDaDat`.

## 2.3. Cấu trúc bảng

### 2.3.1. Bảng `NguoiDung`

| Cột | Kiểu dữ liệu | Quy tắc |
|---|---|---|
| `NguoiDungID` | `INT IDENTITY` | Khóa chính |
| `TenDangNhap` | `NVARCHAR(50)` | Bắt buộc, UNIQUE |
| `MatKhauHash` | `NVARCHAR(255)` | Lưu chuỗi PBKDF2 |
| `TenHienThi` | `NVARCHAR(100)` | Bắt buộc |
| `Email` | `NVARCHAR(255)` | Tùy chọn |
| `NgayTao` | `DATETIME2` | Mặc định `SYSDATETIME()` |
| `TrangThai` | `BIT` | Mặc định `1` |

### 2.3.2. Bảng `DanhMuc`

| Cột | Kiểu dữ liệu | Quy tắc |
|---|---|---|
| `DanhMucID` | `INT IDENTITY` | Khóa chính |
| `NguoiDungID` | `INT` | Khóa ngoại tới người dùng |
| `TenDanhMuc` | `NVARCHAR(100)` | Tên danh mục |
| `LoaiDanhMuc` | `NVARCHAR(10)` | CHECK chỉ nhận `Thu` hoặc `Chi` |
| `MoTa` | Theo thiết kế triển khai | Mô tả tùy chọn |
| `TrangThai` | `BIT` | Trạng thái sử dụng |

### 2.3.3. Bảng `ViTien`

| Cột | Kiểu dữ liệu | Quy tắc |
|---|---|---|
| `ViTienID` | `INT IDENTITY` | Khóa chính |
| `NguoiDungID` | `INT` | Khóa ngoại tới người dùng |
| `TenVi` | `NVARCHAR(100)` | Tên ví |
| `LoaiVi` | `NVARCHAR(30)` | Tiền mặt, ngân hàng, ví điện tử... |
| `SoDuBanDau` | `DECIMAL(18,2)` | Mặc định `0`; được phép sửa |
| `TrangThai` | `BIT` | Trạng thái sử dụng |

### 2.3.4. Bảng `GiaoDich`

Đây là bảng trung tâm lưu mọi dòng tiền đi qua ví.

| Cột | Kiểu dữ liệu | Quy tắc |
|---|---|---|
| `GiaoDichID` | `INT IDENTITY` | Khóa chính |
| `NguoiDungID` | `INT` | Khóa ngoại tới `NguoiDung` |
| `LoaiGiaoDich` | `NVARCHAR(20)` | CHECK theo 9 loại giao dịch |
| `SoTien` | `DECIMAL(18,2)` | CHECK `> 0` |
| `NgayGiaoDich` | `DATE` | Bắt buộc; không vượt ngày hiện tại theo validation ứng dụng |
| `DanhMucID` | `INT NULL` | Khóa ngoại ghép; bắt buộc với Thu/Chi |
| `ViNguonID` | `INT NULL` | Ví tiền đi ra |
| `ViDichID` | `INT NULL` | Ví tiền đi vào |
| `KhoanVayNoID` | `INT NULL` | Bắt buộc với 4 loại giao dịch vay/nợ |
| `MucTieuID` | `INT NULL` | Bắt buộc với `DongGop` và `RutTietKiem` |
| `GhiChu` | `NVARCHAR(500)` | Tùy chọn |
| `NgayTao` | `DATETIME2` | Mặc định `SYSDATETIME()`; phục vụ sắp xếp giao dịch gần đây |

### 2.3.5. Quy ước 9 loại giao dịch

Quy ước chung:

- Tiền vào sử dụng `ViDichID`.
- Tiền ra sử dụng `ViNguonID`.

| `LoaiGiaoDich` | Hướng tiền | Ví sử dụng | Trường bắt buộc đi kèm | Tính vào báo cáo thu/chi? |
|---|---|---|---|---|
| `Thu` | Vào | `ViDichID` | Danh mục loại Thu | Có |
| `Chi` | Ra | `ViNguonID` | Danh mục loại Chi | Có |
| `ChuyenTien` | Ra nguồn, vào đích | Cả hai, phải khác nhau | Không cần danh mục | Không |
| `NhanVay` | Vào | `ViDichID` | `KhoanVayNoID`, loại Vay | Không |
| `TraNo` | Ra | `ViNguonID` | `KhoanVayNoID`, loại Vay | Không |
| `ChoVay` | Ra | `ViNguonID` | `KhoanVayNoID`, loại ChoVay | Không |
| `ThuHoiNo` | Vào | `ViDichID` | `KhoanVayNoID`, loại ChoVay | Không |
| `DongGop` | Ra | `ViNguonID` | `MucTieuID` | Không |
| `RutTietKiem` | Vào | `ViDichID` | `MucTieuID` | Không |

### 2.3.6. Bảng `NganSach`

| Trường | Quy tắc |
|---|---|
| `NganSachID` | Khóa chính |
| `NguoiDungID` | Khóa ngoại tới người dùng |
| `DanhMucID` | Khóa ngoại ghép; danh mục phải là loại Chi, được kiểm tra ở Service |
| `Thang` | CHECK từ 1 đến 12 |
| `Nam` | CHECK từ 1900 đến 2100 |
| `HanMuc` | `DECIMAL(18,2)`, CHECK `> 0` |

**Ràng buộc duy nhất:**

```sql
UNIQUE (NguoiDungID, DanhMucID, Thang, Nam)
```

Mỗi người dùng chỉ có một hạn mức cho một danh mục trong một tháng/năm.

### 2.3.7. Bảng `MucTieuTietKiem`

| Trường | Quy tắc |
|---|---|
| `MucTieuID` | Khóa chính |
| `NguoiDungID` | Khóa ngoại tới người dùng |
| `TenMucTieu` | `NVARCHAR(150)` |
| `SoTienMucTieu` | CHECK `> 0` |
| `NgayBatDau` | Kiểu `DATE` |
| `NgayKetThuc` | Có thể NULL; nếu có phải lớn hơn hoặc bằng ngày bắt đầu |
| `TrangThai` | CHECK trong `DangThucHien`, `HoanThanh`, `TamDung` |

Không có cột `SoTienDaDat`. Tiến độ được tính từ các giao dịch đóng góp và rút tiết kiệm để tránh lưu trùng dữ liệu.

### 2.3.8. Bảng `KhoanVayNo`

| Trường | Quy tắc |
|---|---|
| `KhoanVayNoID` | Khóa chính |
| `NguoiDungID` | Khóa ngoại tới người dùng |
| `LoaiKhoan` | CHECK trong `Vay`, `ChoVay` |
| `TenNguoiLienQuan` | `NVARCHAR(150)` |
| `SoTienGoc` | `DECIMAL(18,2)`, CHECK `> 0` |
| `NgayPhatSinh` | `DATE` |
| `NgayDenHan` | `DATE`, có thể NULL |
| `LaiSuat` | `DECIMAL(9,4)`; tính theo %/năm, lãi đơn, NULL nếu không tính lãi |
| `GhiChu` | Ghi chú tùy chọn |
| `TrangThai` | CHECK trong `DangXuLy`, `HoanThanh` |

### 2.3.9. Bảng `LichSuTraNo`

| Trường | Quy tắc |
|---|---|
| `LichSuTraNoID` | Khóa chính |
| `NguoiDungID` | Dùng trong khóa ngoại ghép |
| `KhoanVayNoID` | Khóa ngoại ghép tới khoản vay/nợ |
| `GiaoDichID` | Bắt buộc; khóa ngoại ghép tới giao dịch `TraNo` |
| `NgayThanhToan` | `DATE` |
| `TienGoc` | Mặc định 0; không âm |
| `TienLaiPhi` | Mặc định 0; không âm |
| `GhiChu` | Ghi chú tùy chọn |

**Ràng buộc:**

- `TienGoc >= 0`.
- `TienLaiPhi >= 0`.
- `TienGoc + TienLaiPhi > 0`.
- Tổng gốc và lãi/phí phải bằng `SoTien` của giao dịch `TraNo` liên kết.
- Giao dịch trả nợ và bản ghi lịch sử phải được ghi trong cùng transaction.

## 2.4. Ràng buộc toàn vẹn và chống dùng chéo người dùng

### 2.4.1. UNIQUE ghép cần tạo

| Bảng | Ràng buộc |
|---|---|
| `ViTien` | `UNIQUE (NguoiDungID, ViTienID)` |
| `DanhMuc` | `UNIQUE (NguoiDungID, DanhMucID)` |
| `MucTieuTietKiem` | `UNIQUE (NguoiDungID, MucTieuID)` |
| `KhoanVayNo` | `UNIQUE (NguoiDungID, KhoanVayNoID)` |
| `NguoiDung` | `UNIQUE (TenDangNhap)` |

### 2.4.2. Khóa ngoại ghép từ bảng con

| Bảng con | Khóa ngoại ghép | Bảng cha |
|---|---|---|
| `GiaoDich` | `(NguoiDungID, ViNguonID)` | `ViTien` |
| `GiaoDich` | `(NguoiDungID, ViDichID)` | `ViTien` |
| `GiaoDich` | `(NguoiDungID, DanhMucID)` | `DanhMuc` |
| `GiaoDich` | `(NguoiDungID, KhoanVayNoID)` | `KhoanVayNo` |
| `GiaoDich` | `(NguoiDungID, MucTieuID)` | `MucTieuTietKiem` |
| `LichSuTraNo` | `(NguoiDungID, KhoanVayNoID)` | `KhoanVayNo` |
| `NganSach` | `(NguoiDungID, DanhMucID)` | `DanhMuc` |

Nhờ khóa ngoại ghép, giao dịch của người dùng A không thể tham chiếu ví hoặc danh mục của người dùng B chỉ bằng cách truyền một ID hợp lệ của người khác.

### 2.4.3. CHECK constraints cho script `003_CreateConstraints.sql`

Các ràng buộc cần triển khai gồm:

- `LoaiGiaoDich` chỉ được nhận một trong 9 giá trị đã quy định.
- `SoTien > 0`.
- Nếu cả `ViNguonID` và `ViDichID` cùng có giá trị thì chúng phải khác nhau.
- `ChuyenTien` phải có cả ví nguồn và ví đích, đồng thời không có danh mục.
- `Thu` phải có ví đích, không có ví nguồn và có danh mục.
- `Chi` phải có ví nguồn, không có ví đích và có danh mục.
- `NhanVay`, `ThuHoiNo`, `RutTietKiem` phải có ví đích và không có ví nguồn.
- `TraNo`, `ChoVay`, `DongGop` phải có ví nguồn và không có ví đích.
- Bốn loại giao dịch vay/nợ phải có `KhoanVayNoID`; các loại còn lại không được có trường này.
- `DongGop` và `RutTietKiem` phải có `MucTieuID`; các loại khác không được có trường này.
- `Thu` và `Chi` phải có `DanhMucID`; các loại còn lại không được có trường này.
- Áp dụng các CHECK về số tiền, trạng thái và giới hạn giá trị đã nêu tại cấu trúc từng bảng.

**Lưu ý:** Các quy tắc phụ thuộc dữ liệu ở bảng khác không thể được bảo đảm chỉ bằng CHECK cùng dòng; chúng phải được kiểm tra tại Service.

### 2.4.4. Quy tắc bắt buộc validation tại Service

| Quy tắc | Service chịu trách nhiệm |
|---|---|
| Danh mục của giao dịch Thu phải có loại Thu; tương tự cho Chi | `GiaoDichService` |
| Danh mục của ngân sách phải là loại Chi | `NganSachService` |
| Loại khoản vay/nợ phải khớp loại giao dịch | `VayNoService` |
| `NgayGiaoDich` không được vượt ngày hiện tại | `GiaoDichService` |
| Ví sử dụng trong giao dịch phải còn hoạt động | `GiaoDichService` |
| Tổng gốc và lãi/phí khớp số tiền giao dịch trả nợ | `VayNoService`, trong transaction |

## 2.5. Chỉ mục đề xuất

| Bảng | Chỉ mục | Mục đích |
|---|---|---|
| `GiaoDich` | `(NguoiDungID, NgayGiaoDich)` | Báo cáo, lọc kỳ, giao dịch gần đây |
| `GiaoDich` | `(NguoiDungID, LoaiGiaoDich)` | Dashboard, thống kê |
| `GiaoDich` | `(ViNguonID)` và `(ViDichID)` | Tính số dư ví |
| `GiaoDich` | `(MucTieuID)` và `(KhoanVayNoID)` | Tính tiến độ tiết kiệm, tổng hợp vay nợ |

## 2.6. Quy tắc tính số dư

**Công thức chính thức:**

\[
\begin{aligned}
\text{Số dư ví} ={}& \text{SoDuBanDau}\\
&+ \text{Tổng tiền vào qua ViDichID}\\
&- \text{Tổng tiền ra qua ViNguonID}
\end{aligned}
\]

Trong đó:

- **Tiền vào** gồm `Thu`, `NhanVay`, `ThuHoiNo`, `RutTietKiem` và chiều vào của `ChuyenTien`.
- **Tiền ra** gồm `Chi`, `TraNo`, `ChoVay`, `DongGop` và chiều ra của `ChuyenTien`.

Không lưu cột số dư hiện tại. Việc sửa hoặc xóa giao dịch khiến số dư được tính lại tự động, không cần đồng bộ hai nơi.

Với quy mô ứng dụng cá nhân, chi phí truy vấn tổng hợp được xem là chấp nhận được. Nếu cần, có thể đóng gói công thức trong một view dùng chung, nhưng không triển khai đồng thời hai cơ chế lưu và tính số dư.

## 2.7. Quy tắc nghiệp vụ tài chính

| Tình huống | Loại giao dịch | Tác động số dư | Báo cáo thu/chi |
|---|---|---|---|
| Nhận lương | `Thu` | Tăng ví nhận | Có, thu nhập |
| Chi mua hàng | `Chi` | Giảm ví nguồn | Có, chi tiêu |
| Chuyển giữa hai ví | `ChuyenTien` | Giảm ví nguồn, tăng ví đích | Không |
| Nhận tiền vay | `NhanVay` | Tăng ví nhận | Không |
| Trả nợ gồm gốc và lãi | `TraNo` | Giảm ví trả | Không; lãi hiển thị riêng |
| Cho vay | `ChoVay` | Giảm ví cho vay | Không |
| Thu hồi khoản cho vay | `ThuHoiNo` | Tăng ví nhận | Không |
| Đóng góp tiết kiệm | `DongGop` | Giảm ví, tăng tiến độ mục tiêu | Không |
| Rút tiền tiết kiệm | `RutTietKiem` | Tăng ví, giảm tiến độ mục tiêu | Không |
| Hoàn tiền mua hàng | `Thu` | Tăng ví nhận | Có, danh mục “Hoàn tiền” |

## 2.8. Bảo vệ dữ liệu giữa các tài khoản

Mọi truy vấn nghiệp vụ phải giới hạn theo `CurrentUser.NguoiDungID`. Không tin tưởng ID được truyền từ giao diện. Khi lưu hoặc sửa giao dịch, Service phải xác minh toàn bộ ví và danh mục liên quan thuộc người dùng hiện tại.

Lớp bảo vệ thứ hai là FK ghép ở database. Nếu ứng dụng có lỗi và cố gắng ghi giao dịch của người dùng A với ID ví thuộc người dùng B, database sẽ từ chối do cặp `(NguoiDungID, ID)` không hợp lệ.

Đây là thiết kế phòng thủ hai lớp: kiểm tra quyền sở hữu ở ứng dụng và ràng buộc toàn vẹn ở database.
