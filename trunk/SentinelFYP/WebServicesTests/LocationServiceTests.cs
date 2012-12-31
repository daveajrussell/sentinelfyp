using System;
using System.ServiceModel;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using Moq;
using WebServices.Services;
using Xunit;

namespace WebServicesTests
{
    public class LocationServiceTests
    {
        LocationServiceClient client;

        public LocationServiceTests()
        {
            client = new LocationServiceClient();
        }

        [Fact]
        public void PassingNonJSONStringToServiceShouldThrow()
        {
            Xunit.Assert.Throws<FaultException<ExceptionDetail>>(() => client.PostGISData("Daft"));
        }

        [Fact]
        public void PassingJSONStringToServiceShouldNotThrow()
        {
            Xunit.Assert.DoesNotThrow(() => client.PostGISData("{\"TestData\": \"Test\"}"));
        }
    }
}