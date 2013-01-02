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
        [WebInvoke(Method = "Post", UriTemplate = "/SubmitGeoTaggedDelivery", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void SubmitGeoTaggedDelivery(string strGeoTaggedDeliveryObject);

        [OperationContract]
        [WebInvoke(Method = "Get", UriTemplate = "/GetDeliveryInformation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetDeliveryInformation(string strItemID);
    }
}
