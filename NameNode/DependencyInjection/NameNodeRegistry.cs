using log4net;
using NameNode.Interfaces;
using Protocols;
using StructureMap;

namespace NameNode.DependencyInjection
{
    public class NameNodeRegistry : Registry
    {
        public NameNodeRegistry()
        {
            For<ILog>().Use(c => LogManager.GetLogger(c.ParentType)).AlwaysUnique();
            For<IClientProtocol>().Use<ClientProtocol>();
            ForConcreteType<DataNodeProtocol>().Configure.Singleton();
            ForConcreteType<NameNodeService>().Configure.Singleton();

            For<IDataNodeProtocol>().Use(c => c.GetInstance<DataNodeProtocol>());
            For<IDataNodeProtocolManagement>().Use(c => c.GetInstance<DataNodeProtocol>());
            For<INameNodeServiceManagement>().Use(c => c.GetInstance<NameNodeService>());
        }
    }
}
