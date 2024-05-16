using Microsoft.EntityFrameworkCore;
using Moq;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using Xunit;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class ModelRepositoryTests
    {
        private readonly Mock<DbSet<Model>> _mockSet;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly ModelRepository _repository;

        public ModelRepositoryTests()
        {
            _mockSet = new Mock<DbSet<Model>>();
            _mockContext = new Mock<ApplicationDbContext>();
            _mockContext.Setup(m => m.Models).Returns(_mockSet.Object);
            _repository = new ModelRepository(_mockContext.Object);
        }

        [Fact]
        public async Task ListAsync_ReturnsAllModels()
        {
            var data = ModelFakeData.GetModels().AsQueryable();

            _mockSet.As<IQueryable<Model>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Model>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Model>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Model>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Model>)data.GetEnumerator());

            var result = await _repository.ListAsync();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count); // Verifica se todos os modelos são retornados
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsModel_WhenExists()
        {
            var models = ModelFakeData.GetModels();
            var model = models.First();
            _mockSet.Setup(m => m.FindAsync(model.Id)).ReturnsAsync(model);

            var result = await _repository.FindByIdAsync(model.Id);

            Assert.Equal(model, result);
        }
    }
}
