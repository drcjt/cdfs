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

        public FileSystem(ILog logger, IFileSystemSerializer fileSystemSerializer)
        {
            _logger = logger;
            _fileSystemSerializer = fileSystemSerializer;
        }

        private INodeDirectory _root = null;

        public INodeDirectory Root {
            get
            {
                if (_root == null)
                {
                    if (!File.Exists("FSImage"))
                    {
                        _root = new NodeDirectory();
                    }
                    else
                    {
                        var lines = File.ReadLines("FSImage");
                        _root = _fileSystemSerializer.Deserialize(lines.GetEnumerator());
                        _logger.Debug("Loaded File Image");
                    }
                }

                return _root;
            }
        }

        private void SaveFileImage()
        {
            _logger.Debug("Saving File Image");
            File.WriteAllText("FSImage", _fileSystemSerializer.Serialize(Root));
        }

        public void Create(string srcFile, string filePath)
        {
            var directory = Root.GetINodeForPath(filePath) as INodeDirectory;
            if (directory != null)
            {
                var fileNode = new NodeFile
                {
                    Name = Path.GetFileName(srcFile)
                };
                directory.AddChild(fileNode);

                _logger.DebugFormat("Created new INode: {0}{1}{2}", filePath, Path.DirectorySeparatorChar, srcFile);

                SaveFileImage();
            }
        }

        public void Delete(string filePath)
        {
            var inode = Root.GetINodeForPath(filePath, false);
            if (inode != null)
            {
                var parentDirectory = inode.Parent as INodeDirectory;
                if (parentDirectory != null)
                {
                    parentDirectory.RemoveChild(inode);
                    _logger.DebugFormat("Deleted file: {0}", filePath);
                }
            }

            SaveFileImage();
        }

        public void Mkdir(string directoryPath)
        {
            Root.CreateINodesInPath(directoryPath);
        }

        public IList<INode> GetListing(string filePath)
        {
            var directory = Root.GetINodeForPath(filePath) as INodeDirectory;

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
