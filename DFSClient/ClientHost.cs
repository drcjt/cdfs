using DFSClient.CommandBuilders;
using DFSClient.Commands;
using DFSClient.Options;
using DFSClient.Protocol;
using System;

namespace DFSClient
{
    public class ClientHost : IClientHost
    {
        private readonly IOptionParser _optionParser;
        private readonly IRestClientProtocol _clientProtocol;
        private readonly IRestDataTransferProtocol _dataTransferProtocol;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ICommandFactory _commandFactory;
        public ClientHost(IOptionParser optionParser, IRestClientProtocol clientProtocol, IRestDataTransferProtocol dataTransferProtocol, ICommandDispatcher commandDispatcher, ICommandFactory commandFactory)
        {
            _optionParser = optionParser;
            _clientProtocol = clientProtocol;
            _dataTransferProtocol = dataTransferProtocol;
            _commandDispatcher = commandDispatcher;
            _commandFactory = commandFactory;
        }

        public void Run(string[] args)
        {
            var options = _optionParser.ParseOptions(args);
            if (options != null)
            {
                _clientProtocol.BaseUrl = new Uri(options.NameNodeUri);
                _dataTransferProtocol.BaseUrl = new Uri(options.NameNodeUri);
                var command = _commandFactory.Build(options);
                _commandDispatcher.Dispatch(command);
            }
        }
    }
}
