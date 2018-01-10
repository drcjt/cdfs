namespace NameNode.FileSystem.Interfaces
{
    public interface INode
    {
        string Name { get; set; }

        INode Parent { get; set; }
        bool IsRoot { get; }

        string FullPath { get; }
    }
}
