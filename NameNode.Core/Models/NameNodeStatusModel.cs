using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameNode.Core.Models
{
    public class NameNodeStatusModel
    {
        public DateTime Started { get; set; }
        public int LiveNodes { get; set; }
        public int DeadNodes { get; set; }
    }
}
