using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public class FileSystemReaderWriter : IFileSystemReaderWriter
    {
        public bool FileSystemImageExists(string path)
        {
            return System.IO.File.Exists(path);
        }

        public IEnumerable<string> ReadFileSystemImageLines(string path)
        {
            return System.IO.File.ReadLines(path);
        }

        public void WriteFileSystemImage(string path, string contents)
        {
            System.IO.File.WriteAllText(path, contents);
        }
    }
}
