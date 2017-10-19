using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.Interfaces
{
    public interface IDataNodeProtocolManagement
    {
        int LiveNodes { get; }
        int DeadNodes { get; }
    }
}
