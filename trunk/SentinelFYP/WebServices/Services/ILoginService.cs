using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebServices.Services
{
    [ServiceContract]
    public interface ILoginService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Authenticate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string Authenticate(string strCredentials);
    }
}
