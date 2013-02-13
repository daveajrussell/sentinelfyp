using System;
using System.Web.Mvc;
using DomainModel.Interfaces.Services;
using Moq;
using Sentinel.Controllers;
using Sentinel.Infrastructure;
using Xunit;
using Sentinel.Tests.TestHelpers;
using Sentinel.Models;
using DomainModel.Models.AuditModels;
using System.Web;
using System.Web.SessionState;
using System.Web.Routing;
using DomainModel.SecurityModels;

namespace Sentinel.Tests.Controllers
{
    public class AccountControllerTest
    {
        private Session _mockSession;
        private User _mockUser;
        private Mock<ISecurityService> _mockSercurityService;
        private Mock<ISentinelAuthProvider> _mockAuthProvider;
        
        public AccountControllerTest()
        {
            _mockSercurityService = new Mock<ISecurityService>();
            _mockAuthProvider = new Mock<ISentinelAuthProvider>();

            _mockAuthProvider.Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((username, password) => AuthenticationTestHelper.MockAuthenticate(username, password, out _mockUser, out _mockSession));
        }

        [Fact]
        public void TestInjectingNullSecurityServiceReferenceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new AccountController(null, _mockAuthProvider.Object));
        }

        [Fact]
        public void TestInjectingNullAuthProviderReferenceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new AccountController(_mockSercurityService.Object, null));
        }

        [Fact]
        public void TestLoginActionReturnsView()
        {
            var controller = new AccountController(_mockSercurityService.Object, _mockAuthProvider.Object);
            
            var target = controller.Login();

            Assert.NotNull(target);
            Assert.IsAssignableFrom<ActionResult>(target);
        }

        [Fact]
        public void TestAuthenticateCredentials()
        {
            var controller = new AccountController(_mockSercurityService.Object, _mockAuthProvider.Object);
            
            var username = "DR_ARCHITECT";
            var password = "Password";
            var model = new LogonUserViewModel() { Username = username, Password = password };

            var result = controller.Login(model);
            Assert.NotNull(result);
            //Assert.IsAssignableFrom<ActionResult>(result);
            //Assert.IsAssignableFrom<RedirectToRouteResult>(result);
        }

        [Fact]
        public void TestLogoutReturnsView()
        {
            var controller = new AccountController(_mockSercurityService.Object, _mockAuthProvider.Object);
            var target = controller.Logout();

            Assert.NotNull(target);
            Assert.IsAssignableFrom<ActionResult>(target);
        }
    }
}
