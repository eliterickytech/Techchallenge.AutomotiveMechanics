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
    public class ModelTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ModelFakeData _modelFaker = new ModelFakeData();

        public ModelTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") // Use in-memory database for testing
                .Options;

            _context = new ApplicationDbContext(options);

        }

        [Fact]
        public async Task Can_Add_Model_To_Database()
        {
            var model = _modelFaker.Generate();

            _context.Models.Add(model);
            await _context.SaveChangesAsync();

            Assert.Equal(1, await _context.Models.CountAsync());
        }

        [Fact]
        public async Task Can_Remove_Model_From_Database()
        {
            var model = _modelFaker.Generate();
            _context.Add(model);
            await _context.SaveChangesAsync();

            _context.Remove(model);
            await _context.SaveChangesAsync();

            var removedModel = _context.Models.FirstOrDefault(c => c.Id == model.Id);

            Assert.Null(removedModel);
        }

        [Fact]
        public async Task Can_Update_Model_In_Database()
        {
            var model = _modelFaker.Generate();
            _context.Add(model);
            await _context.SaveChangesAsync();

            model.Name = "Updated";
            _context.Update(model);
            await _context.SaveChangesAsync();

            var updatedModel = await _context.Models.FirstOrDefaultAsync(c => c.Id == model.Id);
            Assert.Equal("Updated", updatedModel.Name);
        }

        [Fact]
        public async Task Can_Get_All_Models_From_Database()
        {
            var model1 = _modelFaker.Generate();
            var model2 = _modelFaker.Generate();
            _context.AddRange(model1, model2);
            await _context.SaveChangesAsync();

            var models = await _context.Models.ToListAsync();

            Assert.Equal(2, models .Count);
        }

        [Fact]
        public async Task Can_Get_Model_By_Id_From_Database()
        {
            var model = _modelFaker.Generate();
            _context.Add(model);
            await _context.SaveChangesAsync();

            var modelFromDb = await _context.Models.FindAsync(model.Id);

            Assert.NotNull(modelFromDb);
            Assert.Equal(model.Id, modelFromDb.Id);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
