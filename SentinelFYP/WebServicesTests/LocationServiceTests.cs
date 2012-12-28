using System;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebServices.Interfaces;

namespace WebServicesTests
{
    [TestClass]
    public class LocationServiceTests
    {
        private Mock<ILocationService> _locationService;
        private Mock<IGISService> _gisService;

        [TestInitialize]
        public void Init()
        {
            _locationService = new Mock<ILocationService>();
            _gisService = new Mock<IGISService>();

            /*_gisService.Setup(m => m.AddGIS(It.IsAny<GIS>())).

            _locationService.Setup(m => m.PostGISData("Data")).*/
        }
    }
}
