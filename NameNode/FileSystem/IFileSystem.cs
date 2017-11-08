using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public interface IFileSystem
    {
        IDirectory Root { get; }
        void SaveFileImage();

        void Create(string srcFile, string directoryPath);
        void Delete(string filePath);
        void Mkdir(string directoryPath);
        IList<INode> GetListing(string directoryPath);
    }
}
