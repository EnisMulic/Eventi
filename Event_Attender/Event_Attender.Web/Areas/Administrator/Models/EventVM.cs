using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class EventVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        
        [DisplayName("Datum održavanja")]
        public DateTime DatumOdrzavanja { get; set; }
        
        [DisplayName("Vrijeme održavanja")]
        public string VrijemeOdrzavanja { get; set; }
        public Kategorija Kategorija { get; set; }
        public bool IsOdobren { get; set; }
        public bool IsOtkazan { get; set; }
        public byte[] Slika { get; set; }

        public int OrganizatorId { get; set; }

        [DisplayName("Naziv organizatora")]
        public string OrganizatorNaziv { get; set; }

        public int AdministratorId { get; set; }

        public string AdministratorNaziv { get; set; }
        public string AdministratorIme { get; set; }

        public string AdministratorPrezime { get; set; }
        public int ProstorOdrzavanjaId { get; set; }

        [DisplayName("Prostor održavanja")]
        public string ProstorOdrzavanjaNaziv { get; set; }

        public List<SelectListItem> Organizatori { get; set; }
        public List<SelectListItem> Administratori { get; set; }
        public List<SelectListItem> Prostori { get; set; }
    }
}
