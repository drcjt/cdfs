using Protocols;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace DFSClient.Protocol
{
    public class ClientProtocol : IRestClientProtocol
    {
        private const string CreateOperation = "/ClientProtocol/Create";
        private const string AddBlockOperation = "/ClientProtocol/AddBlock";
        private const string MkdirOperation = "/ClientProtocol/Mkdir";
        private const string DeleteOperation = "/ClientProtocol/Delete";
        private const string GetListingOperation = "/ClientProtocol/GetListing";

        private const string SrcFileParameter = "srcFile";
        private const string FilePathParameter = "filePath";
        private const string DirectoryPathParameter = "directoryPath";

        private readonly IRestClient _restClient;

        public ClientProtocol(IRestClient restClient)
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

        private void PerformRequest(IRestRequest request)
        {
            PerformRequest<object>(request);
        }

        // TODO : review with respect to https://stackoverflow.com/questions/31516501/how-to-idiomatically-handle-http-error-codes-when-using-restsharp

        private T PerformRequest<T>(IRestRequest request) where T : new()
        {
            var response = _restClient.Execute<T>(request);
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
