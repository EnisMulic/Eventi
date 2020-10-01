using Microsoft.EntityFrameworkCore;
using Eventi.Domain;
using System;

namespace Eventi.Database
{
    public partial class EventiContext : DbContext
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
        public DbSet<Section> Sections { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<EventPerformer> EventPerformers { get; set; }
        public DbSet<EventSponsor> EventSponsors { get; set; }
        public DbSet<Purchase> Purchases { get; set; }


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
            modelBuilder.Entity<Purchase>()
                .HasKey(k => new { k.TicketID, k.ClientID });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
