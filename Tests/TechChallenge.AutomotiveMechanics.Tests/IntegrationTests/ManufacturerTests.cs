using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;

namespace TechChallenge.AutomotiveMechanics.Tests.IntegrationTests
{
    public class ManufacturerTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ManufacturerFakeData _manufacturerFaker = new ManufacturerFakeData();

        public ManufacturerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") // Use in-memory database for testing
                .Options;

            _context = new ApplicationDbContext(options);

        }

        [Fact]
        public async Task Can_Add_Manufacturer_To_Database()
        {
            var manufacturer = _manufacturerFaker.Generate();

            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();

            Assert.Equal(1, await _context.Manufacturers.CountAsync());
        }

        [Fact]
        public async Task Can_Remove_Manufacturer_From_Database()
        {
            var manufacturer = _manufacturerFaker.Generate();
            _context.Add(manufacturer);
            await _context.SaveChangesAsync();

            _context.Remove(manufacturer);
            await _context.SaveChangesAsync();

            var removedManufacturer = _context.Manufacturers.FirstOrDefault(c => c.Id == manufacturer.Id);

            Assert.Null(removedManufacturer);
        }

        [Fact]
        public async Task Can_Update_Manufacturer_In_Database()
        {
            var manufacturer = _manufacturerFaker.Generate();
            _context.Add(manufacturer);
            await _context.SaveChangesAsync();

            manufacturer.Name = "Honda";
            _context.Update(manufacturer);
            await _context.SaveChangesAsync();

            var updatedManufacturer = await _context.Manufacturers.FirstOrDefaultAsync(c => c.Id == manufacturer.Id);
            Assert.Equal("Honda", updatedManufacturer.Name);
        }

        [Fact]
        public async Task Can_Get_All_Manufacturers_From_Database()
        {
            var manufacturer1 = _manufacturerFaker.Generate();
            var manufacturer2 = _manufacturerFaker.Generate();
            _context.AddRange(manufacturer1, manufacturer2);
            await _context.SaveChangesAsync();

            var manufacturers = await _context.Manufacturers.ToListAsync();

            Assert.Equal(2, manufacturers.Count);
        }

        [Fact]
        public async Task Can_Get_Manufacturer_By_Id_From_Database()
        {
            var manufacturer = _manufacturerFaker.Generate();
            _context.Add(manufacturer);
            await _context.SaveChangesAsync();

            var manufacturerFromDb = await _context.Manufacturers.FindAsync(manufacturer.Id);

            Assert.NotNull(manufacturerFromDb);
            Assert.Equal(manufacturer.Id, manufacturerFromDb.Id);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
