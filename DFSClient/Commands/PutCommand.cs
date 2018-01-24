using DFSClient.Protocol;

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
        public PutCommandHandler(IRestClientProtocol clientProtocol)
        {
            _clientProtocol = clientProtocol;
        }

        public void Handle(PutCommand command)
        {
            _clientProtocol.Create(command.SrcFile, command.FilePath);
        }
    }
}
