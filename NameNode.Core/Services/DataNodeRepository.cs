using Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameNode.Services
{
    class DataNodeRepository : IDataNodeRepository
    {
        private readonly IDictionary<Guid, DataNodeInfo> _dataNodes = new Dictionary<Guid, DataNodeInfo>();

        public Guid AddDataNode(IDataNodeId descriptor)
        {
            // Allocate an ID for the data node
            var dataNodeGuid = Guid.NewGuid();

            // Persist the data node information
            _dataNodes[dataNodeGuid] = new DataNodeInfo { DataNodeId = descriptor };

            // Return the data node ID
            return dataNodeGuid;
        }

        public void SetLastUpdateTicks(Guid dataNodeGuid, long lastUpdateTicks)
        {
            if (_dataNodes.ContainsKey(dataNodeGuid))
            {
                _dataNodes[dataNodeGuid].LastUpdateTicks = lastUpdateTicks;
            }
        }

        public IDataNodeId GetDataNodeDescriptorById(Guid dataNodeGuid)
        {
            // Return a copy of the descriptor to avoid leaky abstraction
            if (_dataNodes.ContainsKey(dataNodeGuid))
            {
                var dataNodeId = _dataNodes[dataNodeGuid].DataNodeId;

                var dataNodeIdCopy = new DataNodeId();
                dataNodeIdCopy.Id = dataNodeId.Id;
                dataNodeIdCopy.HostName = dataNodeId.HostName;
                dataNodeIdCopy.IPAddress = dataNodeId.IPAddress;

                return dataNodeIdCopy;
            }

            return null;
        }

        // Data nodes become dead if they haven't sent a hearbeat in the last 1000 milliseconds
        private readonly int _heartBeatExpireIntervalMilliseconds = 1000;

        // Check if a data node is dead
        public bool IsDataNodeDead(DataNodeInfo dn)
        {
            return dn.LastUpdateTicks < DateTime.Now.AddMilliseconds(-_heartBeatExpireIntervalMilliseconds).Ticks;
        }

        private readonly Random _random = new Random();

        public Guid GetRandomDataNodeId()
        {
            return _dataNodes.Keys.ElementAt(_random.Next(_dataNodes.Count()));
        }

        public int LiveNodes => _dataNodes.Values.Count(c => !IsDataNodeDead(c));
        public int DeadNodes => _dataNodes.Count - LiveNodes;
    }
}
