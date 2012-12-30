using System;
using DomainModel.Interfaces.Repositories;
using DomainModel.Services;
using Moq;
using Xunit;

namespace DomainModel.Test.Tests
{
    public class SettingsServiceTests
    {
        private Mock<ISettingsRepository> _repository;

        public SettingsServiceTests()
        {
            _repository = new Mock<ISettingsRepository>();

            _repository.Setup(m => m.BaseDirectory)
                .Returns(Environment.CurrentDirectory);
        }

        [Fact]
        public void InjectingNullReferenceIntoConstructorShouldThrow()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() => new SettingsService(null));
        }

        [Fact]
        public void InjectingRepositoryReferenceIntoConstructorShouldNotThrow()
        {
            Xunit.Assert.DoesNotThrow(() => new SettingsService(_repository.Object));

            SettingsService service = new SettingsService(_repository.Object);
            Xunit.Assert.NotNull(service);
            Xunit.Assert.IsAssignableFrom<SettingsService>(service);
        }
    }
}
