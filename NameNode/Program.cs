using NameNode.DependencyInjection;
using NameNode.WebApp;
using Protocols;
using StructureMap;
using System;
using System.ServiceModel;
using System.Configuration;
using Topshelf;
using NameNode.Options;
using NameNode.Service;

namespace NameNode
{
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class Program
    {
        static void Main(string[] args)
        {
            // Configure log4net
            log4net.Config.XmlConfigurator.Configure();

            // Create the structuremap container
            var container = new Container(cfg =>
            {
                cfg.AddRegistry<NameNodeRegistry>();
            });

            // TODO: can this be done through structuremap??
            // Get options from app.config
            var options = new NameNodeOptions();
            options.Port = ConfigurationManager.AppSettings.GetValue<int>("Port", 5150);
            options.HttpAddress = new Uri(ConfigurationManager.AppSettings.GetValue<string>("HttpAddress", "http://localhost:5151"));

            // TODO: can we use structuremap to instantiate the service itself??
            // Run the service in topshelf
            HostFactory.Run(x => 
            {
                x.Service<Program>(s =>
                {
                    s.ConstructUsing(p => new Program());
                    s.WhenStarted(p => p.Start(container, options));
                    s.WhenStopped(p => p.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("C# Distributed File System Name Node Service");
                x.SetServiceName("CDFS_NameNode");
            });
        }

        // The WCF service
        private ServiceHost _serviceHost;

        // The self hosted nancy web app
        private CdfsWebApp _webApp;

        public void Start(IContainer container, NameNodeOptions options)
        {
            // Create a service host for the NameNodeService
            _serviceHost = new StructureMapServiceHost(container, typeof(NameNodeService));

            // Add the two endpoints for the protocols the NameNode implements
            _serviceHost.AddServiceEndpoint(typeof(IDataNodeProtocol), new NetTcpBinding(), "net.tcp://localhost:" + options.Port + "/DataNodeProtocol");
            _serviceHost.AddServiceEndpoint(typeof(IClientProtocol), new NetTcpBinding(), "net.tcp://localhost:" + options.Port + "/ClientProtocol");

            // Set the concurrency mode for the WCF service
            _serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>().ConcurrencyMode = ConcurrencyMode.Single;

            // Create the self hosted nancy web app
            _webApp = new CdfsWebApp(container, options.HttpAddress);

            // Start the web app and the WCF service
            _webApp.Start();
            _serviceHost.Open();
        }

        public void Stop()
        {
            // Stop the web app and the WCF service
            _webApp.Stop();
            _serviceHost.Close();
        }
    }
}
