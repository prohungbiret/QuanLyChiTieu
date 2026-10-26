using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Controls
{
    // UserControl placeholder cho các phân hệ chưa dựng (TV2/TV3/TV4).
    // UI dựng trong UcDangXayDung.Designer.cs (mở được WinForms Designer).
    // Kèm demo hợp đồng 5.4: mở FrmMauNhap bằng ShowDialog, chỉ khi thật sự
    // lưu thành công (DialogResult.OK) mới tải lại/hiển thị dữ liệu.
    public partial class UcDangXayDung : UserControl
    {
        private readonly string tenPhanHe;
        private readonly string nguoiPhuTrach;

        // Constructor không tham số — BẮT BUỘC để WinForms Designer mở được.
        // Dùng text mặc định; constructor có tham số sẽ gọi lại và gán text thật.
        public UcDangXayDung()
        {
            InitializeComponent();
            tenPhanHe = "Phân hệ";
            nguoiPhuTrach = "—";
            ApDungTheme();
        }

        // Constructor runtime — dùng khi FrmMain tạo control.
        public UcDangXayDung(string tenPhanHe, string nguoiPhuTrach) : this()
        {
            this.tenPhanHe = tenPhanHe ?? "Phân hệ";
            this.nguoiPhuTrach = nguoiPhuTrach ?? "—";

            Tag = this.tenPhanHe; // Hợp đồng: FrmMain đọc Tag để hiển thị title header

            lblTieuDe.Text = this.tenPhanHe;
            lblThongBao.Text = string.Format(
                "Phân hệ \"{0}\" đang được phát triển — phân công cho {1} " +
                "(Hợp đồng 5.2: mỗi phân hệ một thành viên sở hữu service riêng).",
                this.tenPhanHe, this.nguoiPhuTrach);
        }

        // Áp theme runtime — Designer dùng literal trong Designer.cs, nhưng khi chạy
        // ta set lại từ Theme để đảm bảo luôn khớp nếu Theme.cs đổi màu.
        private void ApDungTheme()
        {
            BackColor = Theme.Background;
            Font = new Font("Segoe UI", 9.5f);
            lblTieuDe.ForeColor = Theme.Text;
            lblThongBao.ForeColor = Theme.TextSub;
            lblKetQua.ForeColor = Theme.Thu;
            btnDemoForm.BackColor = Theme.Primary;
            btnDemoForm.ForeColor = Theme.White;
            btnDemoForm.Size = new Size(256, Theme.CaoNut);
        }

        private void btnDemoForm_Click(object sender, EventArgs e)
        {
            using (Forms.FrmMauNhap frm = new Forms.FrmMauNhap())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lblKetQua.Text = "Đã lưu giá trị mẫu — UserControl tải lại dữ liệu của mình.";
                }
            }
        }
    }
}