using DataNode.Options;
using Newtonsoft.Json;
using Protocols;
using RestSharp;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DataNode.ProtocolWrappers
{
    public class DataNodeProtocol : IDataNodeProtocol
    {
        private RestClient Client { get; set; }

        private const string RegisterOperation = "/DataNodeProtocol/Register";
        private const string SendHeartbeatOperation = "/DataNodeProtocol/SendHeartbeat";

        public DataNodeProtocol(IDataNodeOptions options)
        {
            Client = new RestClient(options.NameNodeUri);
        }

        public Guid RegisterDataNode(IDataNodeId dataNodeId)
        {
            var request = new RestRequest(RegisterOperation, Method.POST);
            request.AddJsonBody(dataNodeId);
            var restResponse = Client.Execute<Guid>(request);
            return restResponse.Data;
        }

        public void SendHeartbeat(Guid dataNodeGuid)
        {
            var request = new RestRequest(SendHeartbeatOperation, Method.POST);
            request.AddJsonBody(dataNodeGuid);
            Client.Execute(request);
        }
    }
}
