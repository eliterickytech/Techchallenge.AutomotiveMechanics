using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;

namespace TechChallenge.AutomotiveMechanics.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OrderController(IBaseNotification baseNotification,
            IHttpClientFactory httpClientFactory)
            : base(baseNotification)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderInsertInput order)
        {
            var azFunctionUrl = "https://az-orderapproval.azurewebsites.net/api/HttpStart_OrderApproval?";

            using(var httpClient = _httpClientFactory.CreateClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(azFunctionUrl, content);

                return CreatedOrBadRequest(response);
            }
        }
    }
}
