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

            _repository.Setup(m => m.GetDrivers(It.IsAny<User>()))
                .Returns(() => new List<User>() { new User(), new User()});
        }

        [Fact]
        public void TestConstructor()
        {
            HistoricalTrackingService target = null;
            Xunit.Assert.DoesNotThrow(() => target = new HistoricalTrackingService(_repository.Object));

            Xunit.Assert.NotNull(target);
            Xunit.Assert.IsAssignableFrom<HistoricalTrackingService>(target);
        }

        [Fact]
        public void TestConstructorShouldThrow()
        {
            HistoricalTrackingService target = null;
            Xunit.Assert.Throws<ArgumentNullException>(() => target = new HistoricalTrackingService(null));
            Xunit.Assert.Null(target);
        }

        [Fact]
        public void TestGetAllHistoricalTrackingDataByDriverKey()
        {
            var target = new HistoricalTrackingService(_repository.Object);
            var data = target.GetAllHistoricalTrackingDataByDriverKey(Guid.NewGuid());

            Xunit.Assert.IsAssignableFrom<IEnumerable<HistoricalGeospatialInformation>>(data);
            Xunit.Assert.NotEmpty(data);
            Xunit.Assert.True(data.Count() > 0);

            foreach (var item in data)
            {
		        Xunit.Assert.NotNull(item);
                Xunit.Assert.NotEmpty(item.PeriodGeographicData);

                foreach (var geographicData in item.PeriodGeographicData)
	            {
                    Xunit.Assert.IsAssignableFrom<GeospatialInformation>(geographicData);
                    Xunit.Assert.NotNull(geographicData);
	            }
            }
        }

        [Fact]
        public void TestGetFilteredHistoricalDataByDriverKey()
        {
            var target = new HistoricalTrackingService(_repository.Object);
            var data = target.GetFilteredHistoricalDataByDriverKey(Guid.NewGuid(), new Random().Next());

            Xunit.Assert.IsAssignableFrom<HistoricalGeospatialInformation>(data);
            Xunit.Assert.NotNull(data);

            Xunit.Assert.NotEmpty(data.PeriodGeographicData);
            Xunit.Assert.True(data.PeriodGeographicData.Count() > 0);

            foreach (var item in data.PeriodGeographicData)
            {
                Xunit.Assert.IsAssignableFrom<GeospatialInformation>(item);
                Xunit.Assert.NotNull(item);
            }
        }

        [Fact]
        public void TestGetDrivers()
        {
            var target = new HistoricalTrackingService(_repository.Object);
            var data = target.GetDrivers(new User());

            Xunit.Assert.IsAssignableFrom<IEnumerable<User>>(data);
            Xunit.Assert.NotEmpty(data);
            Xunit.Assert.True(data.Count() > 0);
        }
    }
}
