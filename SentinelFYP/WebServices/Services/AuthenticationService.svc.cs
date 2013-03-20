using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DomainModel.Abstracts;
using DomainModel.Interfaces.Repositories;
using DomainModel.Models.AuditModels;
using DomainModel.SecurityModels;
using WebServices.DataContracts;
using WebServices.Interfaces;
using SentinelExceptionManagement;
using System.IO;

namespace WebServices.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private ISecurityRepository _securityRepostiory;

        public AuthenticationService(ISecurityRepository securityRepository)
        {
            if (securityRepository == null)
                throw new ArgumentNullException("security repository");

            _securityRepostiory = securityRepository;
        }

        public Stream Authenticate(CredentialsDataContract oCredentialsContract)
        {
            User oUser;
            Session oSession;

            try
            {
                //CredentialsDataContract oCredentialsContract = JsonR.JsonDeserializer<CredentialsDataContract>(strCredentials);
                _securityRepostiory.LogIn(oCredentialsContract.strUsername, oCredentialsContract.strPassword, out oUser, out oSession);

                if (oUser == null && oSession == null)
                {
                    throw new Exception("Username: " + oCredentialsContract.strUsername + " Password: " + oCredentialsContract.strPassword); //WebFaultException(System.Net.HttpStatusCode.Unauthorized);
                }
                else
                {
                    var oUserSession = new { UserKey = oUser.UserKey, SessionID = oSession.SessionID };
                    //return JsonR.JsonSerializer(oUserSession);
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                    return new MemoryStream(Encoding.UTF8.GetBytes(JsonR.JsonSerializer(oUserSession)));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                throw new WebFaultException(System.Net.HttpStatusCode.Unauthorized);
            }
        }


        public void Logout(SessionDataContract oSessionContract)
        {
            //SessionDataContract oSessionContract = JsonR.JsonDeserializer<SessionDataContract>(strSessionInformation);
            var oUserKey = new Guid(oSessionContract.oUserIdentification);
            _securityRepostiory.Logout(oUserKey, oSessionContract.iSessionID);
        }
    }
}
