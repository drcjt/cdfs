using System.Collections.Generic;
using NameNode.FileSystem.Interfaces;
using Protocols;

namespace NameNode.FileSystem
{
    public class File : Node, IFile
    {
        private readonly ICollection<Block> _blocks = new List<Block>();

        public void AddBlock(Block block)
        {
            _blocks.Add(block);
        }

        public ICollection<Block> GetBlocks()
        {
            return _blocks;
        }
    }
}
