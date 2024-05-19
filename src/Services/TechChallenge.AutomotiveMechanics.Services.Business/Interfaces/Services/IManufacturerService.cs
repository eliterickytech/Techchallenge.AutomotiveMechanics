using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services
{
    public interface IManufacturerService
    {
        Task<ManufacturerResult> AddAsync(ManufacturerInsertInput input);
        Task<bool> DeleteAsync(int id);
        Task<ManufacturerResult> FindByIdAsync(int id);
        Task<IList<ManufacturerResult>> ListAsync();
        Task<ManufacturerResult> UpdateAsync(ManufacturerUpdateInput input);
    }
}
