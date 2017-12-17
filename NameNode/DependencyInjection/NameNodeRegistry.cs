using log4net;
using NameNode.FileSystem;
using NameNode.FileSystem.Interfaces;
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

            For<IFileSystemImageFile>().Use<FileSystemImageFile>().Ctor<string>("imageFileName").Is("FSImage").Singleton();

            For<IFileSystem>().Use<FileSystem.FileSystem>().Singleton();
            For<IFileSystemSerializer>().Use<FileSystemSerializer>();
            For<IFileSystemReaderWriter>().Use<FileSystemReaderWriter>();

            For<INodeWalker>().Use<NodeWalker>();
        }
    }
}
