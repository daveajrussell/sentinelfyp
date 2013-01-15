using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Interfaces.Services
{
    public interface IRoleService
    {
        string[] GetRolesForUser(string strUsername);
        bool IsUserInRole(string strUsername, string strRoleName);
    }
}
