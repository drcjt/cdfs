using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocols;
using System.IO;

namespace NameNode.FileSystem
{
    class INodeDirectory : INode, IEnumerable<INode>
    {
        private readonly IList<INode> _children = new List<INode>();

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

        public override bool IsDirectory { get => true; }
        public override bool IsFile { get => false; }

        public INode GetChild(string name)
        {
            foreach(var child in _children)
            {
                if (child.Name == name)
                    return child;
            }

            return null;
        }

        public INodeDirectory GetINodeForFullDirectoryPath(string path)
        {
            var pathComponents = path.Split(Path.DirectorySeparatorChar);

            INodeDirectory currentDirectory = this;
            if (!string.IsNullOrEmpty(pathComponents[0]))
            {
                int level = 0;
                do
                {
                    var nextDirectory = currentDirectory.GetChild(pathComponents[level]);
                    if (nextDirectory != null && nextDirectory is INodeDirectory)
                    {
                        currentDirectory = nextDirectory as INodeDirectory;
                    }
                    level++;
                } while (level < pathComponents.Length && currentDirectory != null);
            }
            return currentDirectory;
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
