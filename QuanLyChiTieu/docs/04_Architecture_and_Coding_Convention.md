# TÀI LIỆU 04 — KIẾN TRÚC, CẤU TRÚC THƯ MỤC VÀ QUY ƯỚC CODE

**File:** `docs/04_Architecture_and_Coding_Convention.md`

---

## 4.1. Kiến trúc ứng dụng

### Luồng xử lý chuẩn

```text
WinForms UI
    ↓
Kiểm tra đầu vào
    ↓
Service
(Nghiệp vụ + Validation)
    ↓
Data / ADO.NET
    ↓
SQL Server LocalDB
```

Không đặt câu lệnh SQL, công thức tính số dư hoặc toàn bộ logic validation trực tiếp trong sự kiện `Click` của giao diện.

### Các quyết định nền tảng

1. **Framework:** Sử dụng .NET Framework 4.8, phù hợp với môi trường phát triển Windows Forms và có thể sử dụng Chart control.
2. **Thư viện truy cập dữ liệu:** Sử dụng `System.Data.SqlClient`, không sử dụng `Microsoft.Data.SqlClient`.
3. **Phiên bản C#:** Tuân thủ C# 7.3, không sử dụng các tính năng ngôn ngữ mới hơn phiên bản này.

---

## 4.2. Thư viện sử dụng

| Hạng mục | Thư viện | Cách thêm | Lý do |
|---|---|---|---|
| Biểu đồ | `System.Windows.Forms.DataVisualization` | Add Reference | Có sẵn trong môi trường .NET Framework 4.8 |
| Xuất Excel | ClosedXML | NuGet | Giấy phép MIT |
| Xuất PDF | PDFsharp/MigraDoc 6 | NuGet | Hỗ trợ .NET Framework phù hợp |
| Hash mật khẩu | PBKDF2 qua `Rfc2898DeriveBytes` | Có sẵn trong framework | Không cần thư viện bên ngoài |

Không sử dụng **iTextSharp (AGPL)** hoặc **EPPlus 5+** trong bộ cấu hình này do các cân nhắc về giấy phép đã được thống nhất trong tài liệu.

---

## 4.3. Cấu trúc thư mục chính thức

```text
QuanLyChiTieu/
├── Program.cs
├── App.config
├── README.md
├── .gitignore
│
├── Database/
│   ├── 001_CreateDatabase.sql
│   ├── 002_CreateTables.sql
│   ├── 003_CreateConstraints.sql
│   └── 004_SeedData.sql
│
├── Models/
│   ├── NguoiDung.cs
│   ├── DanhMuc.cs
│   ├── ViTien.cs
│   ├── GiaoDich.cs
│   ├── NganSach.cs
│   ├── MucTieuTietKiem.cs
│   ├── KhoanVayNo.cs
│   ├── LichSuTraNo.cs
│   └── GiaoDichLoai.cs
│
├── Data/
│   └── Database.cs
│
├── Services/
│   ├── AuthService.cs
│   ├── ViTienService.cs
│   ├── GiaoDichService.cs
│   ├── DanhMucService.cs
│   ├── NganSachService.cs
│   ├── ThongKeService.cs
│   ├── TietKiemService.cs
│   ├── VayNoService.cs
│   └── BaoCaoService.cs
│
├── UI/
│   ├── Theme.cs
│   └── UIHelper.cs
│
├── Forms/
│   ├── FrmLogin
│   ├── FrmRegister
│   ├── FrmChangePassword
│   ├── FrmMain
│   ├── FrmThemViTien
│   └── FrmThemGiaoDich
│
├── Controls/
│   ├── UcDashboard
│   ├── UcViTien
│   ├── UcGiaoDich
│   ├── UcDanhMuc
│   ├── UcNganSach
│   ├── UcThongKe
│   ├── UcTietKiem
│   └── UcVayNo
│
└── docs/
    ├── 01_Software_Requirements.md
    ├── 02_Database_Design.md
    ├── 03_UI_Standard.md
    ├── 04_Architecture_and_Coding_Convention.md
    ├── 05_Integration_and_Git.md
    ├── 06_Task_Assignment.md
    ├── 07_Test_Plan.md
    └── 08_Setup_and_Handover.md
```

### Quy tắc quản lý file WinForms

Các file thuộc cùng một form WinForms phải được quản lý đồng bộ, bao gồm:

- `.cs`
- `.Designer.cs`
- `.resx` (nếu có)

### Quy tắc sở hữu Service

- Mỗi thành viên tự viết Service cho phân hệ mình phụ trách.
- Chỉ `Database.cs` là lớp truy cập kết nối dùng chung.
- Service có thể truy vấn bảng thuộc phân hệ khác khi cần.
- Service không phụ thuộc trực tiếp vào class Service của thành viên khác.

**Ví dụ:** `NganSachService` có thể đọc bảng `GiaoDich` để tính tổng chi, nhưng không phụ thuộc vào `GiaoDichService` do thành viên khác phát triển.

---

## 4.4. Quy ước đặt tên

| Thành phần | Quy tắc | Ví dụ |
|---|---|---|
| Form | Tiền tố `Frm` | `FrmLogin` |
| UserControl | Tiền tố `Uc` | `UcDashboard` |
| Model | PascalCase | `GiaoDich` |
| Method/property | PascalCase | `ThemGiaoDich` |
| Biến/tham số | camelCase | `soTien` |
| Button | Tiền tố `btn` | `btnLuu` |
| TextBox | Tiền tố `txt` | `txtSoTien` |
| DataGridView | Tiền tố `dgv` | `dgvGiaoDich` |
| ComboBox | Tiền tố `cbo` | `cboLoaiGiaoDich` |

Namespace phải tuân theo cấu trúc:

```csharp
QuanLyChiTieu.*
```

---

## 4.5. Hằng số loại giao dịch

Tất cả loại giao dịch phải được khai báo tại một lớp duy nhất, tránh rải các chuỗi tùy ý trong toàn dự án.

**File:** `Models/GiaoDichLoai.cs`

```csharp
namespace QuanLyChiTieu.Models
{
    public static class GiaoDichLoai
    {
        public const string Thu = "Thu";
        public const string Chi = "Chi";
        public const string ChuyenTien = "ChuyenTien";
        public const string NhanVay = "NhanVay";
        public const string TraNo = "TraNo";
        public const string ChoVay = "ChoVay";
        public const string ThuHoiNo = "ThuHoiNo";
        public const string DongGop = "DongGop";
        public const string RutTietKiem = "RutTietKiem";

        // Chỉ Thu và Chi được tính vào báo cáo thu/chi.
        public static readonly string[] TinhVaoBaoCao =
        {
            Thu,
            Chi
        };
    }
}
```

### Các quy tắc code chung

- Không ghép chuỗi SQL để tạo câu truy vấn.
- Không sử dụng `catch` rỗng.
- Không tự ý chỉnh sửa file Designer của thành viên khác.
- Không đổi tên API chung khi thành viên khác đang sử dụng.
- Code phải build thành công trước khi commit.
- Thay đổi nghiệp vụ phải tuân theo schema và các quy ước chung.

---

## 4.6. Quy tắc ADO.NET

### Nguyên tắc

- Sử dụng `SqlConnection`, `SqlCommand` và `SqlDataReader` trong khối `using` để quản lý tài nguyên.
- Sử dụng tham số SQL thay vì ghép chuỗi.
- Dữ liệu tiền tệ sử dụng `SqlDbType.Decimal` và khai báo `Precision`/`Scale` phù hợp.
- Cột ngày có kiểu SQL `DATE` phải sử dụng tham số `SqlDbType.Date`.

### Ví dụ truy vấn chuẩn

```csharp
string sql = @"
SELECT GiaoDichID, LoaiGiaoDich, SoTien, NgayGiaoDich
FROM GiaoDich
WHERE NguoiDungID = @NguoiDungID
  AND LoaiGiaoDich IN (@LoaiThu, @LoaiChi)
  AND NgayGiaoDich >= @TuNgay
  AND NgayGiaoDich <= @DenNgay
ORDER BY NgayGiaoDich DESC, NgayTao DESC;";

using (SqlConnection conn = Database.GetConnection())
using (SqlCommand cmd = new SqlCommand(sql, conn))
{
    cmd.Parameters.Add("@NguoiDungID", SqlDbType.Int)
        .Value = CurrentUser.NguoiDungID;

    cmd.Parameters.Add("@LoaiThu", SqlDbType.NVarChar, 20)
        .Value = GiaoDichLoai.Thu;

    cmd.Parameters.Add("@LoaiChi", SqlDbType.NVarChar, 20)
        .Value = GiaoDichLoai.Chi;

    cmd.Parameters.Add("@TuNgay", SqlDbType.Date)
        .Value = tuNgay;

    cmd.Parameters.Add("@DenNgay", SqlDbType.Date)
        .Value = denNgay;

    conn.Open();

    using (SqlDataReader reader = cmd.ExecuteReader())
    {
        while (reader.Read())
        {
            // Đọc kết quả theo quy ước của phân hệ.
        }
    }
}
```

---

## 4.7. Quy tắc xử lý lỗi

| Loại lỗi | Cách xử lý |
|---|---|
| Lỗi nhập liệu | Chỉ rõ trường không hợp lệ |
| Lỗi nghiệp vụ | Nêu lý do từ chối, không ghi dữ liệu sai |
| Lỗi kết nối | Hướng dẫn kiểm tra cấu hình LocalDB |
| Lỗi SQL | Ghi log phục vụ debug; không tiết lộ connection string |
| Thao tác nhiều bước thất bại | Rollback toàn bộ transaction |

**Nguyên tắc:** Không thông báo thao tác thành công trước khi database xác nhận thao tác đó.

---

## 4.8. CurrentUser

`CurrentUser` là lớp tĩnh lưu giữ thông tin người dùng hiện tại, tối thiểu gồm:

- `NguoiDungID`
- `TenHienThi`

Lớp này giúp các phân hệ lấy thông tin người dùng hiện tại theo một cách thống nhất.

Tuy nhiên, `CurrentUser` **không thay thế việc kiểm tra quyền sở hữu dữ liệu trong SQL**. Các truy vấn và thao tác dữ liệu vẫn phải đảm bảo dữ liệu được truy cập đúng người dùng.

---

## 4.9. Đặc tả hash mật khẩu PBKDF2

### Tham số đã thống nhất

| Tham số | Giá trị |
|---|---|
| Thuật toán | PBKDF2 với HMAC-SHA256 |
| Số vòng lặp | `100000` |
| Salt | 16 byte ngẫu nhiên cho mỗi tài khoản |
| Độ dài hash | 32 byte |
| Định dạng lưu salt và hash | Base64 |

### Định dạng lưu trữ

Giá trị được lưu trong cột `MatKhauHash` theo định dạng:

```text
PBKDF2.<iterations>.<saltBase64>.<hashBase64>
```

Ví dụ minh họa:

```text
PBKDF2.100000.aX9k...c3o=.mQ7vZ2...f4w=
```

> Chuỗi trên chỉ minh họa định dạng. Các đoạn `...` không phải dữ liệu Base64 đầy đủ có thể dùng trực tiếp để xác thực.

### Quy trình xác thực khi đăng nhập

1. Tách chuỗi lưu trữ thành các thành phần.
2. Kiểm tra định dạng và số vòng lặp.
3. Giải mã salt và hash từ Base64.
4. Tính lại hash bằng mật khẩu người dùng nhập vào cùng salt và tham số tương ứng.
5. So sánh hai mảng byte.
6. Nếu chuỗi lưu trữ sai định dạng, coi là xác thực thất bại; không để exception làm sập giao diện.

Lưu số vòng lặp trong chuỗi giúp hệ thống có thể xác minh các hash cũ nếu sau này cần tăng tham số PBKDF2.

---

**Kết thúc Tài liệu 04.**