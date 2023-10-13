using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResult> Login(UserLoginInput input)
        {
            var token =  await _userRepository.Login(input.Email, input.Password);
            var result = new UserResult();
            result.Token = token;
            result.Email = input.Email;
            return result; 
        }

        public async Task<UserResult> Register(UserRegisterInput input)
        {
            var map = _mapper.Map<User>(input);

            var result = new UserResult();

            var registered = await _userRepository.Register(map, input.Password);

            if (registered > 0)
            {
                result = _mapper.Map<UserResult>(map);
            }

            return result;

            //return 
        }
    }
}
