using NameNode.Interfaces;

namespace NameNode.Service
{
    public class DataNodeDescriptor : IDataNodeDescriptor
    {
        public DataNodeDescriptor()
        {
        }

        public DataNodeDescriptor(IDataNodeDescriptor descriptor)
        {
            IPAddress = descriptor.IPAddress;
            HostName = descriptor.HostName;
            LastUpdate = descriptor.LastUpdate;
        }

        public string IPAddress { get; set; }
        public string HostName { get; set; }

        public long? LastUpdate { get; set; }
    }
}
