using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;
using Moq;
using Xunit;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class CarRepositoryTests
    {
        private readonly Mock<DbSet<Car>> _mockSet;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly CarRepository _repository;

        public CarRepositoryTests()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _mockContext.Setup(m => m.Car).Returns(_mockSet.Object);
            _repository = new CarRepository(_mockContext.Object);
        }

        [Fact]
        public async Task ListAsync_ReturnsAllCars()
        {
            var data = CarFakeData.GetCars().AsQueryable();

            _mockSet.As<IQueryable<Car>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var result = await _repository.ListAsync();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count); // Verifica se todos os carros são retornados
        }

        [Fact]
        public async Task AddCarAsync_AddsCarSuccessfully()
        {
            var car = new Car { ModelId = 10, Plate = "XYZ1234", YearManufactured = 2008 };

            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1); // Simula a operação de salvamento retornando '1' para sucesso
            await _repository.AddAsync(car);

            _mockSet.Verify(m => m.Add(It.Is<Car>(c => c == car)), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }
    }
}
