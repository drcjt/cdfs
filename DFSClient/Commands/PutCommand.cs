using DFSClient.Protocol;
using System;
using System.IO;
using System.Linq;

namespace DFSClient.Commands
{
    public class PutCommand : ICommand
    {
        public string SrcFile { get; set; }
        public string FilePath { get; set; }
    }

    public class PutCommandHandler : ICommandHandler<PutCommand>
    {
        private readonly IRestClientProtocol _clientProtocol;
        private readonly IRestDataTransferProtocol _dataTransferProtocol;

        public PutCommandHandler(IRestClientProtocol clientProtocol, IRestDataTransferProtocol dataTransferProtocol)
        {
            _clientProtocol = clientProtocol;
            _dataTransferProtocol = dataTransferProtocol;
        }

        public void Handle(PutCommand command)
        {
            _clientProtocol.Create(command.SrcFile, command.FilePath);

            var filePath = Path.Combine(command.FilePath, Path.GetFileName(command.SrcFile));

            var firstBlock = _clientProtocol.AddBlock(filePath);

            _dataTransferProtocol.BaseUrl = new Uri(firstBlock.Locations.ToArray()[0].IPAddress);

            using (var fileStream = File.OpenRead(command.SrcFile))
            {
                _dataTransferProtocol.WriteBlock(firstBlock.Block, fileStream);
            }
        }
    }
}
