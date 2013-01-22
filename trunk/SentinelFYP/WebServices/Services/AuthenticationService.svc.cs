﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DomainModel.Abstracts;
using DomainModel.Interfaces.Repositories;
using DomainModel.Models.AuditModels;
using DomainModel.SecurityModels;
using WebServices.DataContracts;
using WebServices.Interfaces;

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

        public string Authenticate(string strCredentials)
        {
            User oUser;
            Session oSession;

            CredentialsDataContract oCredentialsContract = JsonR.JsonDeserializer<CredentialsDataContract>(strCredentials);
            _securityRepostiory.LogIn(oCredentialsContract.strUsername, oCredentialsContract.strPassword, out oUser, out oSession);

            var oUserSession = new { UserKey = oUser.UserKey, SessionID = oSession.SessionID };

            return JsonR.JsonSerializer(oUserSession);
        }


        public void Logout(string strSessionInformation)
        {
            SessionDataContract oSessionContract = JsonR.JsonDeserializer<SessionDataContract>(strSessionInformation);
            _securityRepostiory.Logout(oSessionContract.iSessionID);
        }
    }
}
