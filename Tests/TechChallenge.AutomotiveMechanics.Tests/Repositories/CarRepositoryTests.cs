using System;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;
using Xunit;

namespace TechChallenge.AutomotiveMechanics.Tests
{
    public class CarRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<ICarRepository> _carRepositoryMock = new Mock<ICarRepository>();
        private readonly CarFakeData _carFaker = new CarFakeData();


        public CarRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: "TestDatabase")
              .Options;

            _context = new ApplicationDbContext(options);
        }

        [Fact]
        public async Task ListAsync_ReturnsListOfCars()
        {
            var cars = _carFaker.Generate(3);
            _carRepositoryMock.Setup(repo => repo.ListAsync()).ReturnsAsync(cars);

            var result = await _carRepositoryMock.Object.ListAsync();

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsCar_WhenCarExists()
        {
            var car = _carFaker.Generate();
            _carRepositoryMock.Setup(repo => repo.FindByIdAsync(car.Id)).ReturnsAsync(car);

            var result = await _carRepositoryMock.Object.FindByIdAsync(car.Id);

            Assert.NotNull(result);
            Assert.Equal(car.Id, result.Id);
        }


        [Fact]
        public async Task FindByIdAsync_ReturnsNull_WhenCarDoesNotExist()
        {
            var car = _carFaker.Generate();
            _carRepositoryMock.Setup(repo => repo.FindByIdAsync(car.Id)).ReturnsAsync(car);

            var result = await _carRepositoryMock.Object.FindByIdAsync(car.Id);

            Assert.NotNull(result);
            Assert.Equal(car.Id, result.Id);
        }
    }
}
