using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventi.Web.Areas.Administrator.Models
{
    public class OrganizerVM
    {
        public int ID { get; set; }

        public int AccountID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Min 8 Characters")]
        [MaxLength(25)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"\+[0-9]{3}[\s][0-9]{2}[\s][0-9]{3}[\s][0-9]{3}", 
                ErrorMessage = "In Format +387 xx xxx xxx")]
        public string PhoneNumber { get; set; }

        [Required]
        public int? CityID { get; set; }
        public string CityName { get; set; }

        public List<SelectListItem> Cities { get; set; }
    }
}
