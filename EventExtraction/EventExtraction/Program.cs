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
        
        static void Main2(string[] args)
        {
            var content = ContentExtractor.LineJsonReader<IdsEve>(@"E:\data\eve11.json");

            var max = content.GroupBy(x => x.TargetIp).ToDictionary(x => x.Key, x => x.Count());
            var maxip = max.OrderByDescending(x => x.Value).FirstOrDefault().Key;

            var samples = content.Where(x => Statics.IpEquals(maxip, x.TargetIp)).ToArray();
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
            drawer.Save(true);
            
 //           Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.TimeOfDay.TotalMinutes);
            Console.ReadKey();
            //TechAnalyzer.NumberAnalyzer();
        }
    }
}
