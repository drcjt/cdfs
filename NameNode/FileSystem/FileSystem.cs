using Microsoft.Extensions.Logging;
using NameNode.FileSystem.Interfaces;
using System;
using System.Collections.Generic;

namespace NameNode.FileSystem
{
    public class FileSystem : IFileSystem
    {
        private readonly ILogger _logger;
        private readonly INodeWalker _walker;
        private readonly IFileSystemReaderWriter _fileSystemReaderWriter;

        public FileSystem(ILoggerFactory loggerFactory, INodeWalker walker, IFileSystemReaderWriter fileSystemReaderWriter)
        {
            _logger = loggerFactory.CreateLogger<FileSystem>();
            _walker = walker;
            _fileSystemReaderWriter = fileSystemReaderWriter;
        }

        private IDirectory _root = null;

        public IDirectory Root {
            get
            {
                if (_root == null)
                {
                    _root = _fileSystemReaderWriter.ReadFileSystem();
                }
                return _root;
            }
        }

        private void OnFileSystemChanged()
        {
            _fileSystemReaderWriter.WriteFileSystem(Root);
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

            _logger.LogDebug($"Created new INode: {fileNode.FullPath}");

            OnFileSystemChanged();
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
                _logger.LogDebug("Deleted file: {0}", filePath);
            }

            OnFileSystemChanged();
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

            OnFileSystemChanged();
        }

        public IList<INode> GetListing(string directoryPath)
        {
            var directory = _walker.GetNodeByPath(Root, directoryPath, true) as IDirectory;

            if (directory == null)
            {
                throw new ArgumentException("Path does not exist", "directoryPath");
            }

            var results = new List<INode>();
            foreach (var inode in directory)
            {
                results.Add(inode);
            }
            return results;
        }

        public IFile GetFile(string srcFile)
        {
            var file = _walker.GetNodeByPath(Root, srcFile) as IFile;

            return file;
        }
    }
}
