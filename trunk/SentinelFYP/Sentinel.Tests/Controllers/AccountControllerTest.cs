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

            _mockAuthProvider.Setup(m => m.ClearAuthentication())
                .Callback(() => AuthenticationTestHelper.MockClearAuthentication(out _mockUser, out _mockSession));
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
        public void ConstructorShouldNotThrow()
        {
            var controller = new AccountController(_mockSercurityService.Object, _mockAuthProvider.Object);
            Assert.NotNull(controller);
            Assert.IsAssignableFrom<AccountController>(controller);
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
            Assert.NotNull(_mockSession);
            Assert.NotNull(_mockUser);

            Assert.Same(username, _mockUser.UserName);

            Assert.IsAssignableFrom<RedirectToRouteResult>(result);
        }

        [Fact]
        public void TestInvalidCredentials()
        {
            var controller = new AccountController(_mockSercurityService.Object, _mockAuthProvider.Object);

            var username = "WRONG";
            var password = "BAD";
            var model = new LogonUserViewModel() { Username = username, Password = password };

            var result = controller.Login(model);

            Assert.NotNull(result);
            Assert.Null(_mockSession);
            Assert.Null(_mockUser);

            Assert.False(((ViewResult)result).ViewData.ModelState.IsValid);
        }

        [Fact]
        public void TestLogoutReturnsView()
        {
            var controller = new AccountController(_mockSercurityService.Object, _mockAuthProvider.Object);
            var target = controller.Logout();

            Assert.NotNull(target);
            Assert.IsAssignableFrom<ActionResult>(target);

            Assert.Null(_mockUser);
            Assert.Null(_mockSession);
        }
    }
}
