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
    public class ManufacturerControllerTest
    {
        private readonly IManufacturerService _manufacturerService;
        private readonly ManufacturerController controller;
        private readonly ManufacturerResult manufacturerResult;

        public ManufacturerControllerTest(IManufacturerService manufacturerService,
            ManufacturerController controller, ManufacturerResult manufacturerResult)
        {
            _manufacturerService = manufacturerService;
            this.controller = controller;
            this.manufacturerResult = manufacturerResult;
            //this.manufacturerResult = faker;
        }

        [Fact]
        public async Task Get_Ok()
        {
            var controle = new List<CarResult>();

            var resultado = (ObjectResult)await controller.Get();

            await _manufacturerService.Received().ListAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task Get_NotFound()
        {
            _manufacturerService.ListAsync().Returns(new List<ManufacturerResult>());

            var resultado = (ObjectResult)await controller.Get();

            await _manufacturerService.Received().ListAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetById_Ok()
        {
            //_manufacturerService.FindByIdAsync(Arg.Any<int>()).Returns(ManufacturerResult.CloneTipado());

            var resultado = (ObjectResult)await controller.Get(manufacturerResult.Id);

            await _manufacturerService.Received().FindByIdAsync(Arg.Any<int>());
            resultado.Value.Should().BeEquivalentTo(manufacturerResult);
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
        [Fact]
        public async Task GetById_NotFound()
        {
            _manufacturerService.FindByIdAsync(Arg.Any<int>()).Returns(new ManufacturerResult());

            var resultado = (StatusCodeResult)await controller.Get(1);

            await _manufacturerService.Received().FindByIdAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Post_Created()
        {
            //_manufacturerService.AddAsync(Arg.Any<ManufacturerInsertInput>()).
            //Returns(manufacturerResult.CloneTipado());

            //var resultado = (ObjectResult)await controller.Post(newManufacturer); declarar newManufacturer baseado no faker

            await _manufacturerService.Received().AddAsync(Arg.Any<ManufacturerInsertInput>());
            //resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
            //resultado.Value.Should().BeEquivalentTo(manufacturerResult);
        }

        [Fact]
        public async Task Put_Ok()
        {
            //_manufacturerService.UpdateAsync(Arg.Any<ManufacturerUpdateInput>()).
            //Returns(carResult.CloneTipado());

            var resultado = (ObjectResult)await controller.Put(new ManufacturerUpdateInput());

            await _manufacturerService.Received().UpdateAsync(Arg.Any<ManufacturerUpdateInput>());
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(manufacturerResult);
        }

        [Fact]
        public async Task Put_NotFound()
        {
            _manufacturerService.UpdateAsync(Arg.Any<ManufacturerUpdateInput>()).ReturnsNull();

            var resultado = (StatusCodeResult)await controller.Put(new ManufacturerUpdateInput());

            await _manufacturerService.Received().UpdateAsync(Arg.Any<ManufacturerUpdateInput>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Delete_NoContent()
        {
            //_manufacturerService.DeleteAsync(Arg.Any<int>()).Returns(manufacturerResult);

            var resultado = (StatusCodeResult)await controller.Delete(1);

            await _manufacturerService.Received().DeleteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task NotFound_NotFound()
        {
            _manufacturerService.DeleteAsync(Arg.Any<int>()).ReturnsNull();

            var resultado = (StatusCodeResult)await controller.Delete(1);

            await _manufacturerService.Received().DeleteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
