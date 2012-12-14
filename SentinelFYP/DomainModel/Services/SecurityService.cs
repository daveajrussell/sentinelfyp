using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Services;
using DomainModel.Interfaces.Repositories;
using DomainModel.SecurityModels;

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

        public User LogIn(string strUsername, string strPassword, string strUserAgent, string strIPAddress)
        {
            return _securityRepository.LogIn(strUsername, strPassword, strUserAgent, strIPAddress);
        }

        public void Logout(int iSessionID)
        {
            _securityRepository.Logout(iSessionID);
        }
    }
}
