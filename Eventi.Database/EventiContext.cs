using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Eventi.Domain;
using Eventi.Data.Models;

namespace Eventi.Database
{
    public class EventiContext : DbContext
    {
        public EventiContext(DbContextOptions<EventiContext> options)
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<EventPerformer> EventPerformers { get; set; }
        public DbSet<EventSponsor> EventSponsors { get; set; }
        public DbSet<SaleType> SaleTypes { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseType> PurchaseTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .IsUnique();
            });

            //modelBuilder.Entity<Country>(entity =>
            //{
            //    entity.HasIndex(e => e.Name)
            //        .IsUnique();
            //});

            modelBuilder.Entity<EventPerformer>()
                .HasKey(k => new { k.EventID, k.PerformerID});
            modelBuilder.Entity<EventSponsor>()
                .HasKey(k => new { k.EventID, k.SponsorID });
            modelBuilder.Entity<Like>()
                .HasKey(k => new { k.EventID, k.ClientID });
            modelBuilder.Entity<Purchase>()
                .HasKey(k => new { k.EventID, k.ClientID });
        }

    }
}
