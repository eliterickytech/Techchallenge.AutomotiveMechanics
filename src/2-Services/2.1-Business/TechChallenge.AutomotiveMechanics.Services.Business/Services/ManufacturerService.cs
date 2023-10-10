using AutoMapper;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;


namespace TechChallenge.AutomotiveMechanics.Services.Business.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IMapper _mapper;

        public ManufacturerService(IManufacturerRepository manufacturerRepository, IMapper mapper)
        {
            _manufacturerRepository = manufacturerRepository;
            _mapper = mapper;
        }

        public async Task<IList<ManufacturerResult>> ListAsync()
        {
            var result = await _manufacturerRepository.ListAsync();

            return _mapper.Map<IList<ManufacturerResult>>(result);
        }

        public async Task<ManufacturerResult> FindByIdAsync(int id)
        {
            var result = await _manufacturerRepository.GetByIdAsync(id);

            return _mapper.Map<ManufacturerResult>(result);
        }

        public async Task<ManufacturerResult> AddAsync(ManufacturerInsertInput input)
        {
            var map = _mapper.Map<Manufacturer>(input);

            var result = new ManufacturerResult();

            var inserted = await _manufacturerRepository.AddAsync(map);

            if (inserted > 0)
            {
                result = _mapper.Map<ManufacturerResult>(map);
            }

            return result;
        }

        public async Task<ManufacturerResult> UpdateAsync(ManufacturerUpdateInput input)
        {
            var map = _mapper.Map<Model>(input);

            var founded = await _manufacturerRepository.GetByIdAsync(map.Id);

            founded.LastModifiedDate = DateTime.Now;
            founded.Name = map.Name;
            founded.Enabled = true;

            var result = new ManufacturerResult();

            var updated = await _manufacturerRepository.UpdateAsync(founded);

            if (updated > 0)
            {
                result = _mapper.Map<ManufacturerResult>(founded);
            }

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var founded = await _manufacturerRepository.GetByIdAsync(id);

            founded.LastModifiedDate = DateTime.Now;
            founded.Enabled = false;

            return await _manufacturerRepository.UpdateAsync(founded) > 0;
        }
    }
}
