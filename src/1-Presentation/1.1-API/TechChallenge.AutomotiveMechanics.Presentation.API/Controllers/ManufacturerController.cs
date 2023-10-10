using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Services;

namespace TechChallenge.AutomotiveMechanics.Presentation.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ManufacturerController : BaseController
    {
        private readonly IManufacturerService _manufacturerService;
        public ManufacturerController(IBaseNotification baseNotification, IManufacturerService manufacturerService) : base(baseNotification)
        {
            _manufacturerService = manufacturerService;
        }

        [HttpGet]
        public async Task< IActionResult> Get()
        {
            var result = await _manufacturerService.ListAsync();
            return OKOrBadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _manufacturerService.FindByIdAsync(id);

            return OKOrBadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ManufacturerInsertInput input)
        {
            var result = await _manufacturerService.AddAsync(input);

            return CreatedOrBadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ManufacturerUpdateInput input)
        {
            var result = await _manufacturerService.UpdateAsync(input);

            return OKOrBadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _manufacturerService.DeleteAsync(id);

            return OKOrBadRequest(result);
        }
    }
}
