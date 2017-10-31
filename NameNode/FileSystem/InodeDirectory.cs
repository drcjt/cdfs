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
    class InodeDirectory : Inode, IEnumerable<Inode>
    {
        private readonly IList<Inode> _children = new List<Inode>();

        public void AddChild(Inode child)
        {
            child.Parent = this;
            _children.Add(child);
        }

        public void RemoveChild(Inode child)
        {
            child.Parent = null;
            _children.Remove(child);
        }

        public override bool IsDirectory { get => true; }
        public override bool IsFile { get => false; }

        public Inode GetChild(string name)
        {
            foreach(var child in _children)
            {
                if (child.Name == name)
                    return child;
            }

            return null;
        }

        public InodeDirectory GetINodeForFullDirectoryPath(string path)
        {
            var pathComponents = path.Split(Path.DirectorySeparatorChar);

            InodeDirectory currentDirectory = this;
            if (!string.IsNullOrEmpty(pathComponents[0]))
            {
                int level = 0;
                do
                {
                    var nextDirectory = currentDirectory.GetChild(pathComponents[level]);
                    if (nextDirectory != null && nextDirectory is InodeDirectory)
                    {
                        currentDirectory = nextDirectory as InodeDirectory;
                    }
                    level++;
                } while (level < pathComponents.Length && currentDirectory != null);
            }
            return currentDirectory;
        }

        public IEnumerator<Inode> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _children.GetEnumerator();
        }
    }
}
