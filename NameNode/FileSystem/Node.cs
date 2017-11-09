using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public class Node : INode
    {
        public string Name { get; set; }

        public INode Parent { get; set; }
        public bool IsRoot => Parent == null;

        public string FullPath { get => Parent == null ? "" : $"{Parent.FullPath}{Path.DirectorySeparatorChar}{Name}"; }
    }
}
