using Microsoft.AspNetCore.Mvc;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;

namespace TechChallenge.AutomotiveMechanics.Presentation.Web.Controllers
{
    public class ServiceController : BaseController
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IBaseNotification baseNotification, IServiceService serviceService) : base(baseNotification)
        {
            _serviceService = serviceService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _serviceService.ListAsync();

            return View(result);
        }

    }
}
