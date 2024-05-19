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
    public class ServiceControllerTests
    {
        private readonly Mock<IServiceService> _serviceServiceMock = new Mock<IServiceService>();
        private readonly Mock<IBaseNotification> _baseNotificationMock = new Mock<IBaseNotification>();
        private readonly Fixture _fixture = new Fixture();
        private ServiceController _controller;

        [Fact]
        public async Task Get_Service_ReturnOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);
            var serviceList = _fixture.CreateMany<ServiceResult>(3).ToList();

            _serviceServiceMock.Setup(repo => repo.ListAsync()).ReturnsAsync(serviceList);

            _controller = new ServiceController(_baseNotificationMock.Object, _serviceServiceMock.Object);

            var result = await _controller.Get();

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async Task GetById_Service_ReturnOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);
            var service = _fixture.Create<ServiceResult>();

            _serviceServiceMock.Setup(repo => repo.FindByIdAsync(service.Id)).ReturnsAsync(service);

            _controller = new ServiceController(_baseNotificationMock.Object, _serviceServiceMock.Object);

            var result = await _controller.Get(service.Id);

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async Task Create_Service_ReturnsCreated()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);

            var service = _fixture.Create<ServiceInsertInput>();
            var expectService = new ServiceResult();

            _serviceServiceMock.Setup(repo => repo.AddAsync(service)).ReturnsAsync(expectService);

            _controller = new ServiceController(_baseNotificationMock.Object, _serviceServiceMock.Object);

            var result = await _controller.Post(service);

            var obj = result as ObjectResult;

            Assert.Equal(201, obj.StatusCode);
        }

        [Fact]
        public async Task Update_Service_ReturnsOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);

            var service = _fixture.Create<ServiceUpdateInput>();
            var expectService = new ServiceResult();

            _serviceServiceMock.Setup(repo => repo.UpdateAsync(service)).ReturnsAsync(expectService);

            _controller = new ServiceController(_baseNotificationMock.Object, _serviceServiceMock.Object);

            var result = await _controller.Put(service);

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async Task Delete_Service_ReturnsOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);

            var service = _fixture.Create<ServiceResult>();

            _controller = new ServiceController(_baseNotificationMock.Object, _serviceServiceMock.Object);

            var result = await _controller.Delete(service.Id);

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }
    }
}
