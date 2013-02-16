using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Models.GISModels;
using DomainModel.SecurityModels;
using DomainModel.Services;
using Moq;
using Xunit;

namespace DomainModel.Test.Tests
{
    public class HistoricalTrackingServiceTests
    {
        private Mock<IHistoricalTrackingRepository> _repository;

        public HistoricalTrackingServiceTests()
        {
            _repository = new Mock<IHistoricalTrackingRepository>();

            _repository.Setup(m => m.GetAllHistoricalTrackingDataByDriverKey(It.IsAny<Guid>()))
                .Returns(() => new List<HistoricalGeospatialInformation>() 
                                    { 
                                        new HistoricalGeospatialInformation() 
                                        { 
                                            DriverKey = Guid.NewGuid(), 
                                            HistoricalSessionID = new Random().Next(), 
                                            Period = DateTime.Now, 
                                            PeriodGeographicData = new List<GeospatialInformation>() 
                                                                        { 
                                                                            new GeospatialInformation(), 
                                                                            new GeospatialInformation() 
                                                                        } 
                                        },
                                        new HistoricalGeospatialInformation() 
                                        { 
                                            DriverKey = Guid.NewGuid(), 
                                            HistoricalSessionID = new Random().Next(), 
                                            Period = DateTime.Now, 
                                            PeriodGeographicData = new List<GeospatialInformation>() 
                                                                        { 
                                                                            new GeospatialInformation(), 
                                                                            new GeospatialInformation() 
                                                                        } 
                                        }
                                    });

            _repository.Setup(m => m.GetFilteredHistoricalDataByDriverKey(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(() => new HistoricalGeospatialInformation() 
                                    { 
                                        DriverKey = Guid.NewGuid(), 
                                        HistoricalSessionID = new Random().Next(), 
                                        Period = DateTime.Now, 
                                        PeriodGeographicData = new List<GeospatialInformation>() 
                                                                    { 
                                                                        new GeospatialInformation(), 
                                                                        new GeospatialInformation() 
                                                                    } 
                                    });

            _repository.Setup(m => m.GetDrivers())
                .Returns(() => new List<User>() { new User(), new User()});
        }

        [Fact]
        public void TestConstructor()
        {
            HistoricalTrackingService target = null;
            Assert.DoesNotThrow(() => target = new HistoricalTrackingService(_repository.Object));

            Assert.NotNull(target);
            Assert.IsAssignableFrom<HistoricalTrackingService>(target);
        }

        [Fact]
        public void TestConstructorShouldThrow()
        {
            HistoricalTrackingService target = null;
            Assert.Throws<ArgumentNullException>(() => target = new HistoricalTrackingService(null));
            Assert.Null(target);
        }

        [Fact]
        public void TestGetAllHistoricalTrackingDataByDriverKey()
        {
            var target = new HistoricalTrackingService(_repository.Object);
            var data = target.GetAllHistoricalTrackingDataByDriverKey(Guid.NewGuid());

            Assert.IsAssignableFrom<IEnumerable<HistoricalGeospatialInformation>>(data);
            Assert.NotEmpty(data);
            Assert.True(data.Count() > 0);

            foreach (var item in data)
            {
		        Assert.NotNull(item);
                Assert.NotEmpty(item.PeriodGeographicData);

                foreach (var geographicData in item.PeriodGeographicData)
	            {
                    Assert.IsAssignableFrom<GeospatialInformation>(geographicData);
                    Assert.NotNull(geographicData);
	            }
            }
        }

        [Fact]
        public void TestGetFilteredHistoricalDataByDriverKey()
        {
            var target = new HistoricalTrackingService(_repository.Object);
            var data = target.GetFilteredHistoricalDataByDriverKey(Guid.NewGuid(), new Random().Next());

            Assert.IsAssignableFrom<HistoricalGeospatialInformation>(data);
            Assert.NotNull(data);

            Assert.NotEmpty(data.PeriodGeographicData);
            Assert.True(data.PeriodGeographicData.Count() > 0);

            foreach (var item in data.PeriodGeographicData)
            {
                Assert.IsAssignableFrom<GeospatialInformation>(item);
                Assert.NotNull(item);
            }
        }

        [Fact]
        public void TestGetDrivers()
        {
            var target = new HistoricalTrackingService(_repository.Object);
            var data = target.GetDrivers();

            Assert.IsAssignableFrom<IEnumerable<User>>(data);
            Assert.NotEmpty(data);
            Assert.True(data.Count() > 0);
        }
    }
}
