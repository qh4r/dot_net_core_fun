using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controllers
{
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;

    using FirstApp.Models;
    using FirstApp.Services;

    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.Extensions.Logging;

    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> logger;

        private readonly IDummyService sampleService;

        //di
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IDummyService sampleService)
        {
            this.logger = logger;
            this.sampleService = sampleService;
        }

        [HttpGet("{id}/points")]
        public IActionResult GetPointsOfInterest(int id)
        {
            var city = CitiesStore.CurrentStore.Cities.FirstOrDefault(x => x.Id == id);
            if (city != null)
            {
                return this.Ok(city.PointsOfInterest);
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
            var poi =
                CitiesStore.CurrentStore.Cities.FirstOrDefault(x => x.Id == cityId)?
                    .PointsOfInterest.FirstOrDefault(x => x.Id == pointId);
            return poi != null ? (IActionResult)this.Ok(poi) : this.NotFound();
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

            var city = CitiesStore.CurrentStore.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city == null)
            {
                return this.NotFound();
            }

            var newPoi = new PointOfInterestDto
            {
                Id = CitiesStore.CurrentStore.MaxPointId + 1,
                Description = pointofInterest.Description,
                Name = pointofInterest.Name
            };
            city.PointsOfInterest.Add(newPoi);
            return this.CreatedAtRoute("GetPOI", new { cityId = city.Id, pointId = newPoi.Id }, newPoi);
        }


        [HttpPut("{cityId}/points/{pointId}")]
        public IActionResult UpdatePointOfInterest(int cityId, int pointId, [FromBody] PointOfIntrestCreationDto pointofInterest)
        {
            if (pointofInterest == null || !this.ModelState.IsValid)
            {
                return this.BadRequest("you must provide data");
            }

            var city = CitiesStore.CurrentStore.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city == null)
            {
                return this.NotFound();
            }

            var oldPoi = city.PointsOfInterest.FirstOrDefault(x => x.Id == pointId);
            if (oldPoi == null)
            {
                return this.NotFound();
            }

            oldPoi.Description = pointofInterest.Description ?? oldPoi.Description;
            oldPoi.Name = pointofInterest.Name ?? oldPoi.Name;

            return this.CreatedAtRoute("GetPOI", new { cityId = city.Id, pointId = oldPoi.Id }, oldPoi);
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

            var city = CitiesStore.CurrentStore.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city == null)
            {
                return this.NotFound();
            }

            var oldPoi = city.PointsOfInterest.FirstOrDefault(x => x.Id == pointId);
            if (oldPoi == null)
            {
                return this.NotFound();
            }

            var pointToPatch = new PointOfIntrestCreationDto { Name = oldPoi.Name, Description = oldPoi.Description };

            // dzieki przekazaniu model state wszelkie bledy zostana na niego tez zappliowane. 2 argument to cel do apply bledow
            patchDocument.ApplyTo(pointToPatch, this.ModelState);

            //wywoluje ponowna validacje
            this.TryValidateModel(pointToPatch);

            if (!this.ModelState.IsValid)
            {
                //przekazuje errory
                return this.BadRequest(this.ModelState);
            }

            oldPoi.Description = pointToPatch.Description ?? oldPoi.Description;
            oldPoi.Name = pointToPatch.Name ?? oldPoi.Name;

            return this.CreatedAtRoute("GetPOI", new { cityId = city.Id, pointId = oldPoi.Id }, oldPoi);
        }

        [HttpDelete("{cityId}/points/{pointId}")]
        public IActionResult DeletePointOfInterest(int cityId, int pointId)
        {
            this.sampleService.Act($"usuwamy {cityId}");
            var city = CitiesStore.CurrentStore.Cities.FirstOrDefault(x => x.Id == cityId);
            var poi = city?.PointsOfInterest.FirstOrDefault(x => x.Id == pointId);

            if (poi == null)
            {
                return this.NotFound();
            }

            city.PointsOfInterest.Remove(poi);

            return this.NoContent();
        }
    }
}
