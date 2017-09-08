using System.Linq;
using KendoUI.Core.Samples.Models;
using Microsoft.AspNetCore.Mvc;

namespace KendoUI.Core.Samples.Controllers
{
    public class Sample11Controller : Controller
    {
        public IActionResult Index()
        {
            return View(); // shows the page.
        }

        [HttpGet]
        public IActionResult GetBlogComments(int? id)
        {
            if (id == null)
            {
                //دریافت ریشه‌ها
                return Json(
                    BlogCommentsDataSource.LatestComments
                        .Where(x => x.ParentId == null) // ریشه‌ها
                        .ToList());
            }
            else
            {
                //دریافت فرزندهای یک ریشه
                return Json(
                    BlogCommentsDataSource.LatestComments
                              .Where(x => x.ParentId == id)
                              .ToList());
            }
        }
    }
}