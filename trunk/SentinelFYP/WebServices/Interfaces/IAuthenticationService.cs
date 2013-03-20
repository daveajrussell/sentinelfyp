using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WebServices.DataContracts;

namespace WebServices.Interfaces
{
    [ServiceContract]
    public interface IAuthenticationService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Authenticate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream Authenticate(CredentialsDataContract Credentials);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Logout", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void Logout(SessionDataContract Credentials);
    }
}
