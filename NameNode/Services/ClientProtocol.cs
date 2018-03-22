using NameNode.BlockManagement;
using NameNode.FileSystem.Interfaces;
using NameNode.Services.Interfaces;
using Protocols;
using System;
using System.Collections.Generic;

namespace NameNode.Services
{
    public class ClientProtocol : IClientProtocol
    {
        private readonly IFileSystem _fileSystem;
        private readonly IDataNodeRepository _dataNodeRepository;

        public ClientProtocol(IFileSystem fileSystem, IDataNodeRepository dataNodeRepository)
        {
            _fileSystem = fileSystem;
            _dataNodeRepository = dataNodeRepository;
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

        public ICollection<CdfsFileStatus> GetListing(string filePath)
        {
            var nodes = _fileSystem.GetListing(filePath);

            ICollection<CdfsFileStatus> results = new List<CdfsFileStatus>();
            foreach (var inode in nodes)
            {
                var fileStatus = new CdfsFileStatus() { FilePath = inode.Name, IsDirectory = inode is IDirectory };
                results.Add(fileStatus);
            }
            return results;
        }

        public LocatedBlock AddBlock(string srcFile)
        {
            var randomDataNodeID = _dataNodeRepository.GetRandomDataNodeId();
            var dataNodeDescriptor = _dataNodeRepository.GetDataNodeDescriptorById(randomDataNodeID);

            var dataNodeID = new DataNodeId { HostName = dataNodeDescriptor.HostName, IPAddress = dataNodeDescriptor.IPAddress };
            var dataNodeIds = new List<DataNodeId> { dataNodeID };

            var blockId = Guid.NewGuid();
            var block = new Block(blockId, 0, DateTime.Now);

            var blockInfo = new BlockInfo(block, dataNodeIds);

            var node = _fileSystem.GetFile(srcFile);
            node.AddBlock(blockInfo);

            var locatedBlock = new LocatedBlock
            {
                Block = block,
                Locations = dataNodeIds
            };

            return locatedBlock;
        }
    }
}
