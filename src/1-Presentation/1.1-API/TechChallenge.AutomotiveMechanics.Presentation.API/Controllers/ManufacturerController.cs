using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Services;

namespace TechChallenge.AutomotiveMechanics.Presentation.API.Controllers
{
    [Authorize]
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

        /// <summary>
        /// Obtém lista de fabricantes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task< IActionResult> Get()
        {
            var result = await _manufacturerService.ListAsync();
            return OKOrBadRequest(result);
        }

        /// <summary>
        /// Obtém fabricante por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Enviar Id para requisição
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _manufacturerService.FindByIdAsync(id);

            return OKOrBadRequest(result);
        }

        /// <summary>
        /// Cadastrar novo fabricante
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// Dados:
        /// 
        /// Nome do fabricante
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Post(ManufacturerInsertInput input)
        {
            var result = await _manufacturerService.AddAsync(input);

            return CreatedOrBadRequest(result);
        }

        /// <summary>
        /// Atualizar dados do fabricante
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// Inserir novo nome do fabricante
        /// </remarks>
        [HttpPut]
        public async Task<IActionResult> Put(ManufacturerUpdateInput input)
        {
            var result = await _manufacturerService.UpdateAsync(input);

            return OKOrBadRequest(result);
        }

        /// <summary>
        /// Remover fabricante
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Id do fabricante a ser removido
        /// </remarks>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _manufacturerService.DeleteAsync(id);

            return OKOrBadRequest(result);
        }
    }
}
