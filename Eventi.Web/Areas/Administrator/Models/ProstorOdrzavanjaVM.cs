using Eventi.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Administrator.Models
{
    public class ProstorOdrzavanjaVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
       
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        public TipProstoraOdrzavanja TipProstoraOdrzavanja { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        public int GradId { get; set; }
        public string GradNaziv { get; set; }

        public List<SelectListItem> Gradovi { get; set; }
    }
}
