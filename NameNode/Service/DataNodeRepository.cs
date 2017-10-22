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
        IDictionary<Guid, DataNodeDescriptor> _dataNodes = new Dictionary<Guid, DataNodeDescriptor>();

        public Guid AddDataNode(DataNodeDescriptor descriptor)
        {
            // Allocate an ID for the data node
            var dataNodeID = Guid.NewGuid();

            // Persist the data node information
            _dataNodes[dataNodeID] = descriptor;

            // Return the data node ID
            return dataNodeID;
        }

        public DataNodeDescriptor GetDataNodeDescriptorById(Guid dataNodeId)
        {
            return _dataNodes[dataNodeId];
        }

        // Data nodes become dead if they haven't sent a hearbeat in the last 1000 milliseconds
        int _heartBeatExpireIntervalMilliseconds = 1000;

        // Check if a data node is dead
        public bool IsDataNodeDead(DataNodeDescriptor dn)
        {
            return dn.LastUpdate < DateTime.Now.AddMilliseconds(-_heartBeatExpireIntervalMilliseconds).Ticks;
        }

        public int LiveNodes => _dataNodes.Values.Count(c => !IsDataNodeDead(c));
        public int DeadNodes => _dataNodes.Count - LiveNodes;
    }
}
