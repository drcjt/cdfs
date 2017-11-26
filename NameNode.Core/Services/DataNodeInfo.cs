using Protocols.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameNode.Core.Services
{
    public class DataNodeInfo
    {
        public IDataNodeId DataNodeId { get; set; }

        public long? LastUpdateTicks { get; set; }
    }
}
