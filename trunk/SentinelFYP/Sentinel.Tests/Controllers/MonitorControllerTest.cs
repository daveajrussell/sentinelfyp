using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DomainModel.Interfaces.Services;
using DomainModel.SecurityModels;
using Moq;
using Sentinel.Controllers;
using Xunit;

namespace Sentinel.Tests.Controllers
{
    public class MonitorControllerTest
    {
        private Mock<ISecurityService> _service;

        public MonitorControllerTest()
        {
            _service = new Mock<ISecurityService>();
            _service.Setup(m => m.GetUserByUserKey(It.IsAny<Guid>()))
                .Returns<Guid>((userKey) => new User(userKey, "TestUser", "Test", "User", "test@daveajrussell.com"));
        }

        [Fact]
        public void TestConstructor()
        {
            MonitorController target = null;

            Assert.DoesNotThrow(() => target = new MonitorController(_service.Object));
            Assert.NotNull(target);
        }

        [Fact]
        public void TestConstructorShouldThrow()
        {
            MonitorController target = null;

            Assert.Throws<ArgumentNullException>(() => target = new MonitorController(null));
            Assert.Null(target);
        }

        [Fact]
        public void TestGetDriverContactDetailsPartial()
        {
            var target = new MonitorController(_service.Object);

            var result = target.GetDriverContactDetailsPartial("66FBA0E1-6429-4999-9538-6566DEE70048");

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PartialViewResult>(result);

            Assert.NotNull(((ViewResultBase)(result)).Model);
            Assert.IsAssignableFrom<User>(((ViewResultBase)(result)).Model);
            Assert.Equal("TestUser", (((User)(((ViewResultBase)(result)).Model)).UserName));
            Assert.Equal(Guid.Parse("66FBA0E1-6429-4999-9538-6566DEE70048"), (((User)(((ViewResultBase)(result)).Model)).UserKey));
            Assert.Equal("UserContactDetailsPartial", ((ViewResultBase)(result)).ViewName);
        }
    }
}
