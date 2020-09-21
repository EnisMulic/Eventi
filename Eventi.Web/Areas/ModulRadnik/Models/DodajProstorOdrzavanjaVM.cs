using Event_Attender.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.ModulRadnik.Models
{
    public class DodajProstorOdrzavanjaVM
    {
        public string Naziv { get; set; }
        public string Adresa { get; set; }

        public int TipProstoraOdabir { get; set; }
        public int GradOdabir { get; set; }

        public List<SelectListItem> TipProstori { get; set; } = new List<SelectListItem> {
            new SelectListItem {Value="0",Text="Sala"},
            new SelectListItem {Value="1",Text="Dvorana"},
            new SelectListItem {Value="2",Text="Stadion"},
        };
        public List<SelectListItem> Gradovi { get; set; } = new List<SelectListItem>();


    }
}
