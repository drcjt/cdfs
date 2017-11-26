using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Protocols
{
    [DataContract]
    public class LocatedBlock
    {
        [DataMember]
        public Block Block { get; set; }

        // TODO Need to put info about data nodes block is located on here
    }
}
