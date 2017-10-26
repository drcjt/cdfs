using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Protocols;

namespace NameNode.FileSystem
{
    public abstract class INode
    {
        public INode Parent { get; set; }
        public bool IsRoot => Parent == null;

        public abstract bool IsFile { get; set; }
        public abstract bool IsDirectory { get; set; }

        public string Name { get; set; }
    }
}
