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

        public static bool IpComparison(IPAddress a, IPAddress b)
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
    }
}
