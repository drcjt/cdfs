using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameNode.Models
{
    public class FileStatusModel
    {
        public IList<FileStatus> Files { get; set; }
    }

    public class FileStatus
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool IsFile { get; set; }
    }
}
