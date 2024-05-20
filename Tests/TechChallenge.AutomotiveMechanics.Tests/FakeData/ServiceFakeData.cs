using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public class ServiceFakeData : Faker<Service>
    {
        public ServiceFakeData()
        {
            RuleFor(c => c.Id, f => f.Random.Int(1, 99999999));
            RuleFor(c => c.Name, f => f.Random.Word());
            RuleFor(c => c.CarId, f => f.Random.Int(1, 5));
        }
    }
}
