using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.Classes;
using QuanLyChiTieu.Models;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace QuanLyChiTieu.Controls
{
    public partial class UcGiaoDich : UserControl
    {
        DatabaseProcess dtBase = new DatabaseProcess();
        string maGiaoDichDangChon = "";

        public UcGiaoDich()
        {
            InitializeComponent();
            this.Tag = "Giao dịch";
        }

       
        private void UcGiaoDich_Load(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            dtpNgayGiaoDich.MaxDate = DateTime.Today;
            dtpNgayGiaoDich.Value = DateTime.Today;

            dtpDenNgay.Value = DateTime.Today;
            dtpTuNgay.Value = DateTime.Today.AddDays(-30);

            cboLoaiGiaoDich.Items.Clear();
            cboLoaiGiaoDich.Items.Add("Chi tiêu");
            cboLoaiGiaoDich.Items.Add("Thu tiền");
            cboLoaiGiaoDich.Items.Add("Chuyển tiền");
            cboLoaiGiaoDich.SelectedIndex = -1;

            cboLocLoai.Items.Clear();
            cboLocLoai.Items.Add("Tất cả loại");
            cboLocLoai.Items.Add("Chi tiêu");
            cboLocLoai.Items.Add("Thu tiền");
            cboLocLoai.Items.Add("Chuyển tiền");
            cboLocLoai.SelectedIndex = 0;

            FillComboViTien();
            HienThiDanhSach();

            txtTimKiem.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    btnLoc.PerformClick();
                }
            };
        }
        private void FillComboViTien()
        {
            string sqlVi = "select ViTienID, TenVi from ViTien where TrangThai = 1 and NguoiDungID = @NguoiDungID";

            cboViNguon.DisplayMember = "TenVi";
            cboViNguon.ValueMember = "ViTienID";
            cboViNguon.DataSource = dtBase.DataReader(sqlVi, new SqlParameter[] { new SqlParameter("@NguoiDungID", CurrentUser.NguoiDungID) });
            cboViNguon.SelectedIndex = -1;

            cboViDich.DisplayMember = "TenVi";
            cboViDich.ValueMember = "ViTienID";
            cboViDich.DataSource = dtBase.DataReader(sqlVi, new SqlParameter[] { new SqlParameter("@NguoiDungID", CurrentUser.NguoiDungID) });
            cboViDich.SelectedIndex = -1;

            cboLocVi.DisplayMember = "TenVi";
            cboLocVi.ValueMember = "ViTienID";
            cboLocVi.DataSource = dtBase.DataReader(sqlVi, new SqlParameter[] { new SqlParameter("@NguoiDungID", CurrentUser.NguoiDungID) });
            cboLocVi.SelectedIndex = -1;
        }
        private void FillComboDanhMuc(string loai)
        {
            string sqlDM = "select DanhMucID, TenDanhMuc from DanhMuc where TrangThai = 1 and LoaiDanhMuc = @Loai and NguoiDungID = @NguoiDungID";
            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@Loai", loai),
                new SqlParameter("@NguoiDungID", CurrentUser.NguoiDungID)
            };
            cboDanhMuc.DisplayMember = "TenDanhMuc";
            cboDanhMuc.ValueMember = "DanhMucID";
            cboDanhMuc.DataSource = dtBase.DataReader(sqlDM, p);
            cboDanhMuc.SelectedIndex = -1;
        }


        private void cboLoaiGiaoDich_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboLoaiGiaoDich.Text == "Chi tiêu")
            {
                cboDanhMuc.Enabled = true;
                cboViNguon.Enabled = true;
                FillComboDanhMuc("Chi");
                cboViDich.Enabled = false;
                cboViDich.SelectedIndex = -1;
            }
            if(cboLoaiGiaoDich.Text == "Thu tiền")
            {
                cboDanhMuc.Enabled = true;
                cboViDich.Enabled = true;
                FillComboDanhMuc("Thu");
                cboViNguon.Enabled = false;
                cboViNguon.SelectedIndex = -1;
            } 
            if(cboLoaiGiaoDich.Text == "Chuyển tiền")
            {
                cboViNguon.Enabled= true;
                cboViDich.Enabled = true;
                cboDanhMuc.Enabled = false;
                cboDanhMuc.SelectedIndex = -1;
            }
            
        }

        private void HienThiDanhSach(string dieuKienThem = "", List<SqlParameter> listParams = null)
        {
            string sqlHTDS = "select gd.GiaoDichID,gd.NgayGiaoDich,gd.LoaiGiaoDich,gd.SoTien,dm.TenDanhMuc,vn.TenVi as ViNguon,vd.TenVi as ViDich,gd.GhiChu " +
                "from GiaoDich gd left join DanhMuc dm on gd.DanhMucID = dm.DanhMucID " +
                "left join ViTien vn on gd.ViNguonID = vn.ViTienID " +
                "left join ViTien vd on gd.ViDichID = vd.ViTienID where gd.NguoiDungID = @NguoiDungID and gd.LoaiGiaoDich in ('Thu', 'Chi', 'ChuyenTien') " + dieuKienThem + "" +
                "order by gd.NgayGiaoDich desc, gd.GiaoDichID desc";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@NguoiDungID", CurrentUser.NguoiDungID));
            if (listParams != null)
            {
                p.AddRange(listParams);
            }

            dgvGiaoDich.DataSource = dtBase.DataReader(sqlHTDS, p.ToArray());

            dgvGiaoDich.Columns["GiaoDichID"].Visible = false;
            dgvGiaoDich.Columns["NgayGiaoDich"].HeaderText = "Ngày Giao Dịch";
            dgvGiaoDich.Columns["LoaiGiaoDich"].HeaderText = "Loại Giao Dịch";
            dgvGiaoDich.Columns["SoTien"].HeaderText = "Số Tiền";
            dgvGiaoDich.Columns["TenDanhMuc"].HeaderText = "Tên Danh Mục";
            dgvGiaoDich.Columns["ViNguon"].HeaderText = "Ví Nguồn";
            dgvGiaoDich.Columns["ViDich"].HeaderText = "Ví Đích";
            dgvGiaoDich.Columns["GhiChu"].HeaderText = "Ghi Chú";

            dgvGiaoDich.Columns["SoTien"].DefaultCellStyle.Format = "#,##0 đ";
            TinhTongKPI();
        }
        private void TinhTongKPI()
        {
            decimal tongThu = 0, tongChi = 0;

            foreach (DataGridViewRow row in dgvGiaoDich.Rows) 
            {
                if (row.Cells["LoaiGiaoDich"].Value?.ToString() == "Thu")
                {
                    tongThu += Convert.ToDecimal(row.Cells["SoTien"].Value);
                }
                if (row.Cells["LoaiGiaoDich"].Value?.ToString() == "Chi")
                {
                    tongChi += Convert.ToDecimal(row.Cells["SoTien"].Value);
                }
            }

            decimal chenhLech = tongThu - tongChi;

            lblTongThu.Text = "+ " + tongThu.ToString("#,##0") + " đ";
            lblTongChi.Text = "- " + tongChi.ToString("#,##0") + " đ";

            if (chenhLech >= 0)
            {
                lblChenhLech.Text = "+ " + chenhLech.ToString("#,##0") + " đ";
                lblChenhLech.ForeColor = Color.Green; 
            }
            else
            {
                lblChenhLech.Text = chenhLech.ToString("#,##0") + " đ"; 
                lblChenhLech.ForeColor = Color.Red; 
            }
        }




        
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboLoaiGiaoDich.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại giao dịch!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string soTienChuoi = txtSoTien.Text.Replace(".", "").Trim();
            decimal soTien = 0;
            if (string.IsNullOrEmpty(soTienChuoi) || !decimal.TryParse(soTienChuoi, out soTien) || soTien <= 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTien.Focus();
                return;
            }

            string loaiGD = "";
            object maDanhMuc = DBNull.Value;
            object viNguon = DBNull.Value;
            object viDich = DBNull.Value;

            if (cboLoaiGiaoDich.Text == "Chi tiêu")
            {
                if (cboDanhMuc.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn danh mục chi tiêu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cboViNguon.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn ví nguồn chi ra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                loaiGD = "Chi";
                maDanhMuc = cboDanhMuc.SelectedValue;
                viNguon = cboViNguon.SelectedValue;
            }
            else if (cboLoaiGiaoDich.Text == "Thu tiền")
            {
                if (cboDanhMuc.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn danh mục thu tiền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cboViDich.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn ví nhận tiền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                loaiGD = "Thu";
                maDanhMuc = cboDanhMuc.SelectedValue;
                viDich = cboViDich.SelectedValue;
            }
            else if (cboLoaiGiaoDich.Text == "Chuyển tiền")
            {
                if (cboViNguon.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn ví nguồn chuyển đi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cboViDich.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn ví đích nhận tiền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cboViNguon.SelectedValue.ToString() == cboViDich.SelectedValue.ToString())
                {
                    MessageBox.Show("Hai ví nguồn và ví đích không được trùng nhau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                loaiGD = "ChuyenTien";
                viNguon = cboViNguon.SelectedValue;
                viDich = cboViDich.SelectedValue;
            }

            if (loaiGD == "Chi" || loaiGD == "ChuyenTien")
            {
                string sqlVi = "select SoDuHienTai from vwSoDuVi where ViTienID = @ViTienID and NguoiDungID = @NguoiDungID";
                SqlParameter[] p = new SqlParameter[]
                {
                    
                    new SqlParameter("@ViTienID", viNguon),
                    new SqlParameter("@NguoiDungID", CurrentUser.NguoiDungID)
                };
                DataTable dt = dtBase.DataReader(sqlVi, p);
                if (dt.Rows.Count > 0)
                {
                    decimal SoDuHienTai = Convert.ToDecimal(dt.Rows[0]["SoDuHienTai"]);
                    if (soTien > SoDuHienTai)
                    {
                        string canhbao = "Cảnh báo: Ví '" + cboViNguon.Text + "' hiện chỉ còn " + SoDuHienTai.ToString("N0") + " đ.\n"
                            + "Khoản tiền này (" + soTien.ToString("N0") + " đ) sẽ khiến ví bị âm " + (soTien - SoDuHienTai).ToString("N0") + " đ!\n"
                            + "Bạn có chắc chắn muốn tiếp tục ghi nhận không?";

                        if (MessageBox.Show(canhbao, "Cảnh báo thấu chi ví", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
            }


            try
            {
                string sqlInsert = "insert into GiaoDich(NguoiDungID, LoaiGiaoDich, SoTien, NgayGiaoDich, DanhMucID, ViNguonID, ViDichID, GhiChu) " +
                                   "values (@NguoiDungID, @LoaiGD, @SoTien, @NgayGD, @DanhMucID, @ViNguonID, @ViDichID, @GhiChu)";

                SqlParameter[] p = new SqlParameter[]
                {
                    new SqlParameter("@NguoiDungID", CurrentUser.NguoiDungID),
                    new SqlParameter("@LoaiGD", loaiGD),
                    new SqlParameter("@SoTien", soTien),
                    new SqlParameter("@NgayGD", dtpNgayGiaoDich.Value.Date),
                    new SqlParameter("@DanhMucID", maDanhMuc),
                    new SqlParameter("@ViNguonID", viNguon),
                    new SqlParameter("@ViDichID", viDich),
                    new SqlParameter("@GhiChu", txtGhiChu.Text.Trim())
                };

                dtBase.ChangeData(sqlInsert, p);
                MessageBox.Show("Thêm giao dịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HienThiDanhSach();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm giao dịch: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnSua_Click(object sender, EventArgs e)
        {
            if (cboLoaiGiaoDich.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại giao dịch!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string soTienChuoi = txtSoTien.Text.Replace(".", "").Trim();
            decimal soTien = 0;
            if(soTienChuoi == "" || !decimal.TryParse(soTienChuoi, out soTien) || soTien <= 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTien.Focus();
                return;
            }
            string loaiGD = "";
            object maDanhMuc = DBNull.Value;
            object viNguon = DBNull.Value;
            object viDich = DBNull.Value;

            if(cboLoaiGiaoDich.Text == "Chi tiêu")
            {
                if(cboDanhMuc.SelectedIndex == -1 || cboViNguon.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ danh mục và ví nguồn!", "Thông báo");
                    return;
                }
                loaiGD = "Chi";
                maDanhMuc = cboDanhMuc.SelectedValue;
                viNguon = cboViNguon.SelectedValue;
            } else if(cboLoaiGiaoDich.Text == "Thu tiền")
            {
                if (cboDanhMuc.SelectedIndex == -1 || cboViDich.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ danh mục và ví nhận!", "Thông báo");
                    return;
                }
                loaiGD = "Thu";
                maDanhMuc = cboDanhMuc.SelectedValue;
                viDich = cboViDich.SelectedValue;
            }
            else if (cboLoaiGiaoDich.Text == "Chuyển tiền")
            {
                if (cboViNguon.SelectedIndex == -1 || cboViDich.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ 2 ví chuyển và nhận!", "Thông báo");
                    return;
                }
                if (cboViNguon.SelectedValue.ToString() == cboViDich.SelectedValue.ToString())
                {
                    MessageBox.Show("Hai ví nguồn và đích không được trùng nhau!", "Thông báo");
                    return;
                }
                loaiGD = "ChuyenTien";
                viNguon = cboViNguon.SelectedValue;
                viDich = cboViDich.SelectedValue;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn cập nhật giao dịch này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sqlUpdate = "update GiaoDich set LoaiGiaoDich = @LoaiGD,SoTien = @SoTien,NgayGiaoDich = @NgayGD," +
                        "DanhMucID = @DanhMucID,ViNguonID = @ViNguonID,ViDIchID = @ViDichID,GhiChu = @GhiChu " +
                        "where GiaoDichID = @GiaoDichID and NguoiDungID = @NguoiDungID";
                    SqlParameter[] p = new SqlParameter[]
                    {
                        new SqlParameter("LoaiGD",loaiGD),
                        new SqlParameter("SoTien",soTien),
                        new SqlParameter("NgayGD",dtpNgayGiaoDich.Value.Date),
                        new SqlParameter("@DanhMucID", maDanhMuc),
                        new SqlParameter("@ViNguonID", viNguon),
                        new SqlParameter("@ViDichID", viDich),
                        new SqlParameter("@GhiChu", txtGhiChu.Text.Trim()),
                        new SqlParameter("@GiaoDichID", maGiaoDichDangChon),
                        new SqlParameter("@NguoiDungID", CurrentUser.NguoiDungID)
                    };
                    dtBase.ChangeData(sqlUpdate, p);
                    MessageBox.Show("Cập nhật giao dịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThiDanhSach();
                    XoaTrang();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }



        }




        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa vĩnh viễn giao dịch này không?\n(Số dư ví sẽ tự động được hoàn lại)",
                      "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sqlDelete = "delete from GiaoDich where GiaoDichID = @GiaoDichID and NguoiDungID = @NguoiDungID";
                    SqlParameter[] p = new SqlParameter[]
                    {
                new SqlParameter("@GiaoDichID", maGiaoDichDangChon),
                new SqlParameter("@NguoiDungID", CurrentUser.NguoiDungID)
                    };
                    dtBase.ChangeData(sqlDelete, p);
                    MessageBox.Show("Xóa giao dịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThiDanhSach();
                    XoaTrang();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa giao dịch: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrang();
        }



        private void XoaTrang()
        {
            cboLoaiGiaoDich.SelectedIndex = -1;
            cboDanhMuc.SelectedIndex = -1;
            cboDanhMuc.Enabled = false;
            cboViNguon.SelectedIndex = -1;
            cboViNguon.Enabled = false;
            cboViDich.SelectedIndex = -1;
            cboViDich.Enabled = false;

            txtSoTien.Text = "";
            txtGhiChu.Text = "";
            dtpNgayGiaoDich.Value = DateTime.Today;
            maGiaoDichDangChon = "";

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        // 7. Nút Lọc
        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                MessageBox.Show("Ngày lọc không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dieuKien = "and (gd.NgayGiaoDich between @TuNgay and @DenNgay) ";
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@TuNgay", dtpTuNgay.Value.Date));
            p.Add(new SqlParameter("@DenNgay", dtpDenNgay.Value.Date));

            if (cboLocLoai.SelectedIndex != -1 && cboLocLoai.Text != "Tất cả loại")
            {
                if (cboLocLoai.Text == "Chi tiêu")
                {
                    dieuKien += "and gd.LoaiGiaoDich = 'Chi' ";
                }
                else if (cboLocLoai.Text == "Thu tiền")
                {
                    dieuKien += "and gd.LoaiGiaoDich = 'Thu' ";
                }
                else if (cboLocLoai.Text == "Chuyển tiền")
                {
                    dieuKien += "and gd.LoaiGiaoDich = 'ChuyenTien' ";
                }
            }


            if (cboLocVi.SelectedIndex != -1 && cboLocVi.SelectedValue != null && cboLocVi.Text != "Tất cả ví")
            {
                dieuKien += "and (gd.ViNguonID = @ViLocID or gd.ViDichID = @ViLocID) ";
                p.Add(new SqlParameter("@ViLocID", cboLocVi.SelectedValue));
            }

            if (!string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                dieuKien += "and (gd.GhiChu like @TuKhoa or dm.TenDanhMuc like @TuKhoa) ";
                p.Add(new SqlParameter("@TuKhoa", "%" + txtTimKiem.Text.Trim() + "%"));
            }

            HienThiDanhSach(dieuKien, p);
        }

        // 8. Nút Đặt lại bộ lọc
        private void btnDatLai_Click(object sender, EventArgs e)
        {
            dtpDenNgay.Value = DateTime.Today;
            dtpTuNgay.Value = DateTime.Today.AddDays(-30);
        
            cboLocLoai.SelectedIndex = 0; 
            cboLocVi.SelectedIndex = -1;
            txtTimKiem.Text = "";
       
            HienThiDanhSach();
        }

        // Excel
        private void btnXuatExcel_Click(object sender, EventArgs e)
        { 
            if(dgvGiaoDich.Rows.Count == 0)
            {
                MessageBox.Show("Hiện không có dữ liệu giao dịch nào để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Chọn nơi lưu danh sách giao dịch";
            sfd.Filter = "Excel Books|*.xlsx|Excel 97 - 2003|*.xls";
            sfd.FileName = $"GiaoDich_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";
            if (sfd.ShowDialog() != DialogResult.OK) return;


            Excel.Application exApp = new Excel.Application();
            Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet exSheet = exBook.Worksheets[1];
            Excel.Range tenTruong = exSheet.Cells[1, 1];

            //Tiêu đề
            tenTruong.Range["A2:G2"].Merge();
            tenTruong.Range["A2"].Value = "BÁO CÁO LỊCH SỬ GIAO DỊCH CHI TIÊU";
            tenTruong.Range["A2"].Font.Size = 16;
            tenTruong.Range["A2"].Font.Name = "Times New Roman";
            tenTruong.Range["A2"].Font.Bold = true;
            tenTruong.Range["A2"].Font.Color = Color.Red;
            tenTruong.Range["A2"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //Thông tin người xuất
            tenTruong.Range["A3:G3"].Merge();
            tenTruong.Range["A3"].Value = $"Người xuất: {CurrentUser.TenHienThi} | Ngày xuất: {DateTime.Now:dd/MM/yyyy - HH:mm}";
            tenTruong.Range["A3"].Font.Size = 13;
            tenTruong.Range["A3"].Font.Name = "Times New Roman";
            tenTruong.Range["A3"].Font.Italic = true;
            tenTruong.Range["A3"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //Bảng Excel
            tenTruong.Range["A4:G4"].Font.Size = 12;
            tenTruong.Range["A4:G4"].Font.Name = "Times New Roman";
            tenTruong.Range["A4:G4"].Font.Bold = true;
            tenTruong.Range["A4:G4"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            tenTruong.Range["A4"].Value = "Ngày giao dịch";
            tenTruong.Range["B4"].Value = "Loại giao dịch";
            tenTruong.Range["C4"].Value = "Số tiền";
            tenTruong.Range["D4"].Value = "Tên danh mục";
            tenTruong.Range["E4"].Value = "Ví nguồn";
            tenTruong.Range["F4"].Value = "Ví đích";
            tenTruong.Range["G4"].Value = "Ghi chú";

            int hang = 5;
            for(int i = 0;i < dgvGiaoDich.Rows.Count;i++)
            {
                tenTruong.Range["A" + hang].Value = "'" + Convert.ToDateTime(dgvGiaoDich.Rows[i].Cells["NgayGiaoDich"].Value).ToString("dd/MM/yyyy");


                string loai = dgvGiaoDich.Rows[i].Cells["LoaiGiaoDich"].Value?.ToString() ?? "";
                if (loai == "Chi")
                {
                    loai = "Chi tiêu";
                } else if(loai == "Thu")
                {
                    loai = "Thu tiền";
                } else if(loai == "ChuyenTien")
                {
                    loai = "Chuyển tiền";
                }
                tenTruong.Range["B" + hang].Value = loai;
                tenTruong.Range["C" + hang].Value = Convert.ToDecimal(dgvGiaoDich.Rows[i].Cells["SoTien"].Value).ToString("N0");
                tenTruong.Range["D" + hang].Value = dgvGiaoDich.Rows[i].Cells["TenDanhMuc"].Value?.ToString();
                tenTruong.Range["E" + hang].Value = dgvGiaoDich.Rows[i].Cells["ViNguon"].Value?.ToString();
                tenTruong.Range["F" + hang].Value = dgvGiaoDich.Rows[i].Cells["ViDich"].Value?.ToString();
                tenTruong.Range["G" + hang].Value = dgvGiaoDich.Rows[i].Cells["GhiChu"].Value?.ToString();
                hang++;
            }

            
            tenTruong.Range["A4:G" + (hang - 1)].Borders.LineStyle = 1;
            tenTruong.Range["C5:C" + (hang - 1)].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

            tenTruong.Range["B" + (hang + 1)].Value = "Tổng thu:";
            tenTruong.Range["C" + (hang + 1)].Value = lblTongThu.Text;
            tenTruong.Range["B" + (hang + 1)].Font.Bold = true;
            tenTruong.Range["C" + (hang + 1)].Font.Bold = true;

            tenTruong.Range["B" + (hang + 2)].Value = "Tổng chi:";
            tenTruong.Range["C" + (hang + 2)].Value = lblTongChi.Text;
            tenTruong.Range["B" + (hang + 2)].Font.Bold = true;
            tenTruong.Range["C" + (hang + 2)].Font.Bold = true;

            tenTruong.Range["B" + (hang + 3)].Value = "Chênh lệch:";
            tenTruong.Range["C" + (hang + 3)].Value = lblChenhLech.Text;
            tenTruong.Range["B" + (hang + 3)].Font.Bold = true;
            tenTruong.Range["C" + (hang + 3)].Font.Bold = true;

            exSheet.Columns.AutoFit();
            if (System.IO.File.Exists(sfd.FileName))
            {
                System.IO.File.Delete(sfd.FileName);
            }

            exBook.SaveAs(sfd.FileName);
            exBook.Close();
            exApp.Quit();

            MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            System.Diagnostics.Process.Start(sfd.FileName);


        }

    


        private void dgvGiaoDich_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || dgvGiaoDich.CurrentRow == null) return;

            maGiaoDichDangChon = dgvGiaoDich.CurrentRow.Cells["GiaoDichID"].Value?.ToString();

            if (dgvGiaoDich.CurrentRow.Cells["LoaiGiaoDich"].Value?.ToString() == "Chi")
            {
                cboLoaiGiaoDich.Text = "Chi tiêu";
            } else if(dgvGiaoDich.CurrentRow.Cells["LoaiGiaoDich"].Value?.ToString() == "Thu")
            {
                cboLoaiGiaoDich.Text = "Thu tiền";
            } else if(dgvGiaoDich.CurrentRow.Cells["LoaiGiaoDich"].Value?.ToString() == "ChuyenTien")
            {
                cboLoaiGiaoDich.Text = "Chuyển tiền";
            } else
            {
                MessageBox.Show("Đây là giao dịch thuộc phân hệ Vay nợ / Tiết kiệm.\nBạn vui lòng sang màn hình Vay Nợ hoặc Tiết Kiệm để quản lý!",
                   "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XoaTrang();
                return;
            }
            dtpNgayGiaoDich.Value = Convert.ToDateTime(dgvGiaoDich.CurrentRow.Cells["NgayGiaoDich"].Value?.ToString());
            decimal soTienHienThi = 0;
            if (decimal.TryParse(dgvGiaoDich.CurrentRow.Cells["SoTien"].Value?.ToString(), out soTienHienThi))
            {
                txtSoTien.Text = soTienHienThi.ToString("#,##0").Replace(",", ".");
            }
            else
            {
                txtSoTien.Text = "0";
            }
            cboDanhMuc.Text = dgvGiaoDich.CurrentRow.Cells["TenDanhMuc"].Value?.ToString();
            cboViNguon.Text = dgvGiaoDich.CurrentRow.Cells["ViNguon"].Value?.ToString();
            cboViDich.Text = dgvGiaoDich.CurrentRow.Cells["ViDich"].Value?.ToString();
            txtGhiChu.Text = dgvGiaoDich.CurrentRow.Cells["GhiChu"].Value?.ToString();

            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;

        }


        //.000
        private void txtSoTien_TextChanged(object sender, EventArgs e)
        {
            string giaTri = txtSoTien.Text.Replace(".", "").Trim();
            if (giaTri == "") return;

            decimal soTien = 0;
            if (decimal.TryParse(giaTri, out soTien))
            {
                txtSoTien.TextChanged -= txtSoTien_TextChanged;
                txtSoTien.Text = soTien.ToString("#,##0").Replace(",", ".");
                txtSoTien.SelectionStart = txtSoTien.Text.Length;
                txtSoTien.TextChanged += txtSoTien_TextChanged;
            }


        }

      


        private void txtSoTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Chỉ được nhập số!", "Thông báo");
            }
        }
    }
}
