using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using DomainModel.Models.GISModels;
using DomainModel.SecurityModels;
using DomainModel.Services;
using Moq;
using Xunit;

namespace DomainModel.Test.Tests
{
    public class LiveTrackingServiceTest
    {
        private Mock<ILiveTrackingRepository> _liveTrackingRepository;

        public LiveTrackingServiceTest()
        {
            _liveTrackingRepository = new Mock<ILiveTrackingRepository>();

            _liveTrackingRepository.Setup(m => m.GetLiveDrivers())
                .Returns(() => new List<User>() { new User(), new User() });

            _liveTrackingRepository.Setup(m => m.GetLiveUpdate(It.IsAny<Guid>()))
                .Returns(() => new GeospatialInformation(DateTime.Now, 52, -2, 0, 1));

            _liveTrackingRepository.Setup(m => m.GetLiveElapsedRoute(It.IsAny<Guid>()))
                .Returns(() => new List<GeospatialInformation>() { new GeospatialInformation(), new GeospatialInformation(), new GeospatialInformation() });

            _liveTrackingRepository.Setup(m => m.GetAllLiveElapsedRoutes())
                .Returns(() => new List<ElapsedGeospatialInformation>() { new ElapsedGeospatialInformation(), new ElapsedGeospatialInformation(), new ElapsedGeospatialInformation() });
        }

        [Fact]
        public void TestConstructor()
        {
            LiveTrackingService target = null;
            Xunit.Assert.DoesNotThrow(() => target = new LiveTrackingService(_liveTrackingRepository.Object));
            Xunit.Assert.NotNull(target);
            Xunit.Assert.IsAssignableFrom<LiveTrackingService>(target);
        }

        [Fact]
        public void TestConstructorShouldThrow()
        {
            LiveTrackingService target = null;
            Xunit.Assert.Throws<ArgumentNullException>(() => target = new LiveTrackingService(null));
            Xunit.Assert.Null(target);
        }

        [Fact]
        public void TestGetLiveDrivers()
        {
            var target = new LiveTrackingService(_liveTrackingRepository.Object);
            var liveDrivers = target.GetLiveDrivers();

            Xunit.Assert.NotEmpty(liveDrivers);
            Xunit.Assert.IsAssignableFrom<IEnumerable<User>>(liveDrivers);
        }

        [Fact]
        public void TestGetLiveUpdate()
        {
            var target = new LiveTrackingService(_liveTrackingRepository.Object);
            var liveUpdate = target.GetLiveUpdate(Guid.NewGuid());

            Xunit.Assert.NotNull(liveUpdate);
            Xunit.Assert.IsAssignableFrom<GeospatialInformation>(liveUpdate);

            Xunit.Assert.Equal(52, liveUpdate.Latitude);
            Xunit.Assert.Equal(-2, liveUpdate.Longitude);
        }

        [Fact]
        public void TestGetLiveElapsedRoute()
        {
            var target = new LiveTrackingService(_liveTrackingRepository.Object);
            var liveElapsedRoute = target.GetLiveElapsedRoute(Guid.NewGuid());

            Xunit.Assert.NotNull(liveElapsedRoute);
            Xunit.Assert.NotEmpty(liveElapsedRoute);
            Xunit.Assert.True(liveElapsedRoute.Count() > 0);

            Xunit.Assert.IsAssignableFrom<IEnumerable<GeospatialInformation>>(liveElapsedRoute);
        }

        [Fact]
        public void TestGetAllLiveElapsedRoutes()
        {
            var target = new LiveTrackingService(_liveTrackingRepository.Object);
            var allLiveElapsedRoutes = target.GetAllLiveElapsedRoutes();

            Xunit.Assert.NotNull(allLiveElapsedRoutes);
            Xunit.Assert.NotEmpty(allLiveElapsedRoutes);
            Xunit.Assert.True(allLiveElapsedRoutes.Count() > 0);

            Xunit.Assert.IsAssignableFrom<IEnumerable<ElapsedGeospatialInformation>>(allLiveElapsedRoutes);
        }
    }
}
