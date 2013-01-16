using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Services;
using DomainModel.Interfaces.Repositories;
using DomainModel.SecurityModels;
using DomainModel.Models.AuditModels;

namespace DomainModel.Services
{
    public class SecurityService : ISecurityService
    {
        private ISecurityRepository _securityRepository;

        public SecurityService(ISecurityRepository securityRepository)
        {
            if (securityRepository == null)
                throw new ArgumentNullException("security repository");

            _securityRepository = securityRepository;
        }

        public void LogIn(string strUsername, string strPassword)
        {
            if (string.IsNullOrEmpty(strUsername))
                throw new ArgumentNullException("Username");
            if (string.IsNullOrEmpty(strPassword))
                throw new ArgumentNullException("Password");

            User oUser = null;
            Session oSession = null;

            _securityRepository.LogIn(strUsername, strPassword, out oUser, out oSession);

            BeginSession(oUser, oSession);
        }

        private void BeginSession(User oUser, Session oSession)
        {
            if (oSession != null && oUser != null)
            {
                State.Session = oSession;
                State.User = oUser;
            }
        }

        public void Logout()
        {
            if (State.Session.SessionID <= 0)
                throw new ArgumentOutOfRangeException("Session ID");

            _securityRepository.Logout(State.Session.SessionID);
        }

    }
}
