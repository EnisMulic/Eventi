using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Administrator.Models
{
    public class AdministratorVM
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\+[0-9]{3}[\s][0-9]{2}[\s][0-9]{3}[\s][0-9]{3}", 
                ErrorMessage = "In format +387 xx xxx xxx")]
        public string PhoneNumber { get; set; }


        //[Remote(action: "IsEmailUnique", controller: "Administrator", areaName: "Administrator",
        //        AdditionalFields = "LogPodaciId",
        //        ErrorMessage = "Email Vec postoji")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //[Remote(action: "IsUsernameUnique", controller: "Administrator", areaName: "Administrator",
        //        AdditionalFields = "LogPodaciId",
        //        ErrorMessage = "Username Vec postoji")]
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public int AccountID { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(25)]
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public string NewPasswordConfirmed { get; set; }
    }
}
