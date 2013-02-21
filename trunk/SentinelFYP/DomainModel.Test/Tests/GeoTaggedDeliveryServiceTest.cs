using DomainModel.Interfaces.Repositories;
using DomainModel.Models.AssetModels;
using DomainModel.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DomainModel.Test.Tests
{
    public class GeoTaggedDeliveryServiceTest
    {
        private Mock<IGeoTaggedDeliveryRepository> _repository;
        private List<GeoTaggedDeliveryItem> _items;

        private GeoTaggedDeliveryItem itemOne;
        private GeoTaggedDeliveryItem itemTwo;

        public GeoTaggedDeliveryServiceTest()
        {
            _items = new List<GeoTaggedDeliveryItem>();

            itemOne = new GeoTaggedDeliveryItem() { AssetKey = Guid.NewGuid(), DriverKey = Guid.NewGuid(), Latitude = 52, Longitude = -2, TimeStamp = DateTime.Now };
            itemTwo = new GeoTaggedDeliveryItem() { AssetKey = Guid.NewGuid(), DriverKey = Guid.NewGuid(), Latitude = 52, Longitude = -2, TimeStamp = DateTime.Now };

            _repository = new Mock<IGeoTaggedDeliveryRepository>();

            _repository.Setup(m => m.SubmitGeoTaggedDeliveryItem(It.IsAny<GeoTaggedDeliveryItem>()))
                .Callback<GeoTaggedDeliveryItem>((item) => _items.Add(item));

            _repository.Setup(m => m.UnTagDelivery(It.IsAny<Guid>()))
                .Callback<Guid>((itemKey) => _items.Remove(_items.Where(k => k.AssetKey == itemKey).First()));
        }

        [Fact]
        public void TestGeoTaggedDeliveryServiceConstructorShouldThrow()
        {
            GeoTaggedDeliveryService service = null;

            Xunit.Assert.Throws<ArgumentNullException>(() => service = new GeoTaggedDeliveryService(null));
            Xunit.Assert.Null(service);
        }

        [Fact]
        public void TestGEoTaggedDeliveryServiceConstructor()
        {
            GeoTaggedDeliveryService service = null;

            Xunit.Assert.DoesNotThrow(() => service = new GeoTaggedDeliveryService(_repository.Object));
            Xunit.Assert.NotNull(service);
            Xunit.Assert.IsAssignableFrom<GeoTaggedDeliveryService>(service);
        }

        [Fact]
        public void TestSubmitGeoTaggedDeliveryItem()
        {
            var service = new GeoTaggedDeliveryService(_repository.Object);

            Xunit.Assert.Empty(_items);
            Xunit.Assert.DoesNotThrow(() => service.SubmitGeoTaggedDeliveryItem(itemOne));
            Xunit.Assert.NotEmpty(_items);
            Xunit.Assert.Contains(itemOne, _items);
        }

        [Fact]
        public void TestUnTagDelivery()
        {
            var service = new GeoTaggedDeliveryService(_repository.Object);

            service.SubmitGeoTaggedDeliveryItem(itemOne);

            Xunit.Assert.NotEmpty(_items);
            Xunit.Assert.DoesNotThrow(() => service.UnTagDelivery(itemOne.AssetKey));
            Xunit.Assert.DoesNotContain(itemOne, _items);
            Xunit.Assert.Empty(_items);
        }
    }
}
