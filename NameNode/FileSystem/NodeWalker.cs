using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NameNode.FileSystem
{
    public class NodeWalker : INodeWalker
    {
        public INode GetNodeByPath(INode root, string path)
        {
            return TraverseByPath(root, path).Last().child;
        }

        public IEnumerable<(INode parent, INode child, string pathComponent)> TraverseByPath(INode root, string path) //, Func<INode, INode, string, INode> nodeProcessor = null)
        {
            path = path.TrimStart(Path.DirectorySeparatorChar);
            if (!string.IsNullOrEmpty(path))
            {
                INode currentNode = root;
                foreach (var pathComponent in path.Split(Path.DirectorySeparatorChar))
                {
                    if (currentNode != null && currentNode is IDirectory)
                    {
                        var childNode = (currentNode as IDirectory).GetChild(pathComponent);
                        yield return (currentNode, childNode, pathComponent);
                    }
                }
            }
            else
            {
                yield return (root, root, null);
            }
        }
    }
}
