using DomainModel.Interfaces.Services;
using Moq;
using Sentinel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Xunit;

namespace Sentinel.Tests.InfrastructureTests
{
    public class SentinelRoleProviderTests 
    {
        private Mock<IRoleService> _service;

        public SentinelRoleProviderTests()
        {
            _service = new Mock<IRoleService>();
        }

        [Fact]
        public void TestAddUsersToRoles()
        {
            var service = new SentinelRoleProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => service.AddUsersToRoles(null, null));
        }

        [Fact]
        public void TestCreateRole()
        {
            var service = new SentinelRoleProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => service.CreateRole(""));
        }

        [Fact]
        public void TestDeleteRole()
        {
            var service = new SentinelRoleProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => service.DeleteRole("", false));
        }

        [Fact]
        public void TestFindUsersInRole()
        {
            var service = new SentinelRoleProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => service.FindUsersInRole("", ""));
        }

        [Fact]
        public void TestGetAllRoles()
        {
            var service = new SentinelRoleProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => service.GetAllRoles());
        }

        [Fact]
        public void TestGetRolesForUser()
        {
            var service = new SentinelRoleProvider(_service.Object);
            Assert.DoesNotThrow(() => service.GetRolesForUser(""));
        }

        [Fact]
        public void TestGetUsersInRole()
        {
            var service = new SentinelRoleProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => service.GetUsersInRole(""));
        }

        [Fact]
        public void TestIsUserInRole()
        {
            var service = new SentinelRoleProvider(_service.Object);
            Assert.DoesNotThrow(() => service.IsUserInRole("", ""));
        }

        [Fact]
        public void TestRemoveUsersFromRoles()
        {
            var service = new SentinelRoleProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => service.RemoveUsersFromRoles(null, null));
        }

        [Fact]
        public void TestRoleExists()
        {
            var service = new SentinelRoleProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => service.RoleExists(""));
        }
    }
}
