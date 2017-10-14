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
    class Program : IDataNodeProtocol, IClientProtocol
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

            var dataNodeServiceUri = "net.tcp://localhost:" + options.Port + "/DataNodeProtocol";
            serviceHost.AddServiceEndpoint(typeof(IDataNodeProtocol), new NetTcpBinding(), dataNodeServiceUri);

            var clientServiceUri = "net.tcp://localhost:" + options.Port + "/ClientProtocol";
            serviceHost.AddServiceEndpoint(typeof(IClientProtocol), new NetTcpBinding(), clientServiceUri);

            serviceHost.Open();

            Console.ReadLine();

            serviceHost.Close();
        }

        void IClientProtocol.Create(string filePath)
        {
            throw new NotImplementedException();
        }

        void IClientProtocol.Delete(string filePath)
        {
            throw new NotImplementedException();
        }

        CdfsFileStatus[] IClientProtocol.GetListing(string filePath)
        {
            return new CdfsFileStatus[] { new CdfsFileStatus()  };
        }

        void IClientProtocol.ReadBlock()
        {
            throw new NotImplementedException();
        }

        Guid IDataNodeProtocol.RegisterDataNode(DataNodeRegistration dataNodeRegistration)
        {
            return _nameNode.RegisterDataNode(dataNodeRegistration);
        }

        void IDataNodeProtocol.SendHeartbeat(Guid dataNodeID)
        {
            _nameNode.SendHeartbeat(dataNodeID);
        }

        void IClientProtocol.WriteBlock()
        {
            throw new NotImplementedException();
        }
    }
}
