using Fclp;
using Protocols;
using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace DataNode
{
    class Program
    {
        private IDataNodeProtocol _nameNode;

        static void Main(string[] args)
        {
            var parser = new FluentCommandLineParser<DataNodeOptions>();
            parser.Setup(arg => arg.NameNodeUri).As('n', "namenodeuri").SetDefault("tcp://localhost:5150/DataNodeProtocol");

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

        public void Run(DataNodeOptions options)
        {
            var tcpChannel = new TcpChannel();
            ChannelServices.RegisterChannel(tcpChannel, false);

            _nameNode = (IDataNodeProtocol)Activator.GetObject(typeof(IDataNodeProtocol), options.NameNodeUri);

            var dataNode = new DataNode(_nameNode);
            dataNode.Run();
        }
    }
}
