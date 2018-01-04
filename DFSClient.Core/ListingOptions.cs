using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFSClient.Core
{
    public abstract class CommonSubOptions
    {
        [Option('n', "namenodeuri", Required = false, DefaultValue = "http://localhost:5150")]
        public string NameNodeUri { get; set; }
    }

    public class ListingSubOptions : CommonSubOptions
    {
        [ValueList(typeof(List<string>), MaximumElements = 1)]
        public IList<string> FilePath { get; set; }
    }

    public class PutSubOptions : CommonSubOptions
    {
        [ValueList(typeof(List<string>), MaximumElements = 2)]
        public IList<string> PutValues { get; set; }
    }

    public class DeleteSubOptions : CommonSubOptions
    {
        [ValueList(typeof(List<string>), MaximumElements = 1)]
        public IList<string> FilePath { get; set; }
    }

    public class MkdirSubOptions : CommonSubOptions
    {
        [ValueList(typeof(List<string>), MaximumElements = 1)]
        public IList<string> DirectoryPath { get; set; }
    }
}
