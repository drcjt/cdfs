namespace NameNode.Interfaces
{
    public interface IDataNodesStatus
    {
        int LiveNodes { get; }
        int DeadNodes { get; }
    }
}
