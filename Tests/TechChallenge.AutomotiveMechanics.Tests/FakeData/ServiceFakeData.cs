using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public static class ServiceFakeData
    {
        public static List<Service> GetServices()
        {
            var carFaker = new Faker<Car>()
                .RuleFor(c => c.Id, f => f.Random.Int(1))
                .RuleFor(c => c.ModelId, f => f.Random.Int(1, 10))
                .RuleFor(c => c.Plate, f => f.Vehicle.Vin());

            var faker = new Faker<Service>()
                .RuleFor(s => s.Id, f => f.Random.Int(1))
                .RuleFor(s => s.Name, f => f.Lorem.Word())
                .RuleFor(s => s.CarId, f => f.Random.Int(1, 10))
                .RuleFor(s => s.Car, carFaker.Generate());

            return faker.Generate(5); // Gera 5 serviços falsos
        }
    }
}
