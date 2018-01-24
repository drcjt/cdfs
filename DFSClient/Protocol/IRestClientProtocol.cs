using Protocols;
using System;

namespace DFSClient.Protocol
{
    public interface IRestClientProtocol : IClientProtocol 
    {
        Uri BaseUrl { get; set; }
    }
}
