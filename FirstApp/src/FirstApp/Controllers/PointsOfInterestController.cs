using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controllers
{
    using System.Linq;

    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
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
                return this.NotFound();
            }
        }
    }
}
