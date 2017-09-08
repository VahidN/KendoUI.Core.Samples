using Microsoft.AspNetCore.Mvc;

namespace KendoUI.Core.Samples.Controllers
{
    public class Sample01Controller : Controller
    {
        public IActionResult Index()
        {
            return View(); // shows the page.
        }
    }
}