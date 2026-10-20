using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Controls
{
    // UserControl placeholder cho các phân hệ chưa dựng (TV2/TV3/TV4).
    // Kèm demo hợp đồng 5.4: mở FrmMauNhap bằng ShowDialog, chỉ khi thật sự
    // lưu thành công (DialogResult.OK) mới tải lại/hiển thị dữ liệu.
    public class UcDangXayDung : UserControl
    {
        private readonly string tenPhanHe;
        private readonly string nguoiPhuTrach;
        private readonly Label lblKetQua = new Label();

        public UcDangXayDung(string tenPhanHe, string nguoiPhuTrach)
        {
            this.tenPhanHe = tenPhanHe;
            this.nguoiPhuTrach = nguoiPhuTrach;

            Tag = tenPhanHe; // hợp đồng: FrmMain đọc Tag để hiển thị title header
            BackColor = Theme.Background;
            Font = new Font("Segoe UI", 9.5f);

            var lblTieuDe = new Label();
            lblTieuDe.Text = tenPhanHe;
            lblTieuDe.Font = new Font("Segoe UI", 15f, FontStyle.Bold);
            lblTieuDe.ForeColor = Theme.Text;
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(8, 8);

            var lblThongBao = new Label();
            lblThongBao.Text = string.Format(
                "Phân hệ \"{0}\" đang được phát triển — phân công cho {1} " +
                "(Hợp đồng 5.2: mỗi phân hệ một thành viên sở hữu service riêng).",
                tenPhanHe, nguoiPhuTrach);
            lblThongBao.Font = new Font("Segoe UI", 10f);
            lblThongBao.ForeColor = Theme.TextSub;
            lblThongBao.Location = new Point(8, 42);
            lblThongBao.AutoSize = false;
            lblThongBao.Size = new Size(660, 46);

            lblKetQua.Text = "";
            lblKetQua.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblKetQua.ForeColor = Theme.Thu;
            lblKetQua.Location = new Point(8, 100);
            lblKetQua.AutoSize = true;

            var btnDemoForm = new Button();
            btnDemoForm.Text = "Thử form nhập mẫu (demo hợp đồng ShowDialog)";
            btnDemoForm.BackColor = Theme.Primary;
            btnDemoForm.ForeColor = Theme.White;
            btnDemoForm.FlatStyle = FlatStyle.Flat;
            btnDemoForm.FlatAppearance.BorderSize = 0;
            btnDemoForm.Size = new Size(256, Theme.CaoNut);
            btnDemoForm.Location = new Point(8, 132);
            btnDemoForm.Click += BtnDemoForm_Click;

            Controls.Add(lblTieuDe);
            Controls.Add(lblThongBao);
            Controls.Add(lblKetQua);
            Controls.Add(btnDemoForm);
        }

        private void BtnDemoForm_Click(object sender, EventArgs e)
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
