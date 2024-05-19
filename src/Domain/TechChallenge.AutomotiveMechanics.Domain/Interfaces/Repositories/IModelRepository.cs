using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories
{
    public interface IModelRepository : IBaseRepository<Model>
    {
        Task<Model> FindByIdAsync(int id);
        Task<IList<Model>> ListAsync();
    }
}
