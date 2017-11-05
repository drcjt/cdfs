using System.Collections.Generic;
using System.ServiceModel;

namespace Protocols
{
    [ServiceContract]
    public interface IClientProtocol
    {
        [OperationContract]
        void Create(string srcFile, string filePath);

        [OperationContract]
        void Delete(string filePath);

        [OperationContract]
        IList<CdfsFileStatus> GetListing(string filePath);

        [OperationContract]
        void WriteBlock();

        [OperationContract]
        void ReadBlock();
    }
}
