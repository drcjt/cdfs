using Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode
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
