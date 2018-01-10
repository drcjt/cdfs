using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols
{
    public class CdfsFileStatus
    {
        public string FilePath { get; set; }
        public long Length { get; set; }
        public Boolean IsDirectory { get; set; }

        public override string ToString()
        {
            return (IsDirectory ? "<DIR>         " : string.Format("{0,14}", Length)) + " " + FilePath;
        }
    }
}
