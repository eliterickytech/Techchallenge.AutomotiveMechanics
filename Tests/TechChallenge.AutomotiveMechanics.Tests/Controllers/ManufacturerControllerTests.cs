using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Presentation.API.Controllers;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Tests.Controllers
{
    public class ManufacturerControllerTests
    {
        private readonly Mock<IManufacturerService> _manufacturerServiceMock = new Mock<IManufacturerService>();
        private readonly Mock<IBaseNotification> _baseNotificationMock = new Mock<IBaseNotification>();
        private readonly Fixture _fixture = new Fixture();
        private ManufacturerController _controller;

        [Fact]
        public async Task Get_Manufacturer_ReturnOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);
            var manufacturerList = _fixture.CreateMany<ManufacturerResult>(5).ToList();

            _manufacturerServiceMock.Setup(repo => repo.ListAsync()).ReturnsAsync(manufacturerList);

            _controller = new ManufacturerController(_baseNotificationMock.Object, _manufacturerServiceMock.Object);

            var result = await _controller.Get();

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async Task GetById_Manufacturer_ReturnOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);
            var manufacturer = _fixture.Create<ManufacturerResult>();

            _manufacturerServiceMock.Setup(repo => repo.FindByIdAsync(manufacturer.Id)).ReturnsAsync(manufacturer);

            _controller = new ManufacturerController(_baseNotificationMock.Object, _manufacturerServiceMock.Object);

            var result = await _controller.Get(manufacturer.Id);

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async Task Create_Manufacturer_ReturnsCreated()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);

            var manufacturer = _fixture.Create<ManufacturerInsertInput>();
            var expectManufacturer = new ManufacturerResult();

            _manufacturerServiceMock.Setup(repo => repo.AddAsync(manufacturer)).ReturnsAsync(expectManufacturer);

            _controller = new ManufacturerController(_baseNotificationMock.Object, _manufacturerServiceMock.Object);

            var result = await _controller.Post(manufacturer);

            var obj = result as ObjectResult;

            Assert.Equal(201, obj.StatusCode);
        }

        [Fact]
        public async Task Update_Manufacturer_ReturnsOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);

            var manufacturer = _fixture.Create<ManufacturerUpdateInput>();
            var expectManufacturer = new ManufacturerResult();

            _manufacturerServiceMock.Setup(repo => repo.UpdateAsync(manufacturer)).ReturnsAsync(expectManufacturer);

            _controller = new ManufacturerController(_baseNotificationMock.Object, _manufacturerServiceMock.Object);

            var result = await _controller.Put(manufacturer);

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async Task Delete_Manufacturer_ReturnsOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);

            var manufacturer = _fixture.Create<ManufacturerResult>();

            _controller = new ManufacturerController(_baseNotificationMock.Object, _manufacturerServiceMock.Object);

            var result = await _controller.Delete(manufacturer.Id);

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }
    }
}
