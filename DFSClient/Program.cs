using System;
using System.ServiceModel;
using Protocols;

namespace DFSClient
{
    public class Program
    {


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
            var _nameNode = serviceChannelFactory.CreateChannel();

            if (options is ListingSubOptions)
            {
                var listingOptions = options as ListingSubOptions;
                var srcPath = listingOptions.FilePath.Count > 0 ? listingOptions.FilePath[0] : "";
                var listing = _nameNode.GetListing(srcPath);
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
