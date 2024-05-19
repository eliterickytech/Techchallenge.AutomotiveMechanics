using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;

namespace TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories
{
    public class ManufacturerRepository : BaseRepository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IList<Manufacturer>> ListAsync()
        {
            var result = await _context.Manufacturers
                .Include(x => x.Models)
                .Where(x => x.Enabled == true)
                .Where(x => x.Models.Any(x => x.Enabled == true))
                .ToListAsync();

            return result;
        }

        public async Task<Manufacturer> FindByIdAsync(int id)
        {
            var result = await _context.Manufacturers
                .Include(x => x.Models)
                .Where(x => x.Id == id)
                .Where(x => x.Enabled == true)
                .Where(x => x.Models.Any(x => x.Enabled == true))
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
