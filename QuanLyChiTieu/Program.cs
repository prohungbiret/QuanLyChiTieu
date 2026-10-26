using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyChiTieu
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Luồng: FrmLogin (DialogResult) ⇄ FrmMain. Đóng FrmLogin = thoát app.
            while (true)
            {
                using (FrmLogin frm = new FrmLogin())
                {
                    if (frm.ShowDialog() != DialogResult.OK)
                    {
                        break;
                    }
                }

                Application.Run(new FrmMain());

                // Nếu người dùng đóng FrmMain bằng nút X (không phải Đăng xuất), thoát hẳn ứng dụng
                if (CurrentUser.DaDangNhap)
                {
                    break;
                }
            }
        }
    }
}
