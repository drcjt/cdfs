using System.Collections.Generic;

namespace NameNode.FileSystem.Interfaces
{
    public interface IFileSystem
    {
        IDirectory Root { get; }

        void Create(string srcFile, string directoryPath);
        void Delete(string filePath);
        void Mkdir(string directoryPath);
        ICollection<INode> GetListing(string directoryPath);

        IFile GetFile(string srcFile);
    }
}
