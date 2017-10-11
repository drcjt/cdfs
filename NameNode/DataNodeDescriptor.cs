using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode
{
    public class DataNodeDescriptor
    {
        public string IPAddress { get; set; }
        public string HostName { get; set; }

        public long? LastUpdate;
    }
}
