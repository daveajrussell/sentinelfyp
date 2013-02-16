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
            Assert.Throws<ArgumentNullException>(() => new SettingsService(null));
        }

        [Fact]
        public void InjectingRepositoryReferenceIntoConstructorShouldNotThrow()
        {
            Assert.DoesNotThrow(() => new SettingsService(_repository.Object));

            SettingsService service = new SettingsService(_repository.Object);
            Assert.NotNull(service);
            Assert.IsAssignableFrom<SettingsService>(service);
        }

        [Fact]
        public void BaseDirectoryReturnsCurrentWorkingDirectory()
        {
            var service = new SettingsService(_repository.Object);
            var result = service.BaseDirectory;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<string>(result);
            Assert.Equal(Environment.CurrentDirectory, result);
        }
    }
}
