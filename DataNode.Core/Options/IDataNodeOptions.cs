using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataNode.Core.Options
{
    public interface IDataNodeOptions
    {
        string NameNodeUri { get; set; }
    }
}
