using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Services;
using Moq;
using Xunit;

namespace DomainModel.Test.Tests
{
    public class RoleServiceTest
    {
        private Mock<IRoleRepository> _repository;
        private string[] _roles = { "Administrator", "Driver", "Auditor" };
        private string[] _rolesForTestUserOne = { "Administrator" };
        private string[] _rolesForTestUserTwo = { "Administrator", "Auditor" };
        private string[] _rolesForTestUserThree = { "Driver" };

        public static List<TestUser> _users;
        
        public RoleServiceTest()
        {
            _repository = new Mock<IRoleRepository>();

            _users = new List<TestUser>();
            _users.Add(new TestUser() { UserName = "Test User One", Roles = _rolesForTestUserOne });
            _users.Add(new TestUser() { UserName = "Test User Two", Roles = _rolesForTestUserTwo });
            _users.Add(new TestUser() { UserName = "Test User Three", Roles = _rolesForTestUserThree });

            _repository.Setup(m => m.IsUserInRole(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((username, role) => TestHelper.IsUserInRole(username, role));
            
            _repository.Setup(m => m.GetRolesForUser(It.IsAny<string>()))
                .Returns<string>((username) => TestHelper.GetRolesForUser(username));
        }

        [Fact]
        public void TestConstructor()
        {
            RoleService target = null;
            Assert.DoesNotThrow(() => target = new RoleService(_repository.Object));
            Assert.NotNull(target);
            Assert.IsAssignableFrom<RoleService>(target);
        }

        [Fact]
        public void TestConstructorShouldThrow()
        {
            RoleService target = null;
            Assert.Throws<ArgumentNullException>(() => target = new RoleService(null));
            Assert.Null(target);
        }

        [Fact]
        public void TestGetRolesForUser()
        {
            var target = new RoleService(_repository.Object);

            foreach (var user in _users)
            {
                var data = target.GetRolesForUser(user.UserName);
                Assert.NotEmpty(data);

                foreach (var item in data)
                {
                    Assert.True(_roles.Contains(item));
                }
            }
        }

        [Fact]
        public void IsUserInRole()
        {
            var target = new RoleService(_repository.Object);

            foreach (var user in _users)
            {
                switch (user.UserName)
                {
                    case "Test User One":
                        Assert.True(target.IsUserInRole(user.UserName, "Administrator"));
                        Assert.False(target.IsUserInRole(user.UserName, "Auditor"));
                        break;
                    case "Test User Two":
                        Assert.True(target.IsUserInRole(user.UserName, "Auditor"));
                        Assert.False(target.IsUserInRole(user.UserName, "Driver"));
                        break;
                    case "Test User Three":
                        Assert.True(target.IsUserInRole(user.UserName, "Driver"));
                        Assert.False(target.IsUserInRole(user.UserName, "Administrator"));
                        break;
                }
            }
        }

        public class TestUser
        {
            public string UserName { get; set; }
            public string[] Roles {get;set;}
        }

        public static class TestHelper
        {
            public static bool IsUserInRole(string username, string role)
            {
                var _user = (from user in _users
                            where user.UserName == username
                            select user).First();

                return _user.Roles.Contains(role);
            }

            public static string[] GetRolesForUser(string username)
            {
                var _user = (from user in _users
                             where user.UserName == username
                             select user).First();

                return _user.Roles;
            }
        }
    }
}
