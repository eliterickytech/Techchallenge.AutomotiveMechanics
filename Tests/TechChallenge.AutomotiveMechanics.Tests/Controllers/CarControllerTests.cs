using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Presentation.API.Controllers;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Tests.Controllers
{
    public class CarControllerTests 
    {
        private readonly Mock<ICarService> _carServiceMock = new Mock<ICarService>();
        private readonly Mock<IBaseNotification> _baseNotificationMock = new Mock<IBaseNotification>();
        private readonly Fixture _fixture = new Fixture();
        private CarController _controller;

        [Fact]
        public async Task Get_Car_ReturnOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);
            var carList = _fixture.CreateMany<CarResult>(3).ToList();

            _carServiceMock.Setup(repo => repo.ListAsync()).ReturnsAsync(carList);

            _controller = new CarController(_baseNotificationMock.Object, _carServiceMock.Object);

            var result = await _controller.Get();

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async Task GetById_Car_ReturnOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);
            var car = _fixture.Create<CarResult>();

            _carServiceMock.Setup(repo => repo.FindByIdAsync(car.Id)).ReturnsAsync(car);

            _controller = new CarController(_baseNotificationMock.Object, _carServiceMock.Object);

            var result = await _controller.Get(car.Id);

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async Task Create_Car_ReturnsCreated()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);

            var car = _fixture.Create<CarInsertInput>();
            var expectCar = new CarResult();

            _carServiceMock.Setup(repo => repo.AddAsync(car)).ReturnsAsync(expectCar);

            _controller = new CarController(_baseNotificationMock.Object, _carServiceMock.Object);

            var result = await _controller.Post(car);

            var obj = result as ObjectResult;

            Assert.Equal(201, obj.StatusCode);
        }

        [Fact]
        public async Task Update_Car_ReturnsOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);

            var car = _fixture.Create<CarUpdateInput>();
            var expectCar = new CarResult();

            _carServiceMock.Setup(repo => repo.UpdateAsync(car)).ReturnsAsync(expectCar);

            _controller = new CarController(_baseNotificationMock.Object, _carServiceMock.Object);

            var result = await _controller.Put(car);

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async Task Delete_Car_ReturnsOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);

            var car = _fixture.Create<CarResult>();

            _controller = new CarController(_baseNotificationMock.Object, _carServiceMock.Object);

            var result = await _controller.Delete(car.Id);

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }
    }
}
