namespace EventExtraction
{
    using EventExtraction.DataType;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms.DataVisualization.Charting;

    class Program
    {
        
        static void Main(string[] args)
        {
            var content = ContentExtractor.LineJsonReader<IdsEve>(@"E:\data\eve11.json");

            var max = content.GroupBy(x => x.TargetIp).ToDictionary(x => x.Key, x => x.Count());
            var maxip = max.OrderByDescending(x => x.Value).FirstOrDefault().Key;

            var samples = content.Where(x => Statics.IpComparison(maxip, x.TargetIp)).ToArray();
            //var samples = content.ToArray();

            //Console.WriteLine("EventType: " + content.Select(x => x.EventType).Distinct().Count());
            //Console.WriteLine("FlowId: " + content.Select(x => x.FlowId).Distinct().Count());
            //Console.WriteLine("SourceIp: " + content.Select(x => x.SourceIp).Distinct().Count());

            var drawer = new Drawer<DateTime, int>()
            {
                Path = @"E:\data\eve11_by_alert_signature_maxip.jpg"
            };

            drawer.InitialGraph();

            var groups = samples.Where(x => x.EventType == "alert").GroupBy(x => x.Alert.SignatureId);

            foreach (var items in groups)
            {
                var points = items.GroupBy(x => Statics.Timeround(x.Timestamp, TimeSpan.FromMilliseconds(100)))
                    .ToDictionary(x => x.Key, x => x.Count()).OrderBy(x => x.Key);
                drawer.AddSeries(points, SeriesChartType.Line, items.Key);
            }
            //drawer.Chart.ChartAreas["Default"].AxisX.

            drawer.SetAxisLogarithmic(false);
            drawer.Save();
            
 //           Console.ReadKey();
        }

        static void Main1(string[] args)
        {
            string[] strs = File.ReadAllLines(@"E:\data\IDS\241.csv");

            List<IdsTech> mat = new List<IdsTech>();
            HashSet<IdsTech> matd = new HashSet<IdsTech>();

            HashSet<Combination> sourceTargetDic = new HashSet<Combination>();
            HashSet<Combination> typeTargetDic = new HashSet<Combination>();
            

            for (int i = 1; i < strs.Length; i++)
            {
                var item = new IdsTech(strs[i].Split(','));

                if (!matd.Contains(item))
                {
                    Combination sourceTarget = new Combination(item.SourceIp, item.TargetIp);
                    Combination typeTarget = new Combination(item.ThreatType, item.TargetIp);

                    if (!sourceTargetDic.Contains(sourceTarget))
                    {
                        sourceTarget.Index = sourceTargetDic.Count;
                        sourceTargetDic.Add(sourceTarget);
                    }
                    else
                    {
                        sourceTargetDic.TryGetValue(sourceTarget, out var actualValue);
                        sourceTarget.Index = actualValue.Index;
                    }

                    if (!typeTargetDic.Contains(typeTarget))
                    {
                        typeTarget.Index = typeTargetDic.Count;
                        typeTargetDic.Add(typeTarget);
                    }
                    else
                    {
                        typeTargetDic.TryGetValue(typeTarget, out var actualValue);
                        typeTarget.Index = actualValue.Index;
                    }

                    item.GroupSourceTarget = sourceTarget.Index;
                    item.GroupTypeTarget = typeTarget.Index;
                    mat.Add(item);
                }
            }

            Console.WriteLine(sourceTargetDic.Count);
            Console.WriteLine(typeTargetDic.Count);

            var maxSourceTarget = mat.GroupBy(key => key.GroupSourceTarget).ToDictionary(group => group.Key, group => group.Count()).OrderByDescending(x => x.Value);
            var maxTypeTarget = mat.GroupBy(key => key.GroupTypeTarget).ToDictionary(group => group.Key, group => group.Count()).OrderByDescending(x => x.Value);

//            Console.WriteLine(maxSourceTarget.Key + "," + maxSourceTarget.Value);
//            Console.WriteLine(maxTypeTarget.Key + "," + maxTypeTarget.Value);

            var k = 0;

            using (StreamWriter file = new StreamWriter(@"E:\data\IDS\241_an.txt", false))
            {
                foreach (var item in mat)
                {
                    file.WriteLine(k++ + "," + item.GroupSourceTarget + "," + item.GroupTypeTarget + "," + item.ThreatName + "," + item.SourceIp + "," + item.TargetIp);
                }
            }

            Chart chart = new Chart();
            var series = new Series();
            chart.Series.Add(series);
            series.ChartType = SeriesChartType.StackedColumn;
            series.IsVisibleInLegend = true;

            ChartArea chartArea = new ChartArea();
            chartArea.AxisY.LabelStyle = new LabelStyle
            {
                Format = "0",
                IsEndLabelVisible = true
            };
            chartArea.AxisY.Minimum = 0;

            chartArea.AxisX.IntervalType = DateTimeIntervalType.Hours;
            chartArea.AxisX.LabelStyle = new LabelStyle
            {
                Interval = 12,
                Format = "yyyy-MM-dd HH",
                IsEndLabelVisible = true                               
            };
            chartArea.AxisX.IsLogarithmic = true;

            chart.Size = new Size(1600,1000);

            chart.ChartAreas.Add(chartArea);

            Console.WriteLine("adding");


            //foreach (var st in maxTypeTarget)
            //{
            //    //Console.WriteLine(st.Key);
            //    var tar = st.Key;
            //    var point = mat.Where(x => x.GroupTypeTarget == tar);
            //    var y = point.Select(x => x.ThreatName).Distinct().Count();
            //    if (point.Count() < 5)
            //    {
            //        break;

            //    }
            //    series.Points.AddXY(point.Count(), y);
            //}



            var points = mat.Where(x => x.GroupTypeTarget == maxTypeTarget.FirstOrDefault().Key)
                .GroupBy(t => t.StartTime)
                .ToDictionary(m => m.Key, n => n.Count());

            //Console.WriteLine("adding complete");

            foreach (var point in points)
            {
                series.Points.AddXY(point.Key, point.Value);
            }
            //series.Points.AddXY(DateTime.Parse("2018-11-18 23:15:12"), 5);
            //series.Points.AddXY(DateTime.Parse("2018-11-19 00:30:12"), 3);

            chart.SaveImage(@"E:\data\IDS\241_an2.jpg", ChartImageFormat.Jpeg);

            //Console.ReadKey();
        }
    }
}
