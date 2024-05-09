using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Services;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;

namespace TechChallenge.AutomotiveMechanics.Tests.Services
{
    public class ServiceServiceTests
    {
        private readonly Mock<IServiceRepository> _serviceRepositoryMock = new Mock<IServiceRepository>();
        private readonly Mock<ICarRepository> _carRepositoryMock = new Mock<ICarRepository>();
        private readonly Mock<IBaseNotification> _baseNotificationMock = new Mock<IBaseNotification>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly ServiceAddInputFaker _serviceAddInputFaker = new ServiceAddInputFaker();
        private readonly ServiceUpdateInputFaker _serviceUpdateInputFaker = new ServiceUpdateInputFaker();

        [Fact]
        public async Task AddAsync_ShouldReturnTrue_WhenInputIsValid()
        {

            var service = new ServiceService(_mapperMock.Object, _serviceRepositoryMock.Object,
                _baseNotificationMock.Object, _carRepositoryMock.Object);
            var input = _serviceAddInputFaker.Generate();

            var result = await service.AddAsync(input);

            Assert.True(result != null);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenServiceNotFound()
        {
            var service = new ServiceService(_mapperMock.Object, _serviceRepositoryMock.Object,
                _baseNotificationMock.Object, _carRepositoryMock.Object);
            var input = _serviceUpdateInputFaker.Generate();

            _serviceRepositoryMock.Setup(x => x.FindByIdAsync(input.Id)).ReturnsAsync((Service)null);

            var result = await service.UpdateAsync(input);

            Assert.False(result == null);
        }

        [Fact]
        public async Task RemoveAsync_ShouldReturnFalse_WhenServiceNotFound()
        {
            var service = new ServiceService(_mapperMock.Object, _serviceRepositoryMock.Object,
                _baseNotificationMock.Object, _carRepositoryMock.Object);
            int id = 1;

            _serviceRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Service)null);

            var result = await service.DeleteAsync(id);

            Assert.False(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnService_WhenServiceExists()
        {
            // Arrange
            var service = new ServiceService(_mapperMock.Object, _serviceRepositoryMock.Object,
                _baseNotificationMock.Object, _carRepositoryMock.Object);
            int id = 1;
            var expectedService = new Service { Id = id };

            _serviceRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectedService);

            // Act
            var result = await service.FindByIdAsync(id);

            Assert.Null(result);
        }
    }
}
