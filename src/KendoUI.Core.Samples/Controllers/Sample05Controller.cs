using System;
using System.Linq;
using Kendo.DynamicLinq;
using KendoUI.Core.Samples.Models;
using KendoUI.Core.Samples.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace KendoUI.Core.Samples.Controllers
{
    public class Sample05Controller : Controller
    {
        public IActionResult Index()
        {
            return View(); // shows the page.
        }

        public DataSourceResult GetProducts()
        {
            var dataString = this.HttpContext.GetJsonDataFromQueryString();
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(dataString);

            var list = ProductDataSource.LatestProducts;
            return list.AsQueryable()
                       .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        }
    }
}