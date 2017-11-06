using System.Collections.Generic;

namespace NameNode.FileSystem
{
    public interface INodeDirectory : INode, IEnumerable<INode>
    {
        void AddChild(INode child);
        void RemoveChild(INode child);
        INode GetChild(string name);
        int ChildCount { get;  }
        INode GetINodeForPath(string path, bool directoryOnly = true);
        void CreateINodesInPath(string path);
    }
}
