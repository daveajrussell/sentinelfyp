using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.SecurityModels;

namespace DomainModel.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        string[] GetRolesForUser(string strUsername);
        bool IsUserInRole(string strUsername, string strRoleName);
    }
}
