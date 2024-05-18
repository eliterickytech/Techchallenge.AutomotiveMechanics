using Microsoft.EntityFrameworkCore;
using Moq;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using Xunit;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class ModelRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public ModelRepositoryTests()
        {
            // Configure as opções do contexto usando um banco de dados em memória
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
        }

        [Fact]
        public async Task ListAsync_ReturnsListOfModels()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                // Adicionar alguns modelos de teste ao contexto em memória
                var models = new List<Model>
            {
                new Model { Id = 1, Name = "Model 1", ManufacturerId = 1, Cars = new List<Car>() },
                new Model { Id = 2, Name = "Model 2", ManufacturerId = 2, Cars = new List<Car>() }
            };
                context.Models.AddRange(models);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new ModelRepository(context);
                var result = await repository.ListAsync();

                // Assert
                Assert.Equal(2, result.Count);
            }
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsModel_WhenModelExists()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                // Adicionar um modelo de teste ao contexto em memória
                context.Models.Add(new Model { Id = 1, Name = "Test Model", ManufacturerId = 1, Cars = new List<Car>() });
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new ModelRepository(context);
                var result = await repository.FindByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.Id);
            }
        }
    }
}
