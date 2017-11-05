using log4net;
using NameNode.FileSystem;
using NameNode.Interfaces;
using NameNode.Service;
using NameNode.Status;
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

            For<IDataNodeRepository>().Use<DataNodeRepository>().Singleton();

            For<IClientProtocol>().Use<ClientProtocol>();
            For<IDataNodeProtocol>().Use<DataNodeProtocol>();

            For<INameNodeStatus>().Use<NameNodeStatus>().Singleton();
            For<IDataNodesStatus>().Use<DataNodesStatus>();

            For<IDateTimeProvider>().Use<DateTimeProvider>();

            For<IFileSystem>().Use<NameNode.FileSystem.FileSystem>().Singleton();
        }
    }
}
