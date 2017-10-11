using Fclp;
using Protocols;
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.ServiceModel;

namespace NameNode
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class Program : IDataNodeProtocol
    {
        private static IDataNodeProtocol _nameNode = new NameNode();

        static void Main(string[] args)
        {
            var parser = new FluentCommandLineParser<NameNodeOptions>();
            parser.Setup(arg => arg.Port).As('p', "port").SetDefault(5150);

            var result = parser.Parse(args);

            if (result.HasErrors)
            {
                Console.WriteLine("Invalid options");
            }
            else
            {
                var program = new Program();
                program.Run(parser.Object);

                Console.ReadLine();
            }
        }

        public void Run(NameNodeOptions options)
        {
            var serviceHost = new ServiceHost(typeof(Program));
            var serviceUri = "net.tcp://localhost:" + options.Port + "/DataNodeProtocol";
            serviceHost.AddServiceEndpoint(typeof(IDataNodeProtocol), new NetTcpBinding(), serviceUri);
            serviceHost.Open();

            Console.ReadLine();

            serviceHost.Close();
        }

        Guid IDataNodeProtocol.RegisterDataNode(DataNodeRegistration dataNodeRegistration)
        {
            return _nameNode.RegisterDataNode(dataNodeRegistration);
        }

        void IDataNodeProtocol.SendHeartbeat(Guid dataNodeID)
        {
            _nameNode.SendHeartbeat(dataNodeID);
        }
    }
}
