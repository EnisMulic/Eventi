using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class DrzavaVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obavezno polje")]
        public string Naziv { get; set; }
    }
}
