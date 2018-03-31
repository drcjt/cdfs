using DataNode.ProtocolWrappers;
using DataNode.Services;
using RestSharp;
using StructureMap;
using StructureMap.Pipeline;
using Protocols;

namespace DataNode.IoC
{
    public class DataNodeRegistry : Registry
    {
        public DataNodeRegistry()
        {
            For<IDataNodeApplication>().LifecycleIs(Lifecycles.Container).Use<DataNodeService>();
            For<IRestDataNodeProtocol>().LifecycleIs(Lifecycles.Container).Use<DataNodeProtocol>();
            For<IRestClient>().Use<RestClient>();

            For<IDataTransferProtocol>().Use<DataTransferProtocol>();
        }
    }
}
