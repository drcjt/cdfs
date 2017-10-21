using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.WebApp.Models
{
    public class NameNodeStatus
    {
        public DateTime Started { get; set; }
        public int LiveNodes { get; set; }
        public int DeadNodes { get; set; }
    }
}
