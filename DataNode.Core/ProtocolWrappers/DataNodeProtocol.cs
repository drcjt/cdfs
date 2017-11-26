using DataNode.Core.Options;
using Newtonsoft.Json;
using Protocols.Core;
using Refit;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DataNode.Core.ProtocolWrappers
{
    public class DataNodeProtocol : IDataNodeProtocol
    {
        private HttpClient Client { get; set; }

        public DataNodeProtocol(IDataNodeOptions options)
        {
            Client = new HttpClient { BaseAddress = new Uri(options.NameNodeUri) };
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Guid RegisterDataNode(IDataNodeId dataNodeId)
        {
            var serializedDataNodeId = JsonConvert.SerializeObject(dataNodeId);
            var contentData = new StringContent(serializedDataNodeId, System.Text.Encoding.UTF8, "application/json");
            var response = Client.PostAsync("/DataNodeProtocol/Register", contentData).Result;
            return JsonConvert.DeserializeObject<Guid>(response.Content.ReadAsStringAsync().Result);
        }

        public void SendHeartbeat(Guid dataNodeGuid)
        {
            var serializedDataNodeId = JsonConvert.SerializeObject(dataNodeGuid);
            var contentData = new StringContent(serializedDataNodeId, System.Text.Encoding.UTF8, "application/json");
            var response = Client.PostAsync("/DataNodeProtocol/SendHeartbeat", contentData).Result;
        }
    }
}
