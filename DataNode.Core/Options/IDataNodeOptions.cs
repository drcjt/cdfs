using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataNode.Options
{
    public interface IDataNodeOptions
    {
        string NameNodeUri { get; set; }
    }
}
