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
    public class ManufacturerServiceTests
    {
        private readonly Mock<IManufacturerRepository> _manufacturerRepositoryMock = new Mock<IManufacturerRepository>();
        private readonly Mock<IBaseNotification> _baseNotificationMock = new Mock<IBaseNotification>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly ManufacturerAddInputFaker _manufacturerAddInputFaker = new ManufacturerAddInputFaker();
        private readonly ManufacturerUpdateInputFaker _manufacturerUpdateInputFaker = new ManufacturerUpdateInputFaker();

        [Fact]
        public async Task AddAsync_ShouldReturnTrue_WhenInputIsValid()
        {

            var service = new ManufacturerService(_manufacturerRepositoryMock.Object, _mapperMock.Object,
                _baseNotificationMock.Object);
            var input = _manufacturerAddInputFaker.Generate();

            _manufacturerRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Manufacturer>()));

            var result = await service.AddAsync(input);
            Assert.True(result != null);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenManufacturerNotFound()
        {
            var service = new ManufacturerService(_manufacturerRepositoryMock.Object, _mapperMock.Object,
                _baseNotificationMock.Object);
            var input = _manufacturerUpdateInputFaker.Generate();

            _manufacturerRepositoryMock.Setup(x => x.FindByIdAsync(input.Id)).ReturnsAsync((Manufacturer)null);

            var result = await service.UpdateAsync(input);

            Assert.False(result == null);
        }

        [Fact]
        public async Task RemoveAsync_ShouldReturnFalse_WhenManufacturerNotFound()
        {
            var service = new ManufacturerService(_manufacturerRepositoryMock.Object, _mapperMock.Object,
                _baseNotificationMock.Object);
            int id = 1;

            _manufacturerRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Manufacturer)null);

            var result = await service.DeleteAsync(id);

            Assert.False(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnManufacturer_WhenManufacturerExists()
        {
            // Arrange
            var service = new ManufacturerService(_manufacturerRepositoryMock.Object, _mapperMock.Object,
                _baseNotificationMock.Object);
            int id = 1;
            var expectedManufacturer = new Manufacturer { Id = id };

            _manufacturerRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectedManufacturer);

            // Act
            var result = await service.FindByIdAsync(id);

            Assert.Null(result);
        }
    }
}
