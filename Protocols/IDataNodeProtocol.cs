using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocols
{
    public interface IDataNodeProtocol
    {
        Guid RegisterDataNode(IDataNodeRegistration dataNodeRegistration);
        void SendHeartbeat(Guid dataNodeID);
    }
}
