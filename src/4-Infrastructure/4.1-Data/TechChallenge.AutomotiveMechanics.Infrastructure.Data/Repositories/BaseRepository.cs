using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;

namespace TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<int> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IList<T> entities)
        {
            _dbSet.AddRange(entities);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            _dbSet.Remove(await GetByIdAsync(id));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteRangeAsync(IList<T> ids)
        {
            _dbSet.RemoveRange(ids);
            return await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateRangeAsync(IList<T> entities)
        {
            _dbSet.UpdateRange(entities);
            return await _context.SaveChangesAsync();
        }


        public async Task<IList<T>> GetMany(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes)
        {
            var model = _dbSet.Where(where);
            if (includes != null)
            {
                model = includes.Aggregate(model, (current, includeProperty) => current.Include(includeProperty));
            }
            return await model.ToListAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            var model = _dbSet.AsNoTracking().Where(where);
            if (includes != null)
            {
                model = includes.Aggregate(model, (current, includeProperty) => current.Include(includeProperty));
            }
            return await model.FirstOrDefaultAsync();
        }
    }
}