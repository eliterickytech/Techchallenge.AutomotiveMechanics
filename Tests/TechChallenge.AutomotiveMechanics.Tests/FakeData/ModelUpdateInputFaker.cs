using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public class ModelUpdateInputFaker : Faker<ModelUpdateInput>
    {
        public ModelUpdateInputFaker()
        {
            RuleFor(c => c.Id, f => f.Random.Int(1, 99999999));
            RuleFor(c => c.ManufacturerId, f => f.Random.Int(1, 3));
            RuleFor(c => c.Name, f => f.Random.Word());
        }
    }
}
