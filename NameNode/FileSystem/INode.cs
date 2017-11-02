namespace NameNode.FileSystem
{
    public interface INode
    {
        INode Parent { get; set; }
        bool IsRoot { get; }

        bool IsFile { get; }
        bool IsDirectory { get; }

        string Name { get; set; }
    }
}
