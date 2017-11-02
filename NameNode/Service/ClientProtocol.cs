using NameNode.FileSystem;
using Protocols;
using System;
using System.Collections.Generic;

namespace NameNode.Service
{
    class ClientProtocol : IClientProtocol
    {
        private readonly INodeDirectory _rootDir = new NodeDirectory();

        public ClientProtocol()
        {
            _rootDir.Name = "";

            // Simple mock files/directories for initial testing
            /*
            _rootDir.AddChild(new NodeFile() { Name = "test.txt" });
            _rootDir.AddChild(new NodeFile() { Name = "foo.bar" });
            var wibbleDir = new NodeDirectory() { Name = "wibble" };
            _rootDir.AddChild(wibbleDir);
            wibbleDir.AddChild(new NodeFile() { Name = "inwibble.csv " });
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
