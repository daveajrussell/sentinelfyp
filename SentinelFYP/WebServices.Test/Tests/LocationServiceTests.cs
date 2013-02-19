using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using DomainModel.Abstracts;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebServices.DataContracts;
using WebServices.Services;
using Xunit;

namespace WebServices.Test.Tests
{
    [TestClass]
    public class LocationServiceTests
    {
        LocationServiceClient client;

        public LocationServiceTests()
        {
            client = new LocationServiceClient();
        }

        [Fact]
        [TestMethod]
        public void TestClient()
        {
            Xunit.Assert.NotNull(client);
            Xunit.Assert.IsAssignableFrom<LocationServiceClient>(client);
            Xunit.Assert.NotNull(client.Endpoint);
        }

        [Fact]
        [TestMethod]
        public void TestPostGeospatialData()
        {
            var Location = new GeospatialInformationDataContract()
            {
                oUserIdentification = "E5C89B81-F095-4F10-8FCC-72173BC294A1",
                iSessionID = 1693,
                lTimeStamp = 1359309623997,
                dLatitude = 54,
                dLongitude = -2,
                iOrientation = 1,
                dSpeed = 0
            };

            string strLocation = JsonR.JsonSerializer(Location);
            Xunit.Assert.DoesNotThrow(() => client.PostGeospatialData(strLocation));
        }

        [Fact]
        public void TestPostGeospatialDataAsync()
        {
            var Location = new GeospatialInformationDataContract()
            {
                oUserIdentification = "E5C89B81-F095-4F10-8FCC-72173BC294A1",
                iSessionID = 1693,
                lTimeStamp = 1359309623997,
                dLatitude = 54,
                dLongitude = -2,
                iOrientation = 1,
                dSpeed = 0
            };

            string strLocation = JsonR.JsonSerializer(Location);
            Xunit.Assert.DoesNotThrow(() => client.PostGeospatialData(strLocation));
        }

        [Fact]
        [TestMethod]
        public void TestPostInvalidGeospatialData()
        {
            var InvalidLocation = new GeospatialInformationDataContract()
            {
                oUserIdentification = Guid.NewGuid().ToString(),
                iSessionID = 123123123,
                lTimeStamp = 1359309623997,
                dLatitude = 54,
                dLongitude = -2,
                iOrientation = 1,
                dSpeed = 0
            };

            string strLocation = JsonR.JsonSerializer(InvalidLocation);
            Xunit.Assert.Throws<ProtocolException>(() => client.PostGeospatialData(strLocation));
        }

        [Fact]
        [TestMethod]
        public void TestPostMalformedGeospatialData()
        {
            string strLocation = "{\"iSe\":63ssionID0,\"\":\"66oUserIdentificationfba0e1-6429-4999--6566dee70048\",\"l9538Tim1359309623997eStamp\"dLatitude:,\"\":54,\"\":34,dLongitude\"dSpeed\":0,\"iOrientation\":1}";
            Xunit.Assert.Throws<ProtocolException>(() => client.PostGeospatialData(strLocation));
        }

        [Fact]
        [TestMethod]
        public void TestPostBufferedGeospatialDataSet()
        {
            var LocationSet = new GeospatialInformationSetDataContract()
            {
                BufferedData = new List<GeospatialInformationDataContract>()
                {
                    new GeospatialInformationDataContract()
                    {
                        oUserIdentification = "E5C89B81-F095-4F10-8FCC-72173BC294A1",
                        iSessionID = 1693,
                        lTimeStamp = 1359309623997,
                        dLatitude = 54,
                        dLongitude = -2,
                        iOrientation = 1,
                        dSpeed = 0
                    },
                    new GeospatialInformationDataContract()
                    {
                        oUserIdentification = "E5C89B81-F095-4F10-8FCC-72173BC294A1",
                        iSessionID = 1693,
                        lTimeStamp = 1359309624000,
                        dLatitude = 55,
                        dLongitude = -2,
                        iOrientation = 1,
                        dSpeed = 0
                    },
                    new GeospatialInformationDataContract()
                    {
                        oUserIdentification = "E5C89B81-F095-4F10-8FCC-72173BC294A1",
                        iSessionID = 1693,
                        lTimeStamp = 1359309625000,
                        dLatitude = 56,
                        dLongitude = -2,
                        iOrientation = 1,
                        dSpeed = 0
                    }
                }
            };

            string strLocation = JsonR.JsonSerializer(LocationSet);
            Xunit.Assert.DoesNotThrow(() => client.PostBufferedGeospatialDataSet(strLocation));
        }

        [Fact]
        [TestMethod]
        public void TestPostBufferedGeospatialDataSetAsync()
        {
            var LocationSet = new GeospatialInformationSetDataContract()
            {
                BufferedData = new List<GeospatialInformationDataContract>()
                {
                    new GeospatialInformationDataContract()
                    {
                        oUserIdentification = "E5C89B81-F095-4F10-8FCC-72173BC294A1",
                        iSessionID = 1693,
                        lTimeStamp = 1359309623997,
                        dLatitude = 54,
                        dLongitude = -2,
                        iOrientation = 1,
                        dSpeed = 0
                    },
                    new GeospatialInformationDataContract()
                    {
                        oUserIdentification = "E5C89B81-F095-4F10-8FCC-72173BC294A1",
                        iSessionID = 1693,
                        lTimeStamp = 1359309624000,
                        dLatitude = 55,
                        dLongitude = -2,
                        iOrientation = 1,
                        dSpeed = 0
                    },
                    new GeospatialInformationDataContract()
                    {
                        oUserIdentification = "E5C89B81-F095-4F10-8FCC-72173BC294A1",
                        iSessionID = 1693,
                        lTimeStamp = 1359309625000,
                        dLatitude = 56,
                        dLongitude = -2,
                        iOrientation = 1,
                        dSpeed = 0
                    }
                }
            };

            string strLocation = JsonR.JsonSerializer(LocationSet);
            Xunit.Assert.DoesNotThrow(() => client.PostBufferedGeospatialDataSetAsync(strLocation));
        }

        [Fact]
        [TestMethod]
        public void TestPostInvalidBufferedGeospatialDataSet()
        {
            var InvalidLocationSet = new GeospatialInformationSetDataContract()
            {
                BufferedData = new List<GeospatialInformationDataContract>()
                {
                    new GeospatialInformationDataContract()
                    {
                        oUserIdentification = Guid.NewGuid().ToString(),
                        iSessionID = 169234233,
                        lTimeStamp = 2342,
                        dLatitude = 54,
                        dLongitude = -2,
                        iOrientation = 1,
                        dSpeed = 0
                    },
                    new GeospatialInformationDataContract()
                    {
                        oUserIdentification = Guid.NewGuid().ToString(),
                        iSessionID = 12312,
                        lTimeStamp = 654654,
                        dLatitude = 55,
                        dLongitude = -2,
                        iOrientation = 1,
                        dSpeed = 0
                    },
                    new GeospatialInformationDataContract()
                    {
                        oUserIdentification = Guid.NewGuid().ToString(),
                        iSessionID = 654645,
                        lTimeStamp = 3423423,
                        dLatitude = 56,
                        dLongitude = -2,
                        iOrientation = 1,
                        dSpeed = 0
                    }
                }
            };

            string strLocation = JsonR.JsonSerializer(InvalidLocationSet);
            Xunit.Assert.Throws<ProtocolException>(() => client.PostBufferedGeospatialDataSet(strLocation));
        }

        [Fact]
        [TestMethod]
        public void TestPostMalformedBufferedGeospatialDataSet()
        {
            string strLocation = "{\"\":[{\"dBufferedDataSpeed\":0,\"\":\"66FBA0E1-64oUserIdentificat6566DEE70048ion29-4999-9538-\",\"iOrien6566DEE70dLongitude04iSessionID8tation\":1,\"\":89,\"lTimeiSessionIDStamp\":1359154975409,\"iSessionID\":916,\"dLatitude\":76},{\"dSpeed\":0,\"\":\"66FBA0E1-oUserIdentification6429-4999-9538-\",\"\":1,\"dLoiOrientationngitude\":89,\"\":1359lTimeStamp154975409,\"\":916,\"dLatitude\":76},{\"dSpeed\":0,\"oUserIdentification\":\"66FBA0E1-6429-4999-9538-6566DEE70048\",\"iOrientation\":1,\"dLongitude\":89,\"lTimeS1359154975409tamp\":,\"\":916,\"dLatitude\":76}]}";
            Xunit.Assert.Throws<ProtocolException>(() => client.PostBufferedGeospatialDataSet(strLocation));
        }
    }
}