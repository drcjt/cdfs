using Protocols;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFSClient.Protocol
{
    public interface IRestDataTransferProtocol : IDataTransferProtocol
    {
        Uri BaseUrl { get; set; }
    }
}
