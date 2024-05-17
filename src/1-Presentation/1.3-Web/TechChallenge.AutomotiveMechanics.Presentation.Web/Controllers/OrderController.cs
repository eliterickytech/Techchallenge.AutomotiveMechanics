using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.AutomotiveMechanics.Presentation.Web.Models;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Services;

namespace TechChallenge.AutomotiveMechanics.Presentation.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IBaseNotification baseNotification, IOrderService orderService) : base(baseNotification)
        {
            _orderService = orderService;
        }

        // GET: OrderController
        public async Task<IActionResult> Index()
        {
            var result = await _orderService.ListAsync();
            List<OrderModel> orders = result.Select(x => new OrderModel(x)).ToList();
            
            return View(orders);
        }

        //// GET: OrderController/Details/5
        //public async Task<IActionResult> Details(int id)
        //{
        //    return View();
        //}

        //// GET: OrderController/Create
        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}

        //// POST: OrderController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(IFormCollection collection)
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

        //// GET: OrderController/Edit/5
        //public async Task<IActionResult> Edit(int id)
        //{
        //    return View();
        //}

        //// POST: OrderController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, IFormCollection collection)
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

        //// GET: OrderController/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    return View();
        //}

        //// POST: OrderController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id, IFormCollection collection)
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
