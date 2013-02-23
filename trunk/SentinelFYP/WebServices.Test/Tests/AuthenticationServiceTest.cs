using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Abstracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServices.DataContracts;
using Xunit;
using Moq;
using DomainModel.SecurityModels;
using DomainModel.Models.AuditModels;

namespace WebServices.Test.Tests
{
    [TestClass]
    public class AuthenticationServiceTest
    {
        private AuthenticationServiceClient client;

        public AuthenticationServiceTest()
        {
            client = new AuthenticationServiceClient();

        }

        [Fact]
        [TestMethod]
        public void TestAuthenticationServiceClientIsNotNull()
        {
            Xunit.Assert.NotNull(client);
            Xunit.Assert.IsAssignableFrom<AuthenticationServiceClient>(client);
            Xunit.Assert.NotNull(client.Endpoint);
        }

        [Fact]
        [TestMethod]
        public void TestAuthenticate()
        {
            string strResult = null;
            string strCredentials = "{\"strUsername\":\"DR_DRIVER\",\"strPassword\":\"password\"}";

            Xunit.Assert.DoesNotThrow(() => strResult = client.Authenticate(strCredentials));

            Xunit.Assert.NotNull(strResult);

            TestAuthenticateResultObject oResult = JsonR.JsonDeserializer<TestAuthenticateResultObject>(strResult);

            Xunit.Assert.NotNull(oResult);

            Xunit.Assert.Equal(Guid.Parse("66fba0e1-6429-4999-9538-6566dee70048"), oResult.UserKey);
            Xunit.Assert.True(oResult.SessionID > 0);
        }

        [Fact]
        [TestMethod]
        public void TestAuthentiateInvalidCredentinals()
        {
            string strResult = null;
            string strCredentials = "{\"strUsername\":\"WRONG\",\"strPassword\":\"BAD\"}";

            Xunit.Assert.Throws<MessageSecurityException>(() => strResult = client.Authenticate(strCredentials));
            Xunit.Assert.Null(strResult);
        } 
        
        [Fact]
        [TestMethod]
        public void TestAuthenticateCorruptJsonString()
        {
            string strResult = null;
            string strBadJson = "{\\Userord\"\":\"strBAD\nNG\\eam\"WRO:P,\"strassw}";

            Xunit.Assert.Throws<ProtocolException>(() => strResult = client.Authenticate(strBadJson));
            Xunit.Assert.Null(strResult);
        }

        [Fact]
        [TestMethod]
        public void TestLogout()
        {
            string strCredentials = "{\"strUsername\":\"DR_DRIVER\",\"strPassword\":\"password\"}";

            string strResultOne = client.Authenticate(strCredentials);
            TestAuthenticateResultObject oResultOne = JsonR.JsonDeserializer<TestAuthenticateResultObject>(strResultOne);

            SessionDataContract oContract = new SessionDataContract()
            {
                oUserIdentification = oResultOne.UserKey.ToString(),
                iSessionID = oResultOne.SessionID
            };

            string strLogoutCredentials = JsonR.JsonSerializer(oContract);
            Xunit.Assert.DoesNotThrow(() => client.Logout(strLogoutCredentials));

            string strResultTwo = client.Authenticate(strCredentials);
            TestAuthenticateResultObject oResultTwo = JsonR.JsonDeserializer<TestAuthenticateResultObject>(strResultTwo);

            Xunit.Assert.Equal(oResultOne.UserKey, oResultTwo.UserKey);
            Xunit.Assert.NotEqual<int>(oResultOne.SessionID, oResultTwo.SessionID);
        }

        [Fact]
        [TestMethod]
        public void TestLogoutMalformedJson()
        {
            string strLogoutCredentials = "{\"iSesssssssssssssssssionID\":abc,\"oUserIdentification\":\"48765d10-------9f41-481a-b311-2f6ec9e9db6e\"}";
            Xunit.Assert.Throws<ProtocolException>(() => client.Logout(strLogoutCredentials));
        }
    }

    public class TestAuthenticateResultObject
    {
        public Guid UserKey { get; set; }
        public int SessionID { get; set; }
    }
}
