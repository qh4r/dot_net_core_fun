using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp
{
    using FirstApp.Entities;

    public static class CitiesDbExtensions
    {
        public static void EnsureSeedDataForCities(this CitiesDbContext context)
        {
            // seed danych jesli pusto
            if (!context.Cities.Any())
            {
                context.Cities.AddRange(
                    new List<City>
                        {
                            new City()
                                {
                                    Name = "Knurów",
                                    Description = "Taki piekny i kopalnia jest",
                                    PointsOfInterest =
                                        new List<PointOfInterest>
                                            {
                                                new PointOfInterest
                                                    {
                                                        Name =
                                                            "Machoczek",
                                                        Description
                                                            =
                                                            "Jezioro"
                                                    },
                                                new PointOfInterest
                                                    {
                                                        Name =
                                                            "Bunkry",
                                                        Description
                                                            =
                                                            "Czy sa czy nie ma jest zajebiscie"
                                                    }
                                            }
                                },
                            new City()
                                {
                                    Name = "Gliwice",
                                    Description = "Takie piekne i Politechnika jest",
                                    PointsOfInterest =
                                        new List<PointOfInterest>
                                            {
                                                new PointOfInterest
                                                    {
                                                        Name =
                                                            "Czołg",
                                                        Description
                                                            =
                                                            "Został"
                                                    },
                                                new PointOfInterest
                                                    {
                                                        Name =
                                                            "Radiostacja",
                                                        Description
                                                            =
                                                            "Jak w Paryżu",
                                                    },
                                                new PointOfInterest
                                                    {
                                                        Name =
                                                            "Zamek",
                                                        Description
                                                            =
                                                            "Górnych wałów zawsze spoko",
                                                    }
                                            }
                                },
                            new City()
                                {
                                    Name = "Warszawa",
                                    Description = "Gorole",
                                    PointsOfInterest =
                                        new List<PointOfInterest>
                                            {
                                                new PointOfInterest
                                                    {
                                                        Name =
                                                            "Pałąc Kultury",
                                                        Description
                                                            =
                                                            "Widok taki piekny"
                                                    },
                                                new PointOfInterest
                                                    {
                                                        Name =
                                                            "Stadion Narodowy",
                                                        Description
                                                            =
                                                            "Gorsza strona Wisły",
                                                    },
                                                new PointOfInterest
                                                    {
                                                        Name =
                                                            "Muzeum Powstania",
                                                        Description
                                                            = "",
                                                    },
                                                new PointOfInterest
                                                    {
                                                        Name =
                                                            "Pałąc Łazienkowski",
                                                        Description
                                                            =
                                                            "Relaxx",
                                                    }
                                            }
                                },
                            new City()
                                {
                                    Name = "Sosnowiec",
                                    Description = "Szkoda gadac",
                                    PointsOfInterest = new List<PointOfInterest>()
                                },
                        });
                context.SaveChanges();
            }
        }
    }
}
