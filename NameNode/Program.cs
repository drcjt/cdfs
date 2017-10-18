using NameNode.DependencyInjection;
using Protocols;
using System;
using System.ServiceModel;

namespace NameNode
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            Singleton.Container.Configure(config => config.AddRegistry<NameNodeRegistry>());

            var options = new NameNodeOptions();

            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                var program = new Program();
                program.Run(options);
            }
            else
            {
                Console.WriteLine("Invalid options");
            }
        }

        public void Run(NameNodeOptions options)
        {
            var serviceHost = new ServiceHost(typeof(NameNodeService));

            var dataNodeServiceUri = "net.tcp://localhost:" + options.Port + "/DataNodeProtocol";
            serviceHost.AddServiceEndpoint(typeof(IDataNodeProtocol), new NetTcpBinding(), dataNodeServiceUri);

            var clientServiceUri = "net.tcp://localhost:" + options.Port + "/ClientProtocol";
            serviceHost.AddServiceEndpoint(typeof(IClientProtocol), new NetTcpBinding(), clientServiceUri);

            serviceHost.Open();

            Console.ReadLine();

            serviceHost.Close();
        }
    }
}
