using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocols
{
    [Serializable]
    public class DataNodeRegistration : IDataNodeRegistration
    {
        public string IPAddress { get; set; }
        public string HostName { get; set; }
    }
}
