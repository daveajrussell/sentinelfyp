using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebServices.Interfaces
{
    [ServiceContract]
    public interface IDeliveryService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GeoTagDelivery", RequestFormat = WebMessageFormat.Json)]
        void GeoTagDelivery(string strGeoTaggedDeliveryObject);
    }
}
