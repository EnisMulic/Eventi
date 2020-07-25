using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Event_Attender.Web.Areas.ModulRadnik.Models;
using Event_Attender.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Event_Attender.Web.Areas.ModulRadnik.Controllers
{
    
     [Autorizacija(korisnik: false, organizator: false, administrator: false, radnik: true)]
    [Area("ModulRadnik")]
    public class RadnikController : Controller
    {

        private readonly MojContext ctx;

        public RadnikController(MojContext context)
        {
            ctx = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.GetLogiraniUser() == null)
            {
                return Redirect("/Prijava/Index");
            }
            return View();
        }

        public IActionResult PregledProstoraOdrzavanja()
        {
            var model = new PregledProstoraOdrzavanjaVM
            {
                Redovi = ctx.ProstorOdrzavanja.Select(x => new PregledProstoraOdrzavanjaVM.Rows
                {
                    Naziv = x.Naziv,
                    Adresa = x.Adresa,
                    Grad = x.Grad.Naziv,
                    ProstorOdrzavanjaId = x.Id,
                    TipProstora = x.TipProstoraOdrzavanja.ToString()
                }).ToList()
            };

            return View(model);
        }

        public IActionResult DodajProstorOdrzavanja()
        {
            var model = new DodajProstorOdrzavanjaVM
            {
                Gradovi = ctx.Grad.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }).ToList()
            };


            return View(model);
        }


        [HttpPost]
        public IActionResult DodajProstorOdrzavanja(DodajProstorOdrzavanjaVM model)
        {
            var prostor = new ProstorOdrzavanja
            {
                Adresa = model.Adresa,
                GradId = model.GradOdabir,
                Naziv = model.Naziv,
                TipProstoraOdrzavanja = (TipProstoraOdrzavanja)model.TipProstoraOdabir

            };

            ctx.ProstorOdrzavanja.Add(prostor);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult DodajSponzora()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DodajSponzora(DodajSponzorFormVM model)
        {
            var sponzor = new Sponzor
            {
                Naziv = model.NazivSponzora,
                Email = model.EmailSponzora,
                Telefon = model.TelefonSponzora
            };

            ctx.Sponzor.Add(sponzor);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult PregledSponzora()
        {
            var model = new PregledSponzoraVM
            {
                Redovi = ctx.Sponzor.Select(x => new PregledSponzoraVM.Rows
                {
                    Id = x.Id,
                    EmailSponzora = x.Email,
                    NazivSponzora = x.Naziv,
                    TelefonSponzora = x.Telefon
                }).ToList()
            };

            return View(model);
        }

        public string GetProizvodi()
        {
            LogPodaci l = HttpContext.GetLogiraniUser();

            Radnik radnik = ctx.Radnik.Where(ra => ra.Osoba.LogPodaciId == l.Id).SingleOrDefault();
            if (radnik == null)
            {
                return null;   //"Server ce javiti gresku no Content"
            }
            PrikazEvenataVM model = new PrikazEvenataVM();
            model.eventi = ctx.RadnikEvent.Where(r => r.RadnikId == radnik.Id)
                .Select(r => new PrikazEvenataVM.Rows
                {
                    EventId = r.EventId,
                    NazivEventa = r.Event.Naziv,
                    DatumOdrzavanja = r.Event.DatumOdrzavanja.ToShortDateString(),
                    Grad = r.Event.ProstorOdrzavanja.Grad.Naziv,
                    ProstorOdrzavanjaIAdresa = r.Event.ProstorOdrzavanja.Naziv + " " + r.Event.ProstorOdrzavanja.Adresa,
                    RadnikEventId = r.Id,
                    RadnikId = r.RadnikId,
                    Vrijeme = r.Event.VrijemeOdrzavanja,
                    UkupnoZaradaOdEventa = ctx.KupovinaTip.Where(k => k.Kupovina.EventId == r.EventId).Sum(k => k.Cijena)
                }).ToList();

            string eventi = JsonConvert.SerializeObject(model.eventi);
            return eventi;

        }
        public IActionResult ExportToExcel()
        {
            byte[] fileContents;
            
            LogPodaci l = HttpContext.GetLogiraniUser();
            Radnik radnik = ctx.Radnik.Where(ra => ra.Osoba.LogPodaciId == l.Id).Include(r=>r.Osoba).SingleOrDefault();

            PrikazEvenataVM model = new PrikazEvenataVM { RadnikId = radnik.Id };
            model.eventi = ctx.RadnikEvent.Where(r => r.RadnikId == radnik.Id)
              .Select(r => new PrikazEvenataVM.Rows
              {
                  EventId = r.EventId,
                  NazivEventa = r.Event.Naziv,
                  DatumOdrzavanja = r.Event.DatumOdrzavanja.ToShortDateString(),
                  Grad = r.Event.ProstorOdrzavanja.Grad.Naziv,
                  ProstorOdrzavanjaIAdresa = r.Event.ProstorOdrzavanja.Naziv + " " + r.Event.ProstorOdrzavanja.Adresa,
                  RadnikEventId = r.Id,
                  Vrijeme = r.Event.VrijemeOdrzavanja,
                  UkupnoZaradaOdEventa = ctx.KupovinaTip.Where(k => k.Kupovina.EventId == r.EventId).Sum(k => k.Cijena)
              }).ToList();


            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");


                workSheet.Cells[1, 1].Value = "Radnik ";
                workSheet.Cells[1, 2].Value = radnik.Osoba.Ime+" "+radnik.Osoba.Prezime;

                workSheet.Cells[2, 1].Value = "Datum";
                workSheet.Cells[2, 2].Value = DateTime.Now.ToShortDateString();

              
                //Zaglavlje
                workSheet.Cells[4, 1].Value = "Naziv eventa";
                workSheet.Cells[4, 1].Style.Font.Size = 12;
                workSheet.Cells[4, 1].Style.Font.Bold = true;

                workSheet.Cells[4, 2].Value = "Datum održavanja";
                workSheet.Cells[4, 2].Style.Font.Size = 12;
                workSheet.Cells[4, 2].Style.Font.Bold = true;

                workSheet.Cells[4, 3].Value = "Vrijeme";
                workSheet.Cells[4, 3].Style.Font.Size = 12;
                workSheet.Cells[4, 3].Style.Font.Bold = true;

                workSheet.Cells[4, 4].Value = "Grad";
                workSheet.Cells[4, 4].Style.Font.Size = 12;
                workSheet.Cells[4, 4].Style.Font.Bold = true;

                workSheet.Cells[4, 5].Value = "Prostor održavanja - Adresa";
                workSheet.Cells[4, 5].Style.Font.Size = 12;
                workSheet.Cells[4, 5].Style.Font.Bold = true;

                workSheet.Cells[4, 6].Value = "Ukupna zarada od eventa (KM)";
                workSheet.Cells[4, 6].Style.Font.Size = 12;
                workSheet.Cells[4, 6].Style.Font.Bold = true;

                //Redovi
                int startRow = 5;
                foreach (var item in model.eventi)
                {
                    workSheet.Cells[startRow, 1].Value = item.NazivEventa;
                    workSheet.Cells[startRow, 2].Value = item.DatumOdrzavanja;
                    workSheet.Cells[startRow, 3].Value = item.Vrijeme;
                    workSheet.Cells[startRow, 4].Value = item.Grad;
                    workSheet.Cells[startRow, 5].Value = item.ProstorOdrzavanjaIAdresa;
                    workSheet.Cells[startRow, 6].Value = item.UkupnoZaradaOdEventa;
                    startRow++;
                }

                fileContents = package.GetAsByteArray();
            }

            if (fileContents == null || fileContents.Length == 0)
            {
                return NotFound();
            }

            return File(
                fileContents: fileContents,
                 contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                 fileDownloadName: "Eventi.xlsx"
                );
        }
        public IActionResult Detalji(int id)
        {
            if (HttpContext.GetLogiraniUser() == null)
            {
                return Redirect("/Prijava/Index");
            }
            if (id <= 0 || ctx.Event.Where(e=>e.Id==id).Any()==false)
            {
                return Redirect("/Modulradnik/Radnik/Index");
            }
            Event ev = ctx.Event.Where(e => e.Id == id)
                .Include(e=>e.ProstorOdrzavanja).SingleOrDefault();

            EventDetaljiVM model = new EventDetaljiVM
            {
                EventId = ev.Id,
                NazivEventa = ev.Naziv
            };
            model.TipoviProdaje = ctx.ProdajaTip.Where(p=>p.EventId==id)
                .Select(t => new EventDetaljiVM.TipProdaje
                {
                    TipKarte = t.TipKarte.ToString(),
                    UkupnoKarataTip = t.UkupnoKarataTip,
                    CijenaTip = t.CijenaTip,
                    ProdajaTipId = t.Id,
                    BrojProdatihKarataTip = t.BrojProdatihKarataTip,
                    BrojPreostalihKarata = (t.UkupnoKarataTip - t.BrojProdatihKarataTip),
                    ZaradaOdTipa = (t.CijenaTip * t.BrojProdatihKarataTip),

                }).ToList();

            return View(model); 
        }

        public IActionResult ExportDetaljiToExcel(int id)
        {
            if (id <= 0 || ctx.Event.Where(e => e.Id == id).Any() == false)
            {
                return Redirect("/Modulradnik/Radnik/Index");
            }
           
            byte[] fileContents;

            Event ev = ctx.Event.Where(e => e.Id == id)
               .Include(e => e.ProstorOdrzavanja).SingleOrDefault();

            EventDetaljiVM model = new EventDetaljiVM
            {
                EventId = ev.Id,
                NazivEventa = ev.Naziv
            };
            model.TipoviProdaje = ctx.ProdajaTip.Where(p => p.EventId == id)
                .Select(t => new EventDetaljiVM.TipProdaje
                {
                    TipKarte = t.TipKarte.ToString(),
                    UkupnoKarataTip = t.UkupnoKarataTip,
                    CijenaTip = t.CijenaTip,
                    ProdajaTipId = t.Id,
                    BrojProdatihKarataTip = t.BrojProdatihKarataTip,
                    BrojPreostalihKarata = (t.UkupnoKarataTip - t.BrojProdatihKarataTip),
                    ZaradaOdTipa = (t.CijenaTip * t.BrojProdatihKarataTip),

                }).ToList();

            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");


                workSheet.Cells[1, 1].Value = "Event ";
                workSheet.Cells[1, 2].Value = model.NazivEventa;

                workSheet.Cells[2, 1].Value = "Datum";
                workSheet.Cells[2, 2].Value = DateTime.Now.ToShortDateString();


                workSheet.Cells[4, 1].Value = "Tip karte";
                workSheet.Cells[4, 1].Style.Font.Size = 12;
                workSheet.Cells[4, 1].Style.Font.Bold = true;

                workSheet.Cells[4, 2].Value = "Ukupno za prodaju";
                workSheet.Cells[4, 2].Style.Font.Size = 12;
                workSheet.Cells[4, 2].Style.Font.Bold = true;

                workSheet.Cells[4, 3].Value = "Broj prodatih karata";
                workSheet.Cells[4, 3].Style.Font.Size = 12;
                workSheet.Cells[4, 3].Style.Font.Bold = true;

                workSheet.Cells[4, 4].Value = "Broj preostalih karata";
                workSheet.Cells[4, 4].Style.Font.Size = 12;
                workSheet.Cells[4, 4].Style.Font.Bold = true;

                workSheet.Cells[4, 5].Value = "Cijena";
                workSheet.Cells[4, 5].Style.Font.Size = 12;
                workSheet.Cells[4, 5].Style.Font.Bold = true;

                workSheet.Cells[4, 6].Value = "Zarada od tipa (KM)";
                workSheet.Cells[4, 6].Style.Font.Size = 12;
                workSheet.Cells[4, 6].Style.Font.Bold = true;

               
                int startRow = 5;
                foreach (var item in model.TipoviProdaje)
                {
                    workSheet.Cells[startRow, 1].Value = item.TipKarte;
                    workSheet.Cells[startRow, 2].Value = item.UkupnoKarataTip;
                    workSheet.Cells[startRow, 3].Value = item.BrojProdatihKarataTip;
                    workSheet.Cells[startRow, 4].Value = item.BrojPreostalihKarata;
                    workSheet.Cells[startRow, 5].Value = item.CijenaTip;
                    workSheet.Cells[startRow, 6].Value = item.ZaradaOdTipa;
                    startRow++;
                }

                fileContents = package.GetAsByteArray();
            }

            if (fileContents == null || fileContents.Length == 0)
            {
                return NotFound();
            }

            return File(
                fileContents: fileContents,
                 contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                 fileDownloadName: "DetajiEventa.xlsx"
                );
        }
    }
}