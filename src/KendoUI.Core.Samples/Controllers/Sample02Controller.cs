using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace KendoUI.Core.Samples.Controllers
{
    public class MyProduct
    {
        public int Id { set; get; }
        public string Name { set; get; }
    }

    public class Sample02Controller : Controller
    {
        public IActionResult Index()
        {
            return View(); // shows the page.
        }

        public IEnumerable<MyProduct> GetProducts(string param1, string param2)
        {
            var products = new List<MyProduct>();
            for (var i = 1; i <= 100; i++)
            {
                products.Add(new MyProduct { Id = i, Name = "Product " + i });
            }
            return products;
        }
    }
}