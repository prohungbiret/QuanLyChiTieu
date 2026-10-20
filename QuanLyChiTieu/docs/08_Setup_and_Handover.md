# TÀI LIỆU 08 — QUY TRÌNH KHỞI TẠO, TRIỂN KHAI VÀ BÀN GIAO
*File: docs/08_Setup_and_Handover.md — bản rút gọn từ bộ tài liệu chuẩn v2.0*

## 8.1 Việc TV1 làm trước khi chia code
Project → Git + .gitignore → script schema (001→003) → LocalDB + connection string → Database.cs,
CurrentUser, AuthService PBKDF2 → Theme.cs + UIHelper.cs + màn hình mẫu → FrmMain + FrmLogin →
README → chốt API tích hợp → build + chạy thử. Chỉ sau khi nền tảng ổn định mới chia việc phân hệ.

## 8.2 Chuẩn bị máy phát triển
- Visual Studio 2022 (hoặc 2019) + workload **.NET desktop development**
- **.NET Framework 4.8 Developer Pack**
- **SQL Server Express LocalDB** — kiểm tra bằng `sqllocaldb info` (thường `(localdb)\MSSQLLocalDB`)
- Git + clone + pull nhánh thống nhất

## 8.3 Connection string (đã cấu hình trong App.config)
```xml
<add name="QuanLyChiTieuConnection"
     connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyChiTieuDB;Integrated Security=True;Connect Timeout=30;"
     providerName="System.Data.SqlClient" />
```

## 8.4 Thứ tự triển khai theo giai đoạn (phụ thuộc đầu ra, không gắn ngày)
1. **Chốt nền tảng** — cả nhóm đọc Phụ lục B (QD01–QD15), TV1 hoàn thành checklist nền.
2. **Phát triển phân hệ độc lập** — TV2 / TV3 / TV4 theo docs/06, mỗi người một branch feature.
3. **Tích hợp từng phần** — chức năng cốt lõi xong → review, merge, kiểm thử luồng liên quan.
4. **Kiểm thử + hoàn thiện** — số dư, tách dữ liệu tài khoản, báo cáo, nhập liệu, UI, kịch bản demo.

## 8.5 Yêu cầu dữ liệu seed cho demo — đã hiện thực trong 004_SeedData.sql
- 3 tài khoản PBKDF2 đăng nhập được (demo chính: `truongnhom / chitieu123`)
- 4 ví đủ loại + 1 ví đã ngừng; ≥60 giao dịch Thu/Chi trải 2 tháng
- 1 ChuyenTien; ngân sách đủ 3 vùng (bình thường / cảnh báo 80% / đạt + vượt 100%)
- 1 mục tiêu ~70% tiến độ (có DongGop + RutTietKiem); 1 khoản vay dư nợ + lịch sử trả gốc–lãi;
  1 khoản cho vay đã hoàn tất

## 8.6 Quy trình bàn giao phân hệ
Branch riêng → build → chạy với DB từ script → kiểm thử CRUD + biên → kiểm tra quyền sở hữu →
UI theo Standard → commit + push → hướng dẫn chạy + ghi chú tích hợp → TV1 review + merge → test lại.

## 8.7 README tối thiểu (đã trong README.md)
Tên + mô tả; công nghệ; yêu cầu môi trường; clone; tạo DB; cấu hình connection; chạy; tài khoản demo;
phân hệ hoàn thành; giới hạn đã biết.

## 8.8 Tiêu chí hoàn thành dự án
Đủ docs; DB từ script; build OK; đăng ký/đăng nhập chạy; không lộ dữ liệu giữa tài khoản; số dư đúng
sau thêm/sửa/xóa với cả 9 loại (2.6); ChuyenTien không sai báo cáo; vay nợ + tiết kiệm đúng 2.7;
dashboard/thống kê dữ liệu thật; UI nhất quán; TC quan trọng đã chạy; README + kịch bản demo; không lỗi nghiêm trọng.
