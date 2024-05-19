using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<IList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<int> AddAsync(T entity);

        Task<int> UpdateAsync(T entity);

        Task<int> DeleteAsync(int id);

        Task<int> UpdateRangeAsync(IList<T> entities);

        Task<int> DeleteRangeAsync(IList<T> ids);

        Task<IList<T>> GetMany(System.Linq.Expressions.Expression<Func<T, bool>> where, params System.Linq.Expressions.Expression<Func<T, object>>[] includes);

        Task<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> where, params System.Linq.Expressions.Expression<Func<T, object>>[] includes);
    }
}
