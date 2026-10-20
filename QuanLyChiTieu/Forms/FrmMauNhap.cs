using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    // Form nhập mẫu: chỉ đặt DialogResult.OK khi dữ liệu hợp lệ (hợp đồng 5.4).
    public class FrmMauNhap : Form
    {
        private readonly TextBox txtGiaTri = new TextBox();
        private readonly Label lblLoi = new Label();

        public FrmMauNhap()
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(380, 190);
            Font = new Font("Segoe UI", 10f);
            Text = "Form nhập mẫu";

            var lbl = new Label { Text = "Giá trị *", Dock = DockStyle.Top, AutoSize = false, Height = 24 };
            txtGiaTri.Dock = DockStyle.Top; txtGiaTri.Height = Theme.CaoControl;
            lblLoi.Dock = DockStyle.Top; lblLoi.Height = 30; lblLoi.ForeColor = Theme.Chi;

            var pnlNut = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 48, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(8) };
            var btnLuu = new Button { Text = "Lưu", Width = 90, Height = 36, BackColor = Theme.Primary, ForeColor = Theme.White, FlatStyle = FlatStyle.Flat };
            var btnHuy = new Button { Text = "Hủy", Width = 90, Height = 36 };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += (s, e) =>
            {
                if (txtGiaTri.Text.Trim().Length == 0) { lblLoi.Text = "Giá trị không được bỏ trống."; return; }
                DialogResult = DialogResult.OK;   // CHỈ khi hợp lệ
                Close();
            };
            btnHuy.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            pnlNut.Controls.Add(btnHuy);
            pnlNut.Controls.Add(btnLuu);

            Controls.Add(pnlNut);
            Controls.Add(lblLoi);
            Controls.Add(txtGiaTri);
            Controls.Add(lbl);
        }
    }
}