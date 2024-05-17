using Microsoft.AspNetCore.Mvc;
using TechChallenge.AutomotiveMechanics.Presentation.Web.Models;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;

namespace TechChallenge.AutomotiveMechanics.Presentation.Web.Controllers
{
        public class CarController : BaseController
        {
            private readonly ICarService _carService;
            public CarController(IBaseNotification baseNotification, ICarService carService) : base(baseNotification)
            {
                _carService = carService;
            }
            public async Task<IActionResult> Index()
            {
                var result = await _carService.ListAsync();

                return View(result);
            }

        }
    }
