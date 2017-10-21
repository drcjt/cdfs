using NameNode.DependencyInjection;
using NameNode.WebApp;
using Protocols;
using StructureMap;
using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Topshelf;

namespace NameNode
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            var container = new Container(cfg =>
            {
                cfg.AddRegistry<NameNodeRegistry>();
            });

            HostFactory.Run(x => 
            {
                x.Service<Program>(s =>
                {
                    s.ConstructUsing(p => new Program());
                    s.WhenStarted(p => p.Start(container));
                    s.WhenStopped(p => p.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("C# Distributed File System Name Node Service");
                x.SetServiceName("CDFS_NameNode");
            });

            /*
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
            */
        }

        public Program()
        {
        }

        private ServiceHost _serviceHost;
        private CdfsWebApp _webApp;
        public void Start(IContainer container)
        {
            _serviceHost = new StructureMapServiceHost(container, typeof(NameNodeService));

            _serviceHost.AddServiceEndpoint(typeof(IDataNodeProtocol), new NetTcpBinding(), "net.tcp://localhost:5150/DataNodeProtocol");
            _serviceHost.AddServiceEndpoint(typeof(IClientProtocol), new NetTcpBinding(), "net.tcp://localhost:5150/ClientProtocol");

            _webApp = new CdfsWebApp();
            _webApp.Start(container);

            _serviceHost.Open();
        }

        public void Stop()
        {
            _webApp.Stop();

            _serviceHost.Close();
        }
    }
}
