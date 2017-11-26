using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Protocols
{
    public class DataNodeID
    {
        [DataMember]
        public string IPAddress { get; set; }

        [DataMember]
        public string HostName { get; set; }

        [DataMember]
        public Guid Id { get; set; }
    }
}
