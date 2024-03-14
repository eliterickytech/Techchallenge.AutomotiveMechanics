using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using MassTransit;

namespace TechChallenge.AutomotiveMechanics.Presentation.API.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPublishEndpoint _publisher;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IBaseNotification baseNotification,
            IHttpClientFactory httpClientFactory,
            ILogger<OrderController> logger,
            IPublishEndpoint publisher)
            : base(baseNotification)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _publisher = publisher;
        }


        //[HttpPost]
        //public IActionResult Post()
        //{
        //    var factory = new ConnectionFactory()
        //    {
        //        HostName = "localhost",
        //        UserName = "guest",
        //        Password = "guest"
        //    };
        //    using var connection = factory.CreateConnection();
        //    using (var channel = connection.CreateModel())
        //    {
        //        channel.QueueDeclare(
        //            queue: "fila",
        //            durable: false,
        //            exclusive: false,
        //            autoDelete: false,
        //            arguments: null);

        //        var message = System.Text.Json.JsonSerializer.Serialize(
        //            new Order("Porsche 911", 2500, "cliente@email.com"));


        //        var body = Encoding.UTF8.GetBytes(message);
        //        channel.BasicPublish(
        //            exchange: "",
        //            routingKey: "fila",
        //            basicProperties: null,
        //            body: body);
        //    }

        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            await _publisher.Publish<Order>(order);
            _logger.LogInformation($"Send {nameof(Order)}");

            return Ok();
        }
    }
}
