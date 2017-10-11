using Fclp;
using Protocols;
using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.ServiceModel;

namespace DataNode
{
    class Program
    {
        private IDataNodeProtocol _nameNode;

        static void Main(string[] args)
        {
            var parser = new FluentCommandLineParser<DataNodeOptions>();
            parser.Setup(arg => arg.NameNodeUri).As('n', "namenodeuri").SetDefault("net.tcp://localhost:5150/DataNodeProtocol");

            var result = parser.Parse(args);

            if (result.HasErrors)
            {
                Console.WriteLine("Invalid options");
            }
            else
            {
                var program = new Program();
                program.Run(parser.Object);
            }
        }

        public void Run(DataNodeOptions options)
        {
            var serviceChannelFactory = new ChannelFactory<IDataNodeProtocol>(new NetTcpBinding(), options.NameNodeUri);
            _nameNode = serviceChannelFactory.CreateChannel();

            var dataNode = new DataNode(_nameNode);
            dataNode.Run();

            Console.ReadLine();

            (_nameNode as ICommunicationObject).Close();
        }
    }
}
