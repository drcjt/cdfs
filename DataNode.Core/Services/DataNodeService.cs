using Microsoft.Extensions.Configuration;
using Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Timers;

namespace DataNode.Services
{
    public class DataNodeService : IDataNodeApplication
    {
        private readonly IDataNodeProtocol _nameNode;
        private Guid ID { get; set; }

        public DataNodeService(IDataNodeProtocol nameNode, IConfiguration configuration)
        {
            _nameNode = nameNode;
        }

        public void Run()
        {
            var dataNodeId = new DataNodeId { IPAddress = GetLocalIPAddress(), HostName = Dns.GetHostName() };

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
