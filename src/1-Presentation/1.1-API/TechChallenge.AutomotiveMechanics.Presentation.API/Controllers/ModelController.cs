using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;

namespace TechChallenge.AutomotiveMechanics.Presentation.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ModelController : BaseController
    {
        private readonly IModelService _modelService;
        public ModelController(IBaseNotification baseNotification, IModelService modelService) : base(baseNotification)
        {
            _modelService = modelService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _modelService.FindByIdAsync(id);

            return OKOrBadRequest(result);
        }

        [HttpGet]
        public async Task< IActionResult> Get()
        {
            var result = await _modelService.ListAsync();
            return OKOrBadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ModelUpdateInput input)
        {
            var result = await _modelService.AddAsync(input);

            return CreatedOrBadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ModelUpdateInput input)
        {
            var result = await _modelService.UpdateAsync(input);

            return OKOrBadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _modelService.DeleteAsync(id);

            return OKOrBadRequest(result);
        }
    }
}
