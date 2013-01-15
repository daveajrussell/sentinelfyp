using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using DomainModel.SecurityModels;

namespace DomainModel.Services
{
    public class RoleService : IRoleService
    {
        private IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            if (roleRepository == null)
                throw new ArgumentNullException("Role Repository");

            _roleRepository = roleRepository;
        }

        public string[] GetRolesForUser(string strUsername)
        {
            return _roleRepository.GetRolesForUser(strUsername);
        }

        public bool IsUserInRole(string strUsername, string strRoleName)
        {
            return _roleRepository.IsUserInRole(strUsername, strRoleName);
        }
    }
}
