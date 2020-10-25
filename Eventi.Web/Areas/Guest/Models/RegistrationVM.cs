using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventi.Web.Areas.Guest.Models
{
    public class RegistrationVM
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]   
        [RegularExpression(@"\+[0-9]{3}[\s][0-9]{2}[\s][0-9]{3}[\s][0-9]{3}", ErrorMessage ="In format +387 xx xxx xxx")]
        public string PhoneNumber { get; set; }

        [Required]
        public int CountryID { get; set; }

        public int CityID { get; set; }  

        [Required]
        [MaxLength(40)]
        public string Address { get; set; }

        [Required] 
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(40)]
        public string Username { get; set; }  

        [Required]
        [MinLength(8)]
        [MaxLength(25)]
        public string Password { get; set; }
          
        [Required]     
        [RegularExpression(@"[0-9]{14,19}")]
        public string CreditCardNumber{ get; set; }

        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Cities { get; set; }
    }
}
