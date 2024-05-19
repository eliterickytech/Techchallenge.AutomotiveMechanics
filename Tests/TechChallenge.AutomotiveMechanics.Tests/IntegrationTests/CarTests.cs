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
using TechChallenge.AutomotiveMechanics.Tests.FakeData;

namespace TechChallenge.AutomotiveMechanics.Tests.IntegrationTests
{
    public class CarTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly CarFakeData _carFaker = new CarFakeData();

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
            var car = _carFaker.Generate();
            //var car = new Car {Id = 1, ModelId = 1, YearManufactured = 2020, Plate = "ABC-1234" };

            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            Assert.Equal(1, await _context.Car.CountAsync());
        }

        [Fact]
        public async Task Can_Remove_Car_From_Database()
        {
            var car = _carFaker.Generate();
            _context.Add(car);
            await _context.SaveChangesAsync();

            _context.Remove(car);
            await _context.SaveChangesAsync();

            var removedCar = _context.Car.FirstOrDefault(c => c.Id == car.Id);

            Assert.Null(removedCar);
        }

        [Fact]
        public async Task Can_Update_Car_In_Database()
        {
            var car = _carFaker.Generate();
            _context.Add(car);
            await _context.SaveChangesAsync();

            car.Plate = "XYZ-7890";
            _context.Update(car);
            await _context.SaveChangesAsync();

            var updatedCar = await _context.Car.FirstOrDefaultAsync(c => c.Id == car.Id);
            Assert.Equal("XYZ-7890", updatedCar.Plate);
        }

        [Fact]
        public async Task Can_Get_All_Cars_From_Database()
        {
            var car1 = _carFaker.Generate();
            var car2 = _carFaker.Generate();
            //var car1 = new Car { Id = 1, ModelId = 1, YearManufactured = 2020, Plate = "ABC-1234" };
            //var car2 = new Car { Id = 2, ModelId = 2, YearManufactured = 2021, Plate = "XYZ-7890" };
            _context.AddRange(car1, car2);
            await _context.SaveChangesAsync();

            var cars = await _context.Car.ToListAsync();

            Assert.Equal(2, cars.Count);
        }

        [Fact]
        public async Task Can_Get_Car_By_Id_From_Database()
        {
            var car = _carFaker.Generate();
            _context.Add(car);
            await _context.SaveChangesAsync();

            var carFromDb = await _context.Car.FindAsync(car.Id);

            Assert.NotNull(carFromDb);
            Assert.Equal(car.Id, carFromDb.Id);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
