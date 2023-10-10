using Microsoft.AspNetCore.Mvc;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;

namespace TechChallenge.AutomotiveMechanics.Presentation.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ServiceController : BaseController
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IBaseNotification baseNotification, IServiceService serviceService) : base(baseNotification)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _serviceService.ListAsync();
            return OKOrBadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _serviceService.FindByIdAsync(id);

            return OKOrBadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ServiceInsertInput input)
        {
            var result = await _serviceService.AddAsync(input);

            return CreatedOrBadRequest(result);
        }


        [HttpPut]
        public async Task<IActionResult> Put(ServiceUpdateInput input)
        {
            var result = await _serviceService.UpdateAsync(input);

            return OKOrBadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviceService.DeleteAsync(id);

            return OKOrBadRequest(result);
        }
    }
}
