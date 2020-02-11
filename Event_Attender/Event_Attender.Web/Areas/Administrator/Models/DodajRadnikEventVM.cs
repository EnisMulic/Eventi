using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class DodajRadnikEventVM
    {
        public int EventId { get; set; }
        public string EventNaziv { get; set; }
        public string Datum { get; set; }
        public string Vrijeme { get; set; }
        public string ProstorOdrzavanja { get; set; }
        public int RadnikId { get; set; }
        public List<SelectListItem> Radnici { get; set; }
    }
}
