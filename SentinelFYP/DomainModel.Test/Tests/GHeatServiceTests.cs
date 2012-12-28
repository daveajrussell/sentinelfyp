using System;
using System.Collections.Generic;
using System.Drawing;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using DomainModel.Services;
using DomainModel.Test.TestHelpers;
using GMap.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;

namespace DomainModel.Test.Tests
{
    public class GHeatServiceTests
    {
        private Mock<IGHeatRepository> _repository;

        public GHeatServiceTests()
        {
            _repository = new Mock<IGHeatRepository>();

            _repository.Setup(m => m.AvailableColourSchemes())
                .Returns(GHeatServiceTestHelper.AvailableColourSchemesMock());

            _repository.Setup(m => m.GetColourScheme(It.IsAny<string>()))
                .Returns<string>((colourScheme) => GHeatServiceTestHelper.GetColourSchemeMock(colourScheme));

            _repository.Setup(m => m.GetDot(It.IsAny<int>()))
                .Returns<int>((zoom) => GHeatServiceTestHelper.GetDotMock(zoom));

            _repository.Setup(m => m.GetTile(It.IsAny<List<PointLatLng>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns<List<PointLatLng>, string, int, int, int>((points, colourScheme, zoom, x, y) => GHeatServiceTestHelper.GetTileMock(points, colourScheme, zoom, x, y));
        }

        [Fact]
        public void InjectingNullIntoConstructorShouldThrow()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() => new GHeatService(null));
        }

        [Fact]
        public void InjectingInterfaceIntoConstructorShouldWork()
        {
            var service = new GHeatService(_repository.Object);

            Xunit.Assert.NotNull(service);
            Xunit.Assert.IsAssignableFrom<GHeatService>(service);
        }

        [Fact]
        public void AvailableColourSchemesReturnsListOfColourSchemes()
        {
            var service = new GHeatService(_repository.Object);
            var results = service.AvailableColourSchemes();

            Xunit.Assert.NotEmpty(results);
            Xunit.Assert.IsAssignableFrom<string[]>(results);
            Xunit.Assert.Equal(5, results.Length);
            Xunit.Assert.Equal("classic.png", results[0]);
        }

        [Fact]
        public void PassingNullToGetColourSchemeShouldThrow()
        {
            var service = new GHeatService(_repository.Object);
            Xunit.Assert.Throws<ArgumentNullException>(() => service.GetColourScheme(null));
        }

        [Fact]
        public void GetColourSchemeReturnsBitmap()
        {
            var service = new GHeatService(_repository.Object);
            var result = service.GetColourScheme("classic");

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<Bitmap>(result);
        }

        [Fact]
        public void PassingLessThanZeroToGetDotShouldThrow()
        {
            var service = new GHeatService(_repository.Object);

            Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => service.GetDot(-1));
        }

        [Fact]
        public void GetDotReturnsBitmap()
        {
            var service = new GHeatService(_repository.Object);
            var result = service.GetDot(5);

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<Bitmap>(result);
        }

        [Fact]
        public void PassingNullListOfPointsToGetTileShouldThrow()
        {
            var service = new GHeatService(_repository.Object);

            Xunit.Assert.Throws<ArgumentNullException>(() => service.GetTile(null, "classic", 1, 1, 1));
        }

        [Fact]
        public void PassingEmptyColourSchemeToGetTileShouldThrow()
        {
            var _points = GHeatServiceTestHelper.GetListOfMockPointLatLng();
            var service = new GHeatService(_repository.Object);

            Xunit.Assert.Throws<ArgumentNullException>(() => service.GetTile(_points, "", 1, 1, 1));
        }

        [Fact]
        public void GetTileShouldReturnATileObject()
        {
            var _points = GHeatServiceTestHelper.GetListOfMockPointLatLng();
            var service = new GHeatService(_repository.Object);
            var result = service.GetTile(_points, "classic", 1, 1, 1);

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<Bitmap>(result);
            Xunit.Assert.Equal(256, result.Width);
            Xunit.Assert.Equal(256, result.Height);
            Xunit.Assert.Equal(96, result.HorizontalResolution);
        }
    }
}
