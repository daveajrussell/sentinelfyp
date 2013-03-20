using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WebServices.DataContracts;

namespace WebServices.Interfaces
{
    [ServiceContract]
    public interface IDeliveryService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GeoTagDelivery", RequestFormat = WebMessageFormat.Json)]
        void GeoTagDelivery(GeotaggedAssetDataContract oGeotaggedAssetContract);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UnTagDelivery", RequestFormat = WebMessageFormat.Json)]
        void UnTagDelivery(string strAssetKey);
    }
}
