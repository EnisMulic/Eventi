using Eventi.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Client.Models
{
    public class KorisnikPodaciVM
    {   
        public int Id { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [MaxLength(40)]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [MaxLength(10)]
        [RegularExpression(@"\d{1,10}", ErrorMessage = "Niste unijeli pravilan format")]
        public string PostanskiBroj { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [RegularExpression(@"[0-9]{14,19}", ErrorMessage = "Neispravan format kartice")]
        public string BrojKreditneKartice { get; set; } 

        [Required(ErrorMessage = "Obavezno polje")]
        [MaxLength(20)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [MaxLength(30)]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [RegularExpression(@"\+[0-9]{3}[\s][0-9]{2}[\s][0-9]{3}[\s][0-9]{3}", ErrorMessage = "U formatu +387 xx xxx xxx")]
        public string Telefon { get; set; }
        
        [Required(ErrorMessage = "Obavezno polje")]
        [MaxLength(20)]
       // [RegularExpression("[^0-9]", ErrorMessage = "Morate unijeti naziv nekog grada")]
        public string Grad { get; set; } 
        public int gradId { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [EmailAddress(ErrorMessage = "Niste unijeli pravilan format")]
        public string Email { get; set; }
        public int DrzavaId { get; set; }
        public List<SelectListItem> drzave { get; set; }

        public string Slika { get; set; }
    }
}
