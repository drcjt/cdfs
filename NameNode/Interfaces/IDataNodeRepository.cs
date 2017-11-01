using NameNode.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.Interfaces
{
    public interface IDataNodeRepository
    {
        Guid AddDataNode(IDataNodeDescriptor descriptor);
        IDataNodeDescriptor GetDataNodeDescriptorById(Guid dataNodeId);
        void UpdateDataNode(Guid dataNodeId, IDataNodeDescriptor descriptor);

        bool IsDataNodeDead(IDataNodeDescriptor dn);
        int LiveNodes { get; }
        int DeadNodes { get; }
    }
}
