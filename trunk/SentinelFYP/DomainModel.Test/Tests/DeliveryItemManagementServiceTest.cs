using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using DomainModel.Models.AssetModels;
using DomainModel.Services;
using Moq;
using Xunit;

namespace DomainModel.Test.Tests
{
    public class DeliveryItemManagementServiceTest
    {
        private Mock<IDeliveryItemManagementRepository> _repository;

        public static List<DeliveryItem> _unassignedDeliveryItems;
        public static List<AssignedDeliveryItem> _assignedDeliveryItems;
        public static List<AssignedConsignment> _assignedConsignments;

        public DeliveryItemManagementServiceTest()
        {
            ResetCollections();

            _repository = new Mock<IDeliveryItemManagementRepository>();

            _repository.Setup(m => m.AssignDeliveryItemsToConsignment(It.IsAny<IEnumerable<Guid>>(), It.IsAny<Guid>()))
                .Callback<IEnumerable<Guid>, Guid>((itemKeys, consignmentKey) => DeliveryItemManagementServiceTestHelper.AssignDeliveryItemsToConsignment(itemKeys, consignmentKey));

            _repository.Setup(m => m.UnAssignDeliveryItems(It.IsAny<Guid>(), It.IsAny<IEnumerable<Guid>>()))
                .Callback<Guid, IEnumerable<Guid>>((consignmentKey, itemKeys) => DeliveryItemManagementServiceTestHelper.UnAssignDeliveryItems(consignmentKey, itemKeys));

            _repository.Setup(m => m.GetDeliveryItemsByKey(It.IsAny<IEnumerable<Guid>>()))
                .Returns<IEnumerable<Guid>>((itemKeys) => from item in _assignedDeliveryItems
                                                          where itemKeys.Any(k => k == item.DeliveryItemKey)
                                                          select item);

            _repository.Setup(m => m.ReAssignDeliveryItems(It.IsAny<IEnumerable<Guid>>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Callback<IEnumerable<Guid>, Guid, Guid>((itemKeys, previousKey, reassignedKey) => DeliveryItemManagementServiceTestHelper.ReAssignDeliveryItems(itemKeys, previousKey, reassignedKey));

            _repository.Setup(m => m.GetConsignmentDeliveryItems(It.IsAny<Guid>()))
                .Returns<Guid>((consignmentKey) => from item in _assignedDeliveryItems
                                                   where item.ConsignmentKey == consignmentKey
                                                   select item);

            _repository.Setup(m => m.GetAllAssignedDeliveryItems())
                .Returns(() => _assignedDeliveryItems);

            _repository.Setup(m => m.GetAllUnassignedDeliveryItems())
                .Returns(() => _unassignedDeliveryItems);
        }

        [Fact]
        public void TestConstructor()
        {
            DeliveryItemManagementService target = null;
            Assert.Throws<ArgumentNullException>(() => target = new DeliveryItemManagementService(null));
            Assert.Null(target);
        }

        [Fact]
        public void TestConstructorShouldThrowWhenSuppliedNullArguments()
        {
            DeliveryItemManagementService target = null;
            Assert.DoesNotThrow(() => target = new DeliveryItemManagementService(_repository.Object));
            Assert.NotNull(target);
            Assert.IsAssignableFrom<DeliveryItemManagementService>(target);
        }

        [Fact]
        public void TestAssignDeliveryItemsToConsignment()
        {
            var target = new DeliveryItemManagementService(_repository.Object);

            var keys = from item in _unassignedDeliveryItems.Take(2)
                       select item.DeliveryItemKey;

            var consignmentKey = (from consignments in _assignedConsignments.Take(1)
                                  select consignments.ConsignmentKey).First();

            target.AssignDeliveryItemsToConsignment(keys, consignmentKey);

            Assert.Equal(1, _unassignedDeliveryItems.Count);
            Assert.Equal(5, _assignedDeliveryItems.Count);

            ResetCollections();
        }

        [Fact]
        public void TestUnAssignDeliveryItems()
        {
            var target = new DeliveryItemManagementService(_repository.Object);

            var consignmentKey = (from consignments in _assignedConsignments.Take(1)
                                  select consignments.ConsignmentKey).First();

            var keys = from item in _assignedDeliveryItems.Take(2)
                       select item.DeliveryItemKey;

            target.UnAssignDeliveryItems(consignmentKey, keys);

            Assert.Equal(5, _unassignedDeliveryItems.Count);
            Assert.Equal(1, _assignedDeliveryItems.Count);

            ResetCollections();
        }

        [Fact]
        public void TestGetDeliveryItemsByKey()
        {
            var target = new DeliveryItemManagementService(_repository.Object);

            var assignedKeys = from item in _assignedDeliveryItems.Take(3)
                               select item.DeliveryItemKey;

            var assignedDeliveryItems = target.GetDeliveryItemsByKey(assignedKeys);

            Assert.NotEmpty(assignedDeliveryItems);
            Assert.Equal(3, assignedDeliveryItems.Count());

            ResetCollections();
        }

        [Fact]
        public void TestGetAllAssignedDeliveryItems()
        {
            var target = new DeliveryItemManagementService(_repository.Object);
            var assignedItems = target.GetAllAssignedDeliveryItems();

            Assert.NotEmpty(assignedItems);

            foreach (var item in assignedItems)
            {
                Assert.NotNull(item.ConsignmentKey);
                Assert.IsAssignableFrom<AssignedDeliveryItem>(item);
            }

            ResetCollections();
        }

        [Fact]
        public void TestGetAllUnassignedDeliveryItems()
        {
            var target = new DeliveryItemManagementService(_repository.Object);
            var unassignedItems = target.GetAllUnassignedDeliveryItems();

            Assert.NotEmpty(unassignedItems);

            foreach (var item in unassignedItems)
            {
                Assert.IsAssignableFrom<DeliveryItem>(item);
            }

            ResetCollections();
        }

        [Fact]
        public void TestGetConsignmentDeliveryItems()
        {
            var target = new DeliveryItemManagementService(_repository.Object);
            var key = _assignedConsignments.Take(1).First();

            var consignmentItems = target.GetConsignmentDeliveryItems(key.ConsignmentKey);

            Assert.NotEmpty(consignmentItems);
            Assert.IsAssignableFrom<IEnumerable<AssignedDeliveryItem>>(consignmentItems);

            foreach (var item in consignmentItems)
            {
                Assert.NotNull(item);
                Assert.IsAssignableFrom<AssignedDeliveryItem>(item);
                Assert.Equal(key.ConsignmentKey, item.ConsignmentKey);
            }

            ResetCollections();
        }

        [Fact]
        public void TestReAssignDeliveryItems()
        {
            var target = new DeliveryItemManagementService(_repository.Object);

            var previousConsignment = _assignedDeliveryItems.First();
            var reassignedConsignment = _assignedDeliveryItems.Skip(2).First();

            var itemKeys = from item in _assignedDeliveryItems
                           where item.ConsignmentKey == previousConsignment.ConsignmentKey
                           select item.DeliveryItemKey;

            target.ReAssignDeliveryItems(itemKeys, previousConsignment.ConsignmentKey, reassignedConsignment.ConsignmentKey);

            var previousConsignmentItems = from item in _assignedDeliveryItems
                                           where item.ConsignmentKey == previousConsignment.ConsignmentKey
                                           select item;

            var reassignedConsignmentItems = from item in _assignedDeliveryItems
                                             where item.ConsignmentKey == reassignedConsignment.ConsignmentKey
                                             select item;

            Assert.NotEqual(previousConsignmentItems.Count(), reassignedConsignmentItems.Count());
            Assert.Empty(previousConsignmentItems);
            Assert.NotEmpty(reassignedConsignmentItems);

            foreach (var item in reassignedConsignmentItems)
            {
                Assert.Equal(item.ConsignmentKey, reassignedConsignment.ConsignmentKey);
            }

            ResetCollections();
        }

        private static class DeliveryItemManagementServiceTestHelper
        {
            public static void AssignDeliveryItemsToConsignment(IEnumerable<Guid> itemKeys, Guid consignmentKey)
            {
                var consignment = _assignedConsignments.Where(k => k.ConsignmentKey == consignmentKey).First();

                var itemsToRemove = new List<DeliveryItem>();

                foreach (var key in itemKeys)
                {
                    var deliveryItem = _unassignedDeliveryItems.Where(k => k.DeliveryItemKey == key).First();
                    _assignedDeliveryItems.Add(new AssignedDeliveryItem(consignment.ConsignmentKey, deliveryItem.DeliveryItemKey, deliveryItem.RecipientKey));
                    itemsToRemove.Add(deliveryItem);
                }

                foreach (var item in itemsToRemove)
                {
                    _unassignedDeliveryItems.Remove(item);
                }

            }

            public static void UnAssignDeliveryItems(Guid oConsignmentKey, IEnumerable<Guid> arrItemKeys)
            {
                var consignment = _assignedConsignments.Where(k => k.ConsignmentKey == oConsignmentKey).First();

                var itemsToRemove = new List<AssignedDeliveryItem>();

                foreach (var key in arrItemKeys)
                {
                    var assignedDeliveryItem = _assignedDeliveryItems.Where(k => k.DeliveryItemKey == key).First();
                    _unassignedDeliveryItems.Add(new DeliveryItem(assignedDeliveryItem.DeliveryItemKey, assignedDeliveryItem.RecipientKey));
                    itemsToRemove.Add(assignedDeliveryItem);
                }

                foreach (var item in itemsToRemove)
                {
                    _assignedDeliveryItems.Remove(item);
                }
            }

            public static void ReAssignDeliveryItems(IEnumerable<Guid> arrItemKeys, Guid oPreviousConsignmentKey, Guid oReassignedConsignmentKey)
            {
                var itemsToRemove = new List<AssignedDeliveryItem>();
                var itemsToAdd = new List<AssignedDeliveryItem>();

                foreach (var item in _assignedDeliveryItems.Where(k => k.ConsignmentKey == oPreviousConsignmentKey))
                {
                    itemsToRemove.Add(item);
                    itemsToAdd.Add(new AssignedDeliveryItem(oReassignedConsignmentKey, item.DeliveryItemKey, item.RecipientKey));
                }

                foreach (var item in itemsToRemove)
                {
                    _assignedDeliveryItems.Remove(item);
                }

                _assignedDeliveryItems.AddRange(itemsToAdd);
            }
        }

        private void ResetCollections()
        {
            _assignedConsignments = new List<AssignedConsignment>()
            {
                new AssignedConsignment(Guid.NewGuid(), Guid.NewGuid(), "", "", 0, DateTime.Now),
                new AssignedConsignment(Guid.NewGuid(), Guid.NewGuid(), "", "", 0, DateTime.Now),
                new AssignedConsignment(Guid.NewGuid(), Guid.NewGuid(), "", "", 0, DateTime.Now)
            };

            _unassignedDeliveryItems = new List<DeliveryItem>()
            {
                new DeliveryItem(Guid.NewGuid(), Guid.NewGuid()),
                new DeliveryItem(Guid.NewGuid(), Guid.NewGuid()),
                new DeliveryItem(Guid.NewGuid(), Guid.NewGuid())
            };

            _assignedDeliveryItems = new List<AssignedDeliveryItem>()
            {
                new AssignedDeliveryItem(_assignedConsignments.First().ConsignmentKey, Guid.NewGuid(), Guid.NewGuid()),
                new AssignedDeliveryItem(_assignedConsignments.Skip(1).First().ConsignmentKey, Guid.NewGuid(), Guid.NewGuid()),
                new AssignedDeliveryItem(_assignedConsignments.Skip(2).First().ConsignmentKey, Guid.NewGuid(), Guid.NewGuid())
            };
        }
    }
}
