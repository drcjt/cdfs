using DFSClient.Protocol;

namespace DFSClient.Commands
{
    public class MkdirCommand : ICommand
    {
        public string DirectoryPath { get; set; }
    }

    public class MkdirCommandHandler : ICommandHandler<MkdirCommand>
    {
        private readonly IRestClientProtocol _clientProtocol;
        public MkdirCommandHandler(IRestClientProtocol clientProtocol)
        {
            _clientProtocol = clientProtocol;
        }

        public void Handle(MkdirCommand command)
        {
            _clientProtocol.Mkdir(command.DirectoryPath);
        }
    }
}
