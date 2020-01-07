using Event_Attender.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class OrganizatorVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        public string Naziv { get; set; }

        [Remote(action: "IsUsernameUnique", controller: "Administrator", areaName: "Administrator", 
                ErrorMessage = "Username Vec postoji")]
        [Required(ErrorMessage = "Obavezno polje")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [MinLength(8, ErrorMessage = "Minimalno 8 znakova")]
        [MaxLength(25)]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Obavezno polje")]

        [Remote(action: "IsEmailUnique", controller: "Administrator", areaName: "Administrator",
                ErrorMessage = "Email Vec postoji")]
        [Required(ErrorMessage = "Obavezno polje")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Obavezno polje")]
        [RegularExpression(@"\+[0-9]{3}[\s][0-9]{3}[\s][0-9]{3}[\s][0-9]{3}", 
                           ErrorMessage = "U formatu +387 xxx xxx xxx")]
        public string Telefon { get; set; }

        [Required]
        public int GradId { get; set; }
        public string GradNaziv { get; set; }

        public List<SelectListItem> Gradovi { get; set; }
    }
}
