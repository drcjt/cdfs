using Protocols;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace DFSClient
{
    public class ClientProtocol : IClientProtocol
    {
        private RestClient Client { get; set; }

        private const string RegisterOperation = "/DataNodeProtocol/Register";
        private const string SendHeartbeatOperation = "/DataNodeProtocol/SendHeartbeat";

        private const string CreateOperation = "/ClientProtocol/Create";
        private const string AddBlockOperation = "/ClientProtocol/AddBlock";
        private const string MkdirOperation = "/ClientProtocol/Mkdir";
        private const string DeleteOperation = "/ClientProtocol/Delete";
        private const string GetListingOperation = "/ClientProtocol/GetListing";

        private const string SrcFileParameter = "srcFile";
        private const string FilePathParameter = "filePath";
        private const string DirectoryPathParameter = "directoryPath";

        public ClientProtocol(CommonSubOptions options)
        {
            Client = new RestClient(options.NameNodeUri);
        }

        private void PerformRequest(IRestRequest request)
        {
            PerformRequest<object>(request);
        }

        private T PerformRequest<T>(IRestRequest request) where T : new()
        {
            var response = Client.Execute<T>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content);
            }
            return response.Data;
        }

        public void Create(string srcFile, string filePath)
        {
            var request = new RestRequest(CreateOperation, Method.POST);
            request.AddParameter(SrcFileParameter, srcFile);
            request.AddParameter(FilePathParameter, filePath);
            PerformRequest(request);
        }

        public LocatedBlock AddBlock(string srcFile)
        {
            var request = new RestRequest(AddBlockOperation, Method.POST);
            request.AddParameter(SrcFileParameter, srcFile);
            return PerformRequest<LocatedBlock>(request);
        }

        public void Delete(string filePath)
        {
            var request = new RestRequest(DeleteOperation, Method.DELETE);
            request.AddParameter(FilePathParameter, filePath);
            PerformRequest(request);
        }

        public void Mkdir(string directoryPath)
        {
            var request = new RestRequest(MkdirOperation, Method.POST);
            request.AddParameter(DirectoryPathParameter, directoryPath);
            PerformRequest(request);
        }

        public IList<CdfsFileStatus> GetListing(string filePath)
        {
            var request = new RestRequest(GetListingOperation);
            request.AddParameter(FilePathParameter, filePath);
            return PerformRequest<List<CdfsFileStatus>>(request);
        }
    }
}
