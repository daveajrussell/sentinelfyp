using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using DomainModel.Models.AuditModels;
using DomainModel.SecurityModels;
using Moq;
using Sentinel.Controllers;
using Sentinel.Models;
using Xunit;

namespace Sentinel.Tests.Controllers
{
    public class HeaderControllerTest
    {
        private Mock<ControllerContext> _mock;
        private Mock<HttpSessionStateBase> _mockSession;

        public HeaderControllerTest()
        {
            _mock = new Mock<ControllerContext>();
            _mockSession = new Mock<HttpSessionStateBase>();

            _mock.Setup(p => p.HttpContext.Session)
                .Returns(_mockSession.Object);
        }

        [Fact]
        public void TestIndexWithNoLoggedInUser()
        {
            var target = new HeaderController();
            target.ControllerContext = _mock.Object;
            HttpContext.Current = FakeHttpContext();

            var result = target.Index();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<RedirectToRouteResult>(result);
            
            Assert.True(((RedirectToRouteResult)(result)).RouteValues.Any(v => v.Value == "Account"));
            Assert.True(((RedirectToRouteResult)(result)).RouteValues.Any(v => v.Value == "Index"));
        }

        [Fact]
        public void TestIndexWithLoggedInUser()
        {
            var target = new HeaderController();
            target.ControllerContext = _mock.Object;
            
            HttpContext.Current = FakeHttpContext();
            State.User = new User() { UserName = "Test User", LastLoginDate = DateTime.Now };

            var result = target.Index();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PartialViewResult>(result);

            Assert.NotNull(((ViewResultBase)(result)).Model);
            Assert.IsAssignableFrom<UserViewModel>(((ViewResultBase)(result)).Model);

            Assert.Equal("Test User", (((UserViewModel)(((ViewResultBase)(result)).Model)).UserName));

            Assert.Equal("HeaderPartial", ((ViewResultBase)(result)).ViewName);
        }

        public static HttpContext FakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://localhost/Sentinel/", "");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(), new HttpStaticObjectsCollection(), 10, true, HttpCookieMode.AutoDetect, SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, CallingConventions.Standard, new[] { typeof(HttpSessionStateContainer) }, null)
                .Invoke(new object[] { sessionContainer });

            return httpContext;
        }
    }
}
