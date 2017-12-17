namespace NameNode.Interfaces
{
    public interface IDataNodeDescriptor
    {
        string IPAddress { get; set; }
        string HostName { get; set; }

        long? LastUpdate { get; set; }
    }
}
