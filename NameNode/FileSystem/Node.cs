using NameNode.FileSystem.Interfaces;

namespace NameNode.FileSystem
{
    public class Node : INode
    {
        public string Name { get; set; }

        public INode Parent { get; set; }
        public bool IsRoot => Parent == null;

        public string FullPath { get => Parent == null ? Name : FileSystemPath.Combine(Parent.FullPath, Name); }
    }
}
