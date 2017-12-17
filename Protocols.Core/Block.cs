using System;

namespace Protocols.Core
{
    public class Block
    {
        public Guid ID { get; private set; }

        public long LengthInBytes { get; private set; }

        public DateTime GeneratedDateTime { get; private set; }

        public Block(Guid id, long lengthInBytes, DateTime generatedDateTime)
        {
            ID = id;
            LengthInBytes = lengthInBytes;
            GeneratedDateTime = generatedDateTime;
        }
    }
}
