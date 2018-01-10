using Newtonsoft.Json;
using Protocols;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DFSClient
{
    public class ClientProtocol : IClientProtocol
    {
        private HttpClient Client { get; set; }

        private const string RegisterOperation = "/DataNodeProtocol/Register";
        private const string SendHeartbeatOperation = "/DataNodeProtocol/SendHeartbeat";

        private const string CreateOperation = "/ClientProtocol/Create";
        private const string AddBlockOperation = "/ClientProtocol/AddBlock";
        private const string MkdirOperation = "/ClientProtocol/Mkdir";
        private const string DeleteOperation = "/ClientProtocol/Delete?filePath={0}";
        private const string GetListingOperation = "/ClientProtocol/GetListing?filePath={0}";

        public ClientProtocol(CommonSubOptions options)
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

        public void Create(string srcFile, string filePath)
        {
            var values = new Dictionary<string, string>
            {
                { "srcFile", srcFile },
                { "filePath", filePath }
            };
            var contentData = new FormUrlEncodedContent(values);
            Client.PostAsync(CreateOperation, contentData);
        }

        public LocatedBlock AddBlock(string srcFile)
        {
            var values = new Dictionary<string, string>
            {
                { "srcFile", srcFile }
            };
            var contentData = new FormUrlEncodedContent(values);
            var response = Client.PostAsync(AddBlockOperation, contentData).Result;
            return JsonConvert.DeserializeObject<LocatedBlock>(response.Content.ReadAsStringAsync().Result);
        }

        public void Delete(string filePath)
        {
            Client.DeleteAsync(string.Format(DeleteOperation, filePath));
        }

        public void Mkdir(string directoryPath)
        {
            var values = new Dictionary<string, string>
            {
                { "directoryPath", directoryPath }
            };
            var contentData = new FormUrlEncodedContent(values);
            Client.PostAsync(MkdirOperation, contentData);
        }

        public IList<CdfsFileStatus> GetListing(string filePath)
        {
            var response = Client.GetAsync(string.Format(GetListingOperation, filePath)).Result;
            return JsonConvert.DeserializeObject<IList<CdfsFileStatus>>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
