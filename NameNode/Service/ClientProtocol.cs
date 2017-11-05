using NameNode.FileSystem;
using Protocols;
using System;
using System.Collections.Generic;
using System.IO;

namespace NameNode.Service
{
    class ClientProtocol : IClientProtocol
    {
        private readonly IFileSystem _fileSystem;

        public ClientProtocol(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        void IClientProtocol.Create(string srcFile, string filePath)
        {
            _fileSystem.Create(srcFile, filePath);
        }

        void IClientProtocol.Delete(string filePath)
        {
            throw new NotImplementedException();
        }

        IList<CdfsFileStatus> IClientProtocol.GetListing(string filePath)
        {
            var nodes = _fileSystem.GetListing(filePath);

            IList<CdfsFileStatus> results = new List<CdfsFileStatus>();
            foreach (var inode in nodes)
            {
                var fileStatus = new CdfsFileStatus() { FilePath = inode.Name, IsDirectory = inode.IsDirectory };
                results.Add(fileStatus);
            }
            return results;
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
