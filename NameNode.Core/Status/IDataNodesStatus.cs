using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameNode.Status
{
    public interface IDataNodesStatus
    {
        int LiveNodes { get; }
        int DeadNodes { get; }
    }
}
