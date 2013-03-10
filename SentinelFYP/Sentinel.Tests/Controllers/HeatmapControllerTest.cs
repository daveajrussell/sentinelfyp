using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Moq;
using DomainModel.Interfaces.Services;
using DomainModel.Interfaces.Repositories;
using Xunit;
using System.Web.Mvc;
using GMap.NET;
using DomainModel.Test.TestHelpers;
using Sentinel.Helpers.ExtensionMethods;
using DomainModel.Models;
using System.Drawing;
using System.Drawing.Imaging;
using Sentinel.Controllers;
using Sentinel.Tests.TestHelpers;
using DomainModel.Models.GISModels;

namespace Sentinel.Tests.Controllers
{
    public class HeatmapControllerTest
    {
        private Mock<IPointService> _pointService;
        private Mock<IGHeatService> _gheatService;
        private Mock<ILiveTrackingService> _trackingService;

        public HeatmapControllerTest()
        {
            _pointService = new Mock<IPointService>();
            _gheatService = new Mock<IGHeatService>();
            _trackingService = new Mock<ILiveTrackingService>();

            _gheatService.Setup(m => m.GetTile(It.IsAny<List<PointLatLng>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns<List<PointLatLng>, string, int, int, int>((points, colorScheme, zoom, x, y) => TrackingTestHelper.GetTileMock(points, colorScheme, zoom, x, y));

            _pointService.Setup(m => m.LoadSignalBlackspotPoints())
                .Returns(new List<PointLatLng>() { new PointLatLng(12, 12) });

            _trackingService.Setup(m => m.GetAllLiveElapsedRoutes())
                .Returns(() => TrackingTestHelper.MockGetAllLiveElapsedRoutes());
        }

        [Fact]
        public void InjectingNullPointServiceIntoConstructorShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new HeatmappingController(null, null, null));
        }

        [Fact]
        public void InjectingNullGHeatServiceIntoConstructorShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new HeatmappingController(_pointService.Object, null, null));
        }

        [Fact]
        public void InjectingNullTrackingServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new HeatmappingController(_pointService.Object, _gheatService.Object, null));
        }

        [Fact]
        public void ControllerConstructorShouldNotThrow()
        {
            Assert.DoesNotThrow(() => new HeatmappingController(_pointService.Object, _gheatService.Object, _trackingService.Object));
        }

        [Fact]
        public void IndexReturnsActionResult()
        {
            var controller = new HeatmappingController(_pointService.Object, _gheatService.Object, _trackingService.Object);
            ActionResult result = null;

            Assert.DoesNotThrow(() => result = controller.Index());
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult>(result);
        }

        [Fact]
        public void GetTileReturnsTileResult()
        {
            var controller = new HeatmappingController(_pointService.Object, _gheatService.Object, _trackingService.Object);
            TileResult result = null;

            Assert.DoesNotThrow(() => result = controller.BlackSpotTile("5", "12", "12"));
            Assert.NotNull(result);
            Assert.IsAssignableFrom<TileResult>(result);
        }

        /*[Fact]
        public void TestGetAllLiveElapsedRoutes()
        {
            var controller = new HeatmappingController(_pointService.Object, _gheatService.Object, _trackingService.Object);
            ActionResult result = null;

            Assert.DoesNotThrow(() => result = controller.LiveHeatmapData());
            Assert.NotNull(result);
            Assert.IsAssignableFrom<PartialViewResult>(result);

            Assert.NotNull(((ViewResultBase)(result)).Model);
            Assert.IsAssignableFrom<IEnumerable<ElapsedGeospatialInformation>>(((ViewResultBase)(result)).Model);
        }*/
    }
}
