using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;

namespace TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IList<Car>> ListAsync()
        {
            var result = await _context.Car
                .Include(x => x.Model)
                .Where(x => x.Enabled == true)
                .ToListAsync();

            return result;
        }

        public async Task<Car> FindByIdAsync(int id)
        {
            var result = await _context.Car
                .Include(x => x.Model)
                .Where(x => x.Id == id)
                .Where(x => x.Enabled == true)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
