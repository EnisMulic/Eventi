using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Administrator.Models
{
    public class ChangePasswordVM
    {
        //Admin id
        public int ID { get; set; }
        public int AccountID { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(25)]
        [Remote(action: "IsOldPassword", controller: "Administrator", areaName: "Administrator",
                AdditionalFields = "AccountID",
                ErrorMessage = "Wrong Password")]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(25)]
        public string NewPassword { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(25)]
        [Remote(action: "MatchNewPassword", controller: "Administrator", areaName: "Administrator",
                AdditionalFields = "NewPassword",
                ErrorMessage = "Passwords do not match")]
        public string NewPasswordConfirmed { get; set; }
    }
}
