using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DomainModel.Interfaces.Services;
using Ninject;

namespace Sentinel.Infrastructure
{
    public class SentinelRoleProvider : RoleProvider
    {
        [Inject]
        public IRoleService _roleService { get; set; }

        public SentinelRoleProvider(IRoleService roleService)
        {
            if (null == roleService)
                throw new ArgumentNullException("service");

            _roleService = roleService;
        }

        public override string[] GetRolesForUser(string username)
        {
            return _roleService.GetRolesForUser(username);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return _roleService.IsUserInRole(username, roleName);
        }

        /*NON IMPLEMENTED METHODS*/

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}