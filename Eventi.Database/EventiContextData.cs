using Eventi.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Eventi.Database
{
    public partial class EventiContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasData
                (
                    new List<Country>()
                    {
                        new Country { ID = 1, Name = "Bosnia and Herzegovina"},
                        new Country { ID = 2, Name = "Croatia"},
                        new Country { ID = 3, Name = "Serbia"}
                    }
                );

            modelBuilder.Entity<City>()
                .HasData
                (
                    new List<City>()
                    {
                        new City { ID = 1, CountryID = 1, Name = "Sarajevo"},
                        new City { ID = 2, CountryID = 1, Name = "Mostar"},
                        new City { ID = 3, CountryID = 1, Name = "Zenica"},
                        new City { ID = 4, CountryID = 1, Name = "Tuzla"},
                        new City { ID = 5, CountryID = 1, Name = "Banja Luka"},
                        new City { ID = 6, CountryID = 1, Name = "Bihać"},
                        new City { ID = 7, CountryID = 2, Name = "Zagreb"},
                        new City { ID = 8, CountryID = 2, Name = "Split"},
                        new City { ID = 9, CountryID = 3, Name = "Beograd"},
                        new City { ID = 10, CountryID = 3, Name = "Novi Sad"},
                    }
                );
        }
    }
}
