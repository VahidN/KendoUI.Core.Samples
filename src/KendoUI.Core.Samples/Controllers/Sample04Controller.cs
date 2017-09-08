using System.Collections.Generic;
using System.Linq;
using KendoUI.Core.Samples.Models;
using Microsoft.AspNetCore.Mvc;

namespace KendoUI.Core.Samples.Controllers
{
    public class Sample04Controller : Controller
    {
        public IActionResult Index()
        {
            return View(); // shows the page.
        }

        public IEnumerable<Product> GetProducts()
        {
            return ProductDataSource.LatestProducts.Take(10);
        }
    }
}