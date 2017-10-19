using NameNode.DependencyInjection;
using Protocols;
using System;
using System.ServiceModel;
using System.ServiceModel.Web;

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

            serviceHost.AddServiceEndpoint(typeof(IDataNodeProtocol), new NetTcpBinding(), "net.tcp://localhost:" + options.Port + "/DataNodeProtocol");
            serviceHost.AddServiceEndpoint(typeof(IClientProtocol), new NetTcpBinding(), "net.tcp://localhost:" + options.Port + "/ClientProtocol");

            var mgmtServiceHost = new WebServiceHost(typeof(NameNodeManagementService), new Uri("http://localhost:8080"));
            mgmtServiceHost.AddServiceEndpoint(typeof(INameNodeManagement), new WebHttpBinding(), "");

            serviceHost.Open();
            mgmtServiceHost.Open();

            Console.WriteLine("Press <Enter> to stop the service");
            Console.ReadLine();

            serviceHost.Close();
            mgmtServiceHost.Close();
        }
    }
}
