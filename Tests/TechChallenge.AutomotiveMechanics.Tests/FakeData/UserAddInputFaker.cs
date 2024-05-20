using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public class UserAddInputFaker : Faker<UserRegisterInput>
    {
        public UserAddInputFaker()
        {
            RuleFor(c => c.Name, f => f.Random.Word());
            RuleFor(c => c.Email, f => f.Random.Word());
            RuleFor(c => c.Password, f => "senhasenha");
        }
    }
}