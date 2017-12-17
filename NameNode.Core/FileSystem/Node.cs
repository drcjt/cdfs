using NameNode.Core.FileSystem.Interfaces;

namespace NameNode.Core.FileSystem
{
    public class Node : INode
    {
        public string Name { get; set; }

        public INode Parent { get; set; }
        public bool IsRoot => Parent == null;

        public string FullPath { get => Parent == null ? Name : FileSystemPath.Combine(Parent.FullPath, Name); }
    }
}
