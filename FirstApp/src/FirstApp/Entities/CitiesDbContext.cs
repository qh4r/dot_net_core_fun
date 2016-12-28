using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FirstApp.Entities
{
    //referencje do tools w project.json trza bylo dodwac recznie po zaisntalowaniu ef toolsow - wymagane do uzywania migracji
    public sealed class CitiesDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }

        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        // konstruktor uzywany leniwie dopiero przy pierwszej probie odeniesienia sie do db contextu przez apke
        public CitiesDbContext(DbContextOptions<CitiesDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
            Database.Migrate(); // Update-Database
            this.EnsureSeedDataForCities();
        }

        // jednaz mozliwosci okreslenia connection stringa
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            //optionsBuilder.UseSqlServer("nazwaserwera");
            base.OnConfiguring(optionsBuilder);            
        }
    }
}
