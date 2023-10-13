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
    public class CarController : BaseController
    {
        private readonly ICarService _carService;
        public CarController(IBaseNotification baseNotification, ICarService carService) : base(baseNotification)
        {
            _carService = carService;
        }

        /// <summary>
        /// Obtém lista de carros
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task< IActionResult> Get()
        {
            var result = await _carService.ListAsync();
            return OKOrBadRequest(result);
        }

        /// <summary>
        /// Obtém carro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Enviar Id para requisição
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _carService.FindByIdAsync(id);

            return OKOrBadRequest(result);
        }

        /// <summary>
        /// Adicionar um novo carro
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// Dados:
        /// 
        /// Id do modelo, ano de fabricação e placa
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Post(CarInsertInput input)
        {
            var result = await _carService.AddAsync(input);

            return CreatedOrBadRequest(result);
        }

        /// <summary>
        /// Atualizar dados do carro
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// Dados:
        /// 
        /// Id do carro, Id do modelo e ano de fabricação
        /// </remarks>
        [HttpPut]
        public async Task<IActionResult> Put(CarUpdateInput input)
        {
            var result = await _carService.UpdateAsync(input);

            return OKOrBadRequest(result);
        }

        /// <summary>
        /// Excluir Carro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Enviar Id do carro a ser removido
        /// </remarks>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _carService.DeleteAsync(id);

            return OKOrBadRequest(result);
        }
    }
}
