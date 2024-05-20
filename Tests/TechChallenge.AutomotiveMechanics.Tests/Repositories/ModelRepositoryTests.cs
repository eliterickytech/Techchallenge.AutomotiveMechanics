using Microsoft.EntityFrameworkCore;
using Moq;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using Xunit;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class ModelRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<IModelRepository> _modelRepositoryMock = new Mock<IModelRepository>();
        private readonly ModelFakeData _modelFaker = new ModelFakeData();

        public ModelRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: "TestDatabase")
              .Options;

            _context = new ApplicationDbContext(options);
        }

        [Fact]
        public async Task ListAsync_ReturnsListOfModels()
        {
            var models = _modelFaker.Generate(2);
            _modelRepositoryMock.Setup(repo => repo.ListAsync()).ReturnsAsync(models);

            var result = await _modelRepositoryMock.Object.ListAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsModel_WhenModelExists()
        {
            var model = _modelFaker.Generate();
            _modelRepositoryMock.Setup(repo => repo.FindByIdAsync(model.Id)).ReturnsAsync(model);

            var result = await _modelRepositoryMock.Object.FindByIdAsync(model.Id);

            Assert.NotNull(result);
            Assert.Equal(model.Id, result.Id);
        }
    }
}
