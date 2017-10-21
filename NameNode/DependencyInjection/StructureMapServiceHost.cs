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
        public StructureMapServiceHost(IContainer container, Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            foreach (var contract in ImplementedContracts.Values)
            {
                contract.ContractBehaviors.Add(new StructureMapInstanceProvider(container, serviceType));
            }
        }
    }
}
