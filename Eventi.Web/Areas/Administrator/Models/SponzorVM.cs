using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Administrator.Models
{
    public class SponzorVM
    {
        public int Id { get; set; }
        [Required]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [RegularExpression(@"\+[0-9]{3}[\s][0-9]{2}[\s][0-9]{3}[\s][0-9]{3}", 
                ErrorMessage = "U formatu +387 xx xxx xxx")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [EmailAddress(ErrorMessage = "Niste unijeli pravilan format")]
        public string Email { get; set; }
    }
}
