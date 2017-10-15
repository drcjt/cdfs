using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNode
{
    class DataNodeOptions
    {
        [Option('n', "namenodeuri", Required = false, DefaultValue = "net.tcp://localhost:5150/DataNodeProtocol")]
        public string NameNodeUri { get; set; }
    }
}
