using System.Collections.Generic;

namespace NameNode.FileSystem.Interfaces
{
    public interface IFileSystemSerializer
    {
        IEnumerable<string> Serialize(IDirectory root);
        IDirectory Deserialize(IEnumerable<string> fileImageLines);
    }
}
