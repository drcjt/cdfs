using log4net;
using NameNode.Interfaces;
using Protocols;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NameNode.Service
{
    public class DataNodeProtocol : IDataNodeProtocol, IDataNodeProtocolManagement
    {
        ILog _logger;

        IDictionary<Guid, DataNodeDescriptor> _dataNodes = new Dictionary<Guid, DataNodeDescriptor>();

        public DataNodeProtocol(ILog logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Register a new data node
        /// </summary>
        /// <param name="dataNodeRegistration">Data Node registration information</param>
        /// <returns></returns>
        Guid IDataNodeProtocol.RegisterDataNode(DataNodeRegistration dataNodeRegistration)
        {
            _logger.InfoFormat("DataNode registering, Hostname: {0}, IPAddress: {1}", dataNodeRegistration.HostName, dataNodeRegistration.IPAddress);

            // Create a descriptor for the new data node
            var dataNodeDescriptor = new DataNodeDescriptor();
            dataNodeDescriptor.IPAddress = dataNodeRegistration.IPAddress;
            dataNodeDescriptor.HostName = dataNodeRegistration.HostName;

            // Allocate an ID for the data node
            var dataNodeID = Guid.NewGuid();

            // Persist the data node information
            _dataNodes[dataNodeID] = dataNodeDescriptor;

            // Return the data node ID
            return dataNodeID;
        }

        /// <summary>
        /// Process a heart beat sent by a data node
        /// </summary>
        /// <param name="dataNodeID">ID of the data node hearbeating</param>
        void IDataNodeProtocol.SendHeartbeat(Guid dataNodeID)
        {
            _logger.DebugFormat("Hearbeat recevied from datanode {0}", dataNodeID);

            // Update the data node descriptor to reflect the hearbeat
            var dataNodeDescriptor = _dataNodes[dataNodeID];
            if (dataNodeDescriptor != null)
            {
                dataNodeDescriptor.LastUpdate = DateTime.Now.Ticks;
            }
        }

        // Data nodes become dead if they haven't sent a hearbeat in the last 1000 milliseconds
        int _heartBeatExpireIntervalMilliseconds = 1000;

        // Check if a data node is dead
        bool IsDataNodeDead(DataNodeDescriptor dn)
        {
            return dn.LastUpdate < DateTime.Now.AddMilliseconds(-_heartBeatExpireIntervalMilliseconds).Ticks;
        }

        // Get the count of live and dead data nodes
        public int LiveNodes => _dataNodes.Values.Count(c => !IsDataNodeDead(c));
        public int DeadNodes => _dataNodes.Count - LiveNodes;
    }
}
