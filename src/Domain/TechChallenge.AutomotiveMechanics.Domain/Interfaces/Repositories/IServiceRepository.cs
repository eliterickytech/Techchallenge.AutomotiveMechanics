using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories
{
    public interface IServiceRepository : IBaseRepository<Service>
    {
        Task<Service> AddServiceCarAsync(Service service);
        Task<Service> FindByIdAsync(int id);
        Task<IList<Service>> ListAsync();
    }
}
