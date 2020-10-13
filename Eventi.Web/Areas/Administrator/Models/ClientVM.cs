using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventi.Web.Areas.Administrator.Models
{
    public class ClientVM
    {
        public int ID { get; set; }
        public int AccountID { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        //[Remote(action: "IsUsernameUnique", controller: "Administrator", areaName: "Administrator", 
        //        AdditionalFields = "LogPodaciId", 
        //        ErrorMessage = "Username Vec postoji")]
        [Required]
        public string Username { get; set; }

        //[Remote(action: "IsEmailUnique", controller: "Administrator", areaName: "Administrator", 
        //        AdditionalFields = "LogPodaciId",
        //        ErrorMessage = "Email Vec postoji")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"\+[0-9]{3}[\s][0-9]{2}[\s][0-9]{3}[\s][0-9]{3}", ErrorMessage = "In format +387 xx xxx xxx")]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(40)]
        public string Address { get; set; }

        public string CreditCardNumber { get; set; }

        public int CityID { get; set; }
        public string CityName { get; set; }

        public List<SelectListItem> Cities { get; set; }
    }
}
