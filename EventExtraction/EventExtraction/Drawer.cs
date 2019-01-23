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

        private List<UnalignedPoints> UnalignedSeries { get; set; }

        private Dictionary<string, IEnumerable<KeyValuePair<K, BoxPlot>>> UnalignedCandleStckSeries { get; set; }
        
        public Drawer()
        {
            Chart = new Chart();
            UnalignedSeries = new List<UnalignedPoints>();
            UnalignedCandleStckSeries = new Dictionary<string, IEnumerable<KeyValuePair<K, BoxPlot>>>();

            ChartArea chartArea = new ChartArea
            {
                AxisX = GetAxisFormat<K>(),
                AxisY = GetAxisFormat<V>(),
                Name = "Default"
            };

            Chart.Size = new Size(5800, 1000);
            Chart.ChartAreas.Add(chartArea);
        }

        public void InitialGraph()
        {

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
            UnalignedSeries.Add(new UnalignedPoints
            {
                Points = points,
                SeriesChartType = seriesChartType,
                LegendName = legendName
            });
        }

        public void AddBoxPlotSeries(IEnumerable<KeyValuePair<K, BoxPlot>> points, string legendName = null)
        {
            UnalignedCandleStckSeries.Add(legendName ?? string.Empty, points);
        }
        // BoxPlot MSDN
        // https://docs.microsoft.com/zh-cn/previous-versions/visualstudio/visual-studio-2010/dd456709(v%3dvs.100)
        public void SaveBoxPlot()
        {
            foreach (var item in UnalignedCandleStckSeries)
            {
                var series = new Series();
                Chart.Series.Add(series);
                series.ChartType = SeriesChartType.BoxPlot;

                if (!string.IsNullOrEmpty(item.Key))
                {
                    Chart.Legends.Add(new Legend(item.Key) { DockedToChartArea = "Default", Name = item.Key });
                    series.Legend = item.Key;
                    series.LegendText = item.Key + " " + item.Value.Count();
                    series.IsVisibleInLegend = true;
                }
                else
                {
                    Chart.Legends.Add(new Legend(item.Key) { DockedToChartArea = "Default", Name = item.Key, Docking = Docking.Bottom });

                }

                foreach (var ser in item.Value)
                {
                    series.Points.AddXY(ser.Key, ser.Value.Low, ser.Value.High, ser.Value.Open, ser.Value.Close, ser.Value.Average, ser.Value.Median);
                    Console.WriteLine("point: " + ser.Value.Low + "," + ser.Value.High + "," + ser.Value.Open + "," + ser.Value.Close + "," + ser.Value.Average + "," + ser.Value.Median);
                }
            }
            Chart.SaveImage(Path, ChartImageFormat.Jpeg);
        }

        public void Save(bool needAlign)
        {
            Paint(needAlign);
            Chart.SaveImage(Path, ChartImageFormat.Jpeg);
        }

        /// <summary>
        /// low, high, open, close, average, median
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public BoxPlot CreateBoxPlotInstance(params V[] values)
        {
            return new BoxPlot(values);
        }

        private void Paint(bool needAlign)
        {
            if (needAlign)
            {
                IEnumerable<K> finalList = new List<K>();
                UnalignedSeries.ForEach(a => finalList = a.Points.Select(x => x.Key).ToList().Union(finalList));

                foreach (var item in UnalignedSeries)
                {
                    var series = new Series();
                    Chart.Series.Add(series);
                    series.ChartType = item.SeriesChartType;

                    if (!string.IsNullOrEmpty(item.LegendName))
                    {
                        Chart.Legends.Add(new Legend(item.LegendName) { DockedToChartArea = "Default", Name = item.LegendName });
                        series.Legend = item.LegendName;
                        series.LegendText = item.LegendName + " " + item.Points.Count();
                        series.IsVisibleInLegend = true;
                    }

                    foreach (var xPoint in finalList)
                    {
                        var yPoint = item.Points.SingleOrDefault(x => xPoint.Equals(x.Key)).Value;
                        series.Points.AddXY(xPoint, yPoint);
                        Console.WriteLine("point: " + xPoint + "," + yPoint);
                    }
                }
            }
            else
            {
                foreach (var item in UnalignedSeries)
                {
                    var series = new Series();
                    Chart.Series.Add(series);
                    series.ChartType = item.SeriesChartType;
                    //series.Label = "#PERCENT";
                    //series.IsValueShownAsLabel = true;
                    //series.LegendText = "#AXISLABEL";

                    if (!string.IsNullOrEmpty(item.LegendName))
                    {
                        Chart.Legends.Add(new Legend(item.LegendName) { DockedToChartArea = "Default", Name = item.LegendName });
                        series.Legend = item.LegendName;
                        series.LegendText = item.LegendName + " " + item.Points.Count();
                        series.IsVisibleInLegend = true;
                    }
                    else
                    {
                        Chart.Legends.Add(new Legend(item.LegendName) { DockedToChartArea = "Default", Name = item.LegendName, Docking = Docking.Bottom});

                    }

                    foreach (var point in item.Points)
                    {
                        series.Points.AddXY(point.Key, point.Value);
                        Console.WriteLine("point: " + point.Key + "," + point.Value);
                    }
                }
            }
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
                axis.IntervalType = DateTimeIntervalType.Hours;
                axis.LabelStyle.Interval = 24;
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

        public class BoxPlot
        {
            public V High { get; set; }

            public V Close { get; set; }

            public V Open { get; set; }

            public V Low { get; set; }

            public V Average { get; set; }

            public V Median { get; set; }

            public BoxPlot(params V[] values)
            {
                var n = values.Length;
                Low = n > 0 ? values[0] : default(V);
                High = n > 1 ? values[1] : default(V);
                Open = n > 2 ? values[2] : default(V);
                Close = n > 3 ? values[3] : default(V);
                Average = n > 4 ? values[4] : default(V);
                Median = n > 5 ? values[5] : default(V);
            }
        }

        private class UnalignedPoints
        {
            public IEnumerable<KeyValuePair<K, V>> Points { get; set; }
            public SeriesChartType SeriesChartType { get; set; }
            public string LegendName { get; set; }
        }
    }
}
