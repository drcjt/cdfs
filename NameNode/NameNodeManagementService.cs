using NameNode.DependencyInjection;
using NameNode.Interfaces;
using Protocols;
using System;

namespace NameNode
{
    [StructureMapServiceBehavior]
    public class NameNodeManagementService : INameNodeManagement
    {
        IDataNodeProtocolManagement _dataNodeProtocolManagement;
        INameNodeServiceManagement _nameNodeServiceManagement;

        public NameNodeManagementService(IDataNodeProtocolManagement dataNodeProtocolManagement, INameNodeServiceManagement nameNodeServiceManagement)
        {
            _dataNodeProtocolManagement = dataNodeProtocolManagement;
            _nameNodeServiceManagement = nameNodeServiceManagement;
        }

        NameNodeStatus INameNodeManagement.GetNameNodeStatus()
        {
            var status = new NameNodeStatus();

            status.Started = _nameNodeServiceManagement.Started;
            status.LiveNodes = _dataNodeProtocolManagement.LiveNodes;
            status.DeadNodes = _dataNodeProtocolManagement.DeadNodes;

            return status;
        }
    }
}
