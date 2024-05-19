using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Services;

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
        /// <summary>
        /// Listar Orders
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _orderService.ListAsync();

            return OKOrBadRequest(result);
        }
        /// <summary>
        /// Order Insert
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<IActionResult> Post([FromBody] OrderInsertInput input)
        {
            await _orderService.NotifyOrderAsync(input);

            return OKOrBadRequest(true);
        }
    }
}
