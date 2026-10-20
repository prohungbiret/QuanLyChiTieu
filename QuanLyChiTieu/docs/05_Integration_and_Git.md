# TÀI LIỆU 05 — HỢP ĐỒNG TÍCH HỢP VÀ QUY TRÌNH GIT
*File: docs/05_Integration_and_Git.md — bản rút gọn từ bộ tài liệu chuẩn v2.0*

## 5.1 Nguyên tắc
Nhóm trưởng (TV1) dựng nền tảng; các thành viên phát triển phân hệ độc lập; tích hợp qua hợp đồng đã chốt.
Service của phân hệ nào do phân hệ đó tự viết, **không dùng chéo service**; trao đổi giữa các phân hệ đi qua
**database** (bảng + quy ước ở Tài liệu 02) → không ai phải chờ API của ai, chỉ cần schema chung chốt từ P0.

## 5.2 Phân quyền file
| File/khu vực | Chủ sở hữu | Quy tắc |
|---|---|---|
| Program.cs, App.config, Data/Database.cs, CurrentUser, UI/Theme.cs + UIHelper.cs, FrmMain.*, FrmLogin, Services/AuthService.cs, Database/, **004_SeedData.sql** | **TV1** | Khác không tự sửa; thay đổi phải báo nhóm |
| UcViTien.*, UcGiaoDich.*, FrmThemViTien.*, FrmThemGiaoDich.*, **FrmRegister**, ViTienService, GiaoDichService | **TV2** | Sở hữu toàn bộ file liên quan (.cs/.Designer/.resx) |
| UcDanhMuc.*, UcNganSach.*, UcThongKe.*, **FrmChangePassword**, DanhMucService, NganSachService, ThongKeService | **TV3** | Như trên |
| UcTietKiem.*, UcVayNo.*, TietKiemService, VayNoService, BaoCaoService | **TV4** | Như trên |

## 5.3 Hợp đồng MainForm – UserControl
FrmMain: sidebar, pnlContent, điều hướng, giữ phiên, mở UserControl.
UserControl: hiển thị + đọc/ghi phân hệ mình, validation, xử lý lỗi, tải lại danh sách.
Không tự tạo FrmMain mới, không mở nhiều cửa sổ chính, không tự đổi sidebar.

`ShowControl` chuẩn v2 (sửa rò rỉ tài nguyên của v1) — đã triển khai trong `FrmMain.cs`:

```csharp
private void ShowControl(UserControl control)
{
    var oldControls = pnlContent.Controls.OfType<UserControl>().ToList();
    pnlContent.Controls.Clear();
    foreach (var old in oldControls) old.Dispose();
    control.Dock = DockStyle.Fill;
    pnlContent.Controls.Add(control);
}
```
Tạo mới UserControl mỗi lần điều hướng (dữ liệu luôn tươi). Header title đọc `control.Tag`.

## 5.4 Hợp đồng UserControl – form nhập
Mở form bằng `ShowDialog()`; form tự validation + lưu; **chỉ khi lưu thành công mới đặt DialogResult.OK**;
UserControl kiểm tra kết quả và tải lại dữ liệu (mẫu: `UcDangXayDung` + `FrmMauNhap`).

## 5.5 Hợp đồng dữ liệu giữa các phân hệ
| Phân hệ | Dữ liệu cần | Nguồn |
|---|---|---|
| Danh mục / Ví tiền | user, loại, trạng thái | DanhMuc / ViTien |
| Giao dịch | user, ví, danh mục, 9 loại | GiaoDich (hằng số `GiaoDichLoai`) |
| Dashboard | tổng hợp GD + số dư ví | Truy vấn tổng hợp |
| Ngân sách / Thống kê | GD loại Chi hợp lệ, kỳ | NganSach + GiaoDich |
| Tiết kiệm / Vay nợ | mục tiêu, khoản vay, lịch sử trả | MucTieuTietKiem / KhoanVayNo / LichSuTraNo / GiaoDich |
| Xuất báo cáo | GD lọc user + kỳ, chỉ Thu + Chi | GiaoDich + DanhMuc + ViTien |

Cấm tự đổi ý nghĩa trường chung; `LoaiGiaoDich` phải dùng đúng 9 giá trị của lớp `GiaoDichLoai`.

## 5.6 Quy trình Git
Ba cấp: **main** (ổn định) → **develop** (tích hợp) → **feature/...** (auth, wallet-transaction,
category-budget-report, saving-debt-export). TV1 commit nền tảng → thành viên pull → tạo branch →
chỉ sửa file trong phạm vi → commit nhỏ (`feat(auth):`, `fix(transaction):`, `style(ui):`, `docs:`) →
push + PR → TV1 review → build pass → merge.

`.gitignore` tường minh: `bin/ obj/ .vs/ *.mdf *.ldf *.ndf *.user *.suo *.log Exports/`.
Không commit database LocalDB chứa dữ liệu thật — dữ liệu demo chỉ nằm trong `004_SeedData.sql`.

## 5.7 Xử lý xung đột
Không hai người chỉnh cùng file Designer; không đổi schema khi người khác đang dùng bản cũ;
đổi API chung phải báo nhóm + cập nhật tài liệu trước; merge conflict do chủ sở hữu file xác nhận;
sau merge chạy build + kiểm tra màn hình liên quan.

## 5.8 Database khi làm song.song
Mỗi người một LocalDB riêng tạo từ cùng bộ script (001→004); không chia sẻ .mdf đa máy;
không tự đổi cột/kiểu; thay đổi schema đi qua: đề xuất → thống nhất TV1 → sửa script chính thức →
kiểm tra trên DB mới → cập nhật seed → thông báo cả nhóm.
