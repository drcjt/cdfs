using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Protocols
{
    [ServiceContract]
    public interface IClientProtocol
    {
        [OperationContract]
        void Create(string filePath);

        [OperationContract]
        void Delete(string filePath);

        [OperationContract]
        CdfsFileStatus[] GetListing(string filePath);

        [OperationContract]
        void WriteBlock();

        [OperationContract]
        void ReadBlock();
    }
}
