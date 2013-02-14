using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Abstracts;
using WebServices.DataContracts;
using Xunit;

namespace WebServices.Test.Tests
{
    public class AuthenticationServiceTest
    {
        private AuthenticationServiceClient client;

        public AuthenticationServiceTest()
        {
            client = new AuthenticationServiceClient();
        }

        [Fact]
        public void TestAuthenticationServiceClientIsNotNull()
        {
            Assert.NotNull(client);
            Assert.IsAssignableFrom<AuthenticationServiceClient>(client);
            Assert.NotNull(client.Endpoint);
        }

        [Fact]
        public void TestAuthenticate()
        {
            string strResult = null;
            string strCredentials = "{\"strUsername\":\"DR_DRIVER\",\"strPassword\":\"password\"}";

            Assert.DoesNotThrow(() => strResult = client.Authenticate(strCredentials));

            Assert.NotNull(strResult);

            TestAuthenticateResultObject oResult = JsonR.JsonDeserializer<TestAuthenticateResultObject>(strResult);

            Assert.NotNull(oResult);

            Assert.Equal(Guid.Parse("66fba0e1-6429-4999-9538-6566dee70048"), oResult.UserKey);
            Assert.True(oResult.SessionID > 0);
        }

        [Fact]
        public void TestAuthentiateInvalidCredentinals()
        {
            string strResult = null;
            string strCredentials = "{\"strUsername\":\"WRONG\",\"strPassword\":\"BAD\"}";

            Assert.Throws<MessageSecurityException>(() => strResult = client.Authenticate(strCredentials));
            Assert.Null(strResult);
        }

        [Fact]
        public void TestAuthenticateCorruptJsonString()
        {
            string strResult = null;
            string strBadJson = "{\\Userord\"\":\"strBAD\nNG\\eam\"WRO:P,\"strassw}";

            Assert.Throws<ProtocolException>(() => strResult = client.Authenticate(strBadJson));
            Assert.Null(strResult);
        }

        [Fact]
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
            Assert.DoesNotThrow(() => client.Logout(strLogoutCredentials));

            string strResultTwo = client.Authenticate(strCredentials);
            TestAuthenticateResultObject oResultTwo = JsonR.JsonDeserializer<TestAuthenticateResultObject>(strResultTwo);

            Assert.Equal(oResultOne.UserKey, oResultTwo.UserKey);
            Assert.NotEqual<int>(oResultOne.SessionID, oResultTwo.SessionID);
        }

        [Fact]
        public void TestLogoutInvalidCredentials()
        {
            SessionDataContract oContract = new SessionDataContract()
            {
                oUserIdentification = Guid.NewGuid().ToString(),
                iSessionID = new Random().Next()
            };

            string strBadLogoutCredentials = JsonR.JsonSerializer(oContract);
            Assert.Throws<ProtocolException>(() => client.Logout(strBadLogoutCredentials));
        }

        [Fact]
        public void TestLogoutMalformedJson()
        {
            string strLogoutCredentials = "{\"iSesssssssssssssssssionID\":abc,\"oUserIdentification\":\"48765d10-------9f41-481a-b311-2f6ec9e9db6e\"}";
            Assert.Throws<ProtocolException>(() => client.Logout(strLogoutCredentials));
        }
    }

    public class TestAuthenticateResultObject
    {
        public Guid UserKey { get; set; }
        public int SessionID { get; set; }
    }
}
