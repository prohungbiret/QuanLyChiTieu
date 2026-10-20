# TÀI LIỆU 07 — KẾ HOẠCH KIỂM THỨ
*File: docs/07_Test_Plan.md — mỗi TV chạy nhóm TC của phân hệ mình trước PR; TV1 chạy nhóm tích hợp sau mỗi merge*

## 7.2 Tài khoản
| ID | Tình huống | Kết quả mong đợi |
|---|---|---|
| TC-AUTH-01 | Đăng ký hợp lệ | Tạo thành công, MatKhauHash đúng format PBKDF2 |
| TC-AUTH-02 | Đăng ký trùng tên đăng nhập | Từ chối, thông báo rõ |
| TC-AUTH-03 | Đăng nhập sai mật khẩu | Không cho đăng nhập |
| TC-AUTH-04 | Đăng nhập đúng | Mở màn hình chính |
| TC-AUTH-05 | Đăng xuất | Kết thúc phiên, về đăng nhập |
| TC-AUTH-06 | Đổi mật khẩu sai mật khẩu hiện tại | Từ chối |
| TC-AUTH-07 | Hai tài khoản cùng dùng app | Dữ liệu phân tách |
| TC-AUTH-08 | Đổi mật khẩu rồi đăng nhập lại | Mật khẩu mới vào được, mật khẩu cũ bị từ chối |

## 7.3 Ví và giao dịch
| ID | Tình huống | Kết quả mong đợi |
|---|---|---|
| TC-TR-01 | Thêm ví hợp lệ | Lưu, số dư = số dư ban đầu |
| TC-TR-02/03 | Thêm giao dịch Thu / Chi | Số dư ví tăng / giảm đúng |
| TC-TR-04 | Số tiền 0 hoặc âm | Từ chối |
| TC-TR-05 | Ngày giao dịch tương lai | Từ chối, thông báo rõ |
| TC-TR-06 | Chuyển tiền giữa hai ví | Nguồn giảm, đích tăng đúng |
| TC-TR-07 | Chuyển vào chính ví nguồn | Từ chối ở app và ở CHECK |
| TC-TR-08/09 | Sửa / xóa giao dịch | Số dư tính lại đúng |
| TC-TR-10 | Danh mục sai loại | Từ chối |
| TC-TR-11 | Chọn ví thuộc user khác | App từ chối; INSERT bị FK ghép từ chối ở SQL |
| TC-TR-12 | Tìm theo từ khóa (GhiChu + tên danh mục) | Kết quả đúng bộ lọc |
| TC-TR-13 | Sửa số dư ban đầu của ví | Số dư hiển thị tính lại đúng (2.6) |
| TC-TR-14 | Ngừng sử dụng ví đang có giao dịch | Không nhận GD mới, số dư cũ vẫn hiển thị |
| TC-TR-15 | Truy cập dữ liệu user khác bằng ID | Không hiển thị/không cập nhật được |
| TC-TR-16 | NhanVay với KhoanVayNoID sai loại | Từ chối ở Service (Bảng 2.3c) |

## 7.4 Ngân sách và thống kê
| ID | Tình huống | Kết quả mong đợi |
|---|---|---|
| TC-RPT-01 | Không có giao dịch trong kỳ | 0 / trạng thái trống |
| TC-RPT-02 | Có thu và chi | Tổng hợp chính xác |
| TC-RPT-03 | Chuyển tiền nội bộ | Không cộng vào thu/chi |
| TC-RPT-04/05/06 | Đạt 79% / 80% / 100% hạn mức | Chưa cảnh báo / cảnh báo 80% / cảnh báo đạt hạn mức |
| TC-RPT-07 | Đổi tháng thống kê | Dữ liệu đổi đúng |
| TC-RPT-08 | Nhiều danh mục | Biểu đồ phân bổ chính xác |
| TC-RPT-09 | Chi vượt 110% hạn mức | Cảnh báo vượt, phần trăm đúng |
| TC-RPT-10 | Có TraNo chứa tiền lãi | Lãi không vào báo cáo chi tiêu sinh hoạt, chỉ hiện ở Vay/nợ |

## 7.5 Tiết kiệm và vay nợ
| ID | Tình huống | Kết quả mong đợi |
|---|---|---|
| TC-ADV-01 | Tạo mục tiêu tiết kiệm | Lưu, tiến độ = 0 |
| TC-ADV-02 | Đóng góp vào mục tiêu | Ví giảm, tiến độ + số còn thiếu đúng |
| TC-ADV-03 | Đạt mục tiêu | TrangThai = HoanThanh |
| TC-ADV-04 | Đóng góp 2 lần rồi sửa 1 GD | Tiến độ tính lại đúng, không tính trùng |
| TC-ADV-05 | Tạo khoản vay | Theo dõi số gốc + dư nợ gốc |
| TC-ADV-06 | Nhận vay (NhanVay) | Ví tăng đúng, báo cáo thu không tăng |
| TC-ADV-07 | Trả gốc + lãi (TraNo) | Dư nợ giảm đúng; TienGoc + TienLaiPhi = SoTien |
| TC-ADV-08 | Thu hồi nợ (ThuHoiNo) | Ví tăng, dư nợ cho vay giảm, báo cáo thu không tăng |
| TC-ADV-09 | Báo cáo thu chi khi đang có vay | Không tính sai khoản vay/trả gốc |

## 7.6–7.8 Xuất báo cáo, tích hợp, quy trình lỗi
- **Xuất báo cáo (Excel + PDF):** có/không có dữ liệu; đúng khoảng ngày; định dạng tiền/ngày vi-VN;
  tiếng Việt hiển thị đúng; file mở bằng phần mềm tương ứng; không chứa dữ liệu tài khoản khác;
  báo lỗi khi không ghi được file.
- **Tích hợp:** đăng nhập mở đúng FrmMain; sidebar mở đúng UserControl; ShowDialog/DialogResult đúng
  hợp đồng; Dashboard cập nhật sau GD mới; **tạo GD từ phân hệ này → số dư + tiến độ phân hệ khác
  cập nhật đúng**; đăng xuất không lộ dữ liệu; build pass sau merge; chạy được trên máy khác.
- **Quy trình ghi lỗi:** mã lỗi; phân hệ + màn hình; bước tái hiện; dữ liệu đầu vào; kết quả thực tế;
  kết quả mong đợi; ảnh; người phụ trách; trạng thái. Cấm chỉ nhắn "bị lỗi".
