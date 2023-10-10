using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;

namespace TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories
{
    public class ModelRepository : BaseRepository<Model>, IModelRepository
    {
        public ModelRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<IList<Model>> ListAsync()
        {
            var result = await _context.Models
                .Include(x => x.Cars)
                .Where(x => x.Enabled == true)
                .ToListAsync();

            return result;                
        }

        public async Task<Model> FindByIdAsync(int id)
        {
            var result = await _context.Models
                .Include(x => x.Cars)
                .Where(x => x.Id == id)
                .Where(x => x.Enabled == true)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
