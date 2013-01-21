using System;
using System.Collections.Generic;
using DomainModel.Interfaces.Repositories;
using DomainModel.Models;
using DomainModel.Models.GISModels;
using DomainModel.Services;
using Moq;
using Xunit;

namespace DomainModel.Test.Tests
{
    public class GISServiceTests
    {
        private Mock<IGISRepository> _repository;

        public GISServiceTests()
        {
            _repository = new Mock<IGISRepository>();

            _repository.Setup(m => m.AddGIS(It.IsAny<GeographicInformation>()))
                .Verifiable();

            _repository.Setup(m => m.GetGIS())
                .Returns(new GeographicInformation(DateTime.Now, 1, 1, 0, 270));

            _repository.Setup(m => m.GetAllGISData())
                .Returns(new List<GeographicInformation>() 
                { 
                    new GeographicInformation(DateTime.Now, 1, 1, 0, 270), 
                    new GeographicInformation(DateTime.Now, 2, 2, 0, 270) 
                });
        }

        [Fact]
        public void PassingNullToConstructorShouldThrow()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() => new GISService(null));
        }

        [Fact]
        public void PassingRepositoryToConstructorShouldWork()
        {
            Xunit.Assert.DoesNotThrow(() => new GISService(_repository.Object));

            var service = new GISService(_repository.Object);

            Xunit.Assert.NotNull(service);
            Xunit.Assert.IsAssignableFrom<GISService>(service);
        }

        [Fact]
        public void PassingNullGISObjectToGetGISShouldThrow()
        {
            var service = new GISService(_repository.Object);
            Xunit.Assert.Throws<ArgumentNullException>(() => service.AddGIS(null));
        }

        [Fact]
        public void PassingGISObjectToGetGISIsVerifiable()
        {
            var service = new GISService(_repository.Object);
            
            service.AddGIS(new GeographicInformation(DateTime.Now, 1, 2, 0, 270));
        }
    }
}
