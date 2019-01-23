using System;
using System.Net;

namespace EventExtraction
{
    public static class Statics
    {
        public static int LineNumber = 0;

        public static DateTime Timeround(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }

        public static bool IpEquals(IPAddress a, IPAddress b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            else if (a == null || b == null)
            {
                return false;
            }
            else
            {
                return a.Equals(b);
            }
        }

        public static Tuple<double, double, double> Quartiles(double[] afVal)
        {
            Array.Sort(afVal);
            int N = afVal.Length;
            if (N == 1)
            {
                return new Tuple<double, double, double>(afVal[0], afVal[0], afVal[0]);
            }

            double q1 = (N - 1) * .25 + 1;
            double q2 = (N - 1) * .5 + 1;
            double q3 = (N - 1) * .75 + 1;

            int k1 = (int)q1;
            int k2 = (int)q2;
            int k3 = (int)q3;

            double d1 = q1 - k1;
            double d2 = q2 - k2;
            double d3 = q3 - k3;

            return new Tuple<double, double, double>(afVal[k1 - 1] + d1 * (afVal[k1] - afVal[k1 - 1])
                , afVal[k2 - 1] + d2 * (afVal[k2] - afVal[k2 - 1])
                , afVal[k3 - 1] + d3 * (afVal[k3] - afVal[k3 - 1]));
        }

        public static double Percentile(double[] sequence, double excelPercentile)
        {
            Array.Sort(sequence);
            int N = sequence.Length;
            double n = (N - 1) * excelPercentile + 1;
            // Another method: double n = (N + 1) * excelPercentile;
            if (n == 1d) return sequence[0];
            else if (n == N) return sequence[N - 1];
            else
            {
                int k = (int)n;
                double d = n - k;
                return sequence[k - 1] + d * (sequence[k] - sequence[k - 1]);
            }
        }
    }
}
