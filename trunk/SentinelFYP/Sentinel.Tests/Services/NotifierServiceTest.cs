using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sentinel.Services;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sentinel.Services.DataContracts;

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
            GeospatialInformationDataContract contract = new GeospatialInformationDataContract()
            {
                oUserIdentification = "66fba0e1-6429-4999-9538-6566dee70048",
                iSessionID = 630,
                lTimeStamp = 1359309623997,
                dLatitude = 54,
                dLongitude = 34,
                iOrientation = 1
            };
            Xunit.Assert.DoesNotThrow(() => client.GISNotify(contract));
        }

        [Fact]
        public void TestGISNotifyShouldThow()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() => client.GISNotify(null));
        }
    }
}
