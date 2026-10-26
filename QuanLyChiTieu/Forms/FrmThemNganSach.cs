using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.UI;

namespace QuanLyChiTieu.Forms
{
    public partial class FrmThemNganSach : Form
    {
        private readonly int nganSachID;
        public NganSach NganSachKetQua { get; private set; }

        public FrmThemNganSach() : this(DateTime.Now.Month, DateTime.Now.Year) { }
        public FrmThemNganSach(int thang, int nam) : this(null, thang, nam) { }

        public FrmThemNganSach(NganSach sua, int thangMacDinh, int namMacDinh)
        {
            InitializeComponent();

            nganSachID = sua == null ? 0 : sua.NganSachID;
            this.Text = sua == null ? "Thêm ngân sách" : "Sửa ngân sách";
            lblTieuDe.Text = this.Text;

            List<DanhMuc> dsChi = DanhMucService.LayTheoLoai(DanhMucService.LoaiChi, true);
            foreach (DanhMuc dm in dsChi) cboDanhMuc.Items.Add(dm);
            cboDanhMuc.DisplayMember = "TenDanhMuc";
            cboDanhMuc.ValueMember = "DanhMucID";
            if (cboDanhMuc.Items.Count > 0) cboDanhMuc.SelectedIndex = 0;

            nudThang.Value = sua != null ? sua.Thang : thangMacDinh;
            nudNam.Value = sua != null ? sua.Nam : namMacDinh;

            if (sua != null)
            {
                foreach (object item in cboDanhMuc.Items)
                {
                    DanhMuc dm = item as DanhMuc;
                    if (dm != null && dm.DanhMucID == sua.DanhMucID)
                    {
                        cboDanhMuc.SelectedItem = item;
                        break;
                    }
                }
                txtHanMuc.Text = sua.HanMuc.ToString("#,0");
            }

            btnLuu.BackColor = Theme.Primary;
            btnHuy.BackColor = Theme.Background;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DanhMuc dm = cboDanhMuc.SelectedItem as DanhMuc;
            if (dm == null)
            {
                MessageBox.Show("Phải chọn một danh mục loại Chi đang sử dụng. Nếu chưa có, hãy tạo danh mục Chi trước.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDanhMuc.Focus();
                return;
            }

            decimal hanMuc;
            string loi;
            if (!ValidationHelper.SoTien(txtHanMuc.Text, out hanMuc, out loi))
            {
                MessageBox.Show(loi.Replace("Số tiền", "Hạn mức"), "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHanMuc.Focus();
                return;
            }

            NganSachKetQua = new NganSach
            {
                NganSachID = nganSachID,
                NguoiDungID = CurrentUser.NguoiDungID,
                DanhMucID = dm.DanhMucID,
                Thang = (int)nudThang.Value,
                Nam = (int)nudNam.Value,
                HanMuc = hanMuc
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}