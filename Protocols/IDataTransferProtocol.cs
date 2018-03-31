using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Protocols
{
    public interface IDataTransferProtocol
    {
        void WriteBlock(Block block, Stream data);
        //Stream ReadBlock(Block block);
    }
}
