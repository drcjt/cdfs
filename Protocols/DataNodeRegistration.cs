using System.Runtime.Serialization;

namespace Protocols
{
    [DataContract]
    public class DataNodeRegistration
    {
        [DataMember]
        public string IPAddress { get; set; }
        
        [DataMember]
        public string HostName { get; set; }
    }
}
