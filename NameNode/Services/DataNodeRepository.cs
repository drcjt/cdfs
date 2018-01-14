using Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameNode.Services
{
    public class DataNodeRepository : IDataNodeRepository
    {
        private readonly IDictionary<Guid, DataNodeInfo> _dataNodes = new Dictionary<Guid, DataNodeInfo>();

        private readonly IRandomGenerator _randomGenerator;
        private readonly ITimeProvider _timeProvider;

        public DataNodeRepository(IRandomGenerator randomGenerator, ITimeProvider timeProvider)
        {
            _randomGenerator = randomGenerator;
            _timeProvider = timeProvider;
        }

        public Guid AddDataNode(IDataNodeId descriptor)
        {
            // Allocate an ID for the data node
            var dataNodeGuid = Guid.NewGuid();

            // Persist the data node information
            _dataNodes[dataNodeGuid] = new DataNodeInfo { DataNodeId = descriptor, LastUpdateTicks = _timeProvider.Now.Ticks };

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
                dataNodeIdCopy.HostName = dataNodeId.HostName;
                dataNodeIdCopy.IPAddress = dataNodeId.IPAddress;

                return dataNodeIdCopy;
            }

            return null;
        }

        // Data nodes become dead if they haven't sent a hearbeat in the last 1000 milliseconds
        public int HeartBeatExpireIntervalMilliseconds { get; set; } = 1000;

        // Check if a data node is dead
        private bool IsDataNodeDead(DataNodeInfo dn)
        {
            return dn.LastUpdateTicks < _timeProvider.Now.AddMilliseconds(-HeartBeatExpireIntervalMilliseconds).Ticks;
        }

        public Guid GetRandomDataNodeId()
        {
            return _dataNodes.Keys.ElementAt(_randomGenerator.Generate(_dataNodes.Count()));
        }

        public int LiveNodes => _dataNodes.Values.Count(c => !IsDataNodeDead(c));
        public int DeadNodes => _dataNodes.Count - LiveNodes;
    }
}
