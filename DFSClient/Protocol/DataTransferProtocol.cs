using Protocols;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DFSClient.Protocol
{
    public class DataTransferProtocol : IRestDataTransferProtocol
    {
        private readonly IRestClient _restClient;

        public DataTransferProtocol(IRestClient restClient)
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

        public void WriteBlock(Block block, Stream data)
        {
            var request = new RestRequest("DataTransferProtocol/WriteBlock/{id}", Method.PATCH);
            request.AddParameter("id", block.ID, ParameterType.UrlSegment);

            using (MemoryStream ms = new MemoryStream())
            {
                data.CopyTo(ms);

                request.AddParameter("application/pdf", ms.ToArray(), ParameterType.RequestBody);
                PerformRequest(request);
            }
        }
    }
}
