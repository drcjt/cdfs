using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.WebApp.Models
{
    public class FileStatus
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool IsFile { get; set; }
    }
}
