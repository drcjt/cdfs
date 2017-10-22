using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.Interfaces
{
    public interface IDataNodesStatus
    {
        int LiveNodes { get; }
        int DeadNodes { get; }
    }
}
