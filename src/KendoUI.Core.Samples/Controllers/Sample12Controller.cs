using System.Collections.Generic;
using System.Linq;
using KendoUI.Core.Samples.Models;
using Microsoft.AspNetCore.Mvc;

namespace KendoUI.Core.Samples.Controllers
{
    public class Sample12Controller : Controller
    {
        public IActionResult Index()
        {
            return View(); // shows the page.
        }

        [HttpGet]
        public IList<Category> GetCategories()
        {
            return CategoriesDataSource.Items;
        }

        [HttpGet]
        public IList<Product> GetProducts(int categoryId)
        {

            var products = CategoriesDataSource.Items
                            .Where(category => category.CategoryId == categoryId)
                            .SelectMany(category => category.Products)
                            .ToList();
            return products;
        }
    }
}