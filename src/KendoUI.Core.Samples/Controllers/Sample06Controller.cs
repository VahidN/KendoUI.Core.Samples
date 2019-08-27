using System.Linq;
using System.Threading.Tasks;
using Kendo.DynamicLinq;
using KendoUI.Core.Samples.Models;
using KendoUI.Core.Samples.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KendoUI.Core.Samples.Controllers
{
    // با ارث بری، خواص اضافی و سفارشی را به کلاس پایه اضافه می‌کنیم
    public class CustomDataSourceRequest : DataSourceRequest
    {
        public string Param1 { set; get; }
        public string Param2 { set; get; }
    }

    public class Sample06Controller : Controller
    {
        public IActionResult Index()
        {
            return View(); // shows the page.
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int id)
        {
            var item = ProductDataSource.LatestProducts.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();

            ProductDataSource.LatestProducts.Remove(item);

            return Json(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            await Task.Delay(500); //شبیه سازی یک عملیات طولانی برای نمایش منتظر بمانید

            var queryString = this.HttpContext.GetJsonDataFromQueryString();
            //اطلاعات رسیده با فرمت جی‌سون در داخل کوئری استرینگ قرار گرفته‌است
            //{"param1":"val1","param2":"val2","take":10,"skip":0,"page":1,"pageSize":10,"sort":[{"field":"Id","dir":"desc"}]}
            var request = JsonConvert.DeserializeObject<CustomDataSourceRequest>(queryString);

            var list = ProductDataSource.LatestProducts;
            return Json(list.AsQueryable()
                       .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter));
        }

        [HttpPost]
        public IActionResult PostProduct([FromBody]Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = 1;
            var lastItem = ProductDataSource.LatestProducts.LastOrDefault();
            if (lastItem != null)
            {
                id = lastItem.Id + 1;
            }
            product.Id = id;
            ProductDataSource.LatestProducts.Add(product);

            // گرید آی دی جدید را به این صورت دریافت می‌کند
            return Json(new DataSourceResult { Data = new[] { product } });
        }

        [HttpPut] // Add it to fix this error: The requested resource does not support http method 'PUT'
        public IActionResult UpdateProduct(int id, [FromBody]Product product)
        {
            var item = ProductDataSource.LatestProducts
                                        .Select(
                                            (prod, index) =>
                                                new
                                                {
                                                    Item = prod,
                                                    Index = index
                                                })
                                        .FirstOrDefault(x => x.Item.Id == id);
            if (item == null)
                return NotFound();


            if (!ModelState.IsValid || id != product.Id)
                return BadRequest();

            ProductDataSource.LatestProducts[item.Index] = product;

            //Return HttpStatusCode.OK
            return Ok();
        }
    }
}