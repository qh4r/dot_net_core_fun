using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp.Controllers
{
    using AutoMapper;

    using FirstApp.Entities;
    using FirstApp.Models;
    using FirstApp.Services;

    using Microsoft.AspNetCore.Mvc;

    //[controller] to w tym wypadku cities - matchuje po nazwie
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        private readonly ICitiesDbRepository citiesDbRepository;

        public CitiesController(ICitiesDbRepository citiesDbRepository)
        {
            this.citiesDbRepository = citiesDbRepository;
        }

        //[HttpGet("api/cities")]
        // wyciagniety route
        [HttpGet()]
        public IActionResult GetCities()
        {
            //return new JsonResult(CitiesStore.CurrentStore.Cities);
            // lepsze wyjscie
            return new OkObjectResult(this.citiesDbRepository.GetCities().Select(x => new { x.Id, x.Name, x.Description }));
        }

        // parametr matchuje po nazwach
        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePts = false) // incudePts to query argument
        {
            var data = this.citiesDbRepository.GetCity(id, true);
            if (data != null)
            {
                if (includePts)
                {
                    var city = Mapper.Map<CityDto>(data);
                    //var city = new CityDto { Id = data.Id, Name = data.Name, Description = data.Description };
                    //foreach (var pointOfInterest in data.PointsOfInterest)
                    //{
                    //    city.PointsOfInterest.Add(new PointOfInterestDto
                    //    {
                    //        Id = pointOfInterest.Id,
                    //        Name = pointOfInterest.Name,
                    //        Description = pointOfInterest.Description
                    //    });
                    //}
                    return new OkObjectResult(city);
                }
                // dziala wykrywanie nazw propert!!!!!! ~taka destrukturyzacja
                return new OkObjectResult(new { data.Id, data.Name, data.Description, NumberOfPoint = data.PointsOfInterest.Count });
            }
            else
            {
                return new NotFoundResult();
            }
        }
    }
}
