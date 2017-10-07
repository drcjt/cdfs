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
        IList<IDataNodeRegistration> _dataNodes = new List<IDataNodeRegistration>();

        public NameNode()
        {

        }

        void IDataNodeProtocol.RegisterDataNode(IDataNodeRegistration dataNodeRegistration)
        {
            _dataNodes.Add(dataNodeRegistration);
        }

        void IDataNodeProtocol.SendHeartbeat()
        {
        }
    }
}
