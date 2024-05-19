using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class ServiceRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public ServiceRepositoryTests()
        {
            // Configure as opções do contexto usando um banco de dados em memória
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
        }

        [Fact]
        public async Task ListAsync_ReturnsListOfServices()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                // Adicionar alguns serviços de teste ao contexto em memória
                var services = new List<Service>
            {
                new Service { Id = 1, Name = "Service 1", CarId = 1 },
                new Service { Id = 2, Name = "Service 2", CarId = 2 }
            };
                context.Services.AddRange(services);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new ServiceRepository(context);
                var result = await repository.ListAsync();

                // Assert
                Assert.Equal(2, result.Count);
            }
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsService_WhenServiceExists()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                // Adicionar um serviço de teste ao contexto em memória
                context.Services.Add(new Service { Id = 1, Name = "Test Service", CarId = 1 });
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new ServiceRepository(context);
                var result = await repository.FindByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.Id);
            }
        }

        [Fact]
        public async Task AddServiceCarAsync_AddsServiceToDatabase()
        {
            // Arrange
            var service = new Service { Id = 1, Name = "Test Service", CarId = 1 };

            using (var context = new ApplicationDbContext(_options))
            {
                // Act
                var repository = new ServiceRepository(context);
                var result = await repository.AddServiceCarAsync(service);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, context.Services.Count());
                Assert.Contains(service, context.Services);
            }
        }
    }
}
