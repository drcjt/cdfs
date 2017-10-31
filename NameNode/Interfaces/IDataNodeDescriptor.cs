using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.Interfaces
{
    public interface IDataNodeDescriptor
    {
        string IPAddress { get; set; }
        string HostName { get; set; }

        long? LastUpdate { get; set; }
    }
}
