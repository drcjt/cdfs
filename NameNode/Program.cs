using Fclp;
using Protocols;
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace NameNode
{
    class Program : MarshalByRefObject, IDataNodeProtocol
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
            var tcpChannel = new TcpChannel(options.Port);
            ChannelServices.RegisterChannel(tcpChannel, false);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Program), "DataNodeProtocol", WellKnownObjectMode.Singleton);
        }

        Guid IDataNodeProtocol.RegisterDataNode(IDataNodeRegistration dataNodeRegistration)
        {
            return _nameNode.RegisterDataNode(dataNodeRegistration);
        }

        void IDataNodeProtocol.SendHeartbeat(Guid dataNodeID)
        {
            _nameNode.SendHeartbeat(dataNodeID);
        }
    }
}
