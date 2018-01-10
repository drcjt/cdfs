using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameNode.Services
{
    public class NameNodeStatus : INameNodeStatus
    {
        public DateTime Started { get; }

        public NameNodeStatus()
        {
            Started = DateTime.Now;
        }
    }
}
