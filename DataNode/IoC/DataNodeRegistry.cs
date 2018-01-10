using DataNode.ProtocolWrappers;
using DataNode.Services;
using Protocols;
using StructureMap;
using StructureMap.Pipeline;

namespace DataNode.IoC
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
