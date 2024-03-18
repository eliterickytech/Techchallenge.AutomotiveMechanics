
using MassTransit;
using TechChallenge.AutomotiveMechanics.Crosscutting.Shared.Events;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;

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
                    await Task.WhenAll(
                        _orderService.AddAsync(new Order(message.VehicleName, message.ServicePrice, message.Email)),
                        
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
