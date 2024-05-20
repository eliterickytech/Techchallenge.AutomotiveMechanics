using Moq;
using Microsoft.EntityFrameworkCore;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class ManufacturerRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<IManufacturerRepository> _manufacturerRepositoryMock = new Mock<IManufacturerRepository>();
        private readonly ManufacturerFakeData _manufacturerFaker = new ManufacturerFakeData();

        public ManufacturerRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: "TestDatabase")
              .Options;

            _context = new ApplicationDbContext(options);
        }

        [Fact]
        public async Task ListAsync_ReturnsListOfManufacturers()
        {
            var manufacturers = _manufacturerFaker.Generate(2);
            _manufacturerRepositoryMock.Setup(repo => repo.ListAsync()).ReturnsAsync(manufacturers);

            var result = await _manufacturerRepositoryMock.Object.ListAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsManufacturer_WhenManufacturerExists()
        {
            var manufacturer = _manufacturerFaker.Generate();
            _manufacturerRepositoryMock.Setup(repo => repo.FindByIdAsync(manufacturer.Id)).ReturnsAsync(manufacturer);

            var result = await _manufacturerRepositoryMock.Object.FindByIdAsync(manufacturer.Id);

            Assert.NotNull(result);
            Assert.Equal(manufacturer.Id, result.Id);
        }
    }
}
