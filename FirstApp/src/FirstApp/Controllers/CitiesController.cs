using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp.Controllers
{
    using FirstApp.Entities;

    using Microsoft.AspNetCore.Mvc;

    //[controller] to w tym wypadku cities - matchuje po nazwie
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        private readonly CitiesDbContext citiesDbContext;

        public CitiesController(CitiesDbContext citiesDbContext)
        {
            this.citiesDbContext = citiesDbContext;
        }

        //[HttpGet("api/cities")]
        // wyciagniety route
        [HttpGet()]
        public IActionResult GetCities()
        {
            //return new JsonResult(CitiesStore.CurrentStore.Cities);
            // lepsze wyjscie
            return new OkObjectResult(CitiesStore.CurrentStore.Cities.Select(x => new { x.Id, x.Name, x.Description }));
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return this.Ok(this.citiesDbContext.Cities.ToList());
        }

        // parametr matchuje po nazwach
        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var data = CitiesStore.CurrentStore.Cities.FirstOrDefault(x => x.Id == id);
            if (data != null)
            {
                // dziala wykrywanie nazw propert!!!!!! ~taka destrukturyzacja
                return new OkObjectResult(new { data.Id, data.Name, data.Description, NumberOfPoint = data.NumberOfPoints });
            }
            else
            {
                return new NotFoundResult();
            }
        }
    }
}
