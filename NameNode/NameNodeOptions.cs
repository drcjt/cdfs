using CommandLine;

namespace NameNode
{
    class NameNodeOptions
    {
        [Option('p', "port", Required = false, DefaultValue = 5150)]
        public int Port { get; set; }
    }
}
