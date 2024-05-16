using Moq;
using Microsoft.EntityFrameworkCore;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class ManufacturerRepositoryTests
    {
        private readonly Mock<DbSet<Manufacturer>> _mockSet;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly ManufacturerRepository _repository;

        public ManufacturerRepositoryTests()
        {
            _mockSet = new Mock<DbSet<Manufacturer>>();
            _mockContext = new Mock<ApplicationDbContext>();
            _mockContext.Setup(m => m.Manufacturers).Returns(_mockSet.Object);
            _repository = new ManufacturerRepository(_mockContext.Object);
        }

        [Fact]
        public async Task ListAsync_ReturnsAllManufacturers()
        {
            var data = ManufacturerFakeData.GetManufacturers().AsQueryable();

            _mockSet.As<IQueryable<Manufacturer>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Manufacturer>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Manufacturer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Manufacturer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var result = await _repository.ListAsync();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count); // Verifica se todos os fabricantes são retornados
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsManufacturer_WhenExists()
        {
            var manufacturers = ManufacturerFakeData.GetManufacturers();
            var manufacturer = manufacturers.First();
            _mockSet.Setup(m => m.FindAsync(manufacturer.Id)).ReturnsAsync(manufacturer);

            var result = await _repository.FindByIdAsync(manufacturer.Id);

            Assert.Equal(manufacturer, result);
        }
    }
}
