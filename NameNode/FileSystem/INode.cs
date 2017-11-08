using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public interface INode
    {
        string Name { get; set; }

        INode Parent { get; set; }
        bool IsRoot { get; }
    }
}
