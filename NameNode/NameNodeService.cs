using log4net;
using NameNode.DependencyInjection;
using NameNode.Interfaces;
using Protocols;
using System;
using System.Collections.Generic;

namespace NameNode
{
    public class NameNodeService : IDataNodeProtocol, IClientProtocol, INameNodeServiceManagement
    {
        ILog _logger;
        IDataNodeProtocol _dataNodeProtocol;
        IClientProtocol _clientNodeProtocol;

        public DateTime Started { get; }

        public NameNodeService(ILog logger, IDataNodeProtocol dataNodeProtocol, IClientProtocol clientNodeProtocol)
        {
            _logger = logger;
            _dataNodeProtocol = dataNodeProtocol;
            _clientNodeProtocol = clientNodeProtocol;

            Started = DateTime.Now;
        }

        void IClientProtocol.Create(string filePath)
        {
            _clientNodeProtocol.Create(filePath);
        }

        void IClientProtocol.Delete(string filePath)
        {
            _clientNodeProtocol.Delete(filePath);
        }

        CdfsFileStatus[] IClientProtocol.GetListing(string filePath)
        {
            return _clientNodeProtocol.GetListing(filePath);
        }

        void IClientProtocol.ReadBlock()
        {
            _clientNodeProtocol.ReadBlock();
        }

        void IClientProtocol.WriteBlock()
        {
            _clientNodeProtocol.WriteBlock();
        }

        Guid IDataNodeProtocol.RegisterDataNode(DataNodeRegistration dataNodeRegistration)
        {
            return _dataNodeProtocol.RegisterDataNode(dataNodeRegistration);
        }

        void IDataNodeProtocol.SendHeartbeat(Guid dataNodeID)
        {
            _dataNodeProtocol.SendHeartbeat(dataNodeID);
        }
    }
}
