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
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Services;

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class ServiceRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<IServiceRepository> _serviceRepositoryMock = new Mock<IServiceRepository>();
        private readonly ServiceFakeData _serviceFaker = new ServiceFakeData();

        public ServiceRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
        }

        [Fact]
        public async Task ListAsync_ReturnsListOfServices()
        {
            var services = _serviceFaker.Generate(2);
            _serviceRepositoryMock.Setup(repo => repo.ListAsync()).ReturnsAsync(services);

            var result = await _serviceRepositoryMock.Object.ListAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsService_WhenServiceExists()
        {
            var service = _serviceFaker.Generate();
            _serviceRepositoryMock.Setup(repo => repo.FindByIdAsync(service.Id)).ReturnsAsync(service);

            var result = await _serviceRepositoryMock.Object.FindByIdAsync(service.Id);

            Assert.NotNull(result);
            Assert.Equal(service.Id, result.Id);
        }

        [Fact]
        public async Task AddServiceCarAsync_AddsServiceToDatabase()
        {
            var service = _serviceFaker.Generate();

            _serviceRepositoryMock.Setup(repo => repo.AddServiceCarAsync(service)).ReturnsAsync(service);

            var result = await _serviceRepositoryMock.Object.AddServiceCarAsync(service);

            Assert.NotNull(result);
            Assert.Equal(service.Id, result.Id);
            _serviceRepositoryMock.Verify(repo => repo.AddServiceCarAsync(service), Times.Once);
        }
    }
}
