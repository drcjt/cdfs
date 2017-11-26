using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Protocols
{
    [DataContract]
    public class LocatedBlock
    {
        [DataMember]
        public Block Block { get; set; }

        [DataMember]
        public IList<DataNodeID> Locations { get; set; }
    }
}
