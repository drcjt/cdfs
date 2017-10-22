using StructureMap;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace NameNode.DependencyInjection
{
    /// <summary>
    /// A provider for creating service types using a structuremap container
    /// </summary>
    public class StructureMapInstanceProvider : IInstanceProvider, IContractBehavior
    {
        private readonly Type _serviceType;
        private readonly IContainer _container;

        public StructureMapInstanceProvider(IContainer container, Type serviceType)
        {
            // Validate the injected dependencies
            if (container == null) throw new ArgumentNullException("container");
            if (serviceType == null) throw new ArgumentNullException("serviceType");

            _serviceType = serviceType;
            _container = container;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            // Use the structuremap container for getting instance of the service type
            return _container.GetInstance(_serviceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            // Dispose of the instance if possible
            if (instance is IDisposable)
            {
                (instance as IDisposable).Dispose();
            }
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            // TODO: should this do anything?
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            // Set the instance provide to be this object itself
            dispatchRuntime.InstanceProvider = this;
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // TODO: should this do anything?
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            // TODO: should this do anything?
        }
    }
}
