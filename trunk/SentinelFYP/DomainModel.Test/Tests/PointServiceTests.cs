using System;
using System.Collections.Generic;
using System.Drawing;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using DomainModel.Services;
using DomainModel.Test.TestHelpers;
using GMap.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DomainModel.Test.Tests
{
    [TestClass]
    public class PointServiceTests
    {
        private Mock<IPointRepository> _pointRepository;

        [TestInitialize]
        public void Init()
        {
            _pointRepository = new Mock<IPointRepository>();

            _pointRepository.Setup(m => m.LoadPoints())
                .Returns(PointServiceTestHelper.MockPointLatLngList())
                .Verifiable();

            _pointRepository.Setup(m => m.AddLocation(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Verifiable();

            _pointRepository.Setup(m => m.AdjustMapPixelsToTilePixels(It.IsAny<GMap.NET.Point>(), It.IsAny<GMap.NET.Point>()))
                .Callback<GMap.NET.Point, GMap.NET.Point>((tileXYPoint, mapPixelPoint) => PointServiceTestHelper.MockPoint(tileXYPoint, mapPixelPoint))
                .Returns<GMap.NET.Point, GMap.NET.Point>((tileXYPoint, mapPixelPoint) => PointServiceTestHelper.MockPoint(tileXYPoint, mapPixelPoint));

            _pointRepository.Setup(m => m.GetPointsForTile(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Bitmap>(), It.IsAny<int>(), It.IsAny<List<PointLatLng>>()))
                .Returns(PointServiceTestHelper.GetPointsForTileMock(It.IsAny<int>(), It.IsAny<int>()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InjectingNullReferenceIntoConstructorShouldThrow()
        {
            var pointService = new PointService(null);
        }

        [TestMethod]
        public void InjectingInterfaceIntoConsructorShouldPass()
        {
            var pointService = new PointService(_pointRepository.Object);
        }

        [TestMethod]
        public void LoadPointsShouldNotReturnNull()
        {
            var pointService = new PointService(_pointRepository.Object);

            var points = pointService.LoadPoints();
            Xunit.Assert.NotNull(points);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullLocationShouldThrow()
        {
            var pointService = new PointService(_pointRepository.Object);
            pointService.AddLocation(null);
        }

        [TestMethod]
        public void AddLocationShouldNotThrow()
        {
            var pointService = new PointService(_pointRepository.Object);
            pointService.AddLocation(new GIS());
        }

        [TestMethod]
        public void AdjustMapPixelsToTilePixelsShouldNotReturnNullPoint()
        {
            var pointService = new PointService(_pointRepository.Object);
            var result = pointService.AdjustMapPixelsToTilePixels(new GMap.NET.Point(1, 1), new GMap.NET.Point(1, 2));

            Xunit.Assert.NotEqual(0, result.X);
            Xunit.Assert.NotEqual(0, result.Y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetPointsForTileShouldThrowWhenPassedNullCoordinates()
        {
            var pointService = new PointService(_pointRepository.Object);
            pointService.GetPointsForTile(0, 1, null, 0, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetPointsForTileShouldThrowWhenPassedNullBitmap()
        {
            var pointService = new PointService(_pointRepository.Object);
            pointService.GetPointsForTile(12, 13, null, 2, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetPointsForTileShouldThrowWhenPassedNullList()
        {
            var pointService = new PointService(_pointRepository.Object);
            pointService.GetPointsForTile(12, 13, new Bitmap(1, 1), 2, null);
        }

        [TestMethod]
        public void GetPointsForTileShouldReturnArrayOfPoints()
        {
            var pointService = new PointService(_pointRepository.Object);
            var result = pointService.GetPointsForTile(12, 13, new Bitmap(1, 1), 2, new List<PointLatLng>());

            Xunit.Assert.NotEmpty(result);
        }
    }
}
