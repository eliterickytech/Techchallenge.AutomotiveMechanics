using AutoFixture;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;

namespace TechChallenge.AutomotiveMechanics.Tests.IntegrationTests
{
    public class CarTests : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public CarTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") // Use in-memory database for testing
                .Options;

            _context = new ApplicationDbContext(options);

        }

        [Fact]
        public async Task Can_Add_Car_To_Database()
        {
            // Arrange
            var car = new Car {Id = 1, ModelId = 1, YearManufactured = 2020, Plate = "ABC-1234" };

            // Act
            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            // Assert
            Assert.Equal(1, await _context.Car.CountAsync());
        }

        [Fact]
        public async Task Can_Remove_Car_From_Database()
        {
            var car = new Car { Id = 1, ModelId = 1, YearManufactured = 2020, Plate = "ABC-1234" };
            _context.Add(car);
            await _context.SaveChangesAsync();

            _context.Remove(car);
            await _context.SaveChangesAsync();

            var removedCar = _context.Car.FirstOrDefault(c => c.Id == car.Id);

            Assert.Null(removedCar);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
