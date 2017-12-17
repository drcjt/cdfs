using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.Core
{
    public class LocatedBlock
    {
        public Block Block { get; set; }

        public IList<DataNodeId> Locations { get; set; }
    }
}
