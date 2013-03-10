using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace Sentinel.Services
{
    [ServiceContract]
    public interface INotifierService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GISNotify", RequestFormat = WebMessageFormat.Json)]
        void GISNotify(string message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/DeliveryNotify", RequestFormat = WebMessageFormat.Json)]
        void DeliveryNotify(string message);
    }
}
