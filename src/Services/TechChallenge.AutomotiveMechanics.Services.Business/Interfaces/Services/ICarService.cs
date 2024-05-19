using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services
{
    public interface ICarService
    {
        Task<CarResult> AddAsync(CarInsertInput input);
        Task<bool> DeleteAsync(int id);
        Task<CarResult> FindByIdAsync(int id);
        Task<IList<CarResult>> ListAsync();
        Task<CarResult> UpdateAsync(CarUpdateInput input);
    }
}
