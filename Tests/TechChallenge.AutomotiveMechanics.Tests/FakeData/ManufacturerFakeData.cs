using Bogus;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public class ManufacturerFakeData : Faker<Manufacturer>
    {
        public ManufacturerFakeData()
        {
            RuleFor(c => c.Id, f => f.Random.Int(1, 99999999));
            RuleFor(c => c.Name, f => f.Random.Word());
        }
    }
}
