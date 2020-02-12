using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class PromjenaPasswordaVM
    {
        //Admin id
        public int Id { get; set; }
        public int LogPodaciId { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [MinLength(8, ErrorMessage = "Minimalno 8 znakova")]
        [MaxLength(25)]
        [Remote(action: "IsOldPassword", controller: "Administrator", areaName: "Administrator",
                AdditionalFields = "LogPodaciId",
                ErrorMessage = "Password nije ispravan")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [MinLength(8, ErrorMessage = "Minimalno 8 znakova")]
        [MaxLength(25)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [MinLength(8, ErrorMessage = "Minimalno 8 znakova")]
        [MaxLength(25)]
        [Remote(action: "MatchNewPassword", controller: "Administrator", areaName: "Administrator",
                AdditionalFields = "NewPassword",
                ErrorMessage = "Password se ne podudara")]
        public string NewPasswordConfirmed { get; set; }
    }
}
