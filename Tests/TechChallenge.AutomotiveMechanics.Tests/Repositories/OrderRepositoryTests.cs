using Microsoft.EntityFrameworkCore;
using Moq;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using Xunit;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class OrderRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public OrderRepositoryTests()
        {
            // Configure as opções do contexto usando um banco de dados em memória
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
        }

        [Fact]
        public async Task ListAsync_ReturnsListOfOrders()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                // Adicionar alguns pedidos de teste ao contexto em memória
                var orders = new List<Order>
            {
                new Order("Vehicle 1", 100, "customer1@example.com"),
                new Order("Vehicle 2", 200, "customer2@example.com")
            };
                context.Orders.AddRange(orders);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new OrderRepository(context);
                var result = await repository.ListAsync();

                // Assert
                Assert.Equal(2, result.Count);
            }
        }

        [Fact]
        public async Task SaveOrderAsync_SavesOrderToDatabase()
        {
            // Arrange
            var order = new Order("Test Vehicle", 300, "test@example.com");

            using (var context = new ApplicationDbContext(_options))
            {
                // Act
                var repository = new OrderRepository(context);
                await repository.SaveOrderAsync(order);

                // Assert
                Assert.Equal(1, context.Orders.Count());
                Assert.Contains(order, context.Orders);
            }
        }
    }
}
