using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Event_Attender.Web.Areas.ModulKorisnik.Models;
using Event_Attender.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace Event_Attender.Web.Areas.ModulKorisnik.Controllers
{   
    [Autorizacija(korisnik:true,organizator:false,administrator:false,radnik:false)]
    [Area("ModulKorisnik")]
    public class KorisnikController : Controller
    {
        private readonly MojContext ctx;

        public KorisnikController(MojContext context)
        {
            ctx = context;
        }
        public IActionResult Index(string filter)
        {   
            PretragaEventaVM model = new PretragaEventaVM();
           
            LogPodaci l = HttpContext.GetLogiraniUser();
            if (l != null)
            {
                Korisnik k = ctx.Korisnik.Where(k => k.Osoba.LogPodaciId == l.Id).Include(k => k.Osoba).SingleOrDefault();
                model.KorisnikId = k.Id;
            }
        
            DateTime date = DateTime.Now;
           
            if (filter == "Muzika")
            {
                model.Eventi = PretragaPoKategoriji(Kategorija.Muzika);
                return View(model);
            }
            else if (filter == "Sport")
            {
                model.Eventi = PretragaPoKategoriji(Kategorija.Sport);
                return View(model);
            }
            else if(filter == "Kultura")
            {
                model.Eventi = PretragaPoKategoriji(Kategorija.Kultura);
                return View(model);
            }
            if (filter != null)
            {   
                model.Eventi = PretragaPoNazivuLokaciji(filter);
                return View(model);
            }
            
            model.Eventi = PrikazEvenata();

            return View(model);  
        }
        List<PretragaEventaVM.Rows> PrikazEvenata()
        {
            DateTime date = DateTime.Now;
          
            List<PretragaEventaVM.Rows> lista= ctx.Event
                .Where(e => e.IsOdobren == true).Where(e => e.IsOtkazan == false).Where(e => e.DatumOdrzavanja.CompareTo(date) == 1)
                .Select(e => new PretragaEventaVM.Rows
                {
                    EventId = e.Id,
                    Naziv = e.Naziv,
                    Kategorija = e.Kategorija.ToString(),
                    Opis = e.Opis,
                    ProstorOdrzavanjaNaziv = e.ProstorOdrzavanja.Naziv,
                    ProstorOdrzavanjaAdresa = e.ProstorOdrzavanja.Adresa,
                    ProstorOdrzavanjaGrad = e.ProstorOdrzavanja.Grad.Naziv,
                    DatumOdrzavanja = e.DatumOdrzavanja.Day.ToString() + "." + e.DatumOdrzavanja.Month.ToString() + "." + e.DatumOdrzavanja.Year.ToString(),
                    VrijemeOdrzavanja = e.VrijemeOdrzavanja,
                    Slika = e.Slika
                }).ToList();
            return lista;
        }
        List<PretragaEventaVM.Rows> PretragaPoKategoriji(Kategorija k)
        {
            DateTime date = DateTime.Now;
         
            List<PretragaEventaVM.Rows> lista = ctx.Event
            .Where(e => e.Kategorija == k).Where(e => e.IsOdobren == true).Where(e => e.IsOtkazan == false).Where(e => e.DatumOdrzavanja.CompareTo(date) == 1)
            .Select(e => new PretragaEventaVM.Rows
            {
                EventId = e.Id,
                Naziv = e.Naziv,
                Kategorija = e.Kategorija.ToString(),
                Opis = e.Opis,
                ProstorOdrzavanjaNaziv = e.ProstorOdrzavanja.Naziv,
                ProstorOdrzavanjaAdresa = e.ProstorOdrzavanja.Adresa,
                ProstorOdrzavanjaGrad = e.ProstorOdrzavanja.Grad.Naziv,
                DatumOdrzavanja = e.DatumOdrzavanja.Day.ToString() + "." + e.DatumOdrzavanja.Month.ToString() + "." + e.DatumOdrzavanja.Year.ToString(),
                VrijemeOdrzavanja = e.VrijemeOdrzavanja,
                Slika = e.Slika
            }).ToList();
            return lista;
        }
        List<PretragaEventaVM.Rows> PretragaPoNazivuLokaciji(string filter)
        {
            DateTime date = DateTime.Now;
        
            List<PretragaEventaVM.Rows> lista= ctx.Event.Where(e => e.IsOdobren == true)
                .Where(e => e.IsOtkazan == false).Where(e => e.DatumOdrzavanja.CompareTo(date) == 1)
                .Where(e => e.Naziv.ToLower().StartsWith(filter.ToLower())
                       || e.Naziv.ToLower().Contains(filter.ToLower()) ||
                        e.ProstorOdrzavanja.Naziv.ToLower().StartsWith(filter.ToLower()) || e.ProstorOdrzavanja.Naziv.ToLower().Contains(filter.ToLower())
                       || e.ProstorOdrzavanja.Grad.Naziv.ToLower().StartsWith(filter.ToLower()) || e.ProstorOdrzavanja.Grad.Naziv.ToLower().Contains(filter.ToLower()))
                      .Select(e => new PretragaEventaVM.Rows
                      {
                          EventId = e.Id,
                          Naziv = e.Naziv,
                          Kategorija = e.Kategorija.ToString(),
                          Opis = e.Opis,
                          ProstorOdrzavanjaNaziv = e.ProstorOdrzavanja.Naziv,
                          ProstorOdrzavanjaAdresa = e.ProstorOdrzavanja.Adresa,
                          ProstorOdrzavanjaGrad = e.ProstorOdrzavanja.Grad.Naziv,
                          DatumOdrzavanja = e.DatumOdrzavanja.Day.ToString() + "." + e.DatumOdrzavanja.Month.ToString() + "." + e.DatumOdrzavanja.Year.ToString(),
                          VrijemeOdrzavanja = e.VrijemeOdrzavanja,
                          Slika = e.Slika
                      }).ToList();
            return lista;
        }
        public IActionResult LikeEvent(int eId, int korId) {
            
            ViewData["eId"] = eId;
            ViewData["kId"] = korId;
            
            if (eId > 0 && korId > 0)
            {
                // ako taj id eventa i taj id korisnika postoje u bazi
                if (ctx.Event.Where(e => e.Id == eId).Any() && ctx.Korisnik.Where(x => x.Id == korId).Any())
                {
                    Like l = new Like
                    {
                        KorisnikId = korId, 
                        EventId = eId,  
                        DatumLajka = DateTime.Now
                    };
                    ctx.Like.Add(l);
                    ctx.SaveChanges();
                }
            }
            return PartialView();  
        }
        public IActionResult DisLikeEvent(int eId, int korId)
        {
          
            ViewData["eId"] = eId;
            ViewData["kId"] = korId;

            if(eId>0 && korId > 0)
            {
                Like l = ctx.Like.Where(l => l.EventId == eId && l.KorisnikId == korId).FirstOrDefault();
                if (l != null)
                {
                    ctx.Like.Remove(l);
                    ctx.SaveChanges();
                }
            }

            return PartialView();  
        }
        public IActionResult OEventu(int eId, int korId)
        {
            if (eId == 0 || korId == 0)
            {
                return RedirectToAction("Index");
            }

            Korisnik k = ctx.Korisnik.Where(k => k.Id == korId).Include(k => k.Osoba).SingleOrDefault();  // ili getlogiraniUser
            Event e = ctx.Event.Where(e => e.Id == eId).Include(e => e.ProstorOdrzavanja)
                .Include(e => e.ProstorOdrzavanja.Grad).SingleOrDefault();
            Like like = ctx.Like.Where(l => l.EventId == e.Id && l.KorisnikId == k.Id).FirstOrDefault();

            if (k == null)
            {
                return RedirectToAction("Index");
            }

            EventKorisnikVM model = new EventKorisnikVM {
                EventId = e.Id,
                Naziv = e.Naziv,
                Kategorija = e.Kategorija.ToString(),
                Opis = e.Opis,
                ProstorOdrzavanjaGrad = e.ProstorOdrzavanja.Grad.Naziv,
                ProstorOdrzavanjaNaziv = e.ProstorOdrzavanja.Naziv,
                ProstorOdrzavanjaAdresa = e.ProstorOdrzavanja.Adresa,
                DatumOdrzavanja = e.DatumOdrzavanja.Day.ToString() + "." + e.DatumOdrzavanja.Month.ToString() + "." + e.DatumOdrzavanja.Year.ToString(),
                VrijemeOdrzavanja = e.VrijemeOdrzavanja,
                Slika = e.Slika,
                KorisnikId = k.Id,
                KorisnikIme = k.Osoba.Ime,
                KorisnikPrezime = k.Osoba.Prezime,
                KorisnikAdresa = k.Adresa,
                KorisnikBrojracun = k.BrojKreditneKartice,
               
            };
            if (like != null)
                model.IsLikean = true;
            else
                model.IsLikean = false;
            return View(model);
          
        }

        public IActionResult KupiKartu(int eId, int kId)
        {
            if(kId<=0 || eId <= 0)
            {
                return PartialView("NemKupovina");
            }
            Korisnik k = ctx.Korisnik.Where(k => k.Id == kId).Include(k => k.Osoba).Include(k=>k.Osoba.Grad).SingleOrDefault();
            Event e = ctx.Event.Where(e => e.Id == eId).SingleOrDefault();
            if (k == null || e == null)
                return PartialView("NemKupovina");
              
            
            KupiKartuVM model = new KupiKartuVM
            {
                EventId = e.Id,
                KorisnikId = k.Id,
                KorisnikIme = k.Osoba.Ime,
                KorisnikPrezime = k.Osoba.Prezime,
                KorisnikGrad = k.Osoba.Grad.Naziv,
                KorisnikAdresa = k.Adresa,
                KorisnikBrojracun = k.BrojKreditneKartice,
                TipoviProdaje = ctx.ProdajaTip.Where(p => p.EventId == e.Id)
                .Select(p => new KupiKartuVM.TipProdaje {
                    ProdajaTipId = p.Id,
                    TipKarte = p.TipKarte.ToString(),
                    UkupnoKarataTip = p.UkupnoKarataTip,
                    BrojProdatihKarataTip = p.BrojProdatihKarataTip,
                    CijenaTip = p.CijenaTip,
                    PostojeSjedista = p.PostojeSjedista,
                    BrojPreostalihKarata = p.UkupnoKarataTip - p.BrojProdatihKarataTip,
                    IsRasprodano = (p.UkupnoKarataTip - p.BrojProdatihKarataTip) == 0 ? true : false
                }).ToList()
            };
            return PartialView(model); 
        }
        public IActionResult KupovinaSnimi(KupiKartuVM model)
        {
            ProdajaTip pt = ctx.ProdajaTip.Where(p => p.Id == model.OdabraniTipProdajeId).SingleOrDefault();
            Event ev = ctx.Event.Find(model.EventId);
            Korisnik kor = ctx.Korisnik.Find(model.KorisnikId);
            if(pt==null || ev==null|| kor==null|| model.KorisnikId==0 || model.EventId == 0)
            {
                TempData["error_Msg"] = "Niste odabrali tip karte. ";
                return PartialView("NemKupovina");
            }
            float CijenaTrenutneKupovine = 0;
            // provjera da li ima toliko karata
            int zeljeniBrojKarata = model.OdabranBrKarata;
            if (zeljeniBrojKarata <= 0)
            {
                TempData["error_Msg"] = "Nije moguće obaviti kupovinu. Niste unijeli ispravan broj karata.";
                return Redirect("/ModulKorisnik/Korisnik/KupiKartu?eId=" + model.EventId + "&kId=" + model.KorisnikId);
            }
            if(zeljeniBrojKarata> (pt.UkupnoKarataTip - pt.BrojProdatihKarataTip))
            {
                TempData["error_Msg"] = "Nema toliko karata u ponudi, broj " +
                    "preostalih karata je "+ (pt.UkupnoKarataTip - pt.BrojProdatihKarataTip);
                return Redirect("/ModulKorisnik/Korisnik/KupiKartu?eId=" + model.EventId + "&kId=" + model.KorisnikId);
            }
            Kupovina k = ctx.Kupovina.Where(k => k.EventId == model.EventId && k.KorisnikId == model.KorisnikId).SingleOrDefault();
            if (k == null)
            {
                k = new Kupovina {
                    KorisnikId = model.KorisnikId,
                    EventId = model.EventId
                };
                ctx.Kupovina.Add(k);
                ctx.SaveChanges();

                KupovinaTip kt = new KupovinaTip
                {
                    KupovinaId = k.Id,
                    ProdajaTipId = pt.Id,
                    TipKarte = pt.TipKarte,
                    BrojKarata = zeljeniBrojKarata,
                    Cijena = zeljeniBrojKarata * pt.CijenaTip,
                };
                ctx.KupovinaTip.Add(kt);
                ctx.SaveChanges();

                for(int i = 0; i < zeljeniBrojKarata; i++)
                {
                    Karta karta = new Karta
                    {
                        KupovinaTipId = kt.Id,
                        Cijena = pt.CijenaTip,
                        Tip = kt.TipKarte,
                        DatumKupovine=DateTime.Now
                    };
                    CijenaTrenutneKupovine += karta.Cijena;
                    ctx.Karta.Add(karta); 
                    pt.BrojProdatihKarataTip++;
                    if (pt.PostojeSjedista == true)
                    {
                        Sjediste s = new Sjediste {
                            Karta=karta,
                            BrojSjedista = pt.BrojProdatihKarataTip
                        };
                        ctx.Sjediste.Add(s);
                    }
                   
                }
                ctx.SaveChanges();
                PosaljiMail(kor.Id, CijenaTrenutneKupovine, ev.Naziv);
                return PartialView("UspjesnaKupovina",model);
            }
            KupovinaTip kupTip = ctx.KupovinaTip.Where(kt => kt.KupovinaId == k.Id && kt.TipKarte == pt.TipKarte).SingleOrDefault();
            // trazi se da li postoji KupovinaTip, odnosno da li su vec kupovane karte tog tipa
            if (kupTip == null)
            {    // ne postoji
                kupTip = new KupovinaTip
                {
                    KupovinaId = k.Id,
                    ProdajaTipId = pt.Id,
                    TipKarte = pt.TipKarte,
                    BrojKarata = zeljeniBrojKarata,
                    Cijena = zeljeniBrojKarata * pt.CijenaTip
                };
                ctx.KupovinaTip.Add(kupTip);
                ctx.SaveChanges();  
            }
            else
            {
                kupTip.BrojKarata += zeljeniBrojKarata;
                kupTip.Cijena += zeljeniBrojKarata * pt.CijenaTip;
            }
            for (int i = 0; i < zeljeniBrojKarata; i++)
            {
                Karta karta = new Karta
                {
                    KupovinaTipId = kupTip.Id,
                    Cijena = pt.CijenaTip,
                    Tip = kupTip.TipKarte
                };
                CijenaTrenutneKupovine += karta.Cijena;
                ctx.Karta.Add(karta);
                pt.BrojProdatihKarataTip++;
                if (pt.PostojeSjedista == true)
                {
                    Sjediste s = new Sjediste
                    {
                        Karta = karta,
                        BrojSjedista = pt.BrojProdatihKarataTip
                    };
                    ctx.Sjediste.Add(s);
                }
            }
            ctx.SaveChanges();
            //slanje maila o uspjesnoj kupovini korisniku
            PosaljiMail(kor.Id, CijenaTrenutneKupovine, ev.Naziv);

            return PartialView("UspjesnaKupovina", model);
        }
        public void PosaljiMail(int korisnikId, float cijena, string NazivEventa)
        {
            Korisnik k = ctx.Korisnik.Where(x => x.Id == korisnikId).Include(x => x.Osoba).Include(x => x.Osoba.LogPodaci).SingleOrDefault();
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Event Atteder", "event.attender@gmail.com"));
            
            message.To.Add(new MailboxAddress(k.Osoba.Ime,k.Osoba.LogPodaci.Email));  


            message.Subject = "Uspješna kupovina karte";

            message.Body = new TextPart("plain")
            {
                Text = "Poštovani/a " + k.Osoba.Ime+" "+k.Osoba.Prezime+ " uspješno ste obavili kupovinu karte za event "+NazivEventa+", u iznosu od "+ cijena + "KM. Hvala na povjerenju! "
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
               
                client.Authenticate("event.attender@gmail.com", "eventat123");

                client.Send(message);
                client.Disconnect(true);
            }
        }
        public IActionResult UserPodaci(int drzavaId)
        {
            LogPodaci l = HttpContext.GetLogiraniUser();
            if (l == null)
            {
                return Redirect("Index");   // filter null
            }
            Korisnik kor = ctx.Korisnik.Where(k => k.Osoba.LogPodaciId == l.Id).Include(k => k.Osoba)
                .Include(k => k.Osoba.Grad).Include(k=>k.Osoba.LogPodaci)
                .Include(k => k.Osoba.Grad.Drzava).SingleOrDefault();
            if (kor == null)
            {
                return Redirect("Index");   // filter null
            }
            KorisnikPodaciVM model = new KorisnikPodaciVM
            {
                Id = kor.Id,
                Adresa = kor.Adresa,
                BrojKreditneKartice = kor.BrojKreditneKartice,
                Email = kor.Osoba.LogPodaci.Email,
                Grad = kor.Osoba.Grad.Naziv,
                Ime = kor.Osoba.Ime,
                PostanskiBroj = kor.PostanskiBroj,
                Prezime = kor.Osoba.Prezime,
                Telefon = kor.Osoba.Telefon,
                DrzavaId=kor.Osoba.Grad.Drzava.Id,
                Slika=kor.Slika
            };
            model.drzave = ctx.Drzava.Select(d=>new SelectListItem { 
                  Text=d.Naziv,
                   Value=d.Id.ToString()
            }).ToList();
           
          //  model.gradovi = ctx.Grad.ToList();

            return View(model);     
        }

        public async Task<IActionResult> SnimiPodatkeAsync(KorisnikPodaciVM model,int drzavaId,int gradId, IFormFile slika)
        {
            if (slika != null && slika.Length > 0)
            {
                var nazivSlike = Path.GetFileName(slika.FileName);
                var putanja = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\korisnicke", nazivSlike);
                using (var fajlSteam = new FileStream(putanja, FileMode.Create))
                {
                    await slika.CopyToAsync(fajlSteam);
                }

                model.Slika = nazivSlike;
               
            }

                Korisnik kor = ctx.Korisnik.Where(k => k.Id == model.Id).Include(k => k.Osoba)
                    .Include(k => k.Osoba.Grad).Include(k => k.Osoba.LogPodaci)
                    .Include(k => k.Osoba.Grad.Drzava).SingleOrDefault();
                if (kor == null)
                {
                    return Redirect("Index");   // filter null
                }
              
                kor.Osoba.Ime = model.Ime;
                kor.Osoba.Prezime = model.Prezime;
                kor.Osoba.Telefon = model.Telefon;
                kor.Osoba.LogPodaci.Email = model.Email;
                kor.PostanskiBroj = model.PostanskiBroj;
                kor.Adresa = model.Adresa;
                kor.BrojKreditneKartice = model.BrojKreditneKartice;
                kor.Slika = model.Slika;

            //v2 - unosenje grada 
          
            model.Grad = model.Grad.Replace("ć", "c");
            model.Grad = model.Grad.Replace("č", "c");
            model.Grad = model.Grad.Replace("š", "s");
            model.Grad = model.Grad.Replace("đ", "d");
            model.Grad = model.Grad.Replace("ž", "z");
            model.Grad = model.Grad.Replace("Ć", "C");
            model.Grad = model.Grad.Replace("Č", "C");
            model.Grad = model.Grad.Replace("Š", "S");
            model.Grad = model.Grad.Replace("Đ", "D");
            model.Grad = model.Grad.Replace("Ž", "Z");

            bool postoji = false;
            List<Grad> gradovi = ctx.Grad.ToList();
            foreach (Grad g in gradovi)
            {
               
                if (model.Grad.ToLower().Equals(g.Naziv.ToLower()))
                {

                    kor.Osoba.GradId = g.Id;
                    postoji = true;
                    break;
                }
            }
            if (postoji==false)  // znaci da nema u bazi, 
            {
                kor.Osoba.Grad = new Grad { Naziv = model.Grad, DrzavaId = model.DrzavaId };
            }
            await ctx.SaveChangesAsync();
            
            return Redirect("UserPodaci");
        }
       
        
    }
}


