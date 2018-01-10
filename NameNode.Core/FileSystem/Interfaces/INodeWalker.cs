namespace NameNode.FileSystem.Interfaces
{
    public interface INodeWalker
    {
        INode GetNodeByPath(INode root, string path, bool stopAtLastExistingNode = false);
    }
}
