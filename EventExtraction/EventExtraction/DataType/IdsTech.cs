using System;
using System.Collections.Generic;
using System.Net;

namespace EventExtraction.DataType
{
    //威胁名称	威胁类型	威胁子类型	级别	攻击主机	端口	目的	端口	源接口	目的接口	
    //应用/协议	处理动作	安全策略	攻击开始时间	攻击结束时间	检测引擎	额外信息
    public class IdsTech : IdsTechCore
    {
        public string ThreatType { get; private set; }

        public string ThreatSubType { get; private set; }

        public string Level { get; private set; }

        public string SourceInterface { get; private set; }

        public string TargetInterface { get; private set; }

        public string AppProtocol { get; private set; }

        public string Action { get; private set; }

        public string SecurityStrategy { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public string TestEngine { get; private set; }

        public string Comment { get; private set; }

        public int? GroupTypeTarget { get; set; }

        public int? GroupSourceTarget { get; set; }

        public IdsTech()
        {

        }

        public IdsTech(params string [] log): base(log)
        {
            ThreatType = log[1];
            ThreatSubType = log[2];
            Level = log[3];
            SourceInterface = log[8];
            TargetInterface = log[9];
            AppProtocol = log[10];
            Action = log[11];
            SecurityStrategy = log[12];
            DateTime.TryParse(log[13], out DateTime time);
            StartTime = time;
            DateTime.TryParse(log[14], out time);
            EndTime = time;
            TestEngine = log[15];
            Comment = log[16];

            GroupTypeTarget = null;
            GroupSourceTarget = null;
        }

        public bool BaseCompare(object obj)
        {
            return base.Equals(obj);
        }

        public int BaseGetHashcode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var tech = obj as IdsTech;
            return tech != null &&
                   ThreatName == tech.ThreatName &&
                   ThreatType == tech.ThreatType &&
                   ThreatSubType == tech.ThreatSubType &&
                   Level == tech.Level &&
                   Statics.IpEquals(SourceIp, tech.SourceIp) &&
                   SourcePort == tech.SourcePort &&
                   Statics.IpEquals(TargetIp, tech.TargetIp) &&
                   TargetPort == tech.TargetPort &&
                   SourceInterface == tech.SourceInterface &&
                   TargetInterface == tech.TargetInterface &&
                   AppProtocol == tech.AppProtocol &&
                   Action == tech.Action &&
                   SecurityStrategy == tech.SecurityStrategy &&
                   StartTime == tech.StartTime &&
                   EndTime == tech.EndTime &&
                   TestEngine == tech.TestEngine &&
                   Comment == tech.Comment;
        }

        public override int GetHashCode()
        {
            var hashCode = -1042383050;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ThreatName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ThreatType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ThreatSubType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Level);
            hashCode = hashCode * -1521134295 + EqualityComparer<IPAddress>.Default.GetHashCode(SourceIp);
            hashCode = hashCode * -1521134295 + SourcePort.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IPAddress>.Default.GetHashCode(TargetIp);
            hashCode = hashCode * -1521134295 + TargetPort.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SourceInterface);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TargetInterface);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AppProtocol);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Action);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SecurityStrategy);
            hashCode = hashCode * -1521134295 + StartTime.GetHashCode();
            hashCode = hashCode * -1521134295 + EndTime.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TestEngine);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Comment);
            return hashCode;
        }

        public class IdsTechCoreCompare : IEqualityComparer<IdsTech>
        {
            public bool Equals(IdsTech x, IdsTech y)
            {
                if (x == null && y == null)
                {
                    return true;
                }
                else if (x == null || y == null)
                {
                    return false;
                }
                else
                {
                    return y.BaseCompare(x);
                }
            }

            public int GetHashCode(IdsTech obj)
            {
                return obj.BaseGetHashcode();
            }
        }
    }
}
