using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyChiTieu.Controls
{
    public partial class UcViTien : UserControl
    {
        Classes.DatabaseProcess dtBase = new Classes.DatabaseProcess();

        public UcViTien()
        {
            InitializeComponent();
            this.Tag = "Quản lý ví tiền";
        }

        private void UcViTien_Load(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

           
            cboLoaiVi.Items.Clear();
            cboLoaiVi.Items.Add("Tiền mặt");
            cboLoaiVi.Items.Add("Tài khoản ngân hàng");
            cboLoaiVi.Items.Add("Ví điện tử");
            cboLoaiVi.SelectedIndex = 0;

            HienThiDanhSachVi();
        }

       
        private void HienThiDanhSachVi()
        {
            string sql = "select ViTienID, TenVi, LoaiVi, SoDuBanDau, SoDuHienTai from vwSoDuVi where NguoiDungID = @NguoiDungID and TrangThai = 1";
            dgvViTien.DataSource = dtBase.DataReader(sql, new SqlParameter[] { new SqlParameter("@NguoiDungID", CurrentUser.NguoiDungID) });

            if (dgvViTien.Columns["ViTienID"] != null) dgvViTien.Columns["ViTienID"].Visible = false;
            if (dgvViTien.Columns["TenVi"] != null) dgvViTien.Columns["TenVi"].HeaderText = "Tên ví";
            if (dgvViTien.Columns["LoaiVi"] != null) dgvViTien.Columns["LoaiVi"].HeaderText = "Loại ví";
            if (dgvViTien.Columns["SoDuBanDau"] != null)
            {
                dgvViTien.Columns["SoDuBanDau"].HeaderText = "Số dư ban đầu";
                dgvViTien.Columns["SoDuBanDau"].DefaultCellStyle.Format = "#,##0";
            }
            if (dgvViTien.Columns["SoDuHienTai"] != null)
            {
                dgvViTien.Columns["SoDuHienTai"].HeaderText = "Số dư hiện tại";
                dgvViTien.Columns["SoDuHienTai"].DefaultCellStyle.Format = "#,##0";
            }
        }

       
        private void dgvViTien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvViTien.CurrentRow != null && dgvViTien.CurrentRow.Index >= 0)
            {
                txtTenVi.Text = dgvViTien.CurrentRow.Cells["TenVi"].Value?.ToString();

                string loai = dgvViTien.CurrentRow.Cells["LoaiVi"].Value?.ToString();
                if (loai == "NganHang" || loai == "Tài khoản ngân hàng")
                    cboLoaiVi.Text = "Tài khoản ngân hàng";
                else if (loai == "ViDienTu" || loai == "Ví điện tử")
                    cboLoaiVi.Text = "Ví điện tử";
                else
                    cboLoaiVi.Text = "Tiền mặt";

                txtSoDuBanDau.Text = dgvViTien.CurrentRow.Cells["SoDuBanDau"].Value?.ToString();
                decimal soDuHT = 0;
                if (decimal.TryParse(dgvViTien.CurrentRow.Cells["SoDuHienTai"].Value?.ToString(), out soDuHT))
                {
                    txtSoDuHienTai.Text = soDuHT.ToString("#,##0").Replace(",", ".");
                }


                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThem.Enabled = false;
            }
        }

        
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenVi.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên ví!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenVi.Focus();
                return;
            }

            if (cboLoaiVi.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại ví!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtSoDuBanDau.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập số dư ban đầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDuBanDau.Focus();
                return;
            }

            decimal soDu = 0;
            if (!decimal.TryParse(txtSoDuBanDau.Text.Replace(".", "").Trim(), out soDu) || soDu < 0)
            {
                MessageBox.Show("Số dư ban đầu không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDuBanDau.Focus();
                return;
            }

            

            
            string sqlCheck = "select TenVi from ViTien where NguoiDungID = @NguoiDungID and TenVi = @TenVi and TrangThai = 1";
            DataTable dtTen = dtBase.DataReader(sqlCheck, new SqlParameter[]
            {
                new SqlParameter("@NguoiDungID", CurrentUser.NguoiDungID),
                new SqlParameter("@TenVi", txtTenVi.Text.Trim())
            });
            if (dtTen.Rows.Count > 0)
            {
                MessageBox.Show("Tên ví này đã tồn tại trong danh sách của bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenVi.Focus();
                return;
            }

            try
            {
                string sqlInsert = "insert into ViTien (NguoiDungID, TenVi, LoaiVi, SoDuBanDau) values (@NguoiDungID, @TenVi, @LoaiVi, @SoDuBanDau)";
                SqlParameter[] p = new SqlParameter[]
                {
                    new SqlParameter("@NguoiDungID", CurrentUser.NguoiDungID),
                    new SqlParameter("@TenVi", txtTenVi.Text.Trim()),
                    new SqlParameter("@LoaiVi", cboLoaiVi.Text),
                    new SqlParameter("@SoDuBanDau", soDu)
                };

                dtBase.ChangeData(sqlInsert, p);
                MessageBox.Show("Thêm ví tiền thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                HienThiDanhSachVi();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm ví: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvViTien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn ví cần sửa từ danh sách!", "Thông báo");
                return;
            }

            if (txtTenVi.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên ví!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenVi.Focus();
                return;
            }

            decimal soDu = 0;
            if (!decimal.TryParse(txtSoDuBanDau.Text.Replace(".", "").Trim(), out soDu) || soDu < 0)
            {
                MessageBox.Show("Số dư ban đầu không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDuBanDau.Focus();
                return;
            }

            string maVi = dgvViTien.CurrentRow.Cells["ViTienID"].Value?.ToString();

            if (MessageBox.Show("Bạn có chắc chắn muốn cập nhật ví này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sqlUpdate = "update ViTien set TenVi = @TenVi, LoaiVi = @LoaiVi, SoDuBanDau = @SoDuBanDau where ViTienID = @ViTienID and NguoiDungID = @NguoiDungID";
                    SqlParameter[] p = new SqlParameter[]
                    {
                        new SqlParameter("@TenVi", txtTenVi.Text.Trim()),
                        new SqlParameter("@LoaiVi", cboLoaiVi.Text),
                        new SqlParameter("@SoDuBanDau", soDu),
                        new SqlParameter("@ViTienID", maVi),
                        new SqlParameter("@NguoiDungID", CurrentUser.NguoiDungID)
                    };

                    dtBase.ChangeData(sqlUpdate, p);
                    MessageBox.Show("Cập nhật thông tin ví thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    HienThiDanhSachVi();
                    XoaTrang();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật ví: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

       
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvViTien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn ví cần thao tác!", "Thông báo");
                return;
            }

            string maVi = dgvViTien.CurrentRow.Cells["ViTienID"].Value?.ToString();
            string tenVi = txtTenVi.Text.Trim();

            if (MessageBox.Show("Bạn có chắc muốn ngừng sử dụng ví '" + tenVi + "' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sqlDelete = "update ViTien set TrangThai = 0 where ViTienID = @ViTienID and NguoiDungID = @NguoiDungID";
                    SqlParameter[] p = new SqlParameter[]
                    {
                        new SqlParameter("@ViTienID", maVi),
                        new SqlParameter("@NguoiDungID", CurrentUser.NguoiDungID)
                    };
                    dtBase.ChangeData(sqlDelete, p);

                    MessageBox.Show("Đã ngừng sử dụng ví thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    HienThiDanhSachVi();
                    XoaTrang();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi ngừng dùng ví: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrang();
        }

        
        private void XoaTrang()
        {
            txtTenVi.Text = "";
            cboLoaiVi.SelectedIndex = -1;
            txtSoDuBanDau.Text = "0";
            txtSoDuHienTai.Text = "0";

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        //.000
        private void txtSoDuBanDau_TextChanged(object sender, EventArgs e)
        {
            string giaTri = txtSoDuBanDau.Text.Replace(".", "").Trim();
            if (giaTri == "") return;

            decimal soTien = 0;
            if (decimal.TryParse(giaTri, out soTien))
            {
                txtSoDuBanDau.TextChanged -= txtSoDuBanDau_TextChanged;
                txtSoDuBanDau.Text = soTien.ToString("#,##0").Replace(",", ".");
                txtSoDuBanDau.SelectionStart = txtSoDuBanDau.Text.Length;
                txtSoDuBanDau.TextChanged += txtSoDuBanDau_TextChanged;
            }
        }

        private void txtSoDuBanDau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Chỉ được nhập số!","Thông báo");
            }
        }
    }
}
