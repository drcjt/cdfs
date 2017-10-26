using CommandLine;
using System.Collections.Generic;

namespace DFSClient
{
    public abstract class CommonSubOptions
    {
        [Option('n', "namenodeuri", Required = false, DefaultValue = "net.tcp://localhost:5150/ClientProtocol")]
        public string NameNodeUri { get; set; }
    }

    public class ListingSubOptions : CommonSubOptions
    {        
        [ValueList(typeof(List<string>), MaximumElements = 1)]
        public IList<string> FilePath { get; set; }
    }

    public class PutSubOptions : CommonSubOptions
    {
        [ValueList(typeof(List<string>), MaximumElements = 1)]
        public IList<string> FileName { get; set; }

        [ValueList(typeof(List<string>), MaximumElements = 1)]
        public IList<string> FilePath { get; set; }
    }

    public class GetSubOptions : CommonSubOptions
    {
        [ValueList(typeof(List<string>), MaximumElements = 1)]
        public IList<string> FileName { get; set; }
    }
}
