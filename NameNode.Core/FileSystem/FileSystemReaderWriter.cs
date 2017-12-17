using Microsoft.Extensions.Logging;
using NameNode.Core.FileSystem.Interfaces;

namespace NameNode.Core.FileSystem
{
    public class FileSystemReaderWriter : IFileSystemReaderWriter
    {
        private readonly ILogger _logger;
        private readonly IFileSystemSerializer _fileSystemSerializer;
        private readonly IFileSystemImageFile _fileSystemImageFile;

        public FileSystemReaderWriter(ILoggerFactory loggerFactory, IFileSystemSerializer fileSystemSerializer, IFileSystemImageFile fileSystemImageFile)
        {
            _logger = loggerFactory.CreateLogger<FileSystemReaderWriter>();
            _fileSystemSerializer = fileSystemSerializer;
            _fileSystemImageFile = fileSystemImageFile;
        }

        public void WriteFileSystem(IDirectory root)
        {
            _logger.LogDebug("Saving File Image");
            _fileSystemImageFile.WriteFileSystemImage(_fileSystemSerializer.Serialize(root));
        }

        public IDirectory ReadFileSystem()
        {
            IDirectory root = null;
            if (!_fileSystemImageFile.FileSystemImageExists())
            {
                root = new Directory();
            }
            else
            {
                var lines = _fileSystemImageFile.ReadFileSystemImageLines();
                root = _fileSystemSerializer.Deserialize(lines);
                _logger.LogDebug("Loaded File Image");
            }

            return root;
        }
    }
}
