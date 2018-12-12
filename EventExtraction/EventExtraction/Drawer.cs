using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace EventExtraction
{
    class Drawer<K, V>
    {
        public Chart Chart { get; private set; }
        
        public string Path { get; set; }

        public Drawer()
        {
            Chart = new Chart();
        }

        public void InitialGraph()
        {
            ChartArea chartArea = new ChartArea
            {
                AxisX = GetAxisFormat<K>(),
                AxisY = GetAxisFormat<V>(),
                Name = "Default"
            };

            Chart.Size = new Size(1800, 1000);
            Chart.ChartAreas.Add(chartArea);

        }

        public void SetAxisLogarithmic(bool isX)
        {
            if (isX)
            {
                Chart.ChartAreas[0].AxisX.IsLogarithmic = true;
            }
            else
            {
                Chart.ChartAreas[0].AxisY.IsLogarithmic = true;
            }
        }

        public void AddSeries(IEnumerable< KeyValuePair<K,V> > points, SeriesChartType seriesChartType, string legendName = null)
        {
            var series = new Series();
            Chart.Series.Add(series);
            series.ChartType = seriesChartType;

            if (!string.IsNullOrEmpty(legendName))
            {
                Chart.Legends.Add(new Legend(legendName) { DockedToChartArea = "Default", Name = legendName });
                series.Legend = legendName;
                series.LegendText = legendName + " " + points.Count();
                series.IsVisibleInLegend = true;
            }

            foreach (var point in points)
            {
                series.Points.AddXY(point.Key, point.Value);
                Console.WriteLine("point: " + point.Key + "," + point.Value);
            }
        }
        
        public void Save()
        {
            Chart.SaveImage(Path, ChartImageFormat.Jpeg);
        }

        private Axis GetAxisFormat<S>()
        {
            Axis axis = new Axis
            {
                LabelStyle = new LabelStyle
                {
                    IsEndLabelVisible = true
                },
            };

            if (typeof(S) == typeof(DateTime))
            {
                axis.IntervalType = DateTimeIntervalType.Seconds;
                axis.LabelStyle.Interval = 60;
                axis.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss";
            }
            else if (typeof(S) == typeof(int) || typeof(S) == typeof(long))
            {
                axis.LabelStyle.Format = "0";
                axis.IsStartedFromZero = true;
                axis.IsMarginVisible = false;
            }
            else if (typeof(S) == typeof(double))
            {
                axis.LabelStyle.Format = "0.0";
            }
            else if (typeof(S) == typeof(IPAddress))
            {
                axis.LabelStyle.Format = "0";
            }

            return axis;
        }
    }
}
