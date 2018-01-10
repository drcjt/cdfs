using System.Collections.Generic;

namespace NameNode.FileSystem.Interfaces
{
    public interface IFileSystemImageFile
    {
        bool FileSystemImageExists();
        IEnumerable<string> ReadFileSystemImageLines();
        void WriteFileSystemImage(IEnumerable<string> lines);
    }
}
