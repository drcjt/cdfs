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
        private readonly INodeWalker _walker;

        public FileSystem(ILog logger, IFileSystemSerializer fileSystemSerializer, INodeWalker walker)
        {
            _logger = logger;
            _fileSystemSerializer = fileSystemSerializer;
            _walker = walker;
        }

        private IDirectory _root = null;

        public IDirectory Root {
            get
            {
                if (_root == null)
                {
                    if (!System.IO.File.Exists("FSImage"))
                    {
                        _root = new Directory();
                    }
                    else
                    {
                        var lines = System.IO.File.ReadLines("FSImage");
                        _root = _fileSystemSerializer.Deserialize(lines.GetEnumerator());
                        _logger.Debug("Loaded File Image");
                    }
                }

                return _root;
            }
        }

        public void SaveFileImage()
        {
            _logger.Debug("Saving File Image");
            System.IO.File.WriteAllText("FSImage", _fileSystemSerializer.Serialize(Root));
        }

        public void Create(string srcFile, string directoryPath)
        {
            var directory = _walker.GetNodeByPath(Root, directoryPath) as IDirectory;
            if (directory != null)
            {
                var fileNode = new File { Name = FileSystemPath.GetFileName(srcFile) };
                directory.AddChild(fileNode);

                _logger.DebugFormat("Created new INode: {0}{1}{2}", directoryPath, Path.DirectorySeparatorChar, srcFile);

                SaveFileImage();
            }
        }

        public void Delete(string filePath)
        {
            var fileNode = _walker.GetNodeByPath(Root, filePath);
            if (fileNode != null)
            {
                var parentDirectory = fileNode.Parent as IDirectory;
                if (parentDirectory != null)
                {
                    parentDirectory.RemoveChild(fileNode);
                    _logger.DebugFormat("Deleted file: {0}", filePath);
                }
            }

            SaveFileImage();
        }

        public void Mkdir(string directoryPath)
        {
            var parentDirectory = _walker.GetNodeByPath(Root, directoryPath, true) as IDirectory;
            if (parentDirectory != null)
            {
                string parentDirectoryPath = parentDirectory.FullPath;
                int startingComponentIndex = FileSystemPath.GetComponents(parentDirectoryPath).Length;

                var pathComponents = FileSystemPath.GetComponents(directoryPath);
                for (int componentIndex = startingComponentIndex; componentIndex < pathComponents.Length; componentIndex++)
                {
                    var newDirectory = new Directory { Name = pathComponents[componentIndex] };
                    parentDirectory.AddChild(newDirectory);
                    parentDirectory = newDirectory;
                }
            }
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
