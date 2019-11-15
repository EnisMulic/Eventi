using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EventAttender.Data.Models;

namespace EventAttender.Data.EF
{
    public class MojContext:DbContext
    {
        // -> Ovako je u eUniversity i tamo moze MojContext ctx=new MojContext()
        //public MojContext():base("Name=lokalni1") { }  // greska ?
        // lokalni1 je ime connectionstringa u appsettings.json

        // -> Ovako je u dokumentu rs1 2017-18
        //public MojContext(DbContextOptions<MojContext> options)
        // : base(options)
        //{    // ovako treba, i u sturtup konfiguracija je zakomentarisana-odkomentarisati
        //}
        // ali onda sta proslijediti u MojContext ctx=new MojContext(); ?
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=proba1;Trusted_Connection=True;MultipleActiveResultSets=true");
        }    // ovo ne treba, ali ovako radi, tj uspijeva se povezati sa bazom
        // u sql-u ce se napraviti baza proba1

        public DbSet<Osoba> Osoba { get; set; } 
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Grad> Grad { get; set; }
        public DbSet<Izvodjac> Izvodjac { get; set; }
        public DbSet<IzvodjacEvent> IzvodjacEvent { get; set; }
        public DbSet<Karta> Karta { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Kupovina> Kupovina { get; set; }
        public DbSet<KupovinaTip> KupovinaTip { get; set; }
        public DbSet<Like> Like { get; set; }
        public DbSet<LogPodaci> LogPodaci { get; set; }
        public DbSet<Organizator> Organizator{ get; set; }
        public DbSet<ProdajaTip> ProdajaTip { get; set; }
        public DbSet<ProstorOdrzavanja> ProstorOdrzavanja{ get; set; }
        public DbSet<Radnik> Radnik { get; set; }
        public DbSet<RadnikEvent> RadnikEvent { get; set; }
        public DbSet<Recenzija> Recenzija { get; set; }
        public DbSet<Sjediste> Sjediste { get; set; }
        public DbSet<Sponzor>Sponzor { get; set; }
        public DbSet<SponzorEvent> SponzorEvent{ get; set; }
    }
}
