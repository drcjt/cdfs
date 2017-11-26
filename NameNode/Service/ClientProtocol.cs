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

        public void Create(string srcFile, string filePath)
        {            
            _fileSystem.Create(srcFile, filePath);
        }

        public void Delete(string filePath)
        {
            _fileSystem.Delete(filePath);
        }

        public void Mkdir(string directoryPath)
        {
            _fileSystem.Mkdir(directoryPath);
        }

        public IList<CdfsFileStatus> GetListing(string filePath)
        {
            var nodes = _fileSystem.GetListing(filePath);

            IList<CdfsFileStatus> results = new List<CdfsFileStatus>();
            foreach (var inode in nodes)
            {
                var fileStatus = new CdfsFileStatus() { FilePath = inode.Name, IsDirectory = inode is IDirectory };
                results.Add(fileStatus);
            }
            return results;
        }

        public void ReadBlock()
        {
            throw new NotImplementedException();
        }

        public void WriteBlock()
        {
            throw new NotImplementedException();
        }

        public LocatedBlock AddBlock(string srcFile)
        {
            var blockId = Guid.NewGuid();
            var block = new Block(blockId, 0, DateTime.Now);

            var node = _fileSystem.GetFile(srcFile);
            node.AddBlock(block);

            var locatedBlock = new LocatedBlock();
            locatedBlock.Block = block;

            return locatedBlock;
        }
    }
}
