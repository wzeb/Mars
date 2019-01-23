using EventExtraction.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace EventExtraction
{
    public static class TechAnalyzer
    {
        public static void NumberAnalyzer(List<IdsTech> content)
        {
            var drawer = new Drawer<DateTime, double>()
            {
                Path = @"E:\data\IDS\241AN\测试.jpg"
            };

            //var typecontent = content.GroupBy(x => x.ThreatName).Where(t => t.Count() > 1000);


            Dictionary<DateTime, Drawer<DateTime, double>.BoxPlot> dict = new Dictionary<DateTime, Drawer<DateTime, double>.BoxPlot>();
            foreach (var item in content.GroupBy(x => (int)Statics.Timeround(x.StartTime, TimeSpan.FromMinutes(10)).TimeOfDay.TotalMinutes))
            {
                var groups = item.GroupBy(x => x.StartTime.DayOfYear).Select(x => (double)x.Count()).ToArray();
                var percentiles = Statics.Quartiles(groups);
                DateTime time = new DateTime(2019, 1, 1);
                dict.Add(time + TimeSpan.FromMinutes(item.Key), drawer.CreateBoxPlotInstance(groups.Min(), groups.Max(), percentiles.Item1, percentiles.Item3, groups.Average(), percentiles.Item2));
            }

            drawer.AddBoxPlotSeries(dict);

            //foreach (var c in typecontent)
            //{
            //    foreach (var item in c.GroupBy(x => Statics.Timeround(x.StartTime, TimeSpan.FromMinutes(10))).ToDictionary(x => x.Key, x => x.Count()))
            //    {
            //        Console.WriteLine(item.Key + "," + item.Value);
            //    }



            //    Dictionary<DateTime, Drawer<DateTime, double>.BoxPlot> dict = new Dictionary<DateTime, Drawer<DateTime, double>.BoxPlot>();
            //    foreach (var item in c.GroupBy(x => (int)Statics.Timeround(x.StartTime, TimeSpan.FromMinutes(10)).TimeOfDay.TotalMinutes))
            //    {
            //        var groups = item.GroupBy(x => x.StartTime.DayOfYear).Select(x => (double)x.Count()).ToArray();
            //        var percentiles = Statics.Quartiles(groups);
            //        DateTime time = new DateTime(2019, 1, 1);
            //        dict.Add(time + TimeSpan.FromMinutes(item.Key), drawer.CreateBoxPlotInstance(groups.Min(), groups.Max(), percentiles.Item1, percentiles.Item3, groups.Average(), percentiles.Item2));
            //    }

            //    drawer.AddBoxPlotSeries(dict, c.Key);
            //}


            drawer.SaveBoxPlot();

        }

        public static void PortAnalyzer(List<IdsTech> content)
        {
            var s = content.GroupBy(a => a.ThreatName).Where(g => g.Count() > 1000);


            var drawer = new Drawer<DateTime, int>()
            {
                Path = @"E:\data\IDS\241AN\端口分布统计by时间.jpg"
            };
            drawer.InitialGraph();

            foreach (var item in s)
            {
                List<KeyValuePair<DateTime, int>> ed = new List<KeyValuePair<DateTime, int>>();
                foreach (var i in item.ToList())
                {
                    var time = i.StartTime;
                    var adds = true;
                    foreach (var e in ed)
                    {
                        if (e.Key == time && e.Value == i.TargetPort)
                        {
                            adds = false;
                        }
                    }
                    if (adds)
                    {
                        ed.Add(new KeyValuePair<DateTime, int>(time, i.TargetPort));

                    }
                }

                drawer.AddSeries(ed, SeriesChartType.Point, item.Key);
            }

            var ends = new List<KeyValuePair<DateTime, int>>();
            foreach (var i in content.Where(a => !s.ToDictionary(t => t.Key, t => t.Count()).ContainsKey(a.ThreatName)))
            {
                var time = i.StartTime;
                var adds = true;

                foreach (var e in ends)
                {
                    if (e.Key == time && e.Value == i.TargetPort)
                    {
                        adds = false;
                    }
                }
                if (adds)
                {
                    ends.Add(new KeyValuePair<DateTime, int>(time, i.TargetPort));

                }
            }

            drawer.AddSeries(ends, SeriesChartType.Point, "All else");

            //var e = content.Where(a => !s.ToDictionary(x => x.Key, x => x.Count()).ContainsKey(a.ThreatName));
            //drawer.AddSeries(e.ToDictionary(a => a.StartTime, a => a.TargetPort), SeriesChartType.Point, "Else");

            //var elseData = .GroupBy(a => a.TargetPort).ToDictionary(a => a.Key, b => b.Count()).Where(x => x.Value > 10000);
            //drawer.AddSeries(elseData, SeriesChartType.Pie, "all else");

            drawer.Save(false);
        }

        public static void TypeAnalyzer(List<IdsTech> content)
        {
            //var content = ContentExtractor.CsvReader<IdsTech>(@"E:\data\IDS\241.txt");

            var s = content.GroupBy(a => a.ThreatName).Where(g => g.Count() > 1000).ToDictionary(g => g.Key, h => h.Count());

            var drawer = new Drawer<DateTime, int>()
            {
                Path = @"E:\data\IDS\241AN\1000次威胁名称stacked_per1min.jpg"
            };

            drawer.InitialGraph();
            foreach (var item in s)
            {
                var seriesData = content.Where(c => c.ThreatName == item.Key)
                    .GroupBy(x => Statics.Timeround(x.StartTime, TimeSpan.FromMinutes(1)))
                    .ToDictionary(a => a.Key, b => b.Count());

                drawer.AddSeries(seriesData, SeriesChartType.StackedColumn, item.Key.ToString());
            }
            drawer.Save(true);
        }

        public static void Counter()
        {
            var content = ContentExtractor.CsvReader<IdsTech>(@"E:\data\IDS\241.txt");

            var s = content.GroupBy(a => a.TargetIp).ToDictionary(g => g.Key, h => h.Count()).OrderByDescending(d => d.Value);

            int i = 0;
            foreach (var item in s)
            {
                Console.WriteLine(i++ + ":" + item.Value + " " + item.Key);
            }
            Console.WriteLine();

            Console.ReadKey();

        }
    
    }
}
