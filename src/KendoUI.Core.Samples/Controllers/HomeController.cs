using Microsoft.AspNetCore.Mvc;

namespace KendoUI.Core.Samples.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
