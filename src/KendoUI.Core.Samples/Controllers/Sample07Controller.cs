using System.Collections.Generic;
using System.Linq;
using KendoUI.Core.Samples.Models;
using Microsoft.AspNetCore.Mvc;

namespace KendoUI.Core.Samples.Controllers
{
    public class Sample07Controller : Controller
    {
        public IActionResult Index()
        {
            return View(); // shows the page.
        }

        [HttpDelete]
        public IActionResult DeleteItem(int id)
        {
            var item = RegistrationsDataSource.LatestRegistrations.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();

            RegistrationsDataSource.LatestRegistrations.Remove(item);

            return Ok(item);

        }

        [HttpGet]
        public IEnumerable<Registration> GetItem()
        {
            return RegistrationsDataSource.LatestRegistrations;
        }

        [HttpPost]
        public IActionResult PostItem([FromBody]Registration registration)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = 1;
            var lastItem = RegistrationsDataSource.LatestRegistrations.LastOrDefault();
            if (lastItem != null)
            {
                id = lastItem.Id + 1;
            }
            registration.Id = id;
            RegistrationsDataSource.LatestRegistrations.Add(registration);

            // ارسال آی دی مهم است تا از ارسال رکوردهای تکراری جلوگیری شود
            return Created(this.Request.Path, registration);
        }

        [HttpPut]
        public IActionResult UpdateItem(int id, [FromBody]Registration registration)
        {
            var item = RegistrationsDataSource.LatestRegistrations
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


            if (!ModelState.IsValid || id != registration.Id)
                return BadRequest();

            RegistrationsDataSource.LatestRegistrations[item.Index] = registration;
            return Ok();
        }
    }
}