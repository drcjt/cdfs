using Protocols;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFSClient
{
    public interface IRestClientProtocol : IClientProtocol 
    {
        Uri BaseUrl { get; set; }
    }
}
