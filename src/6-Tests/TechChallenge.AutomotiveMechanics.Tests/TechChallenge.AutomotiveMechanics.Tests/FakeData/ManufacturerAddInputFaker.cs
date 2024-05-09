using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public class ManufacturerAddInputFaker : Faker<ManufacturerInsertInput>
    {
        public ManufacturerAddInputFaker()
        {
            RuleFor(c => c.Name, f => f.Random.Word());
        }
    }
}
