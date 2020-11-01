using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Data.EF;
using Eventi.Data.Models;
using Eventi.Web.Areas.Client.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MimeKit;
using Eventi.Common;
using Eventi.Sdk;
using Eventi.Contracts.V1.Requests;
using System.Data;

namespace Eventi.Web.Areas.Client.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Client)]
    [Area("Client")]
    public class ClientController : Controller
    {
        private readonly MojContext ctx;
        private readonly IEventiApi _eventiApi;

        public ClientController(MojContext context, IEventiApi eventiApi)
        {
            _eventiApi = eventiApi;
            ctx = context;
        }
        public async Task<IActionResult> Index(string filter)
        {   
            EventSearchVM model = new EventSearchVM();
           
            var user = await HttpContext.GetLoggedInUser();
            if (user != null)
            {
                model.ClientID = user.ID;
            }
           
            if (filter == "Music")
            {
                model.Events = await SearchByCategory(EventCategory.Music);
            }
            else if (filter == "Sport")
            {
                model.Events = await SearchByCategory(EventCategory.Sport);
            }
            else if(filter == "Coulture")
            {
                model.Events = await SearchByCategory(EventCategory.Coulture);
            }
            else if (filter != null)
            {   
                model.Events = await SearchByNameAndLocation(filter);
            }
            else
            {
                model.Events = await GetEvents();
            }

            return View(model);  
        }
        public async Task<List<EventSearchVM.Rows>> GetEvents()
        {
            var response = await _eventiApi.GetEventAsync(new EventSearchRequest()
            {
                IsApproved = true,
                IsCanceled = true,
                Start = DateTime.Now
            });

            return response.Content.Data
                .Select
                (
                    i => new EventSearchVM.Rows()
                    {
                        EventID = i.ID,
                        Name = i.Name,
                        Category = i.EventCategory.ToString(),
                        Start = i.Start,
                        End = i.End,
                        Image = i.Image
                    }
                ).ToList();
        }
        public async Task<List<EventSearchVM.Rows>> SearchByCategory(EventCategory category)
        {
            var response = await _eventiApi.GetEventAsync(new EventSearchRequest()
            {
                IsApproved = true,
                IsCanceled = true,
                EventCategory = category,
                Start = DateTime.Now
            });

            return response.Content.Data
                .Select
                (
                    i => new EventSearchVM.Rows()
                    {
                        EventID = i.ID,
                        Name = i.Name,
                        Category = i.EventCategory.ToString(),
                        Start = i.Start,
                        End = i.End,
                        Image = i.Image
                    }
                ).ToList();
        }
        public async Task<List<EventSearchVM.Rows>> SearchByNameAndLocation(string filter)
        {
            var response = await _eventiApi.GetEventAsync(new EventSearchRequest()
            {
                Name = filter,
                IsApproved = true,
                IsCanceled = true,
                Start = DateTime.Now
            });

            return response.Content.Data
                .Select
                (
                    i => new EventSearchVM.Rows()
                    {
                        EventID = i.ID,
                        Name = i.Name,
                        Category = i.EventCategory.ToString(),
                        Start = i.Start,
                        End = i.End,
                        Image = i.Image
                    }
                ).ToList();
        }


        public async Task<IActionResult> EventDetails(int EventID, int ClientID)
        {
            if (EventID == 0 || ClientID == 0)
            {
                return RedirectToAction("Index");
            }

            var account = await HttpContext.GetLoggedInUser();

            var clientResponse = await _eventiApi.GetClientAsync(new ClientSearchRequest()
            {
                AccountID = account.ID
            });
            var Client = clientResponse.Content.Data.ToList()[0];


            var eventResponse = await _eventiApi.GetEventAsync(EventID);
            var Event = eventResponse.Content;


            EventClientVM model = new EventClientVM {
                EventID = Event.ID,
                Name = Event.Name,
                Category = Event.EventCategory.ToString(),
                Description = Event.Description,
                Start = Event.Start,
                End = Event.End,
                Image = Event.Image,
                ClientID = Client.ID,
                ClientFirstName = Client.FirstName,
                ClientLastName = Client.LastName,
                ClientAddress = Client.Address
               
            };
            
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
              //  PosaljiMail(kor.Id, CijenaTrenutneKupovine, ev.Naziv);
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
          //  PosaljiMail(kor.Id, CijenaTrenutneKupovine, ev.Naziv);

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
        public async Task<IActionResult> UserDetails() 
        {
            var account = await HttpContext.GetLoggedInUser();
            if (account == null)
            {
                return Redirect("Index");
            }

            var clientResponse = await _eventiApi.GetClientAsync(new ClientSearchRequest()
            {
                AccountID = account.ID
            });
            var client = clientResponse.Content.Data.ToList()[0];


            if (client == null)
            {
                return Redirect("Index");
            }

            ClientDetailsVM model = new ClientDetailsVM
            {
                ID = client.ID,
                Address = client.Address,
                Email = client.Email,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                Image = client.Image
            };

            var countryResponse = await _eventiApi.GetCountryAsync();

            model.Countries = countryResponse.Content.Data.ToList()
                .Select(i=>new SelectListItem { 
                    Text=i.Name,
                    Value=i.ID.ToString()
                })
                .ToList();
           
            return View(model);     
        }
        
        public async Task<IActionResult> Save(ClientDetailsVM model, IFormFile Image)
        {
            if (!ModelState.IsValid)
            {
                model.Countries = ctx.Drzava.Select(d => new SelectListItem
                {
                    Text = d.Naziv,
                    Value = d.Id.ToString()
                }).ToList();
                return View(model);
            }

            if (Image != null && Image.Length > 0)
            {
                var nazivSlike = Path.GetFileName(Image.FileName);
                var putanja = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\korisnicke", nazivSlike);
                using (var fajlSteam = new FileStream(putanja, FileMode.Create))
                {
                      await Image.CopyToAsync(fajlSteam);
                }

                model.Image = nazivSlike;
               
            }

            await _eventiApi.UpdateClientAsync(model.ID, new ClientUpdateRequest()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.LastName,
                Email = model.Email,
                CreditCardNumber = model.CreditCardNumber,
                Image = model.Image
            });
            
            return Redirect("UserDetails");
        }
       
        
    }
}


