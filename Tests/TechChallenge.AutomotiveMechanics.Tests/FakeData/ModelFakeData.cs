using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public class ModelFakeData : Faker<Model>
    {
        public ModelFakeData()
        {
            RuleFor(c => c.Id, f => f.Random.Int(1, 99999999));
            RuleFor(c => c.ManufacturerId, f => f.Random.Int(1, 3));
            RuleFor(c => c.Name, f => f.Random.Word());
        }
    }
}
