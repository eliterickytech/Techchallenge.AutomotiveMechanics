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
    public class ServiceTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ServiceFakeData _serviceFaker = new ServiceFakeData();

        public ServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") // Use in-memory database for testing
                .Options;

            _context = new ApplicationDbContext(options);

        }

        [Fact]
        public async Task Can_Add_Service_To_Database()
        {
            var service = _serviceFaker.Generate();

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            Assert.Equal(1, await _context.Services.CountAsync());
        }

        [Fact]
        public async Task Can_Remove_Service_From_Database()
        {
            var service = _serviceFaker.Generate();
            _context.Add(service);
            await _context.SaveChangesAsync();

            _context.Remove(service);
            await _context.SaveChangesAsync();

            var removedService = _context.Services.FirstOrDefault(c => c.Id == service.Id);

            Assert.Null(removedService);
        }

        [Fact]
        public async Task Can_Update_Service_In_Database()
        {
            var service = _serviceFaker.Generate();
            _context.Add(service);
            await _context.SaveChangesAsync();

            service.Name = "troca de oleo";
            _context.Update(service);
            await _context.SaveChangesAsync();

            var updatedService = await _context.Services.FirstOrDefaultAsync(c => c.Id == service.Id);
            Assert.Equal("troca de oleo", updatedService.Name);
        }

        [Fact]
        public async Task Can_Get_All_Service_From_Database()
        {
            var service1 = _serviceFaker.Generate();
            var service2 = _serviceFaker.Generate();
            _context.AddRange(service1, service2);
            await _context.SaveChangesAsync();

            var services = await _context.Services.ToListAsync();

            Assert.Equal(2, services.Count);
        }

        [Fact]
        public async Task Can_Get_Service_By_Id_From_Database()
        {
            var service = _serviceFaker.Generate();
            _context.Add(service);
            await _context.SaveChangesAsync();

            var serviceFromDb = await _context.Services.FindAsync(service.Id);

            Assert.NotNull(serviceFromDb);
            Assert.Equal(service.Id, serviceFromDb.Id);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
