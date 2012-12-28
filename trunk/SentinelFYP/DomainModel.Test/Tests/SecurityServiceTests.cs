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

            _repository.Setup(m => m.LogIn(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new User(Guid.NewGuid(), "Test User", "Test", "User", "Test@User.com", DateTime.Now, DateTime.Now, DateTime.Now));

            _repository.Setup(m => m.Logout(It.IsAny<int>()));
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
            Xunit.Assert.Throws<ArgumentNullException>(() => service.LogIn(null, "Password", "Test User Agent", "Test IP Address"));
        }

        [Fact]
        public void PassingNullPasswordToLogInShouldThrow()
        {
            var service = new SecurityService(_repository.Object);
            Xunit.Assert.Throws<ArgumentNullException>(() => service.LogIn("Username", null, "Test User Agent", "Test IP Address"));
        }

        [Fact]
        public void PassingNullUserAgentToLogInShouldThrow()
        {
            var service = new SecurityService(_repository.Object);
            Xunit.Assert.Throws<ArgumentNullException>(() => service.LogIn("Username", "Password", null, "Test IP Address"));
        }

        [Fact]
        public void PassingNullIPAddressToLogInShouldThrow()
        {
            var service = new SecurityService(_repository.Object);
            Xunit.Assert.Throws<ArgumentNullException>(() => service.LogIn("Username", "Password", "Test User Agent", null));
        }

        [Fact]
        public void LogInShouldReturnUserObject()
        {
            var service = new SecurityService(_repository.Object);
            var user = new User();

            Xunit.Assert.DoesNotThrow(() => user = service.LogIn("Username", "Password", "User Agent", "IP Address"));
            Xunit.Assert.NotNull(user);
        }

        [Fact]
        public void PassingZeroToLogoutShouldThrow()
        {
            var service = new SecurityService(_repository.Object);
            Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => service.Logout(0));
        }
    }
}
