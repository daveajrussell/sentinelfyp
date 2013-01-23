using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AuditModels;
using DomainModel.SecurityModels;

namespace DomainModel.Interfaces.Repositories
{
    public interface ISecurityRepository
    {
        //string AuthenticateDriver(string strUsername, string strPassword);
        void LogIn(string strUsername, string strPassword, out User oUser, out Session oSession);
        void Logout(Guid oUserKey, int iSessionID);
    }
}
