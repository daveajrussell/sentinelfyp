using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.SecurityModels;

namespace DomainModel.Interfaces.Services
{
    public interface ISecurityService
    {
        void LogIn(string strUsername, string strPassword);
        void Logout();
    }
}
