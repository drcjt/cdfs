using NameNode.Interfaces;

namespace NameNode.Service
{
    public class DataNodeDescriptor : IDataNodeDescriptor
    {
        public string IPAddress { get; set; }
        public string HostName { get; set; }

        public long? LastUpdate { get; set; }
    }
}
