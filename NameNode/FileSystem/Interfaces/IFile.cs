using Protocols;
using System.Collections.Generic;

namespace NameNode.FileSystem.Interfaces
{
    public interface IFile : INode
    {
        // Get blocks for the file
        ICollection<Block> GetBlocks();

        // Add new block for the file
        void AddBlock(Block block);
    }
}
