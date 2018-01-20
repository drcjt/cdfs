using System;
using System.Collections.Generic;
using System.Text;

namespace DFSClient
{
    public interface IOptionParser
    {
        CommonSubOptions ParseOptions(string[] args);
    }
}
