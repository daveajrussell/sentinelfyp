using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sentinel.Services;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sentinel.Tests.Services
{
    [TestClass]
    public class NotifierServiceTest
    {
        private NotifierService client;

        public NotifierServiceTest()
        {
            client = new NotifierService();
        }

        [Fact]
        public void TestClient()
        {
            Xunit.Assert.NotNull(client);
        }

        [Fact]
        [TestMethod]
        public void TestGISNotify()
        {
            string strNotificationString = "{\"iSessionID\":630,\"oUserIdentification\":\"66fba0e1-6429-4999-9538-6566dee70048\",\"lTimeStamp\":1359309623997,\"dLatitude\":54,\"dLongitude\":34,\"dSpeed\":0,\"iOrientation\":1}";
            Xunit.Assert.DoesNotThrow(() => client.GISNotify(strNotificationString));
        }

        [Fact]
        public void TestGISNotifyShouldThow()
        {
            Xunit.Assert.Throws<FormatException>(() => client.GISNotify(""));
        }
    }
}
