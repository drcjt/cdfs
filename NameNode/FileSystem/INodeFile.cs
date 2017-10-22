using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public class INodeFile : INode
    {
        public override bool IsDirectory()
        {
            return false;
        }

        public override bool IsFile()
        {
            return true;
        }
    }
}
