using MailKit;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Consumers
{
    public class Worker : BackgroundWorker
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOrderRepository _orderRepository;

        public Worker(ILogger<Worker> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest"
                };

                using var connection = factory.CreateConnection();
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                    queue: "fila",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += async (sender, eventArgs) =>
                    {
                        var body = eventArgs.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var pedido = JsonSerializer.Deserialize<Order>(message);

                        await _orderRepository.SaveOrderAsync(pedido);
                    };

                    channel.BasicConsume(
                        queue: "fila",
                        autoAck: true,
                        consumer: consumer);
                }
                await Task.Delay(2000, cancellationToken);
            }
        }
    }
}
