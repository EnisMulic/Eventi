using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class AdministratorVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [MaxLength(20)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [MaxLength(30)]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [RegularExpression(@"\+[0-9]{3}[\s][0-9]{2}[\s][0-9]{3}[\s][0-9]{3}", 
                ErrorMessage = "U formatu +387 xx xxx xxx")]
        public string Telefon { get; set; }

        [Required]
        [Remote(action: "IsEmailUnique", controller: "Administrator", areaName: "Administrator",
                AdditionalFields = "LogPodaciId",
                ErrorMessage = "Email Vec postoji")]
        public string Email { get; set; }

        [Required]
        [Remote(action: "IsUsernameUnique", controller: "Administrator", areaName: "Administrator",
                AdditionalFields = "LogPodaciId",
                ErrorMessage = "Username Vec postoji")]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Grad { get; set; }
        public int LogPodaciId { get; set; }
    }
}
