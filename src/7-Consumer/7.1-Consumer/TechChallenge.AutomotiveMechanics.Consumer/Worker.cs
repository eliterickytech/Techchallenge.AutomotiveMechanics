using MassTransit;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;

namespace TechChallenge.AutomotiveMechanics.Consumer
{
    public class WorkerClient : IConsumer<Order>
    {
        private readonly IOrderRepository _orderRepository;

        public WorkerClient(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task Consume(ConsumeContext<Order> context)
        {
            var vehicleName = context.Message.VehicleName;
            var servicePrice = context.Message.ServicePrice;
            var email = context.Message.Email;

            Console.WriteLine($"New order: {vehicleName} - {servicePrice} - {email}");

            var order = new Order(vehicleName, servicePrice, email);

            await _orderRepository.SaveOrderAsync(order);
        }
    }
}