using NameNode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.Service
{
    class DataNodeRepository : IDataNodeRepository
    {
        private readonly IDictionary<Guid, IDataNodeDescriptor> _dataNodes = new Dictionary<Guid, IDataNodeDescriptor>();

        public Guid AddDataNode(IDataNodeDescriptor descriptor)
        {
            // Allocate an ID for the data node
            var dataNodeID = Guid.NewGuid();

            // Persist the data node information
            UpdateDataNode(dataNodeID, descriptor);

            // Return the data node ID
            return dataNodeID;
        }

        public void UpdateDataNode(Guid dataNodeId, IDataNodeDescriptor descriptor)
        {
            _dataNodes[dataNodeId] = descriptor;
        }

        public IDataNodeDescriptor GetDataNodeDescriptorById(Guid dataNodeId)
        {
            // Return a copy of the descriptor to avoid leaky abstraction
            return _dataNodes[dataNodeId] != null ? new DataNodeDescriptor(_dataNodes[dataNodeId]) : null;
        }

        // Data nodes become dead if they haven't sent a hearbeat in the last 1000 milliseconds
        private readonly int _heartBeatExpireIntervalMilliseconds = 1000;

        // Check if a data node is dead
        public bool IsDataNodeDead(IDataNodeDescriptor dn)
        {
            return dn.LastUpdate < DateTime.Now.AddMilliseconds(-_heartBeatExpireIntervalMilliseconds).Ticks;
        }

        public int LiveNodes => _dataNodes.Values.Count(c => !IsDataNodeDead(c));
        public int DeadNodes => _dataNodes.Count - LiveNodes;
    }
}
