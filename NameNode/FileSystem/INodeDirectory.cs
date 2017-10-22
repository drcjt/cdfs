using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    class INodeDirectory : INode, IEnumerable<INode>
    {
        IList<INode> _children = new List<INode>();

        public void AddChild(INode child)
        {
            _children.Add(child);
        }

        public void RemoveChild(INode child)
        {
            _children.Remove(child);
        }

        public override bool IsDirectory()
        {
            return true;
        }

        public override bool IsFile()
        {
            return false;
        }

        public INode GetChild(string name)
        {
            foreach(var child in _children)
            {
                if (child.Name == name)
                    return child;
            }

            return null;
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
