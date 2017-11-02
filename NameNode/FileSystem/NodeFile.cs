namespace NameNode.FileSystem
{
    public class NodeFile : INode
    {
        public INode Parent { get; set; }
        public bool IsRoot => Parent == null;

        public bool IsDirectory { get => false; }
        public bool IsFile { get => true; }

        public string Name { get; set; }
    }
}
