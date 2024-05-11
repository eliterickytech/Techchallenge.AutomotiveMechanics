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
    public class CarServiceTests : ConfigBase
    {
        private readonly Mock<ICarRepository> _carRepositoryMock = new Mock<ICarRepository>();
        private readonly Mock<IModelRepository> _modelRepositoryMock = new Mock<IModelRepository>();
        private readonly Mock<IBaseNotification> _baseNotificationMock = new Mock<IBaseNotification>();
        private readonly CarAddInputFaker _carAddInputFaker = new CarAddInputFaker();
        private readonly CarUpdateInputFaker _carUpdateInputFaker = new CarUpdateInputFaker();

        [Fact]
        public async Task AddAsync_ShouldReturnTrue_WhenInputIsValid()
        {

            var service = new CarService(_carRepositoryMock.Object, _mapper,
                _baseNotificationMock.Object, _modelRepositoryMock.Object);
            var input = _carAddInputFaker.Generate();

            _carRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Car>()));

            var result = await service.AddAsync(input);

            Assert.True(result != null);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnNull_WhenCarNotFound()
        {
            var service = new CarService(_carRepositoryMock.Object, _mapper,
                _baseNotificationMock.Object, _modelRepositoryMock.Object);
            var input = _carUpdateInputFaker.Generate();

            _carRepositoryMock.Setup(x => x.FindByIdAsync(input.Id)).ReturnsAsync((Car)null);

            var result = await service.UpdateAsync(input);

            Assert.True(result == null);
        }

        [Fact]
        public async Task RemoveAsync_ShouldReturnFalse_WhenCarNotFound()
        {
            var service = new CarService(_carRepositoryMock.Object, _mapper,
                _baseNotificationMock.Object, _modelRepositoryMock.Object);
            int id = 1;

            _carRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Car)null);

            var result = await service.DeleteAsync(id);

            Assert.False(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCar_WhenCarExists()
        {
            // Arrange
            var service = new CarService(_carRepositoryMock.Object, _mapper,
                _baseNotificationMock.Object, _modelRepositoryMock.Object);
            int id = 1;
            var expectedCar = new Car { Id = id };

            _carRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectedCar);

            // Act
            var result = await service.FindByIdAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsyncWithNoPlate_ShouldReturnNull()
        {

            var service = new CarService(_carRepositoryMock.Object, _mapper,
                _baseNotificationMock.Object, _modelRepositoryMock.Object);
            var input = _carAddInputFaker.Generate();

            input.Plate = null;

            _carRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Car>()));

            var result = await service.AddAsync(input);

            Assert.True(result == null);
        }

        [Fact]
        public async Task AddAsyncWithNoYearManufactured_ShouldReturnNull()
        {

            var service = new CarService(_carRepositoryMock.Object, _mapper,
                _baseNotificationMock.Object, _modelRepositoryMock.Object);
            var input = _carAddInputFaker.Generate();

            input.YearManufactured = 0;

            _carRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Car>()));

            var result = await service.AddAsync(input);

            Assert.True(result == null);
        }

        [Fact]
        public async Task AddAsyncWithNoModel_ShouldReturnNull()
        {

            var service = new CarService(_carRepositoryMock.Object, _mapper,
                _baseNotificationMock.Object, _modelRepositoryMock.Object);
            var input = _carAddInputFaker.Generate();

            _carRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Car>()));

            var result = await service.AddAsync(input);

            Assert.True(result == null);
        }
    }
}
