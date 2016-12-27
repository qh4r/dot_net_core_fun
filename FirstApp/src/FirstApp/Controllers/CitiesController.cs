using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    //[controller] to w tym wypadku cities - matchuje po nazwie
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        //[HttpGet("api/cities")]
        // wyciagniety route
        [HttpGet()]
        public IActionResult GetCities()
        {
            //return new JsonResult(CitiesStore.CurrentStore.Cities);
            // lepsze wyjscie
            return new OkObjectResult(CitiesStore.CurrentStore.Cities);
        }

        // parametr matchuje po nazwach
        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var data = CitiesStore.CurrentStore.Cities.FirstOrDefault(x => x.Id == id);
            if (data != null)
            {
                return new OkObjectResult(data);
            }
            else
            {
                return new NotFoundResult();
            }
        }
    }
}
