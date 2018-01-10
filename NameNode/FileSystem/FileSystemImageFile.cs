using NameNode.FileSystem.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace NameNode.FileSystem
{
    public class FileSystemImageFile : IFileSystemImageFile
    {
        private readonly string _imageFileName;
        public FileSystemImageFile(string imageFileName)
        {
            _imageFileName = imageFileName;
        }

        public bool FileSystemImageExists()
        {
            return System.IO.File.Exists(_imageFileName);
        }

        public IEnumerable<string> ReadFileSystemImageLines()
        {
            return System.IO.File.ReadLines(_imageFileName);
        }

        public void WriteFileSystemImage(IEnumerable<string> lines)
        {
            System.IO.File.WriteAllLines(_imageFileName, lines.ToArray<string>());
        }
    }
}
