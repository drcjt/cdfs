using log4net;
using NameNode.Interfaces;
using Protocols;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NameNode
{
    public class DataNodeProtocol : IDataNodeProtocol, IDataNodeProtocolManagement
    {
        ILog _logger;

        IDictionary<Guid, DataNodeDescriptor> _dataNodes = new Dictionary<Guid, DataNodeDescriptor>();

        public DataNodeProtocol(ILog logger)
        {
            _logger = logger;
        }

        Guid IDataNodeProtocol.RegisterDataNode(DataNodeRegistration dataNodeRegistration)
        {
            _logger.InfoFormat("DataNode registering, Hostname: {0}, IPAddress: {1}", dataNodeRegistration.HostName, dataNodeRegistration.IPAddress);

            var dataNodeDescriptor = new DataNodeDescriptor();
            dataNodeDescriptor.IPAddress = dataNodeRegistration.IPAddress;
            dataNodeDescriptor.HostName = dataNodeRegistration.HostName;

            var dataNodeID = Guid.NewGuid();
            _dataNodes[dataNodeID] = dataNodeDescriptor;

            return dataNodeID;
        }

        void IDataNodeProtocol.SendHeartbeat(Guid dataNodeID)
        {
            _logger.DebugFormat("Hearbeat recevied from datanode {0}", dataNodeID);

            var dataNodeDescriptor = _dataNodes[dataNodeID];
            if (dataNodeDescriptor != null)
            {
                dataNodeDescriptor.LastUpdate = DateTime.Now.Ticks;
            }
        }

        int _heartBeatExpireIntervalMilliseconds = 1000;

        bool IsDataNodeDead(DataNodeDescriptor dn)
        {
            return dn.LastUpdate < DateTime.Now.AddMilliseconds(-_heartBeatExpireIntervalMilliseconds).Ticks;
        }

        public int LiveNodes => _dataNodes.Values.Count(c => !IsDataNodeDead(c));
        public int DeadNodes => _dataNodes.Count - LiveNodes;
    }
}
