using AutoMapper;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Contract.Car;
using TechChallenge.AutomotiveMechanics.Services.Business.Contract.Model;
using TechChallenge.AutomotiveMechanics.Services.Business.Contract.Service;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ICarRepository _carRepository;
        private IMapper _mapper;
        private readonly IBaseNotification _baseNotification;
        public ServiceService(IMapper mapper, IServiceRepository serviceRepository,
            IBaseNotification baseNotification, ICarRepository carRepository)
        {
            _mapper = mapper;
            _serviceRepository = serviceRepository;
            _baseNotification = baseNotification;
            _carRepository = carRepository;
        }

        public async Task<IList<ServiceResult>> ListAsync()
        {
            var result = await _serviceRepository.ListAsync();

            return _mapper.Map<IList<ServiceResult>>(result);
        }

        public async Task<ServiceResult> FindByIdAsync(int id)
        {
            var result = await _serviceRepository.FindByIdAsync(id);

            return _mapper.Map<ServiceResult>(result);
        }

        public async Task<ServiceResult> AddAsync(ServiceInsertInput input)
        {
            var contract = new AddServiceContract(input);

            if (!contract.IsValid)
            {
                _baseNotification.AddNotifications(contract.Notifications);
                return default;
            }

            var car = await _carRepository.FindByIdAsync(input.CarId);

            var contractCar = new FindServiceCarContract(car);

            if(!contractCar.IsValid)
            {
                _baseNotification.AddNotifications(contractCar.Notifications);
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
            var contract = new UpdateServiceContract(input);

            if (!contract.IsValid)
            {
                _baseNotification.AddNotifications(contract.Notifications);
                return default;
            }

            var car = await _carRepository.FindByIdAsync(input.CarId);

            var contractCar = new FindServiceCarContract(car);

            if (!contractCar.IsValid)
            {
                _baseNotification.AddNotifications(contractCar.Notifications);
                return default;
            }

            var map = _mapper.Map<Service>(input);

            var founded = await _serviceRepository.GetByIdAsync(map.Id);

            var contractService = new FindServiceContract(founded);

            if (!contractService.IsValid)
            {
                _baseNotification.AddNotifications(contractService.Notifications);
                return default;
            }

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
            var input = new DeleteInput { Id = id };
            var contract = new DeleteServiceContract(input);

            if (!contract.IsValid)
            {
                _baseNotification.AddNotifications(contract.Notifications);
                return default;
            }
            var founded = await _serviceRepository.GetByIdAsync(id);

            var contractService = new FindServiceContract(founded);

            if (!contractService.IsValid)
            {
                _baseNotification.AddNotifications(contractService.Notifications);
                return default;
            }

            founded.LastModifiedDate = DateTime.Now;
            founded.Enabled = false;

            return await _serviceRepository.UpdateAsync(founded) > 0;
        }
    }
}
