using log4net;
using NameNode.FileSystem.Interfaces;

namespace NameNode.FileSystem
{
    public class FileSystemReaderWriter : IFileSystemReaderWriter
    {
        private readonly ILog _logger;
        private readonly IFileSystemSerializer _fileSystemSerializer;
        private readonly IFileSystemImageFile _fileSystemImageFile;

        public FileSystemReaderWriter(ILog logger, IFileSystemSerializer fileSystemSerializer, IFileSystemImageFile fileSystemImageFile)
        {
            _logger = logger;
            _fileSystemSerializer = fileSystemSerializer;
            _fileSystemImageFile = fileSystemImageFile;
        }

        public void WriteFileSystem(IDirectory root)
        {
            _logger.Debug("Saving File Image");
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
                _logger.Debug("Loaded File Image");
            }

            return root;
        }
    }
}
