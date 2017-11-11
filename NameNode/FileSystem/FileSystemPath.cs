using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.FileSystem
{
    public static class FileSystemPath
    {
        public static string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        public static string[] GetComponents(string path)
        {
            return Normalize(path).Split(Path.DirectorySeparatorChar);
        }

        public static string Normalize(string path)
        {
            return path.TrimStart(Path.DirectorySeparatorChar);
        }
    }
}
