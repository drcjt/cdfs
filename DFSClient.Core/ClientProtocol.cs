using Newtonsoft.Json;
using Protocols;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DFSClient
{
    public class ClientProtocol : IClientProtocol
    {
        private HttpClient Client { get; set; }

        public ClientProtocol(CommonSubOptions options)
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

        public void Create(string srcFile, string filePath)
        {
            var values = new Dictionary<string, string>();
            values.Add("srcFile", srcFile);
            values.Add("filePath", filePath);
            var contentData = new FormUrlEncodedContent(values);
            var response = Client.PostAsync("/ClientProtocol/Create", contentData).Result;
        }

        public LocatedBlock AddBlock(string srcFile)
        {
            var values = new Dictionary<string, string>();
            values.Add("srcFile", srcFile);
            var contentData = new FormUrlEncodedContent(values);
            var response = Client.PostAsync("/ClientProtocol/AddBlock", contentData).Result;
            return JsonConvert.DeserializeObject<LocatedBlock>(response.Content.ReadAsStringAsync().Result);
        }

        public void Delete(string filePath)
        {
            var response = Client.DeleteAsync(string.Format("/ClientProtocol/Delete?filePath={0}", filePath)).Result;
        }

        public void Mkdir(string directoryPath)
        {
            var values = new Dictionary<string, string>();
            values.Add("directoryPath", directoryPath);
            var contentData = new FormUrlEncodedContent(values);
            var response = Client.PostAsync("/ClientProtocol/Mkdir", contentData).Result;
        }

        public IList<CdfsFileStatus> GetListing(string filePath)
        {
            var response = Client.GetAsync(string.Format("/ClientProtocol/GetListing?filePath={0}", filePath)).Result;
            return JsonConvert.DeserializeObject<IList<CdfsFileStatus>>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
