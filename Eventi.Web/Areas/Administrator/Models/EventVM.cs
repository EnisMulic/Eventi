using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Administrator.Models
{
    public class EventVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        public string Naziv { get; set; }
        public string Opis { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [DisplayName("Datum održavanja")]
        public DateTime DatumOdrzavanja { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [DisplayName("Vrijeme održavanja")]
        public string VrijemeOdrzavanja { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        public Kategorija Kategorija { get; set; }
        public bool IsOdobren { get; set; }
        public bool IsOtkazan { get; set; }

        
        public string Slika { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        public int OrganizatorId { get; set; }

        [DisplayName("Naziv organizatora")]
        public string OrganizatorNaziv { get; set; }

        public int AdministratorId { get; set; }

        public string AdministratorNaziv { get; set; }
        public string AdministratorIme { get; set; }

        public string AdministratorPrezime { get; set; }
        public int ProstorOdrzavanjaId { get; set; }

        [DisplayName("Prostor održavanja")]
        [Required]
        public string ProstorOdrzavanjaNaziv { get; set; }

        public List<SelectListItem> Organizatori { get; set; }
        public List<SelectListItem> Administratori { get; set; }
        public List<SelectListItem> Prostori { get; set; }
    }
}
