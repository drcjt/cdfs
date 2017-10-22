using log4net;
using NameNode.Interfaces;
using Protocols;
using System;

namespace NameNode.Service
{
    /// <summary>
    /// The main NameNode WCF service implementation
    /// 
    /// Limitation of WCF is that you can only have one service implementation class so this must
    /// implement both of the service interfaces.
    /// </summary>
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
