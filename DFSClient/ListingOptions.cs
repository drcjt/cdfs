using CommandLine;
using DFSClient.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFSClient
{
    public abstract class CommonSubOptions
    {
        [Option('n', "namenodeuri", Required = false, DefaultValue = "http://localhost:5150")]
        public string NameNodeUri { get; set; }

        public abstract ICommand Command { get; }
    }

    public class ListingSubOptions : CommonSubOptions
    {
        [ValueList(typeof(List<string>), MaximumElements = 1)]
        public IList<string> FilePath { get; set; }

        public override ICommand Command => new ListingCommand { FilePath = FilePath.Count > 0 ? FilePath[0] : "" };
    }

    public class PutSubOptions : CommonSubOptions
    {
        [ValueList(typeof(List<string>), MaximumElements = 2)]
        public IList<string> PutValues { get; set; }

        public override ICommand Command => new PutCommand { SrcFile = PutValues[0], FilePath = PutValues[1] };
    }

    public class DeleteSubOptions : CommonSubOptions
    {
        [ValueList(typeof(List<string>), MaximumElements = 1)]
        public IList<string> FilePath { get; set; }

        public override ICommand Command => new DeleteCommand { FilePath = FilePath[0] };
    }

    public class MkdirSubOptions : CommonSubOptions
    {
        [ValueList(typeof(List<string>), MaximumElements = 1)]
        public IList<string> DirectoryPath { get; set; }

        public override ICommand Command => new MkdirCommand { DirectoryPath = DirectoryPath[0] };
    }
}
