using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DomainModel.Abstracts;
using WebServices.Interfaces;

namespace WebServices.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public string Authenticate(string strCredentials)
        {
            return JsonR.JsonSerializer(new Guid());
        }


        public void Logout(string strCredentials)
        {
            // logout
        }
    }
}
