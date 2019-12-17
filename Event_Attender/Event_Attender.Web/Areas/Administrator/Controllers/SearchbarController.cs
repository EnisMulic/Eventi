using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Event_Attender.Web.Areas.Administrator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Event_Attender.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class SearchbarController : Controller
    {
        //private readonly MojContext _context;

        //public SearchbarController(MojContext context)
        //{
        //    _context = context;
        //}
        //EventDisplayVM EventModels = new EventDisplayVM();
        public List<GradVM> GetGradovi()
        {
            MojContext ctx = new MojContext();
            var Gradovi = ctx.Grad
                .Select
                (
                    i => new GradVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        DrzavaNaziv = i.Drzava.Naziv
                    }
                )
                .ToList();

            ctx.Dispose();
            return Gradovi;
        }

        public IActionResult _AdminSidebar()
        {
            return PartialView();
        }

        #region Event: ToDo: Uredi, Spasi
        
        public EventDisplayVM GetEventDisplayVMModel(int? Id, bool dodatno = false)
        {
            EventDisplayVM EventModels = new EventDisplayVM();
            MojContext ctx = new MojContext();

            EventModels.Events = ctx.Event
                .Select
                (
                    e => new EventVM
                    {
                        Id = e.Id,
                        Naziv = e.Naziv,
                        Opis = e.Opis,
                        DatumOdrzavanja = e.DatumOdrzavanja,
                        VrijemeOdrzavanja = e.VrijemeOdrzavanja,
                        Kategorija = e.Kategorija,
                        IsOdobren = e.IsOdobren,
                        IsOtkazan = e.IsOtkazan,
                        OrganizatorNaziv = e.Organizator.Naziv,
                        AdministratorNaziv = e.AdministratorId != null ? "N/A"
                                                                       : e.Administrator.Osoba.Ime + " " +
                                                                         e.Administrator.Osoba.Prezime,
                        ProstorOdrzavanjaNaziv = e.ProstorOdrzavanja.Naziv
                    }
                 )
                .ToList();
            if(Id != null)
            {
                EventModels.OnDisplay = ctx.Event
                .Select
                (
                    e => new EventVM
                    {
                        Id = e.Id,
                        Naziv = e.Naziv,
                        Opis = e.Opis,
                        DatumOdrzavanja = e.DatumOdrzavanja,
                        VrijemeOdrzavanja = e.VrijemeOdrzavanja,
                        Kategorija = e.Kategorija,
                        IsOdobren = e.IsOdobren,
                        IsOtkazan = e.IsOtkazan,
                        OrganizatorNaziv = e.Organizator.Naziv,
                        AdministratorNaziv = e.AdministratorId != null ? "N/A"
                                                                       : e.Administrator.Osoba.Ime + " " +
                                                                         e.Administrator.Osoba.Prezime,
                        ProstorOdrzavanjaNaziv = e.ProstorOdrzavanja.Naziv
                    }

                 )
                .Where(e => e.Id == Id)
                .FirstOrDefault();
            }
            if(dodatno == true)
            {
                EventModels.Prostori = ctx.ProstorOdrzavanja
                    .Select
                    (
                        i => new ProstorOdrzavanjaVM
                        {
                            Id = i.Id,
                            Naziv = i.Naziv,
                            TipProstoraOdrzavanja = i.TipProstoraOdrzavanja,
                            GradNaziv = i.Grad.Naziv
                        }
                    )
                    .ToList();
                EventModels.Organizatori = ctx.Organizator
                    .Select
                    (
                        i => new OrganizatorVM
                        {
                            Id = i.Id,
                            Naziv = i.Naziv,
                            Telefon = i.Telefon,
                            GradNaziv = i.Grad.Naziv
                        }
                    )
                    .ToList();
                EventModels.Administratori = ctx.Administrator
                    .Select
                    (
                        i => new AdministratorVM
                        {
                            Id = i.Id,
                            Ime = i.Osoba.Ime,
                            Prezime = i.Osoba.Prezime,
                            Telefon = i.Osoba.Telefon,
                            Grad = i.Osoba.Grad.Naziv
                        }
                    )
                    .ToList();

                
            }

            ctx.Dispose();
            return EventModels;
        }
        public IActionResult _AdminEventDisplay() => View(GetEventDisplayVMModel(null));

        public IActionResult _EventInfo(int Id) => View(GetEventDisplayVMModel(Id));

        public IActionResult ObrisiEvent(int Id)
        {
            MojContext ctx = new MojContext();

            Event item = ctx.Event.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminEventDisplay");
        }

        public IActionResult UrediEvent(int Id) => View("_EventForma", GetEventDisplayVMModel(Id, true));

        public IActionResult SnimiEvent(IFormCollection formCollection)
        {
            MojContext ctx = new MojContext();

            Event item = ctx.Event.Find(int.Parse(formCollection["id"]));
            item.Naziv = formCollection["Naziv"];
            item.Opis = formCollection["Opis"];
            item.DatumOdrzavanja = DateTime.ParseExact(formCollection["Datum"], "yyyy-MM-dd", null);
            item.VrijemeOdrzavanja = formCollection["Vrijeme"];
            item.Kategorija = (Kategorija)Enum.Parse(typeof(Kategorija), formCollection["Kategorija"]);
            item.IsOdobren = formCollection["IsOdobren"] == "true,false" ? true : false;
            item.IsOtkazan = formCollection["IsOtkazan"] == "true,false" ? true : false;
            item.ProstorOdrzavanjaId = int.Parse(formCollection["Prostor"]);
            item.OrganizatorId = int.Parse(formCollection["Organizator"]);
            if(formCollection.ContainsKey("Administrator"))
            {
                item.AdministratorId = int.Parse(formCollection["Administrator"]);
            }

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect($"_EventInfo?id={formCollection["id"]}");
        }

        #endregion
        public IActionResult Odobri(int Id)
        {
            using (MojContext ctx = new MojContext())
            {
                Event e = ctx.Event.FirstOrDefault(e => e.Id == Id);
                if (e != null) e.IsOdobren = true;
                ctx.SaveChanges();
            }
            return Redirect("/Administrator");
        }

        #region Drzava:
        public DrzavaDisplayVM GetDrzavaDisplayVMModel(int? Id)
        {
            DrzavaDisplayVM DrzavaModels = new DrzavaDisplayVM();
            MojContext ctx = new MojContext();

            DrzavaModels.Drzave = ctx.Drzava
                .Select
                (
                    i => new DrzavaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv
                    }
                )
                .ToList();
            if(Id != null)
            {
                DrzavaModels.OnDisplay = ctx.Drzava
                .Select
                (
                    i => new DrzavaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();
            }

            ctx.Dispose();
            return DrzavaModels;
        }
        
        public IActionResult _AdminDrzavaDisplay() => View(GetDrzavaDisplayVMModel(null));
        public IActionResult _DrzavaInfo(int Id) => View(GetDrzavaDisplayVMModel(Id));
        public IActionResult UrediDrzava(int Id) => View("_DrzavaForma", GetDrzavaDisplayVMModel(Id));
        public IActionResult ObrisiDrzava(int Id)
        {
            MojContext ctx = new MojContext();

            Drzava item = ctx.Drzava.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminDrzavaDisplay");
        }
        public IActionResult SnimiDrzava(IFormCollection formCollection)
        {
            MojContext ctx = new MojContext();
            Drzava item = ctx.Drzava.Find(int.Parse(formCollection["Id"]));
            item.Naziv = formCollection["Naziv"];

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect($"_DrzavaInfo?id={formCollection["Id"]}");
        }

        #endregion

        #region Grad:
        public GradDisplayVM GetGradDisplayVMModel(int? gradID, bool dodatno = false)
        {
            GradDisplayVM GradModels = new GradDisplayVM();
            MojContext ctx = new MojContext();

            GradModels.Gradovi = ctx.Grad
                .Select
                (
                    i => new GradVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        DrzavaNaziv = i.Drzava.Naziv
                    }
                )
                .ToList();
            if(gradID != null)
            {
                GradModels.OnDisplay = ctx.Grad
                .Select
                (
                    i => new GradVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        DrzavaNaziv = i.Drzava.Naziv
                    }
                )
                .Where(i => i.Id == gradID)
                .FirstOrDefault();
            }

            if(dodatno == true)
            {
                GradModels.Drzave = ctx.Drzava
                .Select
                (
                    i => new DrzavaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv
                    }
                )
                .ToList();
            }

            ctx.Dispose();
            return GradModels;

        }
        public IActionResult _AdminGradDisplay() => View(GetGradDisplayVMModel(null));

        public IActionResult _GradInfo(int Id) => View(GetGradDisplayVMModel(Id));

        public IActionResult UrediGrad(int Id) => View("_GradForma", GetGradDisplayVMModel(Id, true));

        public IActionResult ObrisiGrad(int Id)
        {
            MojContext ctx = new MojContext();

            Grad item = ctx.Grad.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminGradDisplay");
        }

        public IActionResult SnimiGrad(IFormCollection formCollection)
        {
            MojContext ctx = new MojContext();
            Grad item = ctx.Grad.Find(int.Parse(formCollection["Id"]));
            item.Naziv = formCollection["Naziv"];
            item.DrzavaId = int.Parse(formCollection["Drzava"]);

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect($"_GradInfo?id={formCollection["Id"]}");
        }

        #endregion

        #region Korisnik:
        public KorisnikDisplayVM GetKorisnikDisplayVMModel(int? Id, bool dodatno = false)
        {
            KorisnikDisplayVM KorisnikModels = new KorisnikDisplayVM();
            MojContext ctx = new MojContext();

            KorisnikModels.Korisnici = ctx.Korisnik
                .Select
                (
                    i => new KorisnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Telefon = i.Osoba.Telefon,
                        Adresa = i.Adresa,
                        GradNaziv = i.Osoba.Grad.Naziv,
                        PostanskiBroj = i.PostanskiBroj
                    }
                )
                .ToList();
            if(Id != null)
            {
                KorisnikModels.OnDisplay = ctx.Korisnik
                .Select
                (
                    i => new KorisnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Telefon = i.Osoba.Telefon,
                        Adresa = i.Adresa,
                        GradNaziv = i.Osoba.Grad.Naziv,
                        PostanskiBroj = i.PostanskiBroj
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();
            }
            if(dodatno == true)
            {
                KorisnikModels.Gradovi = GetGradovi();
            }


            ctx.Dispose();
            return KorisnikModels;
        }
        public IActionResult _AdminKorisnikDisplay() => View(GetKorisnikDisplayVMModel(null));

        public IActionResult _KorisnikInfo(int Id) => View(GetKorisnikDisplayVMModel(Id));

        public IActionResult UrediKorisnik(int Id) => View("_KorisnikForma", GetKorisnikDisplayVMModel(Id, true));
        public IActionResult ObrisiKorisnik(int Id)
        {
            MojContext ctx = new MojContext();

            Korisnik item = ctx.Korisnik.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminKorisnikDisplay");
        }

        public IActionResult SnimiKorisnik(IFormCollection formCollection)
        {
            MojContext ctx = new MojContext();
            Korisnik item = ctx.Korisnik
                .Include(i => i.Osoba)
                .Where(i => i.Id == int.Parse(formCollection["Id"]))
                .FirstOrDefault();
                
            item.Osoba.Ime = formCollection["Ime"];
            item.Osoba.Prezime = formCollection["Prezime"];
            item.Osoba.Telefon = formCollection["Telefon"];
            item.Adresa = formCollection["Adresa"];
            item.PostanskiBroj = formCollection["PostanskiBroj"];
            item.Osoba.GradId = int.Parse(formCollection["Grad"]);

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect($"_KorisnikInfo?id={formCollection["Id"]}");
        }

        #endregion

        #region Radnik:
        public RadnikDisplayVM GetRadnikDisplayVMModel(int? Id, bool dodatno = false)
        {
            RadnikDisplayVM RadnikModels = new RadnikDisplayVM();
            MojContext ctx = new MojContext();

            RadnikModels.Radnici = ctx.Radnik
                .Select
                (
                    i => new RadnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Telefon = i.Osoba.Telefon,
                        GradNaziv = i.Osoba.Grad.Naziv
                    }
                )
                .ToList();
            if (Id != null)
            {
                RadnikModels.OnDisplay = ctx.Radnik
                .Select
                (
                    i => new RadnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Telefon = i.Osoba.Telefon,
                        GradNaziv = i.Osoba.Grad.Naziv
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();
            }
            if (dodatno == true)
            {
                RadnikModels.Gradovi = GetGradovi();
            }


            ctx.Dispose();
            return RadnikModels;


        }
        public IActionResult _AdminRadnikDisplay() => View(GetRadnikDisplayVMModel(null));

        public IActionResult _RadnikInfo(int Id) => View(GetRadnikDisplayVMModel(Id));

        public IActionResult UrediRadnik(int Id) => View("_RadnikForma", GetRadnikDisplayVMModel(Id));

        public IActionResult ObrisRadnik(int Id)
        {
            MojContext ctx = new MojContext();

            Radnik item = ctx.Radnik.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminRadnikDisplay");
        }

        public IActionResult SnimiRadnik(IFormCollection formCollection)
        {
            MojContext ctx = new MojContext();
            Radnik item = ctx.Radnik
                .Include(i => i.Osoba)
                .Where(i => i.Id == int.Parse(formCollection["Id"]))
                .FirstOrDefault();

            item.Osoba.Ime = formCollection["Ime"];
            item.Osoba.Prezime = formCollection["Prezime"];
            item.Osoba.Telefon = formCollection["Telefon"];
            item.Osoba.GradId = int.Parse(formCollection["Grad"]);

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect($"_KorisnikInfo?id={formCollection["Id"]}");
        }

        #endregion

        #region Organizator:
        public OrganizatorDisplayVM GetOrganizatorDisplayVMModel(int? Id, bool dodatno = false)
        {
            OrganizatorDisplayVM OrganizatorModels = new OrganizatorDisplayVM();
            MojContext ctx = new MojContext();

            OrganizatorModels.Organizatori = ctx.Organizator
                .Select
                (
                    i => new OrganizatorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        GradNaziv = i.Grad.Naziv
                    }
                )
                .ToList();
            if(Id != null)
            {
                OrganizatorModels.OnDisplay = ctx.Organizator
                .Select
                (
                    i => new OrganizatorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        GradNaziv = i.Grad.Naziv
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();
            }
            if(dodatno == true)
            {
                OrganizatorModels.Gradovi = GetGradovi();
            }

            ctx.Dispose();
            return OrganizatorModels;
        }
        public IActionResult _AdminOrganizatorDisplay() => View(GetOrganizatorDisplayVMModel(null));

        public IActionResult _OrganizatorInfo(int Id) => View(GetOrganizatorDisplayVMModel(Id));

        public IActionResult UrediOrganizator(int Id)
            => View("_OrganizatorForma", GetOrganizatorDisplayVMModel(Id, true));
        public IActionResult ObrisiOrganizator(int Id)
        {
            MojContext ctx = new MojContext();

            Organizator item = ctx.Organizator.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminOrganizatorDisplay");
        }

        public IActionResult SnimiOrganizator(IFormCollection formCollection)
        {
            MojContext ctx = new MojContext();
            Organizator item = ctx.Organizator.Find(int.Parse(formCollection["Id"]));
            item.Naziv = formCollection["Naziv"];
            item.Telefon = formCollection["Telefon"];
            if(formCollection.ContainsKey("Grad"))
                item.GradId = int.Parse(formCollection["Grad"]);

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect($"_OrganizatorInfo?id={formCollection["Id"]}");
        }

        #endregion

        #region Izvodjac
        public IzvodjacDisplayVM GetIzvodjacDisplayVMModel(int? Id)
        {
            IzvodjacDisplayVM IzvodjacModels = new IzvodjacDisplayVM();
            MojContext ctx = new MojContext();

            IzvodjacModels.Izvodjaci = ctx.Izvodjac
                .Select
                (
                    i => new IzvodjacVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        TipIzvodjaca = i.TipIzvodjaca
                    }
                )
                .ToList();
            if(Id != null)
            {
                IzvodjacModels.OnDisplay = ctx.Izvodjac
                .Select
                (
                    i => new IzvodjacVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        TipIzvodjaca = i.TipIzvodjaca
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();
            }

            ctx.Dispose();
            return IzvodjacModels;
        }
        public IActionResult _AdminIzvodjacDisplay() => View(GetIzvodjacDisplayVMModel(null));

        public IActionResult _IzvodjacInfo(int Id) => View(GetIzvodjacDisplayVMModel(Id));

        public IActionResult UrediIzvodjac(int Id) => View("_IzvodjacForma", GetIzvodjacDisplayVMModel(Id));
        public IActionResult ObrisiIzvodjac(int Id)
        {
            MojContext ctx = new MojContext();

            Izvodjac item = ctx.Izvodjac.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminIzvodjacDisplay");
        }

        public IActionResult SnimiIzvodjac(IFormCollection formCollection)
        {
            MojContext ctx = new MojContext();
            Izvodjac item = ctx.Izvodjac.Find(int.Parse(formCollection["Id"]));
            item.Naziv = formCollection["Naziv"];
            item.TipIzvodjaca = (TipIzvodjaca)Enum.Parse(typeof(TipIzvodjaca), formCollection["Tip"]);

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect($"_IzvodjacInfo?id={formCollection["Id"]}");
        }

        #endregion

        #region Sponzor:
        public SponzorDisplayVM GetSponzorDisplayVMModel(int? Id)
        {
            SponzorDisplayVM SponzorModels = new SponzorDisplayVM();
            MojContext ctx = new MojContext();

            SponzorModels.Sponzori = ctx.Sponzor
                .Select
                (
                    i => new SponzorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        Email = i.Email
                    }
                )
                .ToList();
            if(Id != null)
            {
                SponzorModels.OnDisplay = ctx.Sponzor
                .Select
                (
                    i => new SponzorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        Email = i.Email
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();
            }

            ctx.Dispose();
            return SponzorModels;
        }

        public IActionResult _AdminSponzorDisplay() => View(GetSponzorDisplayVMModel(null));

        public IActionResult _SponzorInfo(int Id) => View(GetSponzorDisplayVMModel(Id));

        public IActionResult UrediSponzor(int Id) => View("_SponzorForma", GetSponzorDisplayVMModel(Id));

        public IActionResult ObrisiSponzor(int Id)
        {
            MojContext ctx = new MojContext();

            Sponzor item = ctx.Sponzor.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminSponzorDisplay");
        }

        public IActionResult SnimiSponzor(IFormCollection formCollection)
        {
            MojContext ctx = new MojContext();
            Sponzor item = ctx.Sponzor.Find(int.Parse(formCollection["Id"]));
            item.Naziv = formCollection["Naziv"];
            item.Telefon = formCollection["Telefon"];
            item.Email = formCollection["Email"];

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect($"_SponzorInfo?id={formCollection["Id"]}");
        }

        #endregion

        #region Prostor:
        public ProstorOdrzavanjaDisplayVM GetProstorOdrzavanjaDisplayVMModel(int? Id, bool dodatno = false)
        {
            ProstorOdrzavanjaDisplayVM ProstorOdrzavanjaModels = new ProstorOdrzavanjaDisplayVM();
            MojContext ctx = new MojContext();

            ProstorOdrzavanjaModels.Prostori = ctx.ProstorOdrzavanja
                .Select
                (
                    i => new ProstorOdrzavanjaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Adresa = i.Adresa,
                        GradNaziv = i.Grad.Naziv,
                        TipProstoraOdrzavanja = i.TipProstoraOdrzavanja
                    }
                )
                .ToList();
            if(Id != null)
            {
                ProstorOdrzavanjaModels.OnDisplay = ctx.ProstorOdrzavanja
                .Select
                (
                    i => new ProstorOdrzavanjaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Adresa = i.Adresa,
                        GradNaziv = i.Grad.Naziv,
                        TipProstoraOdrzavanja = i.TipProstoraOdrzavanja
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();
            }
            if (dodatno == true)
            {
                ProstorOdrzavanjaModels.Gradovi = GetGradovi();
            }


            ctx.Dispose();
            return ProstorOdrzavanjaModels;
        }

        public IActionResult _AdminProstorDisplay() => View(GetProstorOdrzavanjaDisplayVMModel(null));

        public IActionResult _ProstorOdrzavanjaInfo(int Id) => View(GetProstorOdrzavanjaDisplayVMModel(Id));

        public IActionResult UrediProstorOdrzavanja(int Id) 
            => View("_ProstorOdrzavanjaForma", GetProstorOdrzavanjaDisplayVMModel(Id, true));

        public IActionResult ObrisiProstorOdrzavanja(int Id)
        {
            MojContext ctx = new MojContext();

            ProstorOdrzavanja item = ctx.ProstorOdrzavanja.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminProstorDisplay");
        }


        public IActionResult SnimiProstorOdrzavanja(IFormCollection formCollection) 
        {
            MojContext ctx = new MojContext();
            ProstorOdrzavanja item = ctx.ProstorOdrzavanja.Find(int.Parse(formCollection["Id"]));
            item.Naziv = formCollection["Naziv"];
            item.Adresa = formCollection["Adresa"];
            item.TipProstoraOdrzavanja = (TipProstoraOdrzavanja)Enum.Parse(
                typeof(TipProstoraOdrzavanja), formCollection["Tip"]
                );
            item.GradId = int.Parse(formCollection["Id"]);

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect($"_ProstorOdrzavanjaInfo?id={formCollection["Id"]}");
        }
        #endregion
    }
}