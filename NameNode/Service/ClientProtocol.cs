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
            var directory = _fileSystem.Root.GetINodeForFullDirectoryPath(filePath);
            var fileNode = new NodeFile();
            fileNode.Name = Path.GetFileName(srcFile);
            directory.AddChild(fileNode);
        }

        void IClientProtocol.Delete(string filePath)
        {
            throw new NotImplementedException();
        }

        IList<CdfsFileStatus> IClientProtocol.GetListing(string filePath)
        {
            // Get Inode corresponding to specified directory
            var directory = _fileSystem.Root.GetINodeForFullDirectoryPath(filePath);

            IList<CdfsFileStatus> results = new List<CdfsFileStatus>();
            foreach (var inode in directory)
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
