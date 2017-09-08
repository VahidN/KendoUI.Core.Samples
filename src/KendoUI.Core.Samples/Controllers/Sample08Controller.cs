using System;
using KendoUI.Core.Samples.Models;
using Microsoft.AspNetCore.Mvc;

namespace KendoUI.Core.Samples.Controllers
{
    public class Sample08Controller : Controller
    {
        public IActionResult Index()
        {
            return View(); // shows the page.
        }

        [HttpPost]
        public IActionResult Index([FromBody]OrderDetailViewModel model)
        {
            this.ModelState.AddModelError("test", "خطای آزمایشی سمت سرور");
            return View(model);
        }

        //[HttpPost]
        public IActionResult DoesUserExist(string username)
        {
            return Json(username == "Vahid");
        }
    }
}