﻿using Protocols.Core;
using System.Collections.Generic;

namespace NameNode.Core.FileSystem.Interfaces
{
    public interface IFile : INode
    {
        // Get blocks for the file
        IList<Block> GetBlocks();

        // Add new block for the file
        void AddBlock(Block block);
    }
}