using log4net;
using NameNode.Core.Services;
using Protocols.Core;
using StructureMap;

namespace NameNode.Core.IoC
{
    public class NameNodeRegistry : Registry
    {
        public NameNodeRegistry()
        {
            For<IDataNodeRepository>().Use<DataNodeRepository>().Singleton();
            For<IDataNodeProtocol>().Use<DataNodeProtocol>();
            For<IDateTimeProvider>().Use<DateTimeProvider>();
        }
    }
}