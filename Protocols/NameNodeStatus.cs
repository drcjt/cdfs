using System;
using System.Runtime.Serialization;

namespace Protocols
{
    [DataContract]
    public class NameNodeStatus
    {
        [DataMember]
        public DateTime Started { get; set; }

        [DataMember]
        public int LiveNodes { get; set; }

        [DataMember]
        public int DeadNodes { get; set; }
    }
}
