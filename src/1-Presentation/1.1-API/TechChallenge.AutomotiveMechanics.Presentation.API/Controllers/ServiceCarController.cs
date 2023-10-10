using Microsoft.AspNetCore.Mvc;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;

namespace TechChallenge.AutomotiveMechanics.Presentation.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ServiceCarController : BaseController
    {
        private readonly IServiceCarService _serviceCarService;
        public ServiceCarController(IBaseNotification baseNotification, IServiceCarService serviceCarService) : base(baseNotification)
        {
            _serviceCarService = serviceCarService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var result = await _serviceService.ListAsync();
        //    return OKOrBadRequest(result);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    var result = await _serviceService.FindByIdAsync(id);

        //    return OKOrBadRequest(result);
        //}

        [HttpPost]
        public async Task<IActionResult> Post(ServiceCarInsertInput input)
        {
            var result = await _serviceCarService.AddServiceCarAsync(input);

            return CreatedOrBadRequest(result);
        }


        //[HttpPut]
        //public async Task<IActionResult> Put(ServiceUpdateInput input)
        //{
        //    var result = await _serviceService.UpdateAsync(input);

        //    return OKOrBadRequest(result);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _serviceService.DeleteAsync(id);

        //    return OKOrBadRequest(result);
        //}
    }
}
