using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols
{
    public interface IDataNodeProtocol
    {
        Guid RegisterDataNode(IDataNodeId dataNodeId);
        void SendHeartbeat(Guid dataNodeGuid);
    }
}
