using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;

namespace TechChallenge.AutomotiveMechanics.Presentation.API.Controllers
{
    //[Authorize]
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

        /// <summary>
        /// Obtém lista de serviços cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _serviceService.ListAsync();
            return OKOrBadRequest(result);
        }

        /// <summary>
        /// Obtém serviço por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Enviar Id do serviço para requisição
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _serviceService.FindByIdAsync(id);

            return OKOrBadRequest(result);
        }

        /// <summary>
        /// Cadastrar novo serviço 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// Dados:
        /// 
        /// Nome do serviço
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Post(ServiceInsertInput input)
        {
            var result = await _serviceService.AddAsync(input);

            return CreatedOrBadRequest(result);
        }

        /// <summary>
        /// Atualizar dados do serviço
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// Dados:
        /// 
        /// Id do serviço, novo nome do serviço
        /// </remarks>
        [HttpPut]
        public async Task<IActionResult> Put(ServiceUpdateInput input)
        {
            var result = await _serviceService.UpdateAsync(input);

            return OKOrBadRequest(result);
        }

        /// <summary>
        /// Remover serviço
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Id do serviço a ser removido
        /// </remarks>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviceService.DeleteAsync(id);

            return OKOrBadRequest(result);
        }
    }
}
