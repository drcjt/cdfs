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
        private readonly IFileSystemSerializer _fileSystemSerializer;

        public FileSystem(IFileSystemSerializer fileSystemSerializer)
        {
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
                    }
                }

                return _root;
            }
        }

        private void SaveFileImage()
        {
            File.WriteAllText("FSImage", _fileSystemSerializer.Serialize(Root));
        }

        public void Create(string srcFile, string filePath)
        {
            var directory = Root.GetINodeForFullDirectoryPath(filePath);
            var fileNode = new NodeFile
            {
                Name = Path.GetFileName(srcFile)
            };
            directory.AddChild(fileNode);

            SaveFileImage();
        }

        public IList<INode> GetListing(string filePath)
        {
            var directory = Root.GetINodeForFullDirectoryPath(filePath);

            var results = new List<INode>();
            foreach (var inode in directory)
            {
                results.Add(inode);
            }
            return results;
        }
    }
}
