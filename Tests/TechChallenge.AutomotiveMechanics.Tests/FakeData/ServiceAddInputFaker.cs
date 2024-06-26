﻿using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public class ServiceAddInputFaker : Faker<ServiceInsertInput>
    {
        public ServiceAddInputFaker()
        {
            RuleFor(c => c.Name, f => f.Random.Word());
            RuleFor(c => c.CarId, f => f.Random.Int(1, 5));
        }
    }
}
