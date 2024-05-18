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
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
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

        public Task<IList<Car>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Car> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(Car entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Car entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRangeAsync(IList<Car> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRangeAsync(IList<Car> ids)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Car>> GetMany(Expression<Func<Car, bool>> where, params Expression<Func<Car, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<Car> Get(Expression<Func<Car, bool>> where, params Expression<Func<Car, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
