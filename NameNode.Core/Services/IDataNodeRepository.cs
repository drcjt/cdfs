using Protocols.Core;
using System;

namespace NameNode.Core.Services
{
    public interface IDataNodeRepository
    {
        Guid AddDataNode(IDataNodeId descriptor);
        IDataNodeId GetDataNodeDescriptorById(Guid dataNodeGuid);
        Guid GetRandomDataNodeId();

        void SetLastUpdateTicks(Guid dataNodeGuid, long lastUpdateTicks);

        bool IsDataNodeDead(DataNodeInfo dn);
        int LiveNodes { get; }
        int DeadNodes { get; }
    }
}
