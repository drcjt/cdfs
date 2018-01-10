using System.Collections.Generic;
using System.Collections;
using System.Linq;
using NameNode.FileSystem.Interfaces;

namespace NameNode.FileSystem
{
    public class Directory : Node, IDirectory
    {
        private readonly IList<INode> _children = new List<INode>();

        public int ChildCount { get => _children.Count; }

        public void AddChild(INode child)
        {
            child.Parent = this;
            _children.Add(child);
        }

        public void RemoveChild(INode child)
        {
            child.Parent = null;
            _children.Remove(child);
        }

        public INode GetChild(string name)
        {
            return _children.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerator<INode> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _children.GetEnumerator();
        }
    }
}