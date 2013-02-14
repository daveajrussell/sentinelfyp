using System;
using System.ServiceModel;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using Moq;
using WebServices.Services;
using Xunit;

namespace WebServices.Test.Tests
{
    public class LocationServiceTests
    {
        LocationServiceClient client;

        public LocationServiceTests()
        {
            client = new LocationServiceClient();
        }





        /*
        [Fact]
        public void PassingNonJSONStringToServiceShouldThrow()
        {
            Assert.Throws<FaultException<ExceptionDetail>>(() => client.PostGISData("Daft"));
        }

        [Fact]
        public void PassingJSONStringToServiceShouldNotThrow()
        {
            Assert.DoesNotThrow(() => client.PostGISData("{\"lngTimeStamp\":1355267165704,\"oUserIdentification\":\"00000000-0000-0000-0000-000000000000\",\"dLatitude\":37.422005,\"dLongitude\":-122.084095,\"intOrientation\":1,\"dSpeed\":0}"));
        }
        */ 
    }
}