using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services
{
    public interface IServiceService
    {
        Task<ServiceResult> AddAsync(ServiceInsertInput input);
        Task<bool> AddServiceCarAsync(ServiceCarInsertInput input);
        Task<bool> DeleteAsync(int id);
        Task<ServiceResult> FindByIdAsync(int id);
        Task<IList<ServiceResult>> ListAsync();
        Task<ServiceResult> UpdateAsync(ServiceUpdateInput input);
    }
}
