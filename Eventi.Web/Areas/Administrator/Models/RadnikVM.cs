using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Administrator.Models
{
    public class RadnikVM
    {
        public int Id { get; set; }

        public int LogPodaciId { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [MaxLength(20)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [MaxLength(30)]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [Remote(action: "IsUsernameUnique", controller: "Administrator", areaName: "Administrator",
                AdditionalFields = "LogPodaciId",
                ErrorMessage = "Username Vec postoji")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [EmailAddress(ErrorMessage = "Niste unijeli pravilan format")]
        [Remote(action: "IsEmailUnique", controller: "Administrator", areaName: "Administrator",
                AdditionalFields = "LogPodaciId",
                ErrorMessage = "Email Vec postoji")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [MinLength(8, ErrorMessage = "Minimalno 8 znakova")]
        [MaxLength(25)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Obavezno polje")]
        [RegularExpression(@"\+[0-9]{3}[\s][0-9]{2}[\s][0-9]{3}[\s][0-9]{3}", 
                ErrorMessage = "U formatu +387 xx xxx xxx")]
        public string Telefon { get; set; }

        [Required]
        public int GradId { get; set; }
        public string GradNaziv { get; set; }
        public List<SelectListItem> Gradovi { get; set; }

        public string ImePrezime
        {
            get { return Ime + " " + Prezime; }
        }
    }
}
