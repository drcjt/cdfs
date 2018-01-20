using Protocols;
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
                c.For<IRestClientProtocol>().Use<ClientProtocol>();
                c.For<IConsole>().Use<ConsoleWrapper>();
            });

            var host = container.GetInstance<IClientHost>();
            host.Run(args);
        }
    }
}
