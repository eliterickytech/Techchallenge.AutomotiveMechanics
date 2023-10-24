using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData.CarData
{
    public class CarResultFaker : Faker<CarResult>
    {
        public CarResultFaker() 
        {
            var id = new Faker().Random.Number(1, 999999);
            var yearManufactured = new Faker().Random.Number(1800, 2023);
            RuleFor(p => p.Id, id);
            RuleFor(p => p.Plate, f => f.Random.Word());
            RuleFor(p => p.YearManufactured, yearManufactured);
            RuleFor(p => p.CreatedDate, f => f.Date.Past());
            RuleFor(p => p.LastModifiedDate, f => f.Date.Past());
        }
    }
}
