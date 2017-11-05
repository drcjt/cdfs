using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public interface IFileSystemSerializer
    {
        string Serialize(INodeDirectory root);
        INodeDirectory Deserialize(IEnumerator<string> fileImageLines);
    }
}
