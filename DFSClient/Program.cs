using Fclp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Protocols;

namespace DFSClient
{
    class Program
    {
        private IClientProtocol _nameNode;

        static void Main(string[] args)
        {
            var options = new Options();

            string invokedVerb = "";
            object invokedVerbInstance = null;
            if (!CommandLine.Parser.Default.ParseArguments(args, options,
                (verb, subOptions) =>
                {
                    invokedVerb = verb;
                    invokedVerbInstance = subOptions;
                }))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }

            var program = new Program();
            program.Run(invokedVerb, invokedVerbInstance as CommonSubOptions);
        }

        public void Run(string verb, CommonSubOptions options)
        {
            var serviceChannelFactory = new ChannelFactory<IClientProtocol>(new NetTcpBinding(), options.NameNodeUri);
            _nameNode = serviceChannelFactory.CreateChannel();

            if (options is ListingSubOptions)
            {
                var listing = _nameNode.GetListing("");
                foreach (var file in listing)
                {
                    Console.WriteLine(file);
                }
            }
            else if (options is GetSubOptions)
            {

            }
            else if (options is PutSubOptions)
            {
                var putSubOptions = options as PutSubOptions;
                _nameNode.Create(putSubOptions.FilePath[0]);
            }
        }
    }
}
