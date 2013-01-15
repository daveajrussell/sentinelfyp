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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainModel.Models;
using System.Drawing;
using System.Drawing.Imaging;
using Sentinel.Controllers;
using Sentinel.Tests.TestHelpers;

namespace Sentinel.Tests.Controllers
{
    //[TestClass]
    public class HeatmapControllerTest
    {
        private Mock<IPointService> _pointService;
        private Mock<IGHeatService> _gheatService;

        public HeatmapControllerTest()
        {
            _pointService = new Mock<IPointService>();
            _gheatService = new Mock<IGHeatService>();

            _gheatService.Setup(m => m.GetTile(It.IsAny<List<PointLatLng>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns<List<PointLatLng>, string, int, int, int>((points, colorScheme, zoom, x, y) => SentinelTestHelper.GetTileMock(points, colorScheme, zoom, x, y));

            _pointService.Setup(m => m.LoadPoints())
                .Returns(new List<PointLatLng>() { new PointLatLng(12, 12) });
        }

        [Fact]
        public void InjectingNullPointServiceIntoConstructorShouldThrow()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() => new HeatmapController(null, _gheatService.Object));
        }

        [Fact]
        public void InjectingNullGHeatServiceIntoConstructorShouldThrow()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() => new HeatmapController(_pointService.Object, null));
        }

        [Fact]
        public void ControllerConstructorShouldNotThrow()
        {
            Xunit.Assert.DoesNotThrow(() => new HeatmapController(_pointService.Object, _gheatService.Object));
        }

        [Fact]
        public void IndexReturnsActionResult()
        {
            var controller = new HeatmapController(_pointService.Object, _gheatService.Object);
            ActionResult result = null;

            Xunit.Assert.DoesNotThrow(() => result = controller.Index());
            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<ActionResult>(result);
        }

        [Fact]
        //[TestMethod]
        public void GetTileReturnsTileResult()
        {
            var controller = new HeatmapController(_pointService.Object, _gheatService.Object);
            TileResult result = null;

            //result = controller.Tile("classic", "5", "12", "12", new Random().Next().ToString());

            Xunit.Assert.DoesNotThrow(() => result = controller.Tile("classic", "5", "12", "12", new Random().Next().ToString()));
            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<TileResult>(result);
        }
    }
}
