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
        public string FilePath { get; set; }
        
        [DataMember]
        public long Length { get; set; }

        [DataMember]
        public Boolean IsDirectory { get; set; }

        public override string ToString()
        {
            return (IsDirectory ? "<DIR>         " : string.Format("{0,14}", Length)) + " " + FilePath;
        }
    }
}
