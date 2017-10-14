using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Protocols
{
    [DataContract]
    public class CdfsFileStatus
    {
        [DataMember]
        string FilePath { get; set; }
        
        [DataMember]
        long Length { get; set; }

        [DataMember]
        Boolean IsDirectory { get; set; }

        public override string ToString()
        {
            return IsDirectory ? "<DIR>         " : Length + " " + FilePath;
        }
    }
}
