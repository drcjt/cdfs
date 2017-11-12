using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public interface IFileSystemReaderWriter
    {
        bool FileSystemImageExists(string path);
        void WriteFileSystemImage(string path, string contents);
        IEnumerable<string> ReadFileSystemImageLines(string path);
    }
}
