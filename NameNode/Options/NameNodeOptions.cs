using CommandLine;
using System;

namespace NameNode.Options
{
    class NameNodeOptions
    {
        // Port for the main NameNode WCF service to run on
        public int Port { get; set; }

        // The URI to run the NameNode health web app on
        public Uri HttpAddress { get; set; }
    }
}
