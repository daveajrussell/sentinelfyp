using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AuditModels;
using DomainModel.SecurityModels;

namespace DomainModel.Interfaces.Services
{
    public interface ISecurityService
    {
        void LogIn(string strUsername, string strPassword, out User oUser, out Session oSession);
        void Logout();
        User GetUserByUserKey(Guid oUserKey);
        IEnumerable<User> GetUsers();
        bool ResetPassword(Guid oUserKey, string strPassword);
    }
}
