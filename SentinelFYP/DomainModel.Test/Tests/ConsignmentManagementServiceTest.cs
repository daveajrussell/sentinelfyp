using System;
using DomainModel.Interfaces.Repositories;
using DomainModel.Models.AssetModels;
using DomainModel.SecurityModels;
using DomainModel.Services;
using DomainModel.Test.TestHelpers;
using Moq;
using Xunit;

namespace DomainModel.Test.Tests
{
    public class ConsignmentManagementServiceTest
    {
        private Mock<IConsignmentManagementRepository> _repository;

        public ConsignmentManagementServiceTest()
        {
            _repository = new Mock<IConsignmentManagementRepository>();

            _repository.Setup(m => m.CreateConsignment(It.IsAny<User>(), It.IsAny<DateTime>()))
                .Returns<User, DateTime>((user, dtConsignmentDeliveryDate) => ConsignmentManagementTestHelper.CreateConsignmentMock(user, dtConsignmentDeliveryDate));

            _repository.Setup(m => m.AssignConsignmentToDriver(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns<Guid, Guid>((oConsignmentKey, oDriverKey) => ConsignmentManagementTestHelper.AssignConsignmentToDriverMock(oConsignmentKey, oDriverKey));

            _repository.Setup(m => m.UnAssignConsignment(It.IsAny<Guid>()));
        }

        [Fact]
        public void InjectingNullIntoConstructorShouldThrow()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() => new ConsignmentManagementService(null));
        }

        [Fact]
        public void CreateConsignmentReturnsNewConsignmentWithID()
        {
            var service = new ConsignmentManagementService(_repository.Object);
            UnAssignedConsignment result = null;

            Xunit.Assert.DoesNotThrow(() => result = service.CreateConsignment(new User(), DateTime.Today));

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<UnAssignedConsignment>(result);

            Xunit.Assert.NotNull(result.ConsignmentKey);
            Xunit.Assert.IsAssignableFrom<Guid>(result.ConsignmentKey);
        }

        [Fact]
        public void PassingEmptyConsignmentGuidToAssignDriverShouldThrow()
        {
            var service = new ConsignmentManagementService(_repository.Object);
            Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => service.AssignConsignmentToDriver(Guid.Empty, new Guid()));
        }

        [Fact]
        public void PassingEmptyDriverGuidToAssignDriverShouldThrow()
        {
            var service = new ConsignmentManagementService(_repository.Object);
            Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => service.AssignConsignmentToDriver(new Guid(), Guid.Empty));
        }

        [Fact]
        public void AssignConsignmentToDriverAssignsADriverToAConsignment()
        {
            var service = new ConsignmentManagementService(_repository.Object);

            UnAssignedConsignment consignment = service.CreateConsignment(new User(), DateTime.Today);
            AssignedConsignment updatedConsignment = null;
            User user = new User() { UserKey = Guid.NewGuid() };
            
            Xunit.Assert.DoesNotThrow(() => updatedConsignment = service.AssignConsignmentToDriver(consignment.ConsignmentKey, user.UserKey));

            Xunit.Assert.NotNull(updatedConsignment);
            Xunit.Assert.Equal(consignment.ConsignmentDateTime, updatedConsignment.ConsignmentDateTime);
        }

        [Fact]
        public void UnAssignConsignmentShouldRemoveAssignedDriver()
        {
            var service = new ConsignmentManagementService(_repository.Object);

            UnAssignedConsignment oUnAssignedConsignment = service.CreateConsignment(new User(), DateTime.Today);
            AssignedConsignment oAssignedConsignment;

            User driver = new User() { UserKey = Guid.NewGuid() };

            oAssignedConsignment = service.AssignConsignmentToDriver(oUnAssignedConsignment.ConsignmentKey, driver.UserKey);
            Xunit.Assert.NotSame(Guid.Empty, oAssignedConsignment.AssignedDriverKey);

            Xunit.Assert.DoesNotThrow(() => service.UnAssignConsignment(oUnAssignedConsignment.ConsignmentKey));
        }
    }
}
