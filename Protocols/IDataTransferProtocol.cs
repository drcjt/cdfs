using System.IO;

namespace Protocols
{
    public interface IDataTransferProtocol
    {
        void WriteBlock(Block block, Stream data);
        //Stream ReadBlock(Block block);
    }
}
