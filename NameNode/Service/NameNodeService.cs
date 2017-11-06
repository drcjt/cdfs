using log4net;
using NameNode.Interfaces;
using Protocols;
using System;
using System.Collections.Generic;

namespace NameNode.Service
{
    /// <summary>
    /// The main NameNode WCF service implementation
    /// 
    /// Limitation of WCF is that you can only have one service implementation class so this must
    /// implement both of the service interfaces.
    /// </summary>
    public class NameNodeService : IDataNodeProtocol, IClientProtocol
    {
        private readonly IDataNodeProtocol _dataNodeProtocol;
        private readonly IClientProtocol _clientNodeProtocol;
        private readonly INameNodeStatus _nameNodeStatus;

        public NameNodeService(IDataNodeProtocol dataNodeProtocol, IClientProtocol clientNodeProtocol, INameNodeStatus nameNodeStatus)
        {
            _dataNodeProtocol = dataNodeProtocol;
            _clientNodeProtocol = clientNodeProtocol;

            // Injection of NameNodeStatus forces the singleton to be created at this point recording the service startup time
            _nameNodeStatus = nameNodeStatus;
        }

        public void Create(string srcFile, string filePath)
        {
            _clientNodeProtocol.Create(srcFile, filePath);
        }

        public void Delete(string filePath)
        {
            _clientNodeProtocol.Delete(filePath);
        }

        public void Mkdir(string directoryPath)
        {
            _clientNodeProtocol.Mkdir(directoryPath);
        }

        public IList<CdfsFileStatus> GetListing(string filePath)
        {
            return _clientNodeProtocol.GetListing(filePath);
        }

        public void ReadBlock()
        {
            _clientNodeProtocol.ReadBlock();
        }

        public void WriteBlock()
        {
            _clientNodeProtocol.WriteBlock();
        }

        public Guid RegisterDataNode(DataNodeRegistration dataNodeRegistration)
        {
            return _dataNodeProtocol.RegisterDataNode(dataNodeRegistration);
        }

        public void SendHeartbeat(Guid dataNodeID)
        {
            _dataNodeProtocol.SendHeartbeat(dataNodeID);
        }
    }
}
