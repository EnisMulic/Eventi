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
            return View();
        }

        public string GetProizvodi()
        {
            LogPodaci l = HttpContext.GetLogiraniUser();

            Radnik radnik = ctx.Radnik.Where(ra => ra.Osoba.LogPodaciId == l.Id).SingleOrDefault();
            //if (r == null)
            //{
            //    return null;
            //}
            PrikazEvenataVM model = new PrikazEvenataVM();
            model.eventi = ctx.RadnikEvent.Where(r => r.Id == radnik.Id)
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
            model.eventi = ctx.RadnikEvent.Where(r => r.Id == radnik.Id)
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
        //public IActionResult Detalji(int id)
        //{
        //    Event ev = ctx.Event.Where(e => e.Id == id).SingleOrDefault();

        //    return View();
        //}
    }
}