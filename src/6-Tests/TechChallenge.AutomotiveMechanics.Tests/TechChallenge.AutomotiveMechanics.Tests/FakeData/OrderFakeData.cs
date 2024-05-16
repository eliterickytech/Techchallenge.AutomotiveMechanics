using Bogus;
using System.Collections.Generic;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Tests.FakeData
{
    public static class OrderFakeData
    {
        public static List<Order> GetOrders()
        {
            var faker = new Faker<Order>()
                .CustomInstantiator(f => new Order(
                    vehicleName: f.Vehicle.Model(),
                    servicePrice: f.Finance.Amount(500, 5000),
                    email: f.Internet.Email()
                ));

            return faker.Generate(5); // Gera 5 pedidos falsos
        }
    }
}
