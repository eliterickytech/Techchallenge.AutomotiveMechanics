using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.AutomotiveMechanics.Presentation.Web.Models;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;

namespace TechChallenge.AutomotiveMechanics.Presentation.Web.Controllers
{
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
        //[HttpGet]
        // GET: ManufacturerController
        public async Task<IActionResult> Index()
        {
            var result = await _manufacturerService.ListAsync();

            return View(result);
        }


        //// GET: ManufacturerController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: ManufacturerController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ManufacturerController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ManufacturerController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ManufacturerController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ManufacturerController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ManufacturerController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
