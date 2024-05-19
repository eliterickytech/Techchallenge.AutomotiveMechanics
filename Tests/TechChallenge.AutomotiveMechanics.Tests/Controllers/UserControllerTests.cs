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
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();
        private readonly Mock<IBaseNotification> _baseNotificationMock = new Mock<IBaseNotification>();
        private readonly Fixture _fixture = new Fixture();
        private UserController _controller;

        [Fact]
        public async Task Login_User_ReturnOk()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);
            var user = _fixture.Create<UserLoginInput>();
            var expectedUser = new UserResult();

            _userServiceMock.Setup(repo => repo.Login(user)).ReturnsAsync(expectedUser);

            _controller = new UserController(_baseNotificationMock.Object, _userServiceMock.Object);

            var result = await _controller.Login(user);

            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async Task Register_User_ReturnsCreated()
        {
            _baseNotificationMock.Setup(x => x.IsValid).Returns(true);

            var user = _fixture.Create<UserRegisterInput>();
            var expectUser = new UserResult();

            _userServiceMock.Setup(repo => repo.Register(user)).ReturnsAsync(expectUser);

            _controller = new UserController(_baseNotificationMock.Object, _userServiceMock.Object);

            var result = await _controller.Register(user);

            var obj = result as ObjectResult;

            Assert.Equal(201, obj.StatusCode);
        }
    }
}
