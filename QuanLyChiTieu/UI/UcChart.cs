using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.UI;
using Chart = System.Windows.Forms.DataVisualization.Charting.Chart;
using CArea = System.Windows.Forms.DataVisualization.Charting.ChartArea;
using CSeries = System.Windows.Forms.DataVisualization.Charting.Series;
using CLegend = System.Windows.Forms.DataVisualization.Charting.Legend;

namespace QuanLyChiTieu.UI
{
    /// <summary>Một điểm dữ liệu của biểu đồ. Mau = null → tự lấy màu chuẩn của TV1.</summary>
    public class DiemBieuDo
    {
        public string Nhan;
        public decimal GiaTri;
        public Color? Mau;
    }

    // Biểu đồ dùng chung (TV1 sở hữu) — bọc System.Windows.Forms.DataVisualization,
    // style sẵn theo Theme v1.1. TV3 (Thống kê) và cả Dashboard dùng lại, KHÔNG tự
    // cấu hình chart nội bộ trong phân hệ để màu/font không lệch nhau.
    //
    // Cách dùng:
    //   var cty = new UcChart(UcChart.KieuBieuDo.Cot);
    //   cty.HienThiCot(new[] { new DiemBieuDo { Nhan = "08/2026", GiaTri = 6300000m }, ... });
    //   var tron = new UcChart(UcChart.KieuBieuDo.Tron);
    //   tron.HienThiTron(new[] { new DiemBieuDo { Nhan = "Chi", GiaTri = 5.8M, Mau = Theme.Chi }, ... });
    //   cty.HienThiTrang() // khi không có dữ liệu → hiện "Chưa có dữ liệu"
    public class UcChart : UserControl
    {
        public enum KieuBieuDo { Cot, Tron }

        // Bảng màu tròn — lấy thẳng màu nhãn 9 loại theo Bảng 3.2
        private static readonly Color[] BangMauTron =
        {
            Theme.Primary, Theme.Thu, Theme.Chi, Theme.CanhBao,
            Theme.NhanVay, Theme.TraNo, Theme.ChoVay, Theme.ThuHoiNo,
            Theme.DongGop, Theme.RutTietKiem, Theme.TextSub, Theme.BlueLight
        };

        private readonly Chart chart = new Chart();
        private readonly CArea area = new CArea();
        private readonly Label lblRong = new Label();

        public UcChart() { }

        public UcChart(KieuBieuDo kieuBieuDo)
        {
            BackColor = Theme.White;
            DoubleBuffered = true;

            chart.Dock = DockStyle.Fill;
            chart.BackColor = Theme.White;
            chart.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.All;
            chart.ChartAreas.Add(area);

            area.BackColor = Theme.White;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8f);
            area.AxisX.LabelStyle.ForeColor = Theme.TextSub;
            area.AxisX.LineColor = Theme.Border;
            area.AxisX.MajorGrid.LineColor = Theme.Border;
            area.AxisX.MajorGrid.LineDashStyle =
                System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;

            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8f);
            area.AxisY.LabelStyle.ForeColor = Theme.TextSub;
            area.AxisY.LabelStyle.Format = "#,0";
            area.AxisY.LineColor = Theme.Border;
            area.AxisY.MajorGrid.LineColor = Theme.Border;

            lblRong.Dock = DockStyle.Fill;
            lblRong.Text = "Chưa có dữ liệu";
            lblRong.ForeColor = Theme.TextSub;
            lblRong.BackColor = Theme.White;
            lblRong.TextAlign = ContentAlignment.MiddleCenter;
            lblRong.Font = new Font("Segoe UI", 10f);
            lblRong.Visible = false;

            Controls.Add(lblRong);
            Controls.Add(chart);
        }

        /// <summary>Biểu đồ cột: mỗi DiemBieuDo một cột (mặc định màu Primary; chỉ định Mau để tô riêng).</summary>
        public void HienThiCot(IList<DiemBieuDo> diem, string tenSerie)
        {
            CSeries s = new CSeries
            {
                Name = tenSerie ?? "Giá trị",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column,
                Color = Theme.Primary,
                IsValueShownAsLabel = true,
                Label = "#,0",
                LabelForeColor = Theme.Text,
                ToolTip = "#VALX: #VAL{#,0}",
                XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String
            };
            s.Font = new Font("Segoe UI", 7.5f);
            s.SetCustomProperty("PointWidth", "0.62");

            if (diem != null)
            {
                foreach (DiemBieuDo d in diem)
                {
                    int i = s.Points.AddXY(d.Nhan, d.GiaTri);
                    if (d.Mau.HasValue) s.Points[i].Color = d.Mau.Value;
                }
            }

            chart.Legends.Clear();
            chart.Series.Clear();
            chart.Series.Add(s);
            Hien();
        }

        /// <summary>Biểu đồ tròn: tỉ trọng theo GiáTri. Không chỉ định Mau → dùng BangMauTron.</summary>
        public void HienThiTron(IList<DiemBieuDo> diem, string tenSerie)
        {
            CSeries s = new CSeries
            {
                Name = tenSerie ?? "Tỉ trọng",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie,
                ToolTip = "#VALX: #VAL{#,0} (#PERCENT{0.##%})",
                XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String
            };
            s.Font = new Font("Segoe UI", 8f);
            s.SetCustomProperty("PieLabelStyle", "Outside");

            int viTri = 0;
            if (diem != null)
            {
                foreach (DiemBieuDo d in diem)
                {
                    int i = s.Points.AddXY(d.Nhan, d.GiaTri);
                    s.Points[i].Color = d.Mau ?? BangMauTron[viTri % BangMauTron.Length];
                    s.Points[i].LegendText = d.Nhan;
                    viTri++;
                }
            }

            CLegend legend = new CLegend
            {
                Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom,
                BackColor = Theme.White,
                Font = new Font("Segoe UI", 8f)
            };
            chart.Legends.Clear();
            chart.Series.Clear();
            chart.Legends.Add(legend);
            chart.Series.Add(s);
            Hien();
        }

        /// <summary>Không có dữ liệu hiển thị (gọi từ dashboard/thống kê khi kết quả truy vấn trống).</summary>
        public void HienThiTrang()
        {
            chart.Series.Clear();
            lblRong.Visible = true;
            chart.Visible = false;
        }

        private void Hien()
        {
            lblRong.Visible = chart.Series.Count == 0 ||
                              chart.Series[0].Points.Count == 0;
            chart.Visible = !lblRong.Visible;
        }
    }
}
