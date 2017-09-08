using System.Collections.Generic;
using KendoUI.Core.Samples.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KendoUI.Core.Samples.Controllers
{
    public class Sample09Controller : Controller
    {
        public IActionResult Index()
        {
            return View(); // shows the page.
        }

        [HttpPost]
        public ActionResult Index([FromBody]OrderDetailViewModel model)
        {
            this.ModelState.AddModelError("test", "خطای آزمایشی سمت سرور");
            return View();
        }


        [HttpPost]
        public ActionResult Save(IEnumerable<IFormFile> files, string codeId)
        {
            if (files != null)
            {
                // ...
                // Process the files and save them
                // ...
            }

            // Return an empty string to signify success
            return Content("");
        }

        [HttpPost]
        public ContentResult Remove(string[] fileNames)
        {
            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    // ...
                    // delete the files
                    // ...
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

    }
}