using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public class CarUpdateInputFaker : Faker<CarUpdateInput>
    {
        public CarUpdateInputFaker()
        {
            RuleFor(c => c.Id, f => f.Random.Int(1, 99999999));
            RuleFor(c => c.ModelId, f => f.Random.Int(1000, 99999999));
            RuleFor(c => c.YearManufactured, f => f.Random.Int(1900, 2024));
        }
    }
}
