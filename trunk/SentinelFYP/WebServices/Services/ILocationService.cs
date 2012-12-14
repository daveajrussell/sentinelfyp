using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WebServices.Services
{
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/PostGISData", RequestFormat = WebMessageFormat.Json)]
        void PostGISData(string strGISObject);
    }
}
