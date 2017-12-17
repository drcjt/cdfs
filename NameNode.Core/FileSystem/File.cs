using System.Collections.Generic;
using NameNode.Core.FileSystem.Interfaces;
using Protocols.Core;

namespace NameNode.Core.FileSystem
{
    public class File : Node, IFile
    {
        private readonly IList<Block> _blocks = new List<Block>();

        public void AddBlock(Block block)
        {
            _blocks.Add(block);
        }

        public IList<Block> GetBlocks()
        {
            return _blocks;
        }
    }
}
