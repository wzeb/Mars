using EventExtraction.Json.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace EventExtraction.DataType
{
    //{
    //"timestamp": "2011-08-18T21:47:52.701077+0800",
    //"flow_id": 94588311080160,
    //"pcap_cnt": 17,
    //"event_type": "dns",
    //"src_ip": "147.32.84.165",
    //"src_port": 1025,
    //"dest_ip": "147.32.80.9",
    //"dest_port": 53,
    //"proto": "UDP",
    //"dns": {
    //	"type": "query",
    //	"id": 19568,
    //	"rrname": "wpad",
    //	"rrtype": "A",
    //	"tx_id": 0
    //    }
    //   }
    //   {
	//"timestamp": "2011-08-18T21:52:59.671929+0800",
	//"pcap_cnt": 200829,
	//"event_type": "alert",
	//"src_ip": "147.32.84.191",
	//"dest_ip": "147.32.96.69",
	//"proto": "ICMP",
	//"icmp_type": 6,
	//"icmp_code": 188,
	//"alert": {
	//	"action": "allowed",
	//	"gid": 1,
	//	"signature_id": 2200024,
	//	"rev": 1,
	//	"signature": "SURICATA ICMPv4 unknown type",
	//	"category": "",
	//	"severity": 3
	//    }
    //   }

    public class IdsEve
    {
        public int LineNumber { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; private set; }

        [JsonProperty(PropertyName = "flow_id")]
        public string FlowId { get; private set; }

        [JsonProperty(PropertyName = "pcap_cnt")]
        public int PcapCnt { get; private set; }

        [JsonProperty(PropertyName = "event_type")]
        public string EventType { get; private set; }

        [JsonProperty(PropertyName = "src_ip")]
        [JsonConverter(typeof(IPAddressConverter))]
        public IPAddress SourceIp { get; private set; }

        [JsonProperty(PropertyName = "src_port")]
        public int SourcePort { get; private set; }

        [JsonProperty(PropertyName = "dest_ip")]
        [JsonConverter(typeof(IPAddressConverter))]
        public IPAddress TargetIp { get; private set; }

        [JsonProperty(PropertyName = "dest_port")]
        public int TargetPort { get; private set; }

        [JsonProperty(PropertyName = "proto")]
        public string Protocol { get; private set; }

        [JsonProperty(PropertyName = "dns")]
        public DnsEve Dns { get; private set; }

        [JsonProperty(PropertyName = "alert")]
        public AlertEve Alert { get; private set; }

        public IdsEve()
        {
        }

        [JsonConstructor]
        public IdsEve(DateTime timestamp, string flowId, int pcapCnt, string eventType,
            IPAddress srcIp, int srcPort, IPAddress destIp, int destPort, string proto,
            string type, int id, string rrname, string rrtype, int txId)
        {
            LineNumber = Statics.LineNumber++;
            Timestamp = timestamp;
            FlowId = flowId;
            PcapCnt = pcapCnt;
            EventType = eventType;
            SourceIp = srcIp;
            SourcePort = srcPort;
            TargetIp = destIp;
            TargetPort = destPort;
            Protocol = proto;
            Dns = new DnsEve(type, id, rrname, rrtype, txId);
        }

        public override bool Equals(object obj)
        {
            var eve = obj as IdsEve;
            return eve != null &&
                   Timestamp == eve.Timestamp &&
                   FlowId == eve.FlowId &&
                   PcapCnt == eve.PcapCnt &&
                   EventType == eve.EventType &&
                   Statics.IpEquals(SourceIp, eve.SourceIp) &&
                   Statics.IpEquals(TargetIp, eve.TargetIp) &&
                   SourcePort == eve.SourcePort &&
                   TargetPort == eve.TargetPort &&
                   Protocol == eve.Protocol &&
                   EqualityComparer<DnsEve>.Default.Equals(Dns, eve.Dns);
        }

        public override int GetHashCode()
        {
            var hashCode = 1818777531;
            hashCode = hashCode * -1521134295 + Timestamp.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FlowId);
            hashCode = hashCode * -1521134295 + PcapCnt.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EventType);
            hashCode = hashCode * -1521134295 + EqualityComparer<IPAddress>.Default.GetHashCode(SourceIp);
            hashCode = hashCode * -1521134295 + SourcePort.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IPAddress>.Default.GetHashCode(TargetIp);
            hashCode = hashCode * -1521134295 + TargetPort.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Protocol);
            hashCode = hashCode * -1521134295 + EqualityComparer<DnsEve>.Default.GetHashCode(Dns);
            return hashCode;
        }
        
        public class DnsEve
        {
            [JsonProperty(PropertyName = "type")]
            public string Type { get; private set; }

            [JsonProperty(PropertyName = "id")]
            public int Id { get; private set; }

            [JsonProperty(PropertyName = "rrname")]
            public string Rrname { get; private set; }

            [JsonProperty(PropertyName = "rrtype")]
            public string Rrtype { get; private set; }

            [JsonProperty(PropertyName = "tx_id")]
            public int TxId { get; private set; }

            public DnsEve()
            {
            }

            [JsonConstructor]
            public DnsEve(string type, int id, string rrname, string rrtype, int txId)
            {
                Type = type;
                Id = id;
                Rrname = rrname;
                Rrtype = rrtype;
                TxId = txId;
            }

            public override bool Equals(object obj)
            {
                var eve = obj as DnsEve;
                return eve != null &&
                       Type == eve.Type &&
                       Id == eve.Id &&
                       Rrname == eve.Rrname &&
                       Rrtype == eve.Rrtype &&
                       TxId == eve.TxId;
            }

            public override int GetHashCode()
            {
                var hashCode = 1633434300;
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
                hashCode = hashCode * -1521134295 + Id.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Rrname);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Rrtype);
                hashCode = hashCode * -1521134295 + TxId.GetHashCode();
                return hashCode;
            }
        }

        public class AlertEve
        {
            //[JsonProperty(PropertyName = "action")]
            //public string Action { get; private set; }

            //[JsonProperty(PropertyName = "gid")]
            //public int Gid { get; private set; }

            [JsonProperty(PropertyName = "signature_id")]
            public string SignatureId { get; private set; }

            //[JsonProperty(PropertyName = "rev")]
            //public int Rev { get; private set; }

            //[JsonProperty(PropertyName = "signature")]
            //public string Signature { get; private set; }

            //[JsonProperty(PropertyName = "category")]
            //public string Category { get; private set; }

            //[JsonProperty(PropertyName = "severity")]
            //public string Severity { get; private set; }

            public AlertEve()
            {
            }

            [JsonConstructor]
            public AlertEve(string signatureId)
            {
                SignatureId = signatureId;
            }

            public override bool Equals(object obj)
            {
                var eve = obj as AlertEve;
                return eve != null &&
                       SignatureId == eve.SignatureId;
            }

            public override int GetHashCode()
            {
                return -1141014920 + EqualityComparer<string>.Default.GetHashCode(SignatureId);
            }
        }
    }
}
