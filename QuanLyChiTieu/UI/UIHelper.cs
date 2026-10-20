using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using QuanLyChiTieu.Models;

namespace QuanLyChiTieu.UI
{
    // TV1 sở hữu. Hàm dùng chung cho mọi phân hệ: định dạng, nhãn, style DataTable.
    public static class UIHelper
    {
        public static string FormatTien(decimal soTien)
        {
            return soTien.ToString("#,0", new CultureInfo("vi-VN")) + "\u20AB";
        }

        /// <summary>Hiển thị có dấu +/− theo hướng tiền (giao dịch gần đây, mockup).</summary>
        public static string FormatTienCoHuong(decimal soTien, string loai)
        {
            if (Array.IndexOf(GiaoDichLoai.TienVao, loai) >= 0)
                return "+" + FormatTien(soTien);
            if (Array.IndexOf(GiaoDichLoai.TienRa, loai) >= 0)
                return "-" + FormatTien(soTien);
            return FormatTien(soTien); // ChuyenTien: trung tính
        }

        public static string FormatNgay(DateTime ngay)
        {
            return ngay.ToString("dd/MM/yyyy");
        }

        // Nhãn 9 loại giao dịch theo Bảng 3.2 — dùng chung mọi màn hình
        public static string NhanLoaiGiaoDich(string loai)
        {
            switch (loai)
            {
                case GiaoDichLoai.Thu: return "Thu";
                case GiaoDichLoai.Chi: return "Chi";
                case GiaoDichLoai.ChuyenTien: return "Chuyển";
                case GiaoDichLoai.NhanVay: return "Nhận vay";
                case GiaoDichLoai.TraNo: return "Trả nợ";
                case GiaoDichLoai.ChoVay: return "Cho vay";
                case GiaoDichLoai.ThuHoiNo: return "Thu hồi nợ";
                case GiaoDichLoai.DongGop: return "Đóng góp";
                case GiaoDichLoai.RutTietKiem: return "Rút tiết kiệm";
                default: return loai;
            }
        }

        // Màu nhãn theo Bảng 3.2 (Thu xanh, Chi đỏ, còn lại theo từng loại)
        public static Color MauTheoLoai(string loai)
        {
            switch (loai)
            {
                case GiaoDichLoai.Thu: return Theme.Thu;
                case GiaoDichLoai.Chi: return Theme.Chi;
                case GiaoDichLoai.ChuyenTien: return Theme.Primary;
                case GiaoDichLoai.NhanVay: return Theme.NhanVay;
                case GiaoDichLoai.TraNo: return Theme.TraNo;
                case GiaoDichLoai.ChoVay: return Theme.ChoVay;
                case GiaoDichLoai.ThuHoiNo: return Theme.ThuHoiNo;
                case GiaoDichLoai.DongGop: return Theme.DongGop;
                case GiaoDichLoai.RutTietKiem: return Theme.RutTietKiem;
                default: return Theme.TextSub;
            }
        }

        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Theme.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Theme.White;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.Navy;
            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgv.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 40;

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            dgv.DefaultCellStyle.SelectionBackColor = Theme.BlueLight;
            dgv.DefaultCellStyle.SelectionForeColor = Theme.Text;
            dgv.RowTemplate.Height = 34;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 249, 252);
            dgv.GridColor = Theme.Border;
            dgv.BorderStyle = BorderStyle.None;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
