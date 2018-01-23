using DFSClient.Commands;
using Protocols;
using RestSharp;
using StructureMap;
using System;

namespace DFSClient
{
    static public class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(c =>
            {
                c.For<IClientHost>().Use<ClientHost>();
                c.For<IRestClientProtocol>().Use<ClientProtocol>().Singleton();
                c.For<IConsole>().Use<ConsoleWrapper>();
                c.For<ICommandDispatcher>().Use<CommandDispatcher>();
                c.For<IOptionParser>().Use<OptionParser>();
                c.For<IRestClient>().Use<RestClient>().SelectConstructor(() => new RestClient());

                c.For<ICommandHandler<ListingCommand>>().Use<ListingCommandHandler>();
                c.For<ICommandHandler<PutCommand>>().Use<PutCommandHandler>();
                c.For<ICommandHandler<MkdirCommand>>().Use<MkdirCommandHandler>();
                c.For<ICommandHandler<DeleteCommand>>().Use<DeleteCommandHandler>();
            });

            var host = container.GetInstance<IClientHost>();
            host.Run(args);
        }
    }
}
