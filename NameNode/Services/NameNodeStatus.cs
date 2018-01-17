using NameNode.Services.Interfaces;
using System;

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
