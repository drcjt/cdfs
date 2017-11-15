using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public class FileSystem : IFileSystem
    {
        private readonly ILog _logger;
        private readonly IFileSystemSerializer _fileSystemSerializer;
        private readonly IFileSystemReaderWriter _fileSystemReaderWriter;
        private readonly INodeWalker _walker;

        public FileSystem(ILog logger, INodeWalker walker, IFileSystemSerializer fileSystemSerializer, IFileSystemReaderWriter fileSystemReaderWriter)
        {
            _logger = logger;
            _walker = walker;
            _fileSystemSerializer = fileSystemSerializer;
            _fileSystemReaderWriter = fileSystemReaderWriter;
        }

        private IDirectory _root = null;

        public IDirectory Root {
            get
            {
                if (_root == null)
                {
                    if (!_fileSystemReaderWriter.FileSystemImageExists("FSImage"))
                    {
                        _root = new Directory();
                    }
                    else
                    {
                        var lines = _fileSystemReaderWriter.ReadFileSystemImageLines("FSImage");
                        _root = _fileSystemSerializer.Deserialize(lines);
                        _logger.Debug("Loaded File Image");
                    }
                }

                return _root;
            }
        }

        public void SaveFileImage()
        {
            _logger.Debug("Saving File Image");
            _fileSystemReaderWriter.WriteFileSystemImage("FSImage", _fileSystemSerializer.Serialize(Root));
        }

        public void Create(string srcFile, string directoryPath)
        {
            var directory = _walker.GetNodeByPath(Root, directoryPath) as IDirectory;

            if (directory == null)
            {
                throw new ArgumentException("Path does not exist", "directoryPath");
            }

            var fileNode = new File { Name = FileSystemPath.GetFileName(srcFile) };
            directory.AddChild(fileNode);

            _logger.DebugFormat($"Created new INode: {fileNode.FullPath}");

            SaveFileImage();
        }

        public void Delete(string filePath)
        {
            var fileNode = _walker.GetNodeByPath(Root, filePath);
            
            if (fileNode == null)
            {
                throw new ArgumentException("Path does not exist", "filePath");
            }

            var parentDirectory = fileNode.Parent as IDirectory;
            if (parentDirectory != null)
            {
                parentDirectory.RemoveChild(fileNode);
                _logger.DebugFormat("Deleted file: {0}", filePath);
            }

            SaveFileImage();
        }

        public void Mkdir(string directoryPath)
        {
            var parentDirectory = _walker.GetNodeByPath(Root, directoryPath, true) as IDirectory;

            if (parentDirectory == null)
            {
                throw new ArgumentException("Parent directory does not exist", "directoryPath");
            }

            if (string.IsNullOrEmpty(FileSystemPath.Normalize(directoryPath)))
            {
                throw new ArgumentException("Must specify a directory to create", "directoryPath");
            }

            string parentDirectoryPath = parentDirectory.FullPath;
            int startingComponentIndex = FileSystemPath.GetComponents(parentDirectoryPath).Length - 1;

            var pathComponents = FileSystemPath.GetComponents(directoryPath);
            for (int componentIndex = startingComponentIndex; componentIndex < pathComponents.Length; componentIndex++)
            {
                var newDirectory = new Directory { Name = pathComponents[componentIndex] };
                parentDirectory.AddChild(newDirectory);
                parentDirectory = newDirectory;
            }

            SaveFileImage();
        }

        public IList<INode> GetListing(string directoryPath)
        {
            var directory = _walker.GetNodeByPath(Root, directoryPath, true) as IDirectory;

            var results = new List<INode>();
            if (directory != null)
            {
                foreach (var inode in directory)
                {
                    results.Add(inode);
                }
            }
            return results;
        }
    }
}
