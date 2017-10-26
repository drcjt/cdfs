using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocols;

namespace NameNode.FileSystem
{
    public class INodeFile : INode
    {
        public override bool IsDirectory { get => false; set { } }
        public override bool IsFile { get => true; set { } }
    }
}
