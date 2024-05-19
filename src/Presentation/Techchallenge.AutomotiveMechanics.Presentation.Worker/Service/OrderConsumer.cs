
using MassTransit;
using TechChallenge.AutomotiveMechanics.Crosscutting.Shared.Events;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Presentation.Worker.Services
{
    public class OrderConsumer : IConsumer<OrderEvents>
    {
        private readonly IServiceProvider _serviceProvider;

        public OrderConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<OrderEvents> context)
        {
            var message = context.Message;

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var _orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

                try
                {
                    var order = new Order(message.VehicleName, message.ServicePrice, message.Email);
                    await Task.WhenAll(
                        
                        _orderService.AddAsync(order),

                        Console.Out.WriteLineAsync($"Order received for {message.VehicleName}"),

                        Task.CompletedTask
                    );
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}
