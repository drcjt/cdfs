using Protocols;
using System;
using System.ServiceModel;

namespace DataNode
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new DataNodeOptions();

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

        public void Run(DataNodeOptions options)
        {
            var serviceChannelFactory = new ChannelFactory<IDataNodeProtocol>(new NetTcpBinding(), options.NameNodeUri);
            var _nameNode = serviceChannelFactory.CreateChannel();

            var dataNode = new DataNode(_nameNode);
            dataNode.Run();

            Console.ReadLine();

            (_nameNode as ICommunicationObject).Close();
        }
    }
}
