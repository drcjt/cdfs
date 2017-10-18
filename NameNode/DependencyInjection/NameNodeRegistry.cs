using log4net;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.DependencyInjection
{
    public class NameNodeRegistry : Registry
    {
        public NameNodeRegistry()
        {
            For<ILog>().Use(c => LogManager.GetLogger(c.ParentType)).AlwaysUnique();
        }
    }
}
