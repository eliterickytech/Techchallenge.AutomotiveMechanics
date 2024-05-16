using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public static class ModelFakeData
    {
        public static List<Model> GetModels()
        {
            var faker = new Faker<Model>()
                .RuleFor(m => m.Id, f => f.Random.Int(1))
                .RuleFor(m => m.Name, f => f.Vehicle.Model())
                .RuleFor(m => m.ManufacturerId, f => f.Random.Int(1, 10));

            return faker.Generate(5); // Gera 5 modelos de carros falsos
        }
    }
}
