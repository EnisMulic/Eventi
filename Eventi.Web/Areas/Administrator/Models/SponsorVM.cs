using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Administrator.Models
{
    public class SponsorVM
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"\+[0-9]{3}[\s][0-9]{2}[\s][0-9]{3}[\s][0-9]{3}", 
                ErrorMessage = "In Format +387 xx xxx xxx")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
