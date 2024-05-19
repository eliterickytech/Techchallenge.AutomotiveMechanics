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
    public class ModelControllerTests
    {
        private readonly Mock<IModelService> _modelServiceMock = new Mock<IModelService>();
        private readonly Mock<IBaseNotification> _baseNotificationMock = new Mock<IBaseNotification>();
        private readonly Fixture _fixture = new Fixture();
        private ModelController _controller;

        [Fact]
        public async Task Get_Model_ReturnOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);
            var modelList = _fixture.CreateMany<ModelResult>(3).ToList();

            _modelServiceMock.Setup(repo => repo.ListAsync()).ReturnsAsync(modelList);

            _controller = new ModelController(_baseNotificationMock.Object, _modelServiceMock.Object);

            var result = await _controller.Get();

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async Task GetById_Model_ReturnOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);
            var model = _fixture.Create<ModelResult>();

            _modelServiceMock.Setup(repo => repo.FindByIdAsync(model.Id)).ReturnsAsync(model);

            _controller = new ModelController(_baseNotificationMock.Object, _modelServiceMock.Object);

            var result = await _controller.Get(model.Id);

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async Task Create_Model_ReturnsCreated()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);

            var model = _fixture.Create<ModelInsertInput>();
            var expectModel = new ModelResult();

            _modelServiceMock.Setup(repo => repo.AddAsync(model)).ReturnsAsync(expectModel);

            _controller = new ModelController(_baseNotificationMock.Object, _modelServiceMock.Object);

            var result = await _controller.Post(model);

            var obj = result as ObjectResult;

            Assert.Equal(201, obj.StatusCode);
        }

        [Fact]
        public async Task Update_Model_ReturnsOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);

            var model = _fixture.Create<ModelUpdateInput>();
            var expectModel = new ModelResult();

            _modelServiceMock.Setup(repo => repo.UpdateAsync(model)).ReturnsAsync(expectModel);

            _controller = new ModelController(_baseNotificationMock.Object, _modelServiceMock.Object);

            var result = await _controller.Put(model);

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async Task Delete_Model_ReturnsOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);

            var model = _fixture.Create<ModelResult>();

            _controller = new ModelController(_baseNotificationMock.Object, _modelServiceMock.Object);

            var result = await _controller.Delete(model.Id);

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }
    }
}
