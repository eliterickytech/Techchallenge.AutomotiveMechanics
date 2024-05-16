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

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class ServiceRepositoryTests
    {
        private readonly Mock<DbSet<Service>> _mockSet;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly ServiceRepository _repository;

        public ServiceRepositoryTests()
        {
            _mockSet = new Mock<DbSet<Service>>();
            _mockContext = new Mock<ApplicationDbContext>();
            _mockContext.Setup(m => m.Services).Returns(_mockSet.Object);
            _repository = new ServiceRepository(_mockContext.Object);
        }

        [Fact]
        public async Task ListAsync_ReturnsAllEnabledServices()
        {
            var data = ServiceFakeData.GetServices().AsQueryable();

            _mockSet.As<IQueryable<Service>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Service>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Service>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Service>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var result = await _repository.ListAsync();

            Assert.NotNull(result);
            Assert.True(result.All(s => s.Enabled)); // Verifica se todos os serviços estão habilitados
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsService_WhenExists()
        {
            var services = ServiceFakeData.GetServices();
            var service = services.First();
            _mockSet.Setup(m => m.FindAsync(service.Id)).ReturnsAsync(service);

            var result = await _repository.FindByIdAsync(service.Id);

            Assert.Equal(service, result);
        }

        [Fact]
        public async Task AddServiceCarAsync_AddsAndCommitsService()
        {
            var service = ServiceFakeData.GetServices().First();

            // Configurando o comportamento do contexto para transações
            var transactionMock = new Mock<IDbContextTransaction>();
            _mockContext.Setup(m => m.Database.BeginTransaction()).Returns(transactionMock.Object);

            // Simulando a adição do serviço e salvamento das mudanças
            _mockContext.Setup(m => m.Services.Add(It.IsAny<Service>())).Verifiable();
            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            var result = await _repository.AddServiceCarAsync(service);

            // Verifica se o serviço foi adicionado e se o método SaveChanges foi chamado
            _mockContext.Verify(m => m.Services.Add(It.IsAny<Service>()), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());

            // Verifica se a transação foi realizada com commit
            transactionMock.Verify(m => m.CommitAsync(default), Times.Once());

            // Verifica se o serviço retornado é o mesmo que foi adicionado
            Assert.Equal(service, result);
        }

    }
}
