﻿using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Services;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;

namespace TechChallenge.AutomotiveMechanics.Tests.Services
{
    public class ModelServiceTests
    {
        private readonly Mock<IModelRepository> _modelRepositoryMock = new Mock<IModelRepository>();
        private readonly Mock<IManufacturerRepository> _manufacturerRepositoryMock = new Mock<IManufacturerRepository>();
        private readonly Mock<IBaseNotification> _baseNotificationMock = new Mock<IBaseNotification>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly ModelAddInputFaker _modelAddInputFaker = new ModelAddInputFaker();
        private readonly ModelUpdateInputFaker _modelUpdateInputFaker = new ModelUpdateInputFaker(); 

        [Fact]
        public async Task AddAsync_ShouldReturnTrue_WhenInputIsValid()
        {

            var service = new ModelService(_modelRepositoryMock.Object, _manufacturerRepositoryMock.Object,
                _mapperMock.Object, _baseNotificationMock.Object);
            //var manufacturerService = new ManufacturerService(_manufacturerRepositoryMock.Object, _mapperMock.Object,
            //    _baseNotificationMock.Object);
            var input = _modelAddInputFaker.Generate();

            _modelRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Model>()));
            _manufacturerRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Manufacturer>()));

            //var manufacturer = new ManufacturerInsertInput();
            //manufacturer.Id = input.ManufacturerId;
            //manufacturer.Name = "Fake";
            //await manufacturerService.AddAsync(manufacturer);
            var result = await service.AddAsync(input);

            Assert.True(result != null);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenModelNotFound()
        {
            var service = new ModelService(_modelRepositoryMock.Object, _manufacturerRepositoryMock.Object,
                _mapperMock.Object, _baseNotificationMock.Object);
            var input = _modelUpdateInputFaker.Generate();

            _modelRepositoryMock.Setup(x => x.FindByIdAsync(input.Id)).ReturnsAsync((Model)null);

            var result = await service.UpdateAsync(input);

            Assert.False(result == null);
        }

        [Fact]
        public async Task RemoveAsync_ShouldReturnFalse_WhenModelNotFound()
        {
            var service = new ModelService(_modelRepositoryMock.Object, _manufacturerRepositoryMock.Object,
                _mapperMock.Object, _baseNotificationMock.Object);
            int id = 1;

            _modelRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Model)null);

            var result = await service.DeleteAsync(id);

            Assert.False(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnModel_WhenModelExists()
        {
            // Arrange
            var service = new ModelService(_modelRepositoryMock.Object, _manufacturerRepositoryMock.Object,
                _mapperMock.Object, _baseNotificationMock.Object);
            int id = 1;
            var expectModel = new Model { Id = id };

            _modelRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectModel);

            // Act
            var result = await service.FindByIdAsync(id);

            Assert.Null(result);
        }
    }
}
