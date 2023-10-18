using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using TechChallenge.AutomotiveMechanics.Presentation.API.Controllers;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.WebAPI.Tests.Controllers
{
    public class CarsControllerTest
    {
        private readonly ICarService _carService;
        private readonly CarController controller;
        private readonly CarResult carResult;

        public CarsControllerTest(ICarService carService, CarController controller,
            CarResult carResult)
        {
            _carService = carService;
            this.controller = controller;
            this.carResult = carResult;
            //this.carResult = faker;
        }

        [Fact]
        public async Task Get_Ok()
        {
            var controle = new List<CarResult>();

            var resultado = (ObjectResult)await controller.Get();

            await _carService.Received().ListAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task Get_NotFound()
        {
            _carService.ListAsync().Returns(new List<CarResult>());

            var resultado = (ObjectResult)await controller.Get();

            await _carService.Received().ListAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetById_Ok()
        {
            //_carService.FindByIdAsync(Arg.Any<int>()).Returns(carResult.CloneTipado());

            var resultado = (ObjectResult)await controller.Get(carResult.Id);

            await _carService.Received().FindByIdAsync(Arg.Any<int>());
            resultado.Value.Should().BeEquivalentTo(carResult);
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
        [Fact]
        public async Task GetById_NotFound()
        {
            _carService.FindByIdAsync(Arg.Any<int>()).Returns(new CarResult());

            var resultado = (StatusCodeResult)await controller.Get(1);

            await _carService.Received().FindByIdAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Post_Created()
        {
            //_carService.AddAsync(Arg.Any<CarInsertInput>()).Returns(carResult.CloneTipado());

            //var resultado = (ObjectResult)await controller.Post(newCar); declarar newCar baseado no faker

            await _carService.Received().AddAsync(Arg.Any<CarInsertInput>());
            //resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
            //resultado.Value.Should().BeEquivalentTo(carResult);
        }

        [Fact]
        public async Task Put_Ok()
        {
            //_carService.UpdateAsync(Arg.Any<CarUpdateInput>()).Returns(carResult.CloneTipado());

            var resultado = (ObjectResult)await controller.Put(new CarUpdateInput());

            await _carService.Received().UpdateAsync(Arg.Any<CarUpdateInput>());
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(carResult);
        }

        [Fact]
        public async Task Put_NotFound()
        {
            _carService.UpdateAsync(Arg.Any<CarUpdateInput>()).ReturnsNull();

            var resultado = (StatusCodeResult)await controller.Put(new CarUpdateInput());

            await _carService.Received().UpdateAsync(Arg.Any<CarUpdateInput>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Delete_NoContent()
        {
            //_carService.DeleteAsync(Arg.Any<int>()).Returns(carResult);

            var resultado = (StatusCodeResult)await controller.Delete(1);

            await _carService.Received().DeleteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task NotFound_NotFound()
        {
            _carService.DeleteAsync(Arg.Any<int>()).ReturnsNull();

            var resultado = (StatusCodeResult)await controller.Delete(1);

            await _carService.Received().DeleteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
