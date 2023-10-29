using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Contract.Car;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;
        private readonly IBaseNotification _baseNotification;

        public CarService(ICarRepository carRepository, IMapper mapper,
            IBaseNotification baseNotification, IModelRepository modelRepository)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _baseNotification = baseNotification;
            _modelRepository = modelRepository;
        }

        public async Task<IList<CarResult>> ListAsync()
        {
            var result = await _carRepository.ListAsync();

            return _mapper.Map<IList<CarResult>>(result);
        }

        public async Task<CarResult> FindByIdAsync(int id)
        {
            var result = await _carRepository.FindByIdAsync(id);

            return _mapper.Map<CarResult>(result);
        }

        public async Task<CarResult> AddAsync(CarInsertInput input)
        {
            var contract = new AddCarContract(input);

            if (!contract.IsValid)
            {
                _baseNotification.AddNotifications(contract.Notifications);
                return default;
            }

            var model = await _modelRepository.FindByIdAsync(input.ModelId);

            var contractModel = new FindCarModelContract(model);

            if (!contractModel.IsValid)
            {
                _baseNotification.AddNotifications(contractModel.Notifications);
                return default;
            }

            var map = _mapper.Map<Car>(input);

            var result = new CarResult();

            var inserted = await _carRepository.AddAsync(map);

            if (inserted > 0)
            {
                result = _mapper.Map<CarResult>(map);
            }

            return result;
        }

        public async Task<CarResult> UpdateAsync(CarUpdateInput input)
        {
            var contract = new UpdateCarContract(input);

            if (!contract.IsValid)
            {
                _baseNotification.AddNotifications(contract.Notifications);
                return default;
            }

            var model = await _modelRepository.FindByIdAsync(input.ModelId);

            var contractModel = new FindCarModelContract(model);

            if (!contractModel.IsValid)
            {
                _baseNotification.AddNotifications(contractModel.Notifications);
                return default;
            }

            var map = _mapper.Map<Car>(input);

            var founded = await _carRepository.GetByIdAsync(map.Id);

            var contractCar = new FindCarContract(founded);

            if (!contractCar.IsValid)
            {
                _baseNotification.AddNotifications(contractCar.Notifications);
                return default;
            }

            founded.YearManufactured = input.YearManufactured;
            founded.LastModifiedDate = DateTime.Now;
            founded.Enabled = true;

            var result = new CarResult();

            var updated = await _carRepository.UpdateAsync(founded);

            if (updated > 0)
            {
                result = _mapper.Map<CarResult>(founded);
            }

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var input = new DeleteInput { Id = id };
            var contract = new DeleteCarContract(input);

            if (!contract.IsValid)
            {
                _baseNotification.AddNotifications(contract.Notifications);
                return default;
            }

            var founded = await _carRepository.GetByIdAsync(id);

            var contractCar = new FindCarContract(founded);

            if (!contractCar.IsValid)
            {
                _baseNotification.AddNotifications(contractCar.Notifications);
                return default;
            }

            founded.LastModifiedDate = DateTime.Now;
            founded.Enabled = false;

            return await _carRepository.UpdateAsync(founded) > 0;
        }

    }
}
