using System;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using Moq;
using WebServices.Interfaces;
using WebServices.Test.Clients;
using Xunit;

namespace WebServicesTests
{
    public class LocationServiceTests
    {
        LocationServiceClient oLocationServiceClient;

        public LocationServiceTests()
        {
            oLocationServiceClient = new LocationServiceClient();
        }

        [Fact]
        public void TestCeption()
        {
            Xunit.Assert.DoesNotThrow(() => oLocationServiceClient.PostGISData("Blah"));
        }
    }
}
