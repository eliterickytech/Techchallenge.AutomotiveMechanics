using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories
{
    public interface IServiceCarRepository : IBaseRepository<Service>
    {
        Task<Service> AddServiceCarAsync(Service service);
    }
}
