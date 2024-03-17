using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using MassTransit;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;

namespace TechChallenge.AutomotiveMechanics.Presentation.API.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IBaseNotification baseNotification, IOrderService orderService)
            : base(baseNotification)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderInsertInput input)
        {
            await _orderService.NotifyOrderAsync(input);

            return OKOrBadRequest(true);
        }
    }
}
