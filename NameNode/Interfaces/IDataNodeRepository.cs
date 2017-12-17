using System;

namespace NameNode.Interfaces
{
    public interface IDataNodeRepository
    {
        Guid AddDataNode(IDataNodeDescriptor descriptor);
        IDataNodeDescriptor GetDataNodeDescriptorById(Guid dataNodeId);
        Guid GetRandomDataNodeId();

        void UpdateDataNode(Guid dataNodeId, IDataNodeDescriptor descriptor);

        bool IsDataNodeDead(IDataNodeDescriptor dn);
        int LiveNodes { get; }
        int DeadNodes { get; }
    }
}
