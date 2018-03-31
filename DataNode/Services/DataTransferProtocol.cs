using Protocols;
using System;

namespace DataNode.Services
{
    public class DataTransferProtocol : IDataTransferProtocol
    {
	    public DataTransferProtocol()
	    {
	    }

        public void WriteBlock(Block block, Stream data)
        {
            // TODO : Implement write block
            // Read data from stream and write to block file
        }
    }
}