using Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameNode.BlockManagement
{
    public class BlockInfo : Block
    {
        public IList<DataNodeId> DataBlockIds { get; private set; }

        public BlockInfo(Block block, IList<DataNodeId> dataNodeBlockIds) : base(block.ID, block.LengthInBytes, block.GeneratedDateTime)
        {
            DataBlockIds = dataNodeBlockIds;
        }
    }
}
