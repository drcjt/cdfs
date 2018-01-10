using Microsoft.Extensions.Logging;
using Protocols;
using System;

namespace NameNode.Services
{
    public class DataNodeProtocol : IDataNodeProtocol
    {
        private readonly ILogger<DataNodeProtocol> _logger;
        private readonly IDataNodeRepository _dataNodeRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public DataNodeProtocol(ILoggerFactory loggerFactory, IDataNodeRepository dataNodeRepository, IDateTimeProvider dateTimeProvider)
        {
            _logger = loggerFactory.CreateLogger<DataNodeProtocol>();
            _dataNodeRepository = dataNodeRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Register a new data node
        /// </summary>
        /// <param name="dataNodeRegistration">Data Node registration information</param>
        /// <returns></returns>
        public Guid RegisterDataNode(IDataNodeId dataNodeId)
        {
            _logger.LogInformation("DataNode registering, Hostname: {0}, IPAddress: {1}", dataNodeId.HostName, dataNodeId.IPAddress);

            return _dataNodeRepository.AddDataNode(dataNodeId);
        }

        /// <summary>
        /// Process a heart beat sent by a data node
        /// </summary>
        /// <param name="dataNodeGuid">GUID of the data node hearbeating</param>
        public void SendHeartbeat(Guid dataNodeGuid)
        {
            _logger.LogDebug("Heartbeat recevied from datanode {0}", dataNodeGuid);

            // Update the data node descriptor to reflect the hearbeat
            _dataNodeRepository.SetLastUpdateTicks(dataNodeGuid, _dateTimeProvider.Now.Ticks);
        }
    }
}
