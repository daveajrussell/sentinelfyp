using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.SecurityModels;

namespace DomainModel.Interfaces.Services
{
    public interface ISecurityService
    {
        User LogIn(string strUsername, string strPassword, string strUserAgent, string strIPAddress);
        void Logout(int iSessionID);
    }
}
