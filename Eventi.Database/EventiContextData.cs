using Eventi.Core.Helpers;
using Eventi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Eventi.Database
{
    public partial class EventiContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            var tempSalt = HashHelper.GenerateSalt();
            modelBuilder.Entity<Account>()
                .HasData
                (
                    new List<Account>()
                    {
                        new Account
                        {
                            ID = 1,
                            AccountCategory = Common.AccountCategory.Organizer,
                            Email = "org@eventi.com",
                            PasswordSalt = tempSalt,
                            PasswordHash = HashHelper.GenerateHash(tempSalt, "testtest"),
                            Username = "org"
                        }
                    }
                );


            modelBuilder.Entity<Organizer>()
                .HasData
                (
                    new List<Organizer>()
                    {
                        new Organizer
                        {
                            ID = 1, AccountID = 1
                        }
                    }
                );

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


            modelBuilder.Entity<Venue>()
                .HasData
                (
                    new List<Venue>()
                    {
                        new Venue { ID = 1,  CityID = 1, Name = "Venue 01", Address = "Address", VenueCategory = Common.VenueCategory.Arena},
                        new Venue { ID = 2,  CityID = 1, Name = "Venue 02", Address = "Address", VenueCategory = Common.VenueCategory.Arena},
                        new Venue { ID = 3,  CityID = 1, Name = "Venue 03", Address = "Address", VenueCategory = Common.VenueCategory.Arena},
                        new Venue { ID = 4,  CityID = 1, Name = "Venue 04", Address = "Address", VenueCategory = Common.VenueCategory.Arena},
                        new Venue { ID = 5,  CityID = 1, Name = "Venue 05", Address = "Address", VenueCategory = Common.VenueCategory.Hall},
                        new Venue { ID = 6,  CityID = 1, Name = "Venue 06", Address = "Address", VenueCategory = Common.VenueCategory.Hall},
                        new Venue { ID = 7,  CityID = 2, Name = "Venue 07", Address = "Address", VenueCategory = Common.VenueCategory.Hall},
                        new Venue { ID = 8,  CityID = 2, Name = "Venue 08", Address = "Address", VenueCategory = Common.VenueCategory.Stadium},
                        new Venue { ID = 9,  CityID = 3, Name = "Venue 09", Address = "Address", VenueCategory = Common.VenueCategory.Stadium},
                        new Venue { ID = 10, CityID = 3, Name = "Venue 10", Address = "Address", VenueCategory = Common.VenueCategory.Stadium}
                    }
                );


            modelBuilder.Entity<Event>()
                .HasData
                (
                    new List<Event>()
                    {
                        new Event { ID = 1,  OrganizerID = 1, VenueID = 1,  Name = "Event 01", Start = DateTime.Now.AddDays(100), End = DateTime.Now.AddDays(101), IsApproved = true, IsCanceled = false},
                        new Event { ID = 2,  OrganizerID = 1, VenueID = 2,  Name = "Event 01", Start = DateTime.Now.AddDays(100), End = DateTime.Now.AddDays(101), IsApproved = true, IsCanceled = false},
                        new Event { ID = 3,  OrganizerID = 1, VenueID = 3,  Name = "Event 01", Start = DateTime.Now.AddDays(100), End = DateTime.Now.AddDays(101), IsApproved = true, IsCanceled = false},
                        new Event { ID = 4,  OrganizerID = 1, VenueID = 4,  Name = "Event 01", Start = DateTime.Now.AddDays(100), End = DateTime.Now.AddDays(101), IsApproved = true, IsCanceled = false},
                        new Event { ID = 5,  OrganizerID = 1, VenueID = 5,  Name = "Event 01", Start = DateTime.Now.AddDays(100), End = DateTime.Now.AddDays(101), IsApproved = true, IsCanceled = false},
                        new Event { ID = 6,  OrganizerID = 1, VenueID = 6,  Name = "Event 01", Start = DateTime.Now.AddDays(100), End = DateTime.Now.AddDays(101), IsApproved = true, IsCanceled = false},
                        new Event { ID = 7,  OrganizerID = 1, VenueID = 7,  Name = "Event 01", Start = DateTime.Now.AddDays(100), End = DateTime.Now.AddDays(101), IsApproved = true, IsCanceled = false},
                        new Event { ID = 8,  OrganizerID = 1, VenueID = 8,  Name = "Event 01", Start = DateTime.Now.AddDays(100), End = DateTime.Now.AddDays(101), IsApproved = true, IsCanceled = false},
                        new Event { ID = 9,  OrganizerID = 1, VenueID = 9,  Name = "Event 01", Start = DateTime.Now.AddDays(100), End = DateTime.Now.AddDays(101), IsApproved = true, IsCanceled = false},
                        new Event { ID = 10, OrganizerID = 1, VenueID = 10, Name = "Event 01", Start = DateTime.Now.AddDays(100), End = DateTime.Now.AddDays(101), IsApproved = true, IsCanceled = false}
                    }
                );
        }
    }
}
