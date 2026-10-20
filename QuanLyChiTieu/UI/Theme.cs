using System.Drawing;

namespace QuanLyChiTieu.UI
{
    // Nguồn DUY NHẤT của màu và kích thước — khớp UI Standard v1.1 (Tài liệu 03) + mockup.
    // WinForms không tự áp theme: control phải được cấu hình qua UIHelper / designer.
    public static class Theme
    {
        public static readonly Color Navy    = Color.FromArgb(23, 43, 77);      // #172B4D
        public static readonly Color Primary = Color.FromArgb(37, 99, 235);    // #2563EB
        public static readonly Color BlueLight = Color.FromArgb(219, 234, 254);// #DBEAFE
        public static readonly Color Background = Color.FromArgb(244, 247, 251);// #F4F7FB
        public static readonly Color White = Color.White;
        public static readonly Color Text = Color.FromArgb(23, 32, 51);        // #172033
        public static readonly Color TextSub = Color.FromArgb(100, 116, 139);  // #64748B
        public static readonly Color Border = Color.FromArgb(226, 232, 240);   // #E2E8F0
        public static readonly Color Thu = Color.FromArgb(22, 163, 74);        // #16A34A
        public static readonly Color Chi = Color.FromArgb(220, 38, 38);        // #DC2626
        public static readonly Color CanhBao = Color.FromArgb(217, 119, 6);    // #D97706

        // Màu nhãn 9 loại giao dịch theo Bảng 3.2.1 (màu không là dấu hiệu duy nhất)
        public static readonly Color NhanVay   = Color.FromArgb(124, 58, 237); // #7C3AED
        public static readonly Color TraNo     = Color.FromArgb(147, 51, 234); // #9333EA
        public static readonly Color ChoVay    = Color.FromArgb(14, 116, 144); // #0E7490
        public static readonly Color ThuHoiNo  = Color.FromArgb(8, 145, 178);  // #0891B2
        public static readonly Color DongGop   = Color.FromArgb(217, 119, 6);  // #D97706
        public static readonly Color RutTietKiem = Color.FromArgb(202, 138, 4);// #CA8A04

        public const int GapNho = 8;
        public const int GapVua = 12;
        public const int GapChuan = 16;
        public const int GapLon = 24;
        public const int CaoControl = 36;
        public const int CaoNut = 38;
        public const int LeSidebar = 232;
        public const int CaoHeader = 60;
        public const int CaoNav = 44;
    }
}
