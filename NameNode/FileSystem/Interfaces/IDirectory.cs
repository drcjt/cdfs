using System;
using System.Collections.Generic;

namespace NameNode.FileSystem.Interfaces
{
    public interface IDirectory : INode, IEnumerable<INode>
    {
        int ChildCount { get; }

        void AddChild(INode child);
        void RemoveChild(INode child);

        INode GetChild(string name);
    }
}
