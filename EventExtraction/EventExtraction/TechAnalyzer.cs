﻿using EventExtraction.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace EventExtraction
{
    public static class TechAnalyzer
    {
        public static void NumberAnalyzer()
        {
            var content = ContentExtractor.CsvReader<IdsTech>(@"E:\data\IDS\241.txt");

            var drawer = new Drawer<DateTime, double>()
            {
                Path = @"E:\data\IDS\241AN\测试.jpg"
            };


            content.GroupBy(x => (long)x.StartTime.TimeOfDay.TotalMinutes);



            var dict = new Dictionary<DateTime, Drawer<DateTime, double>.BoxPlot>
            {
                { DateTime.Now, drawer.CreateBoxPlotInstance(5.6, 4.1, 3.7, 2.4) }
            };




            drawer.AddBoxPlotSeries(dict);

            drawer.SaveBoxPlot();
        }

        public static void PortAnalyzer()
        {
            var content = ContentExtractor.CsvReader<IdsTech>(@"E:\data\IDS\241.txt");

            var s = content.GroupBy(a => a.ThreatName).Where(g => g.Count() > 10000);


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

            var names = s.ToDictionary(a => a.Key, a => a.Count());
            List<KeyValuePair<DateTime, int>> ends = new List<KeyValuePair<DateTime, int>>();
            foreach (var i in content.Where(a => !names.ContainsKey(a.ThreatName)))
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

        public static void TypeAnalyzer()
        {
            var content = ContentExtractor.CsvReader<IdsTech>(@"E:\data\IDS\241.txt");

            var s = content.GroupBy(a => a.TargetIp).ToDictionary(g => g.Key, h => h.Count());

            var drawer = new Drawer<DateTime, int>()
            {
                Path = @"E:\data\IDS\241AN\1000次目标IPstacked_per10min.jpg"
            };

            drawer.InitialGraph();
            foreach (var item in s.Where(i => i.Value > 1000))
            {
                var seriesData = content.Where(c => Statics.IpEquals(c.TargetIp, item.Key))
                    .GroupBy(x => Statics.Timeround(x.StartTime, TimeSpan.FromMinutes(10)))
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
