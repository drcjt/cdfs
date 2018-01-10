using Microsoft.Extensions.Logging;
using NameNode.FileSystem.Interfaces;
using Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public LocatedBlock AddBlock(string srcFile)
        {
            var blockId = Guid.NewGuid();
            var block = new Block(blockId, 0, DateTime.Now);

            var node = _fileSystem.GetFile(srcFile);
            node.AddBlock(block);

            var randomDataNodeID = _dataNodeRepository.GetRandomDataNodeId();
            var dataNodeDescriptor = _dataNodeRepository.GetDataNodeDescriptorById(randomDataNodeID);

            var dataNodeID = new DataNodeId { HostName = dataNodeDescriptor.HostName, Id = randomDataNodeID, IPAddress = dataNodeDescriptor.IPAddress };

            var locatedBlock = new LocatedBlock
            {
                Block = block,
                Locations = new List<DataNodeId> { dataNodeID }
            };

            return locatedBlock;
        }
    }
}
