using NameNode.Interfaces;
using System;

namespace NameNode.Status
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
