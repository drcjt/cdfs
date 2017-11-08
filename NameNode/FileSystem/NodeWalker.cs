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
            return TraverseByPath(root, path).Last();
        }

        public IEnumerable<INode> TraverseByPath(INode root, string path, Func<INode, string, INode> nodeProcessor = null)
        {
            INode currentNode = root;
            if (!string.IsNullOrEmpty(path))
            {
                foreach (var pathComponent in path.Split(Path.DirectorySeparatorChar))
                {
                    if (currentNode != null && currentNode is IDirectory)
                    {
                        var childNode = (currentNode as IDirectory).GetChild(pathComponent);
                        currentNode = nodeProcessor != null ? nodeProcessor(childNode, pathComponent) : childNode;
                        yield return currentNode;
                    }
                }
            }
            else
            {
                yield break;
            }
        }
    }
}
