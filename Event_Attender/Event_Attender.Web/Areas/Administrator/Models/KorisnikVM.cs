using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class KorisnikVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obavezno polje")]
        [MaxLength(20)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [MaxLength(30)]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [Remote(action: "VerifyUserName", controller: "Guest", areaName: "ModulGuest",
            ErrorMessage = "Ovaj username je vec u upotrebi")]  // probati
        public string Username { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [EmailAddress(ErrorMessage = "Niste unijeli pravilan format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [MinLength(8, ErrorMessage = "Minimalno 8 znakova")]
        [MaxLength(25)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [RegularExpression(@"\+[0-9]{3}[\s][0-9]{3}[\s][0-9]{3}[\s][0-9]{3}",
            ErrorMessage = "U formatu +387 xxx xxx xxx")]
        public string Telefon { get; set; }

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
        public int GradId { get; set; }
        public string GradNaziv { get; set; }

        public List<SelectListItem> Gradovi { get; set; }

        public string ImePrezime
        {
            get
            {
                return Ime + " " + Prezime;
            }
        }

    }
}
