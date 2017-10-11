using Protocols;
using System;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace DataNode
{
    public class DataNode
    {
        private IDataNodeProtocol _nameNode;
        private Guid ID { get; set; }

        public DataNode(IDataNodeProtocol nameNode)
        {
            _nameNode = nameNode;
        }

        public void Run()
        {
            var dataNodeRegistration = new DataNodeRegistration();
            dataNodeRegistration.IPAddress = GetLocalIPAddress();
            dataNodeRegistration.HostName = Dns.GetHostName();

            ID = _nameNode.RegisterDataNode(dataNodeRegistration);

            var timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(SendHeartbeat);
            timer.Start();
        }

        public void SendHeartbeat(object source, ElapsedEventArgs args)
        {
            _nameNode.SendHeartbeat(ID);
        }

        public static string GetLocalIPAddress()
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
