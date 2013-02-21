using System;
using DomainModel.Interfaces.Repositories;
using DomainModel.Models.AuditModels;
using DomainModel.SecurityModels;
using DomainModel.Services;
using DomainModel.Test.TestHelpers;
using Moq;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainModel.Test.Tests
{
    [TestClass]
    public class SecurityServiceTest
    {
        private Mock<ISecurityRepository> _repository;
        private User _user = null;
        private Session _session = null;

        public SecurityServiceTest()
        {
            _repository = new Mock<ISecurityRepository>();

            _repository.Setup(m => m.LogIn(It.IsAny<string>(), It.IsAny<string>(), out _user, out _session))
                .Callback(() => SecurityTestHelper.MockLogin("Username", "Password", out _user, out _session));

            _repository.Setup(m => m.GetUserByUserKey(It.IsAny<Guid>()))
                .Returns<Guid>((userKey) => new User() { UserKey = userKey });
        }

        [Fact]
        public void InjectingNullIntoConstructorShouldThrow()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() => new SecurityService(null));
        }

        [Fact]
        public void InjectingInterfaceIntoConstructorShouldNotThrow()
        {
            Xunit.Assert.DoesNotThrow(() => new SecurityService(_repository.Object));
            
            var service = new SecurityService(_repository.Object);

            Xunit.Assert.NotNull(service);
            Xunit.Assert.IsAssignableFrom<SecurityService>(service);
        }

        [Fact]
        public void PassingNullUserNameToLogInShouldThrow()
        {
            var service = new SecurityService(_repository.Object);
            var user = new User();
            var session = new Session();
            Xunit.Assert.Throws<ArgumentNullException>(() => service.LogIn(null, "Password", out user, out session));
        }

        [Fact]
        public void PassingNullPasswordToLogInShouldThrow()
        {
            var service = new SecurityService(_repository.Object);
            var user = new User();
            var session = new Session();
            Xunit.Assert.Throws<ArgumentNullException>(() => service.LogIn("Username", null, out user, out session));
        }

        [Fact]
        [TestMethod]
        public void LogInShouldReturnUserObject()
        {
            var service = new SecurityService(_repository.Object);
            var user = new User();
            var session = new Session();

            Xunit.Assert.DoesNotThrow(() => service.LogIn("Username", "Password", out user, out session));
            Xunit.Assert.NotNull(_user);
        }

        [Fact]
        public void TestGetUserByUserKey()
        {
            var key = Guid.NewGuid();
            var service = new SecurityService(_repository.Object);

            var user = service.GetUserByUserKey(key);

            Xunit.Assert.NotNull(user);
            Xunit.Assert.IsAssignableFrom<User>(user);

            Xunit.Assert.Equal(key, user.UserKey);
        }
    }
}
