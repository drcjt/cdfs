using DataNode.Options;
using DataNode.ProtocolWrappers;
using Microsoft.Extensions.Configuration;
using Protocols;
using System;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace DataNode.Services
{
    public class DataNodeService : IDataNodeApplication
    {
        private readonly IRestDataNodeProtocol _nameNode;
        private readonly IDataNodeOptions _dataNodeOptions;
        private Guid ID { get; set; }

        public DataNodeService(IRestDataNodeProtocol nameNode, IConfiguration configuration, IDataNodeOptions dataNodeOptions)
        {
            _nameNode = nameNode;
            _dataNodeOptions = dataNodeOptions;
        }

        public void Run(string url)
        {
            var dataNodeId = new DataNodeId { IPAddress = url, HostName = Dns.GetHostName() };

            _nameNode.BaseUrl = new Uri(_dataNodeOptions.NameNodeUri);
            ID = _nameNode.RegisterDataNode(dataNodeId);

            var timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(SendHeartbeat);
            timer.Start();
        }

        public void SendHeartbeat(object source, ElapsedEventArgs args)
        {
            _nameNode.SendHeartbeat(ID);
        }

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
