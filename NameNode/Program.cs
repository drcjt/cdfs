using Protocols;
using System;
using System.ServiceModel;

namespace NameNode
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class Program : IDataNodeProtocol, IClientProtocol
    {
        private static IDataNodeProtocol _nameNode = new NameNode();

        static void Main(string[] args)
        {
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
