# TÀI LIỆU 01 — ĐẶC TẢ YÊU CẦU PHẦN MỀM

**File:** `docs/01_Software_Requirements.md`

## 1.1. Thông tin chung

| Hạng mục | Giá trị đã chốt |
|---|---|
| Tên dự án | Phần mềm Quản lý chi tiêu cá nhân |
| Solution/namespace | `QuanLyChiTieu` / `QuanLyChiTieu.*` |
| Database | `QuanLyChiTieuDB` |
| Hệ quản trị dữ liệu | SQL Server LocalDB |
| Nền tảng | Windows Desktop |
| Ngôn ngữ và giao diện | C# / Windows Forms |
| Truy cập dữ liệu | ADO.NET — `System.Data.SqlClient` |
| Framework | .NET Framework 4.8 |
| IDE | Visual Studio 2022, workload .NET desktop development |
| Quy mô nhóm | 4 thành viên |

**Lưu ý:** Tên solution phải được chốt ngay từ đầu để tránh tình trạng mỗi thành viên tạo project với namespace khác nhau.

## 1.2. Mục tiêu

Phần mềm cho phép người dùng:

- Đăng ký, đăng nhập, đăng xuất và đổi mật khẩu an toàn.
- Quản lý danh mục thu/chi.
- Quản lý nhiều ví và tính số dư tự động.
- Ghi nhận thu, chi, chuyển tiền, vay nợ và tiết kiệm thông qua giao dịch.
- Theo dõi số dư, lịch sử giao dịch, thống kê và dashboard.
- Quản lý ngân sách theo danh mục.
- Theo dõi mục tiêu tiết kiệm và các khoản vay/cho vay.
- Xuất báo cáo Excel và PDF.

**Yêu cầu quan trọng nhất:** Tính đúng đắn của dữ liệu tài chính. Số dư ví phải khớp với lịch sử giao dịch trong mọi tình huống thêm, sửa và xóa. Báo cáo không được tính nhầm chuyển tiền nội bộ hoặc các khoản vay nợ.

## 1.3. Phạm vi chức năng

| Mã | Phân hệ | Chức năng |
|---|---|---|
| FR01 | Tài khoản | Đăng ký, đăng nhập, đăng xuất, đổi mật khẩu bằng PBKDF2. |
| FR02 | Danh mục | Thêm, xem, sửa và ngừng sử dụng danh mục thu/chi. |
| FR03 | Ví tiền | Thêm, xem, sửa, ngừng sử dụng ví; tính số dư động. |
| FR04 | Giao dịch | Thêm, sửa, xóa có xác nhận; tìm kiếm theo ghi chú và tên danh mục; lọc theo ngày, loại, danh mục và ví. |
| FR05 | Chuyển tiền | Chuyển giữa hai ví, không tính vào báo cáo thu/chi. |
| FR06 | Dashboard | Tổng thu, tổng chi, chênh lệch, tổng số dư và giao dịch gần đây. |
| FR07 | Thống kê | Báo cáo theo tháng, khoảng ngày và danh mục; hiển thị biểu đồ. |
| FR08 | Ngân sách | Hạn mức theo danh mục Chi và tháng; cảnh báo 80%/100%. |
| FR09 | Tiết kiệm | Quản lý mục tiêu, tính tiến độ từ giao dịch `DongGop`. |
| FR10 | Vay/nợ | Vay, cho vay, trả nợ và thu hồi qua ví; tách gốc và lãi/phí. |
| FR11 | Xuất báo cáo | Xuất Excel bằng ClosedXML và PDF bằng PDFsharp/MigraDoc. |

## 1.4. Phân loại ưu tiên

| Mức | Nội dung | Điều kiện hoàn thành |
|---|---|---|
| P0 | Project, database, kiến trúc chung và `AuthService` có hash mật khẩu. | Hoàn thành trước khi chia code song song. |
| P1 | Tài khoản, danh mục, ví và giao dịch. | Schema phải hỗ trợ đủ 9 loại giao dịch ngay từ script `002`. |
| P2 | Chuyển tiền, dashboard, thống kê, ngân sách, tiết kiệm và vay nợ. | Triển khai sau khi nền tảng dữ liệu ổn định. |
| P3 | Xuất Excel/PDF, hoàn thiện giao diện và kiểm thử tổng thể. | Thực hiện khi các phân hệ chính đã tích hợp. |

**Nguyên tắc thiết kế:** Các loại giao dịch mới phải được đưa vào schema và ràng buộc `CHECK` ngay từ P1. Màn hình nghiệp vụ chuyên sâu có thể triển khai ở P2 để tránh sửa cấu trúc database khi đã có dữ liệu.

## 1.5. Yêu cầu chức năng chi tiết

### A. Tài khoản

- Tên đăng nhập phải duy nhất.
- Mật khẩu được băm bằng PBKDF2, tối thiểu 100.000 vòng, có salt ngẫu nhiên riêng.
- Không lưu mật khẩu dạng văn bản thuần.
- Hỗ trợ đăng nhập, đăng xuất và đổi mật khẩu sau khi xác minh mật khẩu hiện tại.
- Dữ liệu của mỗi người dùng phải độc lập tuyệt đối, được bảo vệ ở cả tầng ứng dụng và database.

### B. Danh mục

- Mỗi danh mục thuộc một người dùng và có loại `Thu` hoặc `Chi`.
- Hỗ trợ thêm, sửa, xem và ngừng sử dụng.
- Không cho chọn danh mục sai loại trong giao dịch.
- Không xóa cứng danh mục đang được giao dịch tham chiếu.
- Danh mục ngừng sử dụng vẫn xuất hiện trong lịch sử cũ nhưng không được chọn cho giao dịch mới.

### C. Ví tiền

- Hỗ trợ ví tiền mặt, ngân hàng và ví điện tử.
- Lưu tên ví, loại ví và số dư ban đầu.
- Số dư hiện tại được tính động, không lưu riêng.
- Được phép sửa `SoDuBanDau`; số dư sẽ được tính lại theo công thức chung.
- Ví ngừng hoạt động không nhận giao dịch mới nhưng vẫn hiển thị số dư và lịch sử cũ.

### D. Giao dịch

- Mỗi giao dịch có loại, số tiền, ngày, ví theo quy ước hướng tiền và ghi chú.
- Giao dịch Thu/Chi phải có danh mục phù hợp.
- Hỗ trợ thêm, xem, sửa và xóa.
- Xóa giao dịch là xóa cứng, phải có hộp thoại xác nhận và thông báo rằng số dư sẽ được tính lại.
- Tìm kiếm theo `GhiChu` và tên danh mục.
- Lọc theo ngày, loại giao dịch, danh mục và ví.
- Không cho nhập ngày giao dịch trong tương lai.

### E. Dashboard

- Hiển thị tổng thu, tổng chi, chênh lệch và tổng số dư các ví đang hoạt động.
- Hiển thị giao dịch gần đây, sắp xếp theo `NgayGiaoDich DESC`, sau đó `NgayTao DESC` để ổn định khi trùng ngày.
- Sử dụng `System.Windows.Forms.DataVisualization`.
- Dữ liệu phải lấy từ database thực tế, không dùng số liệu minh họa cố định.

### F. Ngân sách

- Hạn mức được thiết lập theo danh mục Chi và tháng.
- Chỉ cộng giao dịch loại `Chi`.
- Không cộng chuyển tiền hoặc trả nợ vào chi tiêu sinh hoạt.
- Hiển thị số đã chi, số còn lại và tỷ lệ sử dụng.
- Cảnh báo khi đạt 80%, 100% hoặc vượt hạn mức.

### G. Mục tiêu tiết kiệm

- Mỗi mục tiêu có tên, số tiền mục tiêu, ngày bắt đầu và ngày kết thúc.
- Tiến độ được tính bằng công thức:

\[
\text{Tiến độ} = \sum \text{DongGop} - \sum \text{RutTietKiem}
\]

- Không lưu tiến độ nhập tay trong cột `SoTienDaDat`, nhằm tránh tính trùng dữ liệu.

### H. Vay/nợ

- Quản lý khoản vay và cho vay với số tiền gốc, lãi suất, ngày phát sinh, ngày đến hạn và trạng thái.
- Lãi suất quy ước theo phần trăm mỗi năm: `12.0000` tương đương 12%/năm.
- Sử dụng lãi đơn; `NULL` biểu thị khoản vay không tính lãi.
- Mọi dòng tiền đi qua ví và bảng `GiaoDich`:
  - Nhận vay: `NhanVay`.
  - Trả nợ: `TraNo`.
  - Cho vay: `ChoVay`.
  - Thu hồi nợ: `ThuHoiNo`.
- Mỗi lần trả nợ phải tạo bản ghi `LichSuTraNo`, tách tiền gốc và lãi/phí trong cùng transaction với giao dịch `TraNo`.
- Lãi/phí không được cộng vào báo cáo chi tiêu sinh hoạt; chỉ hiển thị trong phân hệ Vay/nợ.

### I. Xuất báo cáo

- Cho phép chọn tháng hoặc khoảng ngày.
- Báo cáo phải tuân theo bộ lọc đã chọn.
- Báo cáo thu/chi chỉ bao gồm giao dịch `Thu` và `Chi`.
- Nếu không có dữ liệu, file vẫn có tiêu đề và ghi chú phù hợp.
- Chỉ xuất dữ liệu của người dùng hiện tại.

## 1.6. Yêu cầu phi chức năng

| Mã | Yêu cầu |
|---|---|
| NFR01 | Giao diện nhất quán theo UI Standard v1.1. |
| NFR02 | Dữ liệu được lưu bền vững trong LocalDB. |
| NFR03 | SQL tham số hóa, không ghép chuỗi truy vấn. |
| NFR04 | Không lưu mật khẩu dạng văn bản thuần. |
| NFR05 | Không để lộ dữ liệu giữa các tài khoản; có bảo vệ ở database. |
| NFR06 | Kiểm tra dữ liệu ở cả UI và Service. |
| NFR07 | Thông báo lỗi database phù hợp, không làm lộ connection string. |
| NFR08 | Tiền dùng `DECIMAL(18,2)`, không dùng `FLOAT`. |
| NFR09 | Thao tác nhiều bước dùng transaction và rollback khi thất bại. |
| NFR10 | Có thể cài đặt và chạy theo README. |
| NFR11 | PBKDF2 với tối thiểu 100.000 vòng và salt ngẫu nhiên riêng cho mỗi tài khoản. |

## 1.7. Ngoài phạm vi

Các chức năng không thuộc phiên bản này:

- Đồng bộ ngân hàng thực.
- Thanh toán trực tuyến.
- Khôi phục mật khẩu qua email.
- Giao dịch định kỳ tự động.
- Nhập hàng loạt CSV/Excel.
- Chatbot AI.
- Đồng bộ cloud.
- Lãi kép và lịch thanh toán tự động cho khoản vay.
