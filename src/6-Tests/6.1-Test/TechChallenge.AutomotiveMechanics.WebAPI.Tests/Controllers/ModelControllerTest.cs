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
    public class ModelControllerTest
    {
        private readonly IModelService _modelService;
        private readonly ModelController controller;
        private readonly ModelResult modelResult;

        public ModelControllerTest(IModelService modelService,
            ModelController controller, ModelResult modelResult)
        {
            _modelService = modelService;
            this.controller = controller;
            this.modelResult = modelResult;
        }

        [Fact]
        public async Task Get_Ok()
        {
            var controle = new List<ModelResult>();

            var resultado = (ObjectResult)await controller.Get();

            await _modelService.Received().ListAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task Get_NotFound()
        {
            _modelService.ListAsync().Returns(new List<ModelResult>());

            var resultado = (ObjectResult)await controller.Get();

            await _modelService.Received().ListAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetById_Ok()
        {
            //_modelService.FindByIdAsync(Arg.Any<int>()).Returns(ModelResult.CloneTipado());

            var resultado = (ObjectResult)await controller.Get(modelResult.Id);

            await _modelService.Received().FindByIdAsync(Arg.Any<int>());
            resultado.Value.Should().BeEquivalentTo(modelResult);
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
        [Fact]
        public async Task GetById_NotFound()
        {
            _modelService.FindByIdAsync(Arg.Any<int>()).Returns(new ModelResult());

            var resultado = (StatusCodeResult)await controller.Get(1);

            await _modelService.Received().FindByIdAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Post_Created()
        {
            //_modelService.AddAsync(Arg.Any<ModelInsertInput>()).
            //Returns(manufacturerResult.CloneTipado());

            //var resultado = (ObjectResult)await controller.Post(newManufacturer); declarar newManufacturer baseado no faker

            await _modelService.Received().AddAsync(Arg.Any<ModelInsertInput>());
            //resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
            //resultado.Value.Should().BeEquivalentTo(modelResult);
        }

        [Fact]
        public async Task Put_Ok()
        {
            //_modelService.UpdateAsync(Arg.Any<ManufacturerUpdateInput>()).
            //Returns(carResult.CloneTipado());

            var resultado = (ObjectResult)await controller.Put(new ModelUpdateInput());

            await _modelService.Received().UpdateAsync(Arg.Any<ModelUpdateInput>());
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(modelResult);
        }

        [Fact]
        public async Task Put_NotFound()
        {
            _modelService.UpdateAsync(Arg.Any<ModelUpdateInput>()).ReturnsNull();

            var resultado = (StatusCodeResult)await controller.Put(new ModelUpdateInput());

            await _modelService.Received().UpdateAsync(Arg.Any<ModelUpdateInput>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Delete_NoContent()
        {
            //_modelService.DeleteAsync(Arg.Any<int>()).Returns(manufacturerResult);

            var resultado = (StatusCodeResult)await controller.Delete(1);

            await _modelService.Received().DeleteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task Delete_NotFound()
        {
            _modelService.DeleteAsync(Arg.Any<int>()).ReturnsNull();

            var resultado = (StatusCodeResult)await controller.Delete(1);

            await _modelService.Received().DeleteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
