using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp.Services
{
    using FirstApp.Entities;

    public interface ICitiesDbRepository
    {
        bool CityExists(int id);

        IEnumerable<City> GetCities();

        City GetCity(int city, bool includePoints);

        IEnumerable<PointOfInterest> GetPointOfInterests(int cityId);

        PointOfInterest GetPointOfInterest(int cityId, int pointId);

        void AddPointOfInterest(int cityId, PointOfInterest poiEntity);

        bool Save();
    }
}
