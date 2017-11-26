using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Protocols
{
    [DataContract]
    public class Block
    {
        [DataMember]
        public Guid ID { get; private set; }

        [DataMember]
        public long LengthInBytes { get; private set; }

        [DataMember]
        public DateTime GeneratedDateTime { get; private set; }

        public Block(Guid id, long lengthInBytes, DateTime generatedDateTime)
        {
            ID = id;
            LengthInBytes = lengthInBytes;
            GeneratedDateTime = generatedDateTime;
        }
    }
}

