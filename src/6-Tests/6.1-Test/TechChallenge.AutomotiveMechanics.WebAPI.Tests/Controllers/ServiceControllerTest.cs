using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Presentation.API.Controllers;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;
using TechChallenge.AutomotiveMechanics.Services.Business.Services;

namespace TechChallenge.AutomotiveMechanics.WebAPI.Tests.Controllers
{
    public class ServiceControllerTest
    {
        private readonly IServiceService _serviceService;
        private readonly ServiceController controller;
        private readonly ServiceResult serviceResult;

        public ServiceControllerTest(IServiceService serviceService,
            ServiceController controller, ServiceResult serviceResult)
        {
            _serviceService = serviceService;
            this.controller = controller;
            this.serviceResult = serviceResult;
        }

        [Fact]
        public async Task Get_Ok()
        {
            var controle = new List<ModelResult>();

            var resultado = (ObjectResult)await controller.Get();

            await _serviceService.Received().ListAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task Get_NotFound()
        {
            _serviceService.ListAsync().Returns(new List<ServiceResult>());

            var resultado = (ObjectResult)await controller.Get();

            await _serviceService.Received().ListAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetById_Ok()
        {
            //_serviceService.FindByIdAsync(Arg.Any<int>()).Returns(ServiceResult.CloneTipado());

            var resultado = (ObjectResult)await controller.Get(serviceResult.Id);

            await _serviceService.Received().FindByIdAsync(Arg.Any<int>());
            resultado.Value.Should().BeEquivalentTo(serviceResult);
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
        [Fact]
        public async Task GetById_NotFound()
        {
            _serviceService.FindByIdAsync(Arg.Any<int>()).Returns(new ServiceResult());

            var resultado = (StatusCodeResult)await controller.Get(1);

            await _serviceService.Received().FindByIdAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Post_Created()
        {
            //_serviceService.AddAsync(Arg.Any<ServiceInsertInput>()).
            //Returns(serviceResult.CloneTipado());

            //var resultado = (ObjectResult)await controller.Post(newService); declarar newService baseado no faker

            await _serviceService.Received().AddAsync(Arg.Any<ServiceInsertInput>());
            //resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
            //resultado.Value.Should().BeEquivalentTo(serviceResult);
        }

        [Fact]
        public async Task Put_Ok()
        {
            //_serviceService.UpdateAsync(Arg.Any<ServiceUpdateInput>()).
            //Returns(serviceResult.CloneTipado());

            var resultado = (ObjectResult)await controller.Put(new ServiceUpdateInput());

            await _serviceService.Received().UpdateAsync(Arg.Any<ServiceUpdateInput>());
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(serviceResult);
        }

        [Fact]
        public async Task Put_NotFound()
        {
            _serviceService.UpdateAsync(Arg.Any<ServiceUpdateInput>()).ReturnsNull();

            var resultado = (StatusCodeResult)await controller.Put(new ServiceUpdateInput());

            await _serviceService.Received().UpdateAsync(Arg.Any<ServiceUpdateInput>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Delete_NoContent()
        {
            //_serviceService.DeleteAsync(Arg.Any<int>()).Returns(serviceResult);

            var resultado = (StatusCodeResult)await controller.Delete(1);

            await _serviceService.Received().DeleteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task Delete_NotFound()
        {
            _serviceService.DeleteAsync(Arg.Any<int>()).ReturnsNull();

            var resultado = (StatusCodeResult)await controller.Delete(1);

            await _serviceService.Received().DeleteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
