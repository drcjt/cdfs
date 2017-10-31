using NameNode.FileSystem;
using Protocols;
using System;
using System.Collections.Generic;

namespace NameNode.Service
{
    class ClientProtocol : IClientProtocol
    {
        private readonly InodeDirectory _rootDir = new InodeDirectory();

        public ClientProtocol()
        {
            _rootDir.Name = "";

            /*
            // Simple mock files/directories for initial testing
            _rootDir.AddChild(new INodeFile() { Name = "test.txt" });
            _rootDir.AddChild(new INodeFile() { Name = "foo.bar" });
            var wibbleDir = new INodeDirectory() { Name = "wibble" };
            _rootDir.AddChild(wibbleDir);
            wibbleDir.AddChild(new INodeFile() { Name = "inwibble.csv " });
            */
        }

        void IClientProtocol.Create(string filePath)
        {
            throw new NotImplementedException();
        }

        void IClientProtocol.Delete(string filePath)
        {
            throw new NotImplementedException();
        }

        IList<CdfsFileStatus> IClientProtocol.GetListing(string filePath)
        {
            // Get Inode corresponding to specified directory
            var directory = _rootDir.GetINodeForFullDirectoryPath(filePath);

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
