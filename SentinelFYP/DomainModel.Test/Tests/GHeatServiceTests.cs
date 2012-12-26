using System;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using DomainModel.Services;
using DomainModel.Test.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DomainModel.Test.Tests
{
    [TestClass]
    public class GHeatServiceTests
    {
        private Mock<IGHeatRepository> _repository;

        [TestInitialize]
        public void Init()
        {
            _repository = new Mock<IGHeatRepository>();

            _repository.Setup(m => m.AvailableColourSchemes())
                .Returns(GHeatServiceTestHelper.AvailableColourSchemesMock());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InjectingNullIntoConstructorShouldThrow()
        {
            var service = new GHeatService(null);
        }

        [TestMethod]
        public void InjectingInterfaceIntoConstructorShouldWork()
        {
            var service = new GHeatService(_repository.Object);

            Xunit.Assert.NotNull(service);
            Xunit.Assert.IsAssignableFrom<GHeatService>(service);
        }
    }
}
