using System;
using System.ServiceModel;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using Moq;
using WebServices.Interfaces;
using WebServices.Services;
using Xunit;

namespace WebServicesTests
{
    public class LocationServiceTests
    {
        Uri oBaseAddress = null;
        ServiceHost oServiceHost = null;
        LocationServiceClient oLocationServiceClient;

        public LocationServiceTests()
        {
            oBaseAddress = new Uri("http://localhost/WebServices/Services/LocationService.svc");
            oServiceHost = new ServiceHost(typeof(LocationService), oBaseAddress);
            oLocationServiceClient = new LocationServiceClient();   
        }

        [Fact]
        public void TestCeption()
        {
            oServiceHost.Open();
            Xunit.Assert.DoesNotThrow(() => oLocationServiceClient.PostGISData("Blah"));
            oServiceHost.Close();
        }
    }
}
