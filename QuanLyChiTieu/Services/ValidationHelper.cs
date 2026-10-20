using System;
using System.Globalization;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Services
{
    // Validation dùng chung (TV1 sở hữu) — quy tắc bóc từ Tài liệu 01 (FR) + CHECK đã có
    // trong schema (Đặc tả 02). Mọi phân hệ kiểm tra TRƯỚC KHI GHI (tầng Service) dùng đều,
    // KHÔNG tự viết quy phạm riêng — tránh 2 phân hệ 2 chuẩn.
    //
    // Cú pháp gọi (mẫu trong FrmDialogNhap):
    //   if (!ValidationHelper.BatBuoc(txtTen.Text, "Tên", out string loi))
    //   { BaoLoi(txtTen, loi); return false; }
    //
    // Câu chữ thông báo tập trung ở đây để sửa một chỗ, mọi màn hình đồng nhất.
    public static class ValidationHelper
    {
        public const int DoDaiToiDa = 100;                 // tên ví / danh mục...
        public const decimal SoTienToiDa = 999999999999m;  // giới hạn số tiền có nghĩa trong app

        /// <summary>Không được bỏ trống (khoảng trắng coi như rỗng) và tối đa DoDaiToiDa ký tự.</summary>
        public static bool BatBuoc(string giaTri, string tenTruong, out string loi)
        {
            loi = null;
            if (string.IsNullOrWhiteSpace(giaTri))
            {
                loi = tenTruong + " không được bỏ trống.";
                return false;
            }
            if (giaTri.Trim().Length > DoDaiToiDa)
            {
                loi = tenTruong + " tối đa " + DoDaiToiDa + " ký tự.";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Số tiền nhập tay hợp lệ: nhận "1250000" / "1.250.000" / "1,5" / "1.500,75"
        /// và chấp nhận dán có ₫/VND/khoảng trắng.
        /// </summary>
        public static bool SoTien(string giaTri, out decimal ketQua, out string loi)
        {
            ketQua = 0m;
            loi = null;

            string s = (giaTri ?? "").Trim()
                .Replace(" ", "").Replace("₫", "").Replace("đ", "").Replace("VND", "");
            if (s.Length == 0) { loi = "Số tiền không được bỏ trống."; return false; }

            CultureInfo vi = new CultureInfo("vi-VN");
            CultureInfo inv = CultureInfo.InvariantCulture;
            decimal t;

            bool ok = decimal.TryParse(s, NumberStyles.Number, vi, out t)
                   || decimal.TryParse(s, NumberStyles.Number, inv, out t);

            if (!ok) // "1.250.000" trên máy set culture English: bỏ dấu '.' khi không có dấu ',' thập phân
            {
                string alt = s.Contains(",") ? s.Replace(".", "") : s;
                if (alt != s) ok = decimal.TryParse(alt, NumberStyles.Number, inv, out t);
            }

            if (!ok) { loi = "Số tiền không hợp lệ."; return false; }

            if (t <= 0m) { loi = "Số tiền phải lớn hơn 0."; return false; }
            if (t > SoTienToiDa) { loi = "Số tiền vượt giới hạn."; return false; }

            ketQua = t;
            return true;
        }

        /// <summary>Ngày hợp lệ cho giao dịch — cấm tương lai (FR: kiểm tra cấm ngày tương lai).</summary>
        public static bool Ngay(DateTime ngay, out string loi)
        {
            loi = null;
            if (ngay.Date > DateTime.Today)
            {
                loi = "Ngày không được ở tương lai.";
                return false;
            }
            if (ngay.Date < DateTime.Today.AddYears(-100))
            {
                loi = "Ngày quá xa trong quá khứ.";
                return false;
            }
            return true;
        }

        /// <summary>Khoảng ngày lọc (thống kê, báo cáo): Từ &lt;= Đến và Đến không tương lai.</summary>
        public static bool KhoangNgay(DateTime tu, DateTime den, out string loi)
        {
            loi = null;
            if (tu > den) { loi = "Khoảng ngày không hợp lệ (Từ lớn hơn Đến)."; return false; }
            if (den.Date > DateTime.Today) { loi = "Đến ngày không được sau hôm nay."; return false; }
            return true;
        }

        /// <summary>
        /// Quy tắc chéo bảng: GD Thu → danh mục Thu; GD Chi → danh mục Chi;
        /// 7 loại khác không gắn danh mục (DanhMucID null) — đối chiếu CHECK schema + Tài liệu 1.5F.
        /// Truyền loại danh mục ĐANG CHỌN trên form (không chọn → null).
        /// </summary>
        public static bool DanhMucTuongThich(string loaiGiaoDich, string loaiDanhMucChon, out string loi)
        {
            loi = null;
            bool laThuChi = loaiGiaoDich != null &&
                            Array.IndexOf(GiaoDichLoai.TinhVaoBaoCao, loaiGiaoDich) >= 0;

            if (!laThuChi)
            {
                if (loaiDanhMucChon != null)
                {
                    loi = "Giao dịch " + UIHelper.NhanLoaiGiaoDich(loaiGiaoDich) + " không dùng danh mục.";
                    return false;
                }
                return true;
            }

            if (loaiDanhMucChon == null)
            {
                loi = "Giao dịch " + UIHelper.NhanLoaiGiaoDich(loaiGiaoDich) + " phải chọn danh mục.";
                return false;
            }

            if (!string.Equals(loaiGiaoDich, loaiDanhMucChon, StringComparison.OrdinalIgnoreCase))
            {
                loi = "Danh mục phải thuộc loại " +
                      (string.Equals(loaiGiaoDich, GiaoDichLoai.Thu, StringComparison.Ordinal) ? "Thu" : "Chi") + ".";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Giao dịch theo hướng tiền — quy ước ViNguonID / ViDichID (Bảng 2.2e):
        /// TienVao → chỉ ViDichID; TienRa → chỉ ViNguonID; ChuyenTien → cả hai và phải khác nhau.
        /// viNguonID / viDichID là ID đã chọn trên form (không chọn → null).
        /// </summary>
        public static bool GiaoDichTheoVi(string loaiGiaoDich, int? viNguonID, int? viDichID, out string loi)
        {
            loi = null;
            bool laChuyen = string.Equals(loaiGiaoDich, GiaoDichLoai.ChuyenTien, StringComparison.Ordinal);

            if (laChuyen)
            {
                if (!viNguonID.HasValue || !viDichID.HasValue)
                {
                    loi = "Chuyển tiền phải chọn cả ví nguồn và ví đích.";
                    return false;
                }
                if (viNguonID.Value == viDichID.Value)
                {
                    loi = "Ví nguồn và ví đích phải khác nhau.";
                    return false;
                }
                return true;
            }

            if (Array.IndexOf(GiaoDichLoai.TienVao, loaiGiaoDich) >= 0)
            {
                if (!viDichID.HasValue)
                {
                    loi = "Giao dịch " + UIHelper.NhanLoaiGiaoDich(loaiGiaoDich) + " phải chọn ví nhận tiền.";
                    return false;
                }
                if (viNguonID.HasValue)
                {
                    loi = "Giao dịch " + UIHelper.NhanLoaiGiaoDich(loaiGiaoDich) + " không có ví nguồn.";
                    return false;
                }
                return true;
            }

            if (Array.IndexOf(GiaoDichLoai.TienRa, loaiGiaoDich) >= 0)
            {
                if (!viNguonID.HasValue)
                {
                    loi = "Giao dịch " + UIHelper.NhanLoaiGiaoDich(loaiGiaoDich) + " phải chọn ví chi ra.";
                    return false;
                }
                if (viDichID.HasValue)
                {
                    loi = "Giao dịch " + UIHelper.NhanLoaiGiaoDich(loaiGiaoDich) + " không có ví đích.";
                    return false;
                }
                return true;
            }

            loi = "Loại giao dịch không hợp lệ: " + loaiGiaoDich;
            return false;
        }

        /// <summary>TraNo: SoTien = TienGoc + TienLaiPhi, không âm — khớp CHECK đã có trong schema (TV4 gọi).</summary>
        public static bool TienTraNo(decimal tienGoc, decimal tienLaiPhi, decimal soTien, out string loi)
        {
            loi = null;
            if (tienGoc < 0m || tienLaiPhi < 0m)
            {
                loi = "Tiền gốc và tiền lãi/phí không được âm.";
                return false;
            }
            if (tienGoc + tienLaiPhi != soTien)
            {
                loi = "Số tiền phải bằng tiền gốc cộng tiền lãi/phí.";
                return false;
            }
            return true;
        }

        public static bool TraNoPhaiCoKhoanVay(int? khoanVayNoID, out string loi)
        {
            loi = null;
            if (!khoanVayNoID.HasValue)
            {
                loi = "Trả nợ phải tương ứng một khoản vay.";
                return false;
            }
            return true;
        }
    }
}
