using Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode
{
    public class NameNode : IDataNodeProtocol
    {
        IDictionary<Guid, DataNodeDescriptor> _dataNodes = new Dictionary<Guid, DataNodeDescriptor>();

        public NameNode()
        {

        }

        Guid IDataNodeProtocol.RegisterDataNode(IDataNodeRegistration dataNodeRegistration)
        {
            var dataNodeDescriptor = new DataNodeDescriptor();
            dataNodeDescriptor.IPAddress = dataNodeRegistration.IPAddress;
            dataNodeDescriptor.HostName = dataNodeRegistration.HostName;

            var dataNodeID = Guid.NewGuid();
            _dataNodes[dataNodeID] = dataNodeDescriptor;

            return dataNodeID;
        }

        void IDataNodeProtocol.SendHeartbeat(Guid dataNodeID)
        {
            var dataNodeDescriptor = _dataNodes[dataNodeID];
            if (dataNodeDescriptor != null)
            {
                dataNodeDescriptor.LastUpdate = DateTime.Now.Ticks;
            }
        }
    }
}
