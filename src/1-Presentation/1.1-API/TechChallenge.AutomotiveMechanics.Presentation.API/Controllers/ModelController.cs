using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;

namespace TechChallenge.AutomotiveMechanics.Presentation.API.Controllers
{
    //[Authorize]
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

        /// <summary>
        /// Obtém modelo por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Enviar Id para requisição
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _modelService.FindByIdAsync(id);

            return OKOrBadRequest(result);
        }

        /// <summary>
        /// Obtém lista de modelos cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task< IActionResult> Get()
        {
            var result = await _modelService.ListAsync();
            return OKOrBadRequest(result);
        }

        /// <summary>
        /// Cadastrar novo modelo de veículo
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// Dados:
        /// 
        /// Nome do modelo
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Post(ModelInsertInput input)
        {
            var result = await _modelService.AddAsync(input);

            return CreatedOrBadRequest(result);
        }

        /// <summary>
        /// Atualizar modelo de veículos
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// Dados:
        /// 
        /// Id do modelo, Id do fabricante, Novo nome de modelo
        /// </remarks>
        [HttpPut]
        public async Task<IActionResult> Put(ModelUpdateInput input)
        {
            var result = await _modelService.UpdateAsync(input);

            return OKOrBadRequest(result);
        }

        /// <summary>
        /// Remover modelo de veículo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Id do modelo a ser removido
        /// </remarks>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _modelService.DeleteAsync(id);

            return OKOrBadRequest(result);
        }
    }
}
