using DataNode.Options;
using Microsoft.Extensions.Logging;
using Protocols;
using System.IO;

namespace DataNode.Services
{
    public class DataTransferProtocol : IDataTransferProtocol
    {
        private readonly ILogger<DataTransferProtocol> _logger;
        private readonly IDataNodeOptions _dataNodeOptions;

        public DataTransferProtocol(ILoggerFactory loggerFactory, IDataNodeOptions dataNodeOptions)
	    {
            _logger = loggerFactory.CreateLogger<DataTransferProtocol>();
            _dataNodeOptions = dataNodeOptions;
        }

        public void WriteBlock(Block block, Stream data)
        {
            _logger.LogDebug("Write Block {0}", block.ID);

            // Read data from stream and write to block file
            var blockFile = Path.Combine(_dataNodeOptions.BlocksPath, $"Block_{block.ID}.bin");

            using (var blockFileStream = File.Create(blockFile))
            {
                data.CopyTo(blockFileStream);
            }
        }
    }
}