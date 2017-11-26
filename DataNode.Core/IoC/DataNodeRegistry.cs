using DataNode.Core.ProtocolWrappers;
using DataNode.Core.Services;
using Protocols.Core;
using StructureMap;
using StructureMap.Pipeline;

namespace DataNode.Core.IoC
{
    public class DataNodeRegistry : Registry
    {
        public DataNodeRegistry()
        {
            For<IDataNodeApplication>().LifecycleIs(Lifecycles.Container).Use<DataNodeService>();
            For<IDataNodeProtocol>().LifecycleIs(Lifecycles.Container).Use<DataNodeProtocol>();
        }
    }
}
