using AutoMapper;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Contract;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private IMapper _mapper;
        private readonly IBaseNotification _baseNotification;
        public ServiceService(IMapper mapper, IServiceRepository serviceRepository,
            IBaseNotification baseNotification)
        {
            _mapper = mapper;
            _serviceRepository = serviceRepository;
            _baseNotification = baseNotification;
        }

        public async Task<IList<ServiceResult>> ListAsync()
        {
            var result = await _serviceRepository.ListAsync();

            return _mapper.Map<IList<ServiceResult>>(result);
        }

        public async Task<ServiceResult> FindByIdAsync(int id)
        {
            var result = await _serviceRepository.GetByIdAsync(id);

            return _mapper.Map<ServiceResult>(result);
        }

        public async Task<ServiceResult> AddAsync(ServiceInsertInput input)
        {
            var contract = new ServiceContract(input);

            if (!contract.IsValid)
            {
                _baseNotification.AddNotifications(contract.Notifications);
                return default;
            }

            var map = _mapper.Map<Service>(input);

            var result = new ServiceResult();

            var inserted = await _serviceRepository.AddAsync(map);

            if (inserted > 0)
            {
                result = _mapper.Map<ServiceResult>(map);
            }

            return result;
        }

        public async Task<ServiceResult> UpdateAsync(ServiceUpdateInput input)
        {
            var map = _mapper.Map<Service>(input);

            var founded = await _serviceRepository.GetByIdAsync(map.Id);

            founded.LastModifiedDate = DateTime.Now;
            founded.Name = map.Name;
            founded.Enabled = true;

            var result = new ServiceResult();

            var updated = await _serviceRepository.UpdateAsync(founded);

            if (updated > 0)
            {
                result = _mapper.Map<ServiceResult>(founded);
            }

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var founded = await _serviceRepository.GetByIdAsync(id);

            founded.LastModifiedDate = DateTime.Now;
            founded.Enabled = false;

            return await _serviceRepository.UpdateAsync(founded) > 0;
        }

        public async Task<bool> AddServiceCarAsync(ServiceCarInsertInput input)
        {

            var founded = await _serviceRepository.FindByIdAsync(input.ServiceId);

            founded.Cars.Add(new Car
            {
                Id = input.CarId
            });

            var result = await _serviceRepository.AddServiceCarAsync(founded);


            return result != null;
        }
    }
}
