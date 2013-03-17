using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AuditModels;
using DomainModel.SecurityModels;
using DomainModel.Models.SecurityModels;

namespace DomainModel.Interfaces.Repositories
{
    public interface ISecurityRepository
    {
        void LogIn(string strUsername, string strPassword, out User oUser, out Session oSession);
        void Logout(Guid oUserKey, int iSessionID);
        User GetUserByUserKey(Guid oUserKey);
        Vehicle GetUserVehicle(Guid oUserKey);  
        IEnumerable<User> GetUsers(User oUser);
        IEnumerable<Role> GetRolesForUser(User oUser);
        bool ResetPassword(Guid oUserKey, string strSalt, string strHash);
        Guid CreateUser(string strUsername, Guid oCompanyKey, string strRoles, string strFirstName, string strLastName, string strNumber, string strEmail, string strSalt, string strHash);
    }
}
