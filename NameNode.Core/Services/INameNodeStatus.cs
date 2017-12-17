using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameNode.Core.Services
{
    public interface INameNodeStatus
    {
        DateTime Started { get; }
    }
}
