using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventi.Web.Areas.Client.Models
{
    public class ClientDetailsVM
    {   
        public int ID { get; set; }

        [Required]
        [MaxLength(40)]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{14,19}", ErrorMessage = "Wrong format")]
        public string CreditCardNumber { get; set; } 

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\+[0-9]{3}[\s][0-9]{2}[\s][0-9]{3}[\s][0-9]{3}", ErrorMessage = "In format +387 xx xxx xxx")]
        public string PhoneNumber { get; set; }
        
        [Required]
        public string City { get; set; } 
        public int CityID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public int CountryID { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public string Image { get; set; }
    }
}
