using System;

namespace Protocols
{
    public class DataNodeId : IDataNodeId
    {
        public string IPAddress { get; set; }
        public string HostName { get; set; }
        public Guid Id { get; set; }
    }
}