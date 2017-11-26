using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.Core
{
    public interface IDataNodeId
    {
        string IPAddress { get; set; }
        string HostName { get; set; }
        Guid Id { get; set; }
    }
}
