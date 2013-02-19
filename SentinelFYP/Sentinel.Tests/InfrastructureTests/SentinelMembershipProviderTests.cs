using DomainModel.Interfaces.Services;
using DomainModel.Models.AuditModels;
using DomainModel.SecurityModels;
using Moq;
using Sentinel.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Xunit;

namespace Sentinel.Tests.InfrastructureTests
{
    public class SentinelMembershipProviderTests
    {
        private Mock<ISecurityService> _service;

        public SentinelMembershipProviderTests()
        {
            _service = new Mock<ISecurityService>();

            HttpContext.Current = FakeHttpContext();

            User user = new User();
            Session session = new Session();

            _service.Setup(m => m.LogIn(It.IsAny<string>(), It.IsAny<string>(), out user, out session));
        }

        [Fact]
        public void TestValidateUser()
        {
            var provider = new SentinelMembershipProvider(_service.Object);

            var result = provider.ValidateUser("", "");
        }

        [Fact]
        public void TestChangePassword()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.ChangePassword("", "", ""));
        }

        [Fact]
        public void TestChangePasswordQuestionAndAnswer()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.ChangePasswordQuestionAndAnswer("", "", "", ""));
        }

        [Fact]
        public void TestCreateUser()
        {
            var test = MembershipCreateStatus.DuplicateEmail;
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.CreateUser("", "", "", "", "", false, null, out test));
        }

        [Fact]
        public void TestDeleteUser()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.DeleteUser("", false));
        }

        [Fact]
        public void TestEnablePasswordReset()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.EnablePasswordReset);
        }

        [Fact]
        public void TestEnablePasswordRetrieval()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.EnablePasswordRetrieval);
        }

        [Fact]
        public void TestFindUsersByEmail()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            int iOut;
            Assert.Throws<NotImplementedException>(() => provider.FindUsersByEmail("", 0, 0, out iOut));
        }

        [Fact]
        public void TestFindUsersByName()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            int iOut;
            Assert.Throws<NotImplementedException>(() => provider.FindUsersByName("", 0, 0, out iOut));
        }

        [Fact]
        public void TestGetAllUsers()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            int iOut;
            Assert.Throws<NotImplementedException>(() => provider.GetAllUsers(0, 0, out iOut));
        }

        [Fact]
        public void TestGetNumberOfUsersOnline()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.GetNumberOfUsersOnline());
        }

        [Fact]
        public void TestGetPassword()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.GetPassword("", ""));
        }

        [Fact]
        public void TestGetUser()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.GetUser(null, false));
        }

        [Fact]
        public void TestGetUserOverload()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.GetUser("", false));
        }

        [Fact]
        public void TestGetUserNameByEmail()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.GetUserNameByEmail(""));
        }

        [Fact]
        public void TestMaxInvalidPasswordAttempts()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.MaxInvalidPasswordAttempts); 
        }

        [Fact]
        public void TestMinRequiredNonAlphanumericCharacters()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.MinRequiredNonAlphanumericCharacters);
        }

        [Fact]
        public void TestMinRequiredPasswordLength()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.MinRequiredPasswordLength);
        }

        [Fact]
        public void TestPasswordAttemptWindow()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.PasswordAttemptWindow);
        }

        [Fact]
        public void TestPasswordFormat()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.PasswordFormat);
        }

        [Fact]
        public void TestPasswordStrengthRegularExpression()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.PasswordStrengthRegularExpression);
        }

        [Fact]
        public void TestRequiresQuestionAndAnswer()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.RequiresQuestionAndAnswer);
        }

        [Fact]
        public void TestRequiresUniqueEmail()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.RequiresUniqueEmail);
        }

        [Fact]
        public void TestResetPassword()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.ResetPassword("", ""));
        }

        [Fact]
        public void TestUnlockUser()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.UnlockUser(""));
        }

        [Fact]
        public void TestUpdateUser()
        {
            var provider = new SentinelMembershipProvider(_service.Object);
            Assert.Throws<NotImplementedException>(() => provider.UpdateUser(null));
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
