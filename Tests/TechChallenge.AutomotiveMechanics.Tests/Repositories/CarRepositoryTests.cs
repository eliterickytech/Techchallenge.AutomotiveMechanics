using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Moq;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using Xunit;
namespace TechChallenge.AutomotiveMechanics.Tests
{
    public class CarRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public CarRepositoryTests()
        {
            // Configure as opções do contexto usando um banco de dados em memória
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
        }

        [Fact]
        public async Task ListAsync_ReturnsListOfCars()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                // Adicionar alguns carros de teste ao contexto em memória
                context.Car.AddRange(
                    new Car { Id = 1, Plate = "ABC123", ModelId = 1, YearManufactured = 2020, Enabled = true },
                    new Car { Id = 2, Plate = "DEF456", ModelId = 2, YearManufactured = 2019, Enabled = true },
                    new Car { Id = 3, Plate = "GHI789", ModelId = 3, YearManufactured = 2018, Enabled = false }
                );
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new CarRepository(context);
                var result = await repository.ListAsync();

                // Assert
                Assert.Equal(2, result.Count);
            }
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsCar_WhenCarExists()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                // Adicionar um carro de teste ao contexto em memória
                context.Car.Add(new Car { Id = 1, Plate = "ABC123", ModelId = 1, YearManufactured = 2020, Enabled = true });
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new CarRepository(context);
                var result = await repository.FindByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.Id);
            }
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsNull_WhenCarDoesNotExist()
        {
            // Arrange & Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new CarRepository(context);
                var result = await repository.FindByIdAsync(100);

                // Assert
                Assert.Null(result);
            }
        }
    }
}