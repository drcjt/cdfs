using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public abstract class INode
    {
        public INode Parent { get; set; }

        public bool IsRoot => Parent == null;

        public abstract bool IsFile();
        public abstract bool IsDirectory();

        public string Name { get; set; }
    }
}
