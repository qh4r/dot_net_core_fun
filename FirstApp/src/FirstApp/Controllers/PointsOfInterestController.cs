using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;

    using AutoMapper;

    using FirstApp.Entities;
    using FirstApp.Models;
    using FirstApp.Services;

    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.Extensions.Logging;

    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        //wymaga add logging w startupie
        private ILogger<PointsOfInterestController> logger;

        private readonly IDummyService sampleService;

        private readonly ICitiesDbRepository citiesDbRepository;

        //di
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IDummyService sampleService, ICitiesDbRepository citiesDbRepository)
        {
            this.logger = logger;
            this.sampleService = sampleService;
            this.citiesDbRepository = citiesDbRepository;
        }

        [HttpGet("{id}/points")]
        public IActionResult GetPointsOfInterest(int id)
        {
            if (this.citiesDbRepository.CityExists(id))
            {
                var pts = this.citiesDbRepository.GetPointsOfInterestList(id);
                return this.Ok(Mapper.Map<IEnumerable<PointOfInterestDto>>(pts));
            }
            else
            {
                this.logger.LogInformation($"Miasto nie znalezione id: {id}");
                return this.NotFound();
            }
        }

        // name sprawia ze mozna sie do tego odnosic na przyklad przy redirectach
        [HttpGet("{cityId}/points/{pointId}", Name = "GetPOI")]
        public IActionResult GetPointOfInterest(int cityId, int pointId)
        {
            var poi = this.citiesDbRepository.GetPointOfInterest(cityId, pointId);

            return poi != null ? (IActionResult)this.Ok(Mapper.Map<PointOfInterestDto>(poi)) : this.NotFound();
        }

        [HttpPost("{cityId}/points")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfIntrestCreationDto pointofInterest)
        {
            // mozna dodawac wlasne checki recznie i wrzucac errory do this.ModelState.AddModelError()

            // MOdelState sprawdza poprawnosc przekazanego modelu po adnotacjach
            if (pointofInterest == null || !this.ModelState.IsValid)
            {
                return this.BadRequest("you must provide data");
            }

            if (!this.citiesDbRepository.CityExists(cityId))
            {
                return this.NotFound();
            }

            var poiEntity = Mapper.Map<PointOfInterest>(pointofInterest);
            this.citiesDbRepository.AddPointOfInterest(cityId, poiEntity);
            if (this.citiesDbRepository.Save())
            {
                var poiDto = Mapper.Map<PointOfInterestDto>(poiEntity);
                return this.CreatedAtRoute("GetPOI", new { cityId, pointId = poiDto.Id }, poiDto);
            }
            return this.StatusCode(500, "blad podczas zapisu");
        }


        [HttpPut("{cityId}/points/{pointId}")]
        public IActionResult UpdatePointOfInterest(int cityId, int pointId, [FromBody] PointOfIntrestCreationDto pointofInterest)
        {
            if (pointofInterest == null || !this.ModelState.IsValid)
            {
                return this.BadRequest("you must provide data");
            }


            if (!this.citiesDbRepository.CityExists(cityId))
            {
                return this.NotFound();
            }

            var oldPoi = this.citiesDbRepository.GetPointOfInterest(cityId, pointId);
            if (oldPoi == null)
            {
                return this.NotFound();
            }

            // updatuje encje nowymi danymi
            Mapper.Map(pointofInterest, oldPoi);


            //oldPoi.Description = pointofInterest.Description ?? oldPoi.Description;
            //oldPoi.Name = pointofInterest.Name ?? oldPoi.Name;
            if (this.citiesDbRepository.Save())
            {
                var poiDto = Mapper.Map<PointOfInterestDto>(oldPoi);
                return this.CreatedAtRoute("GetPOI", new { cityId, pointId = poiDto.Id }, poiDto);
            }
            return this.StatusCode(500, "blad podczas zapisu");
        }


        // poprawny format json patcha:
        //        [{
        //	        "op": "replace",
        //	        "path": "/name",
        //	        "value": "zmienione imieeee"
        //        }]

        [HttpPatch("{cityId}/points/{pointId}")]
        public IActionResult PatchPointOfInterest(int cityId, int pointId, [FromBody] JsonPatchDocument<PointOfIntrestCreationDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return this.BadRequest("you must provide data");
            }

            if (!this.citiesDbRepository.CityExists(cityId))
            {
                return this.NotFound();
            }

            var oldPoi = this.citiesDbRepository.GetPointOfInterest(cityId, pointId);
            if (oldPoi == null)
            {
                return this.NotFound();
            }
            var pointToPatch = new PointOfIntrestCreationDto();

            Mapper.Map(oldPoi, pointToPatch);


            // dzieki przekazaniu model state wszelkie bledy zostana na niego tez zappliowane. 2 argument to cel do apply bledow
            patchDocument.ApplyTo(pointToPatch, this.ModelState);

            //wywoluje ponowna validacje
            this.TryValidateModel(pointToPatch);

            if (!this.ModelState.IsValid)
            {
                //przekazuje errory
                return this.BadRequest(this.ModelState);
            }

            Mapper.Map(pointToPatch, oldPoi);
            if (this.citiesDbRepository.Save())
            {
                var poiDto = Mapper.Map<PointOfInterestDto>(oldPoi);
                return this.CreatedAtRoute("GetPOI", new { cityId, pointId = poiDto.Id }, poiDto);
            }
            return this.StatusCode(500, "blad podczas zapisu");
        }

        [HttpDelete("{cityId}/points/{pointId}")]
        public IActionResult DeletePointOfInterest(int cityId, int pointId)
        {
            this.sampleService.Act($"usuwamy {cityId}");
            if (!this.citiesDbRepository.CityExists(cityId))
            {
                return this.NotFound();
            }
            var poi = this.citiesDbRepository.GetPointOfInterest(cityId, pointId);

            if (poi == null)
            {
                return this.NotFound();
            }

            this.citiesDbRepository.DeletePointOfInterest(poi);
            if (this.citiesDbRepository.Save())
            {
                return this.NoContent();
            }
            return this.StatusCode(500, "blad podczas zapisu");
        }
    }
}
