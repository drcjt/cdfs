using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public interface INodeWalker
    {
        INode GetNodeByPath(INode root, string path);
        IEnumerable<(INode parent, INode child, string pathComponent)> TraverseByPath(INode root, string path);
    }
}
