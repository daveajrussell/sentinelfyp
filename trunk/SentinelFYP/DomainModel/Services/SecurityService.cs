using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Services;
using DomainModel.Interfaces.Repositories;
using DomainModel.SecurityModels;
using DomainModel.Models.AuditModels;
using DomainModel.Security;
using DomainModel.Models.SecurityModels;
using System.Xml.Linq;

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

        public IEnumerable<User> GetUsers(User oUser)
        {
            return _securityRepository.GetUsers(oUser);
        }

        public IEnumerable<Role> GetRolesForUser(User oUser)
        {
            return _securityRepository.GetRolesForUser(oUser);
        }


        public bool ResetPassword(Guid oUserKey, string strPassword)
        {
            string strSalt;
            string strHash;

            SaltedHashGenerator.CreateSaltAndHashFromPassword(strPassword, out strSalt, out strHash);

            return _securityRepository.ResetPassword(oUserKey, strSalt, strHash);
        }

        public Vehicle GetUserVehicle(Guid oUserKey)
        {
            return _securityRepository.GetUserVehicle(oUserKey);
        }


        public Guid CreateUser(string strUsername, string strRoles, string strFirstName, string strLastName, string strNumber, string strEmail)
        {
            string strSalt;
            string strHash;
            string strRolesXML;

            SaltedHashGenerator.CreateSaltAndHashFromPassword("password", out strSalt, out strHash);

            strRolesXML = new XDocument(new XElement("ROLES", from key in strRoles.Split(',')
                                                              select new XElement("ROLE", new XAttribute("KEY", key)))).ToString();

            return _securityRepository.CreateUser(strUsername, State.User.UserCompanyKey, strRolesXML, strFirstName, strLastName, strNumber, strEmail, strSalt, strHash);
        }
    }
}
