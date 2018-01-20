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
        public ClientHost(IOptionParser optionParser, IRestClientProtocol clientProtocol, IConsole console)
        {
            _optionParser = optionParser;
            _clientProtocol = clientProtocol;
            _console = console;
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

            if (options is ListingSubOptions)
            {
                var listingOptions = options as ListingSubOptions;
                var srcPath = listingOptions.FilePath.Count > 0 ? listingOptions.FilePath[0] : "";
                var listing = _clientProtocol.GetListing(srcPath);
                foreach (var file in listing)
                {
                    _console.WriteLine(file);
                }
            }
            else if (options is PutSubOptions)
            {
                var putSubOptions = options as PutSubOptions;
                _clientProtocol.Create(putSubOptions.PutValues[0], putSubOptions.PutValues[1]);
            }
            else if (options is DeleteSubOptions)
            {
                var deleteSubOptions = options as DeleteSubOptions;
                _clientProtocol.Delete(deleteSubOptions.FilePath[0]);
            }
            else if (options is MkdirSubOptions)
            {
                var mkdirSubOptions = options as MkdirSubOptions;
                _clientProtocol.Mkdir(mkdirSubOptions.DirectoryPath[0]);
            }
        }
    }
}
