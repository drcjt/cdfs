using Protocols;
using System;

namespace NameNode.Service
{
    class ClientProtocol : IClientProtocol
    {
        void IClientProtocol.Create(string filePath)
        {
            throw new NotImplementedException();
        }

        void IClientProtocol.Delete(string filePath)
        {
            throw new NotImplementedException();
        }

        CdfsFileStatus[] IClientProtocol.GetListing(string filePath)
        {
            return new CdfsFileStatus[] { new CdfsFileStatus() };
        }

        void IClientProtocol.ReadBlock()
        {
            throw new NotImplementedException();
        }

        void IClientProtocol.WriteBlock()
        {
            throw new NotImplementedException();
        }
    }
}
