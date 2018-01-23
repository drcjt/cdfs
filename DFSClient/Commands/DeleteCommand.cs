namespace DFSClient.Commands
{
    public class DeleteCommand : ICommand
    {
        public string FilePath { get; set; }
    }

    public class DeleteCommandHandler : ICommandHandler<DeleteCommand>
    {
        private readonly IRestClientProtocol _clientProtocol;
        public DeleteCommandHandler(IRestClientProtocol clientProtocol)
        {
            _clientProtocol = clientProtocol;
        }

        public void Handle(DeleteCommand command)
        {
            _clientProtocol.Delete(command.FilePath);
        }
    }
}
