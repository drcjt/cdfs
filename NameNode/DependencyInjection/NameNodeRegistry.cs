using log4net;
using NameNode.Interfaces;
using NameNode.Service;
using Protocols;
using StructureMap;

namespace NameNode.DependencyInjection
{
    public class NameNodeRegistry : Registry
    {
        public NameNodeRegistry()
        {
            // Use log4net for logging
            For<ILog>().Use(c => LogManager.GetLogger(c.ParentType)).AlwaysUnique();

            // We must only have one instance of the main WCF service implementation class
            // which is also used for the management interface for the service
            ForSingletonOf<NameNodeService>().Use<NameNodeService>();
            For<INameNodeServiceManagement>().Use(c => c.GetInstance<NameNodeService>());

            // We must only have one instance of the main client protocol
            ForSingletonOf<ClientProtocol>().Use<ClientProtocol>();
            For<IClientProtocol>().Use(c => c.GetInstance<ClientProtocol>());

            // We must only have one instance of the main data node protocol
            // which is also used for the management interface for the data nodes
            ForSingletonOf<DataNodeProtocol>().Use<DataNodeProtocol>();
            For<IDataNodeProtocol>().Use(c => c.GetInstance<DataNodeProtocol>());
            For<IDataNodeProtocolManagement>().Use(c => c.GetInstance<DataNodeProtocol>());
        }
    }
}
