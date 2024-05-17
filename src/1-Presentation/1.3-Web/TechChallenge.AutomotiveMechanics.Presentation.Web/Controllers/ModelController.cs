using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.AutomotiveMechanics.Presentation.Web.Filters;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;

namespace TechChallenge.AutomotiveMechanics.Presentation.Web.Controllers
{
    [RoleAuthorize]
    public class ModelController : BaseController
    {
        private readonly IModelService _modelService;
        public ModelController(IBaseNotification baseNotification, IModelService modelService) : base(baseNotification)
        {
            _modelService = modelService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _modelService.ListAsync();

            return View(result);
        }

    }
}
