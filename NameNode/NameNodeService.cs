using log4net;
using NameNode.DependencyInjection;
using Protocols;
using System;
using System.Collections.Generic;

namespace NameNode
{
    [StructureMapServiceBehaviorAttribute]
    class NameNodeService : IDataNodeProtocol, IClientProtocol
    {
        IDictionary<Guid, DataNodeDescriptor> _dataNodes = new Dictionary<Guid, DataNodeDescriptor>();
        ILog _logger;

        public NameNodeService(ILog logger)
        {
            _logger = logger;
        }

        void IClientProtocol.Create(string filePath)
        {
            throw new NotImplementedException();
        }

        void IClientProtocol.Delete(string filePath)
        {
            throw new NotImplementedException();
        }

        CdfsFileStatus[] IClientProtocol.GetListing(string filePath)
        {
            return new CdfsFileStatus[] { new CdfsFileStatus() };
        }

        void IClientProtocol.ReadBlock()
        {
            throw new NotImplementedException();
        }

        Guid IDataNodeProtocol.RegisterDataNode(DataNodeRegistration dataNodeRegistration)
        {
            _logger.Info("DataNode registering");

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

        void IClientProtocol.WriteBlock()
        {
            throw new NotImplementedException();
        }
    }
}
