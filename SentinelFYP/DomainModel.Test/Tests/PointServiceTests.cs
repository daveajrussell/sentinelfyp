﻿using System;
using System.Collections.Generic;
using System.Drawing;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using DomainModel.Models.GISModels;
using DomainModel.Services;
using DomainModel.Test.TestHelpers;
using GMap.NET;
using Moq;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainModel.Test.Tests
{
    [TestClass]
    public class PointServiceTests
    {
        private Mock<IPointRepository> _pointRepository;

        public PointServiceTests()
        {
            _pointRepository = new Mock<IPointRepository>();

            _pointRepository.Setup(m => m.LoadSignalBlackspotPoints())
                .Returns(PointServiceTestHelper.MockPointLatLngList())
                .Verifiable();

            _pointRepository.Setup(m => m.AdjustMapPixelsToTilePixels(It.IsAny<GMap.NET.Point>(), It.IsAny<GMap.NET.Point>()))
                .Callback<GMap.NET.Point, GMap.NET.Point>((tileXYPoint, mapPixelPoint) => PointServiceTestHelper.MockPoint(tileXYPoint, mapPixelPoint))
                .Returns<GMap.NET.Point, GMap.NET.Point>((tileXYPoint, mapPixelPoint) => PointServiceTestHelper.MockPoint(tileXYPoint, mapPixelPoint));

            _pointRepository.Setup(m => m.GetPointsForTile(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Bitmap>(), It.IsAny<int>(), It.IsAny<List<PointLatLng>>()))
                .Returns(PointServiceTestHelper.GetPointsForTileMock(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Fact]
        public void InjectingNullReferenceIntoConstructorShouldThrow()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() => new PointService(null));
        }

        [Fact]
        public void InjectingInterfaceIntoConsructorShouldWork()
        {
            Xunit.Assert.DoesNotThrow(() => new PointService(_pointRepository.Object));
        }

        [Fact]
        public void LoadPointsShouldNotReturnNull()
        {
            var pointService = new PointService(_pointRepository.Object);
            var points = pointService.LoadSignalBlackspotPoints();

            Xunit.Assert.NotNull(points);
        }

        [Fact]
        [TestMethod]
        public void AdjustMapPixelsToTilePixelsShouldNotReturnNullPoint()
        {
            var pointService = new PointService(_pointRepository.Object);
            var result = pointService.AdjustMapPixelsToTilePixels(new GMap.NET.Point(1, 1), new GMap.NET.Point(1, 2));

            Xunit.Assert.NotEqual(0, result.X);
            Xunit.Assert.NotEqual(0, result.Y);
        }

        [Fact]
        public void GetPointsForTileShouldThrowWhenPassedNullCoordinates()
        {
            var pointService = new PointService(_pointRepository.Object);
            Xunit.Assert.Throws<ArgumentNullException>(() => pointService.GetPointsForTile(0, 1, null, 0, null));
        }

        [Fact]
        public void GetPointsForTileShouldThrowWhenPassedNullBitmap()
        {
            var pointService = new PointService(_pointRepository.Object);
            Xunit.Assert.Throws<ArgumentNullException>(() => pointService.GetPointsForTile(12, 13, null, 2, null));
        }

        [Fact]
        public void GetPointsForTileShouldThrowWhenPassedNullList()
        {
            var pointService = new PointService(_pointRepository.Object);
            Xunit.Assert.Throws<ArgumentNullException>(() => pointService.GetPointsForTile(12, 13, new Bitmap(1, 1), 2, null));
        }

        [Fact]
        [TestMethod]
        public void GetPointsForTileShouldReturnArrayOfPoints()
        {
            var pointService = new PointService(_pointRepository.Object);
            var result = pointService.GetPointsForTile(12, 13, new Bitmap(1, 1), 2, new List<PointLatLng>());

            Xunit.Assert.NotEmpty(result);
        }
    }
}
