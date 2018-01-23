namespace DFSClient.Commands
{
    public class ListingCommand : ICommand
    {
        public string FilePath { get; set; }
    }

    public class ListingCommandHandler : ICommandHandler<ListingCommand>
    {
        private readonly IRestClientProtocol _clientProtocol;
        private readonly IConsole _console;
        public ListingCommandHandler(IRestClientProtocol clientProtocol, IConsole console)
        {
            _clientProtocol = clientProtocol;
            _console = console;
        }

        public void Handle(ListingCommand command)
        {
            var listing = _clientProtocol.GetListing(command.FilePath);
            foreach (var file in listing)
            {
                _console.WriteLine(file);
            }
        }
    }
}
