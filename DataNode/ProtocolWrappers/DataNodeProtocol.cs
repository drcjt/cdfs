using DataNode.Options;
using Newtonsoft.Json;
using Protocols;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DataNode.ProtocolWrappers
{
    public class DataNodeProtocol : IDataNodeProtocol
    {
        private HttpClient Client { get; set; }

        private const string RegisterOperation = "/DataNodeProtocol/Register";
        private const string SendHeartbeatOperation = "/DataNodeProtocol/SendHeartbeat";

        public DataNodeProtocol(IDataNodeOptions options)
        {
            Client = new HttpClient { BaseAddress = new Uri(options.NameNodeUri) };
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Guid RegisterDataNode(IDataNodeId dataNodeId)
        {
            var serializedDataNodeId = JsonConvert.SerializeObject(dataNodeId);
            var contentData = new StringContent(serializedDataNodeId, System.Text.Encoding.UTF8, "application/json");
            var response = Client.PostAsync(RegisterOperation, contentData).Result;
            return JsonConvert.DeserializeObject<Guid>(response.Content.ReadAsStringAsync().Result);
        }

        public void SendHeartbeat(Guid dataNodeGuid)
        {
            var serializedDataNodeId = JsonConvert.SerializeObject(dataNodeGuid);
            var contentData = new StringContent(serializedDataNodeId, System.Text.Encoding.UTF8, "application/json");
            Client.PostAsync(SendHeartbeatOperation, contentData);
        }
    }
}
