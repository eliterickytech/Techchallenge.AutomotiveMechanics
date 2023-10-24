
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Contract.Car;
using TechChallenge.AutomotiveMechanics.Services.Business.Contract.Model;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Services
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IMapper _mapper;
        private readonly IBaseNotification _baseNotification;

        public ModelService(IModelRepository modelRepository, IManufacturerRepository manufacturerRepository, IMapper mapper,
            IBaseNotification baseNotification)
        {
            _modelRepository = modelRepository;
            _manufacturerRepository = manufacturerRepository;
            _mapper = mapper;
            _baseNotification = baseNotification;
        }

        public async Task<IList<ModelResult>> ListAsync()
        {
            var result = await _modelRepository.ListAsync();

            return _mapper.Map<IList<ModelResult>>(result);
        }

        public async Task<ModelResult> FindByIdAsync(int id)
        {
            var result = await _modelRepository.FindByIdAsync(id);

            return _mapper.Map<ModelResult>(result);
        }

        public async Task<ModelResult> AddAsync(ModelInsertInput input)
        {
            var contract = new AddModelContract(input);

            if (!contract.IsValid)
            {
                _baseNotification.AddNotifications(contract.Notifications);
                return default;
            }

            var manufacturer = await _manufacturerRepository.FindByIdAsync(input.ManufacturerId);
            var contractManufacturer = new FindManufacturerModelContract(manufacturer);

            if (!contractManufacturer.IsValid)
            {
                _baseNotification.AddNotifications(contract.Notifications);
                return default;
            }

            var map = _mapper.Map<Model>(input);

            var result = new ModelResult();

            var inserted = await _modelRepository.AddAsync(map);

            if (inserted > 0)
            {
                result = _mapper.Map<ModelResult>(map);
            }

            return result;
        }

        public async Task<ModelResult> UpdateAsync(ModelUpdateInput input)
        {
            var contract = new UpdateModelContract(input);

            if (!contract.IsValid)
            {
                _baseNotification.AddNotifications(contract.Notifications);
                return default;
            }

            var map = _mapper.Map<Model>(input);

            var founded = await _modelRepository.GetByIdAsync(map.Id);

            founded.LastModifiedDate = DateTime.Now;
            founded.Name = map.Name;
            founded.Enabled = true;

            var result = new ModelResult();

            var updated = await _modelRepository.UpdateAsync(founded);

            if (updated > 0)
            {
                result = _mapper.Map<ModelResult>(founded);
            }

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var input = new DeleteInput { Id = id };
            var contract = new DeleteModelContract(input);

            if (!contract.IsValid)
            {
                _baseNotification.AddNotifications(contract.Notifications);
                return default;
            }

            var founded = await _modelRepository.GetByIdAsync(id);

            founded.LastModifiedDate = DateTime.Now;
            founded.Enabled = false;

            return await _modelRepository.UpdateAsync(founded) > 0;
        }
    }
}
