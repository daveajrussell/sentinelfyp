using System;
using DomainModel.Interfaces.Repositories;
using DomainModel.SecurityModels;
using DomainModel.Services;
using Moq;
using Xunit;

namespace DomainModel.Test.Tests
{
    public class SecurityServiceTest
    {
        private Mock<ISecurityRepository> _repository;

        public SecurityServiceTest()
        {
            _repository = new Mock<ISecurityRepository>();

            User _user = new User();
            Session _session = new Session();

            _repository.Setup(m => m.LogIn(It.IsAny<string>(), It.IsAny<string>(), out _user, out _session));
            //   .Returns(new User(Guid.NewGuid(), "Test User", "Test", "User", "Test@User.com", DateTime.Now, DateTime.Now, DateTime.Now));

            //_repository.Setup(m => m.Logout());
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
            Xunit.Assert.Throws<ArgumentNullException>(() => service.LogIn(null, "Password"));
        }

        [Fact]
        public void PassingNullPasswordToLogInShouldThrow()
        {
            var service = new SecurityService(_repository.Object);
            Xunit.Assert.Throws<ArgumentNullException>(() => service.LogIn("Username", null));
        }

        [Fact]
        public void LogInShouldReturnUserObject()
        {
            var service = new SecurityService(_repository.Object);
            var user = new User();

            Xunit.Assert.DoesNotThrow(() => service.LogIn("Username", "Password"));
            Xunit.Assert.NotNull(user);
        }

        [Fact]
        public void PassingZeroToLogoutShouldThrow()
        {
            var service = new SecurityService(_repository.Object);
            Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => service.Logout());
        }
    }
}
