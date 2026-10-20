# TÀI LIỆU 03 — TIÊU CHUẨN GIAO DIỆN UI STANDARD v1.1

**File:** `docs/03_UI_Standard.md`

## 3.1. Bảng màu chính thức

| Tên màu | Mã màu | Mục đích sử dụng |
|---|---|---|
| Navy | `#172B4D` | Sidebar, header bảng, vùng nhấn |
| Primary blue | `#2563EB` | Nút chính, mục đang chọn, liên kết |
| Blue light | `#DBEAFE` | Nền được chọn, vùng nhấn nhẹ |
| Background | `#F4F7FB` | Nền nội dung chính |
| White | `#FFFFFF` | Card, bảng, form |
| Primary text | `#172033` | Tiêu đề, nội dung chính |
| Secondary text | `#64748B` | Chú thích, nhãn phụ |
| Border | `#E2E8F0` | Viền card, bảng và control |
| Thu/thành công | `#16A34A` | Giao dịch thu |
| Chi/lỗi | `#DC2626` | Giao dịch chi, lỗi |
| Cảnh báo | `#D97706` | Cảnh báo ngân sách |

## 3.2. Quy tắc hiển thị 9 loại giao dịch

Màu sắc không được là dấu hiệu duy nhất để nhận biết loại giao dịch. Mỗi giao dịch phải có nhãn chữ.

| Loại giao dịch | Nhãn hiển thị | Màu nhãn |
|---|---|---|
| `Thu` | Thu | `#16A34A` |
| `Chi` | Chi | `#DC2626` |
| `ChuyenTien` | Chuyển | `#2563EB` |
| `NhanVay` | Nhận vay | `#7C3AED` |
| `TraNo` | Trả nợ | `#9333EA` |
| `ChoVay` | Cho vay | `#0E7490` |
| `ThuHoiNo` | Thu hồi nợ | `#0891B2` |
| `DongGop` | Đóng góp | `#D97706` |
| `RutTietKiem` | Rút tiết kiệm | `#CA8A04` |

## 3.3. Font, kích thước và bố cục

### 3.3.1. Quy chuẩn kích thước

- **Font chính:** Segoe UI.
- **Tiêu đề màn hình:** 20 px, semibold.
- **Card:** 14–16 px.
- **Nội dung:** 13–14 px.
- **Bảng:** 12–13 px.
- **Control:** cao 34–38 px.
- **Nút:** cao 36–40 px.
- **Khoảng cách chuẩn:** 8, 12, 16 và 24 px.

### 3.3.2. Kích thước cửa sổ và thành phần

| Thành phần | Kích thước đề xuất |
|---|---|
| `MainForm` | Khoảng 1280 × 800 px |
| Kích thước tối thiểu | Khoảng 1024 × 650 px |
| Sidebar | Rộng 220–240 px |
| Header | Cao 56–64 px |

Bắt buộc sử dụng `Dock`, `Anchor`, `TableLayoutPanel` hoặc `FlowLayoutPanel` phù hợp. Không thiết kế bằng tọa độ cố định khiến giao diện vỡ khi thay đổi kích thước cửa sổ.

### 3.3.3. Quy tắc DPI

- Các form chính phải thống nhất `AutoScaleMode` theo `Dpi` hoặc `Font`, không trộn lẫn tùy tiện.
- Bắt buộc kiểm thử ở tỷ lệ hiển thị 125% và 150%.

## 3.4. DataGridView và form nhập liệu

### 3.4.1. Màn hình danh sách

Bố cục chuẩn gồm:

1. Tiêu đề màn hình.
2. Tìm kiếm và bộ lọc.
3. Các nút thao tác.
4. `DataGridView`.
5. Số bản ghi hoặc trạng thái rỗng.

**Quy tắc hiển thị:**

- Header dùng nền Navy.
- Các dòng có màu xen kẽ.
- Chiều cao dòng 32–36 px.
- Số tiền căn phải.
- Ngày theo định dạng `dd/MM/yyyy`.
- Sử dụng `UIHelper.StyleDataGridView` để thống nhất giao diện.

### 3.4.2. Form nhập liệu

- Nhãn đặt phía trên trường nhập.
- Dùng dấu `*` cho trường bắt buộc.
- Nút Lưu dùng màu chủ đạo.
- Khi validation thất bại, giữ nguyên dữ liệu người dùng đã nhập.
- ComboBox thay đổi theo loại giao dịch:
  - Danh sách ví tuân theo hướng tiền vào/ra.
  - Danh mục chỉ xuất hiện cho Thu/Chi và phải đúng loại.
  - Khung mục tiêu tiết kiệm hoặc khoản vay/nợ được hiển thị khi phù hợp.
- Tiền theo định dạng Việt Nam, ví dụ `1.250.000 ₫`.
- Không lạm dụng MessageBox cho các thông báo không cần thiết.

## 3.5. Theme.cs, UIHelper.cs và icon

Thư mục `UI/` chứa:

- `Theme.cs`: màu sắc, font, kích thước và khoảng cách chuẩn.
- `UIHelper.cs`: định dạng tiền, ngày, nhãn giao dịch, màu sắc và style DataGridView.

WinForms không tự áp dụng theme. Mọi control phải được cấu hình dựa trên các giá trị dùng chung.

**Quy ước icon:**

- Ưu tiên nhãn chữ để tiết kiệm công sức và đảm bảo đồng nhất.
- Nếu bổ sung icon, sử dụng ký tự từ font Segoe MDL2 Assets thông qua một hàm tạo chung trong `UIHelper`.
- Không trộn các bộ PNG có kích thước không đồng bộ.

## 3.6. Checklist nghiệm thu giao diện

- [ ] Đúng bảng màu và font.
- [ ] Không vỡ bố cục khi resize.
- [ ] Hiển thị đúng ở DPI 125% và 150%.
- [ ] Có trạng thái rỗng khi chưa có dữ liệu.
- [ ] Dữ liệu dài không bị che hoặc cắt sai.
- [ ] Mỗi loại giao dịch có nhãn và màu đúng quy định.
- [ ] Có xác nhận khi xóa.
- [ ] Tiền và ngày đúng định dạng.
- [ ] Không dùng số liệu mẫu cố định thay cho dữ liệu thật.
- [ ] Kiểm thử thực tế trên ít nhất hai máy.
