using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventExtraction.DataType
{
    public class IdsTechCore
    {
        public string ThreatName { get; private set; }

        public IPAddress SourceIp { get; private set; }

        public int SourcePort { get; private set; }

        public IPAddress TargetIp { get; private set; }

        public int TargetPort { get; private set; }

        public IdsTechCore()
        {
        }

        public IdsTechCore(string threatName, IPAddress sourceIp, int sourcePort, IPAddress targetIp, int targetPort)
        {
            ThreatName = threatName;
            SourceIp = sourceIp;
            SourcePort = sourcePort;
            TargetIp = targetIp;
            TargetPort = targetPort;
        }

        public IdsTechCore(params string[] log)
        {
            ThreatName = log[0];
            SourceIp = log[4] == null ? null : IPAddress.Parse(log[4]);
            SourcePort = log[5] == null ? 0 : int.Parse(log[5]);
            TargetIp = log[6] == null ? null : IPAddress.Parse(log[6]); ;
            TargetPort = log[7] == null ? 0 : int.Parse(log[7]);
        }

        public override bool Equals(object obj)
        {
            return obj is IdsTechCore core
                && ThreatName == core.ThreatName 
                && Statics.IpEquals(SourceIp, core.SourceIp) 
                && SourcePort == core.SourcePort                    
                && Statics.IpEquals(TargetIp, core.TargetIp) 
                && TargetPort == core.TargetPort;
                   
        }

        public override int GetHashCode()
        {
            var hashCode = 561579234;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ThreatName);
            hashCode = hashCode * -1521134295 + EqualityComparer<IPAddress>.Default.GetHashCode(SourceIp);
            hashCode = hashCode * -1521134295 + SourcePort.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IPAddress>.Default.GetHashCode(TargetIp);
            hashCode = hashCode * -1521134295 + TargetPort.GetHashCode();
            return hashCode;
        }
    }
}
