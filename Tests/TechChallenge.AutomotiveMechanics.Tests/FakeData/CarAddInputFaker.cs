using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public class CarAddInputFaker : Faker<CarInsertInput>
    {
        public CarAddInputFaker()
        {
            RuleFor(c => c.ModelId, f => f.Random.Int(1000, 99999999));
            RuleFor(c => c.YearManufactured, f => f.Random.Int(1900, 2024));
            RuleFor(c => c.Plate, f => f.Random.Word());
        }
    }
}
