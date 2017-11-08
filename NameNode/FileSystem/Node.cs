using System;
using System.Collections.Generic;
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
    }
}
