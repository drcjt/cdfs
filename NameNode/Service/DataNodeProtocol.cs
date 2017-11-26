using log4net;
using NameNode.Interfaces;
using Protocols;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NameNode.Service
{
    public class DataNodeProtocol : IDataNodeProtocol
    {
        private readonly ILog _logger;
        private readonly IDataNodeRepository _dataNodeRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public DataNodeProtocol(ILog logger, IDataNodeRepository dataNodeRepository, IDateTimeProvider dateTimeProvider)
        {
            _logger = logger;
            _dataNodeRepository = dataNodeRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Register a new data node
        /// </summary>
        /// <param name="dataNodeRegistration">Data Node registration information</param>
        /// <returns></returns>
        public Guid RegisterDataNode(DataNodeRegistration dataNodeRegistration)
        {
            _logger.InfoFormat("DataNode registering, Hostname: {0}, IPAddress: {1}", dataNodeRegistration.HostName, dataNodeRegistration.IPAddress);

            // Create a descriptor for the new data node
            var dataNodeDescriptor = new DataNodeDescriptor();
            dataNodeDescriptor.IPAddress = dataNodeRegistration.IPAddress;
            dataNodeDescriptor.HostName = dataNodeRegistration.HostName;
            
            return _dataNodeRepository.AddDataNode(dataNodeDescriptor);
        }

        /// <summary>
        /// Process a heart beat sent by a data node
        /// </summary>
        /// <param name="dataNodeID">ID of the data node hearbeating</param>
        public void SendHeartbeat(Guid dataNodeID)
        {
            _logger.DebugFormat("Hearbeat recevied from datanode {0}", dataNodeID);

            // Update the data node descriptor to reflect the hearbeat
            var dataNodeDescriptor = _dataNodeRepository.GetDataNodeDescriptorById(dataNodeID);
            if (dataNodeDescriptor != null)
            {
                dataNodeDescriptor.LastUpdate = _dateTimeProvider.Now.Ticks;
                _dataNodeRepository.UpdateDataNode(dataNodeID, dataNodeDescriptor);
            }
        }
    }
}
