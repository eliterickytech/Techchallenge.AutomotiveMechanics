using Microsoft.EntityFrameworkCore;
using Moq;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using Xunit;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class OrderRepositoryTests
    {
        private readonly Mock<DbSet<Order>> _mockSet;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly OrderRepository _repository;

        public OrderRepositoryTests()
        {
            _mockSet = new Mock<DbSet<Order>>();
            _mockContext = new Mock<ApplicationDbContext>();
            _mockContext.Setup(m => m.Orders).Returns(_mockSet.Object);
            _repository = new OrderRepository(_mockContext.Object);
        }

        [Fact]
        public async Task ListAsync_ReturnsAllOrders()
        {
            var data = OrderFakeData.GetOrders().AsQueryable();

            _mockSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var result = await _repository.ListAsync();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count); // Verifica se todos os pedidos são retornados
        }

        [Fact]
        public async Task SaveOrderAsync_AddsOrderSuccessfully()
        {
            var order = new Order("Car X", 1200m, "test@example.com");

            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1); // Simula a operação de salvamento retornando '1' para sucesso
            await _repository.SaveOrderAsync(order);

            _mockSet.Verify(m => m.Add(It.Is<Order>(o => o == order)), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }
    }
}
