using DFSClient.Commands;
using Protocols;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFSClient
{
    public class ClientHost : IClientHost
    {
        private readonly IOptionParser _optionParser;
        private readonly IRestClientProtocol _clientProtocol;
        private readonly IConsole _console;
        private readonly ICommandDispatcher _commandDispatcher;
        public ClientHost(IOptionParser optionParser, IRestClientProtocol clientProtocol, IConsole console, ICommandDispatcher commandDispatcher)
        {
            _optionParser = optionParser;
            _clientProtocol = clientProtocol;
            _console = console;
            _commandDispatcher = commandDispatcher;
        }

        public void Run(string[] args)
        {
            var options = _optionParser.ParseOptions(args);
            if (options != null)
            {
                Run(options);
            }
        }

        private void Run(CommonSubOptions options)
        {
            _clientProtocol.BaseUrl = new Uri(options.NameNodeUri);
            _commandDispatcher.Dispatch(options.Command);
        }
    }
}
