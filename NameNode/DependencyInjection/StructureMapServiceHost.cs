using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.DependencyInjection
{
    public class StructureMapServiceHost : ServiceHost
    {
        /// <summary>
        /// Create a service host that can use structuremap for object resolution
        /// including the service implementation class
        /// </summary>
        /// <param name="container">structuremap container</param>
        /// <param name="serviceType">type of service class to host</param>
        /// <param name="baseAddresses">base addresses to host the service on</param>
        public StructureMapServiceHost(IContainer container, Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
        {
            // Validate the injected dependencies
            if (container == null) throw new ArgumentNullException("container");

            // Setup the service behaviours
            foreach (var contract in ImplementedContracts.Values)
            {
                contract.ContractBehaviors.Add(new StructureMapInstanceProvider(container, serviceType));
            }
        }
    }
}
