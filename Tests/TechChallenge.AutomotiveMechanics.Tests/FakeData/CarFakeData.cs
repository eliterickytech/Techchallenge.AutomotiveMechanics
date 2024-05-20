using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public class CarFakeData : Faker<Car>
    {
        public CarFakeData()
        {
            RuleFor(c => c.Id, f => f.Random.Int(1, 99999999));
            RuleFor(c => c.ModelId, f => f.Random.Int(1000, 99999999));
            RuleFor(c => c.YearManufactured, f => f.Random.Int(1900, 2024));
            RuleFor(c => c.Plate, f => f.Random.Word());
        }
    }
}
