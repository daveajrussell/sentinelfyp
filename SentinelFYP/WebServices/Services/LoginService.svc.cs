using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DomainModel.Abstracts;

namespace WebServices.Services
{
    public class LoginService : ILoginService
    {
        public string Authenticate(string strCredentials)
        {
            return JsonR.JsonSerializer(new Guid());
        }
    }
}
