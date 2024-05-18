using Moq;
using Microsoft.EntityFrameworkCore;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class ManufacturerRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public ManufacturerRepositoryTests()
        {
            // Configure as opções do contexto usando um banco de dados em memória
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
        }

        [Fact]
        public async Task ListAsync_ReturnsListOfManufacturers()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                // Adicionar alguns fabricantes de teste ao contexto em memória
                var manufacturers = new List<Manufacturer>
            {
                new Manufacturer { Id = 1, Name = "Manufacturer 1", Models = new List<Model>() },
                new Manufacturer { Id = 2, Name = "Manufacturer 2", Models = new List<Model>() }
            };
                context.Manufacturers.AddRange(manufacturers);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new ManufacturerRepository(context);
                var result = await repository.ListAsync();

                // Assert
                Assert.Equal(2, result.Count);
            }
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsManufacturer_WhenManufacturerExists()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                // Adicionar um fabricante de teste ao contexto em memória
                context.Manufacturers.Add(new Manufacturer { Id = 1, Name = "Test Manufacturer", Models = new List<Model>() });
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new ManufacturerRepository(context);
                var result = await repository.FindByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.Id);
            }
        }
    }
}
