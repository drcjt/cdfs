using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols
{
    public interface IClientProtocol
    {
        void Create(string srcFile, string filePath);
        LocatedBlock AddBlock(string srcFile);
        void Delete(string filePath);
        void Mkdir(string directoryPath);
        ICollection<CdfsFileStatus> GetListing(string filePath);
    }
}
