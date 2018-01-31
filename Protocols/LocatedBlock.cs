using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols
{
    public class LocatedBlock
    {
        public Block Block { get; set; }

        public ICollection<DataNodeId> Locations { get; set; }
    }
}
