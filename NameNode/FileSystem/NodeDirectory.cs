using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace NameNode.FileSystem
{
    public class NodeDirectory : INodeDirectory
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

        public INode Parent { get; set; }
        public bool IsRoot => Parent == null;

        public string Name { get; set; }

        public bool IsDirectory { get => true; }
        public bool IsFile { get => false; }

        public INode GetChild(string name)
        {
            foreach (var child in _children)
            {
                if (child.Name == name)
                    return child;
            }

            return null;
        }

        public int ChildCount { get => _children.Count; }
        
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
