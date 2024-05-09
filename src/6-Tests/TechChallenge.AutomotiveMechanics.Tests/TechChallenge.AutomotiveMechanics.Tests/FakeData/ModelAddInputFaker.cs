using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public class ModelAddInputFaker : Faker<ModelInsertInput>
    {
        public ModelAddInputFaker()
        {
            RuleFor(c => c.ManufacturerId, f => f.Random.Int(1, 999999));
            RuleFor(c => c.Name, f => f.Random.Word());
        }
    }
}
