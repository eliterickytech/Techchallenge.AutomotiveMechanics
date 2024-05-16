using Bogus;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public static class ManufacturerFakeData
    {
        public static List<Manufacturer> GetManufacturers()
        {
            var faker = new Faker<Manufacturer>()
                .RuleFor(m => m.Id, f => f.Random.Int(1))
                .RuleFor(m => m.Name, f => f.Company.CompanyName());

            return faker.Generate(5); // Gera 5 fabricantes falsos
        }
    }
}
