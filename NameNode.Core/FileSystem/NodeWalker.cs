using NameNode.Core.FileSystem.Interfaces;

namespace NameNode.Core.FileSystem
{
    public class NodeWalker : INodeWalker
    {
        public INode GetNodeByPath(INode root, string path, bool stopAtLastExistingNode = false)
        {
            INode currentNode = root;

            if (!string.IsNullOrEmpty(path))
            {
                var pathComponents = FileSystemPath.GetComponents(path);

                foreach (var pathComponent in pathComponents)
                {
                    if (currentNode != null && currentNode is IDirectory)
                    {
                        var childNode = (currentNode as IDirectory).GetChild(pathComponent);
                        currentNode = childNode ?? (stopAtLastExistingNode ? currentNode : null);
                    }
                }
            }
            return currentNode;
        }
    }
}
