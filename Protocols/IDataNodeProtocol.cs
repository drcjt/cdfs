using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;

namespace Protocols
{
    [ServiceContract]
    public interface IDataNodeProtocol
    {
        [OperationContract]
        Guid RegisterDataNode(DataNodeRegistration dataNodeRegistration);

        [OperationContract]
        void SendHeartbeat(Guid dataNodeID);
    }
}
