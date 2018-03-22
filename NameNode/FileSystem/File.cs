using System.Collections.Generic;
using NameNode.BlockManagement;
using NameNode.FileSystem.Interfaces;
using Protocols;

namespace NameNode.FileSystem
{
    public class File : Node, IFile
    {
        private readonly ICollection<BlockInfo> _blocks = new List<BlockInfo>();

        public void AddBlock(BlockInfo blockInfo)
        {
            _blocks.Add(blockInfo);
        }

        public ICollection<BlockInfo> GetBlocks()
        {
            return _blocks;
        }
    }
}
