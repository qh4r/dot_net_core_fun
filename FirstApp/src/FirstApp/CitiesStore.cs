using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp
{
    using FirstApp.Models;

    public class CitiesStore
    {
        public static CitiesStore CurrentStore { get; } = new CitiesStore();

        public CitiesStore()
        {
            Cities = new List<CityDto>
                         {
                             new CityDto()
                                 {
                                     Id = 1,
                                     Name = "Knurów",
                                     Description = "Taki piekny i kopalnia jest",
                                     PointsOfInterest = new List<PointOfInterestDto>
                                                            {
                                                                new PointOfInterestDto
                                                                    {
                                                                        Id = 1,
                                                                        Name = "Machoczek",
                                                                        Description = "Jezioro"
                                                                    },
                                                                new PointOfInterestDto
                                                                    {
                                                                        Id = 2,
                                                                        Name="Bunkry",
                                                                        Description = "Czy sa czy nie ma jest zajebiscie"
                                                                    }
                                                            }
                                 },
                             new CityDto()
                                 {
                                     Id = 2,
                                     Name = "Gliwice",
                                     Description = "Takie piekne i Politechnika jest",
                                     PointsOfInterest = new List<PointOfInterestDto>
                                                            {
                                                                new PointOfInterestDto
                                                                    {
                                                                        Id = 3,
                                                                        Name = "Czołg",
                                                                        Description = "Został"
                                                                    },
                                                                new PointOfInterestDto
                                                                    {
                                                                        Id = 4,
                                                                        Name="Radiostacja",
                                                                        Description = "Jak w Paryżu",
                                                                    },
                                                                new PointOfInterestDto
                                                                    {
                                                                        Id = 5,
                                                                        Name="Zamek",
                                                                        Description = "Górnych wałów zawsze spoko",
                                                                    }
                                                            }
                                 },
                             new CityDto()
                                 {
                                     Id = 3, Name = "Warszawa", Description = "Gorole",
                                     PointsOfInterest = new List<PointOfInterestDto>
                                                            {
                                                                new PointOfInterestDto
                                                                    {
                                                                        Id = 6,
                                                                        Name = "Pałąc Kultury",
                                                                        Description = "Widok taki piekny"
                                                                    },
                                                                new PointOfInterestDto
                                                                    {
                                                                        Id = 7,
                                                                        Name="Stadion Narodowy",
                                                                        Description = "Gorsza strona Wisły",
                                                                    },
                                                                new PointOfInterestDto
                                                                    {
                                                                        Id = 8,
                                                                        Name="Muzeum Powstania",
                                                                        Description = "",
                                                                    },
                                                                new PointOfInterestDto
                                                                    {
                                                                        Id = 9,
                                                                        Name="Pałąc Łazienkowski",
                                                                        Description = "Relaxx",
                                                                    }
                                                            }
                                 },
                             new CityDto()
                                 {
                                     Id = 4, Name = "Sosnowiec", Description = "Szkoda gadac",
                                     PointsOfInterest = new List<PointOfInterestDto>()
                                 },
                         };
        }

        public List<CityDto> Cities { get; set; }
    }
}
