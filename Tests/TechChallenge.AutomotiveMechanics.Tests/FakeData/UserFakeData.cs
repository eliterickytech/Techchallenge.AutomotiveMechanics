using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public static class UserFakeData
    {
        public static Faker<User> GetUserFaker()
        {
            return new Faker<User>()
                .RuleFor(u => u.Id, f => f.Random.Int(1, 1000))
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.PasswordHash, f => Encoding.UTF8.GetBytes(f.Internet.Password()))
                .RuleFor(u => u.PasswordSalt, f => Encoding.UTF8.GetBytes(f.Random.AlphaNumeric(10)));
        }
    }
}
