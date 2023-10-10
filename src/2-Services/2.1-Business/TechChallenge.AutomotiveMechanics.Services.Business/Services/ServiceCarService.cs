using AutoMapper;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Services
{
    public class ServiceCarService : IServiceCarService
    {
        private readonly IServiceRepository _serviceRepository;
        private IMapper _mapper;
        public ServiceCarService(IMapper mapper, IServiceRepository serviceRepository)
        {
            _mapper = mapper;
            _serviceRepository = serviceRepository;
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
