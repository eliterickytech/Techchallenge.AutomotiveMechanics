using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public static class CarFakeData
    {
        public static List<Car> GetCars()
        {
            var faker = new Faker<Car>()
                .RuleFor(c => c.Id, f => f.Random.Int(1))
                .RuleFor(c => c.ModelId, f => f.Random.Int(1, 100))
                .RuleFor(c => c.Plate, f => f.Vehicle.Vin())
                .RuleFor(c => c.YearManufactured, f => f.Date.Past(30).Year);

            return faker.Generate(5); // Gera 5 carros falsos
        }
    }
}
