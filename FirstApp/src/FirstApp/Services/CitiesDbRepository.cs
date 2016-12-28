using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp.Services
{
    using FirstApp.Entities;

    using Microsoft.EntityFrameworkCore;

    public class CitiesDbRepository : ICitiesDbRepository
    {
        private readonly CitiesDbContext context;

        public CitiesDbRepository(CitiesDbContext context)
        {
            this.context = context;
        }

        public bool CityExists(int id)
        {
            return this.context.Cities.Any(x => x.Id == id);
        }

        public IEnumerable<City> GetCities()
        {
            return this.context.Cities.OrderBy(x => x.Name).ToList();
        }

        public City GetCity(int cityId, bool includePoints)
        {
            if (includePoints)
            {
                return this.context.Cities.Include(x => x.PointsOfInterest).FirstOrDefault(x => x.Id == cityId);
            }
            return this.context.Cities.FirstOrDefault(x => x.Id == cityId);
        }

        public IEnumerable<PointOfInterest> GetPointOfInterests(int cityId)
        {
            return this.context.PointsOfInterest.Where(x => x.CityId == cityId).ToList();
        }

        public PointOfInterest GetPointOfInterest(int cityId, int pointId)
        {
            return this.context.PointsOfInterest.FirstOrDefault(x => x.CityId == cityId && x.Id == pointId);
        }

        public void AddPointOfInterest(int cityId, PointOfInterest poiEntity)
        {
            var city = GetCity(cityId, false);
            city.PointsOfInterest.Add(poiEntity);
        }

        public bool Save()
        {
            return this.context.SaveChanges() >= 0;
        }
    }
}
