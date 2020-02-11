using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestovi
{
    public class TestingDataBase : IDisposable
    {
        protected readonly MojContext ctx;

        public TestingDataBase()
        {
            var options = new DbContextOptionsBuilder<MojContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            ctx = new MojContext(options);

            ctx.Database.EnsureCreated();

            // postaviti potrebne objekte
            var drzava = new Drzava { Naziv = "Drzava1" };
            ctx.Drzava.Add(drzava);

            var grad = new Grad { Naziv = "Grad1", DrzavaId = 1 };
            ctx.Grad.Add(grad);

            var logPodaci = new LogPodaci { Email = "azra.becirevic1998@gmail.com", Username = "User1", Password = "password1" };
            ctx.LogPodaci.Add(logPodaci);

            var osoba = new Osoba {  LogPodaciId=1, Ime="Ime", Prezime="Prezime", GradId=1, Telefon="+387 62 980 370"};
            ctx.Osoba.Add(osoba);

            var korisnik = new Korisnik { OsobaId = 1, Adresa = "adresa", Slika = null, PostanskiBroj = "98765", BrojKreditneKartice = "7642829030308750" };
            ctx.Korisnik.Add(korisnik);

            var prostorOdrzavanja = new ProstorOdrzavanja { GradId = 1, Adresa = "adresa1", Naziv = "Prostor", TipProstoraOdrzavanja = TipProstoraOdrzavanja.Sala };
            ctx.ProstorOdrzavanja.Add(prostorOdrzavanja);

            var eventi = new[]
            {
                new Event{ Naziv="EventTest1" ,Opis=null ,   DatumOdrzavanja=DateTime.Now,
                    VrijemeOdrzavanja="20:00", Kategorija=Kategorija.Kultura,  IsOdobren=true ,  IsOtkazan=false,
                    Slika=null,   OrganizatorId=1,  AdministratorId=null, ProstorOdrzavanjaId=1
                },
                 new Event{ Naziv="EventTest2" ,Opis=null ,   DatumOdrzavanja=new DateTime(2020,3,3),
                    VrijemeOdrzavanja="20:00", Kategorija=Kategorija.Muzika,  IsOdobren=true ,  IsOtkazan=false,
                    Slika=null,   OrganizatorId=1,  AdministratorId=null, ProstorOdrzavanjaId=1
                },
                 new Event{ Naziv="EventTest3" ,Opis=null ,   DatumOdrzavanja=new DateTime(2020,4,15),
                    VrijemeOdrzavanja="20:00", Kategorija=Kategorija.Sport,  IsOdobren=true ,  IsOtkazan=false,
                    Slika=null,   OrganizatorId=1,  AdministratorId=null, ProstorOdrzavanjaId=1
                },
                  new Event{ Naziv="EventTest4" ,Opis=null ,   DatumOdrzavanja=new DateTime(2020,5,5),
                    VrijemeOdrzavanja="20:00", Kategorija=Kategorija.Kultura,  IsOdobren=true ,  IsOtkazan=false,
                    Slika=null,   OrganizatorId=1,  AdministratorId=null, ProstorOdrzavanjaId=1
                },
            };
            ctx.Event.AddRange(eventi);

            var tipProdaje = new ProdajaTip { TipKarte = TipKarte.Obicna, CijenaTip = 5, BrojProdatihKarataTip = 0, UkupnoKarataTip = 10, EventId = 2, PostojeSjedista = false };
            ctx.ProdajaTip.Add(tipProdaje);

            var kupovina = new Kupovina { EventId = 2, KorisnikId = 1 };
            ctx.Kupovina.Add(kupovina);

            var recenzija = new Recenzija { KupovinaId = 1, Komentar = "komentar", Ocjena = 3 };
            ctx.Recenzija.Add(recenzija);

            ctx.SaveChanges();
        }

        public void Dispose()
        {
            ctx.Database.EnsureDeleted();
            ctx.Dispose();
        }
    }
}
