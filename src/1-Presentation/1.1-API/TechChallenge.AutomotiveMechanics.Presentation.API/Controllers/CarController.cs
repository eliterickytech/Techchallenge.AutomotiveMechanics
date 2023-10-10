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
    public class CarController : BaseController
    {
        private readonly ICarService _carService;
        public CarController(IBaseNotification baseNotification, ICarService carService) : base(baseNotification)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task< IActionResult> Get()
        {
            var result = await _carService.ListAsync();
            return OKOrBadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _carService.FindByIdAsync(id);

            return OKOrBadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CarInsertInput input)
        {
            var result = await _carService.AddAsync(input);

            return CreatedOrBadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(CarUpdateInput input)
        {
            var result = await _carService.UpdateAsync(input);

            return OKOrBadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _carService.DeleteAsync(id);

            return OKOrBadRequest(result);
        }
    }
}
