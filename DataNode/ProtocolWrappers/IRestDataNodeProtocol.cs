using Protocols;
using System;

namespace DataNode.ProtocolWrappers
{
    public interface IRestDataNodeProtocol : IDataNodeProtocol
    {
        Uri BaseUrl { get; set; }
    }
}
