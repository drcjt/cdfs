using System.ServiceModel;
using System.ServiceModel.Web;

namespace Protocols
{
    [ServiceContract]
    public interface INameNodeManagement
    {
        [OperationContract]
        [WebGet]
        NameNodeStatus GetNameNodeStatus();
    }
}
