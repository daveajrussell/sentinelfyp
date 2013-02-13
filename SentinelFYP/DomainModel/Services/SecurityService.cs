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

        public void LogIn(string strUsername, string strPassword, out User oUser, out Session oSession)
        {
            if (string.IsNullOrEmpty(strUsername))
                throw new ArgumentNullException("Username");
            if (string.IsNullOrEmpty(strPassword))
                throw new ArgumentNullException("Password");

            _securityRepository.LogIn(strUsername, strPassword, out oUser, out oSession);
        }

        public void Logout()
        {
            if (State.Session != null && State.Session.SessionID <= 0)
                _securityRepository.Logout(State.User.UserKey, State.Session.SessionID);
        }

        public User GetUserByUserKey(Guid oUserKey)
        {
            return _securityRepository.GetUserByUserKey(oUserKey);
        }
    }
}
