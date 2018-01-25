using Protocols;
using RestSharp;
using System;

namespace DataNode.ProtocolWrappers
{
    public class DataNodeProtocol : IRestDataNodeProtocol
    {
        private const string RegisterOperation = "/DataNodeProtocol/Register";
        private const string SendHeartbeatOperation = "/DataNodeProtocol/SendHeartbeat";

        private readonly IRestClient _restClient;

        public DataNodeProtocol(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Uri BaseUrl
        {
            get
            {
                return _restClient.BaseUrl;
            }
            set
            {
                _restClient.BaseUrl = value;
            }
        }

        public Guid RegisterDataNode(IDataNodeId dataNodeId)
        {
            var request = new RestRequest(RegisterOperation, Method.POST);
            request.AddJsonBody(dataNodeId);
            var restResponse = _restClient.Execute<Guid>(request);
            return restResponse.Data;
        }

        public void SendHeartbeat(Guid dataNodeGuid)
        {
            var request = new RestRequest(SendHeartbeatOperation, Method.POST);
            request.AddJsonBody(dataNodeGuid);
            _restClient.Execute(request);
        }
    }
}
