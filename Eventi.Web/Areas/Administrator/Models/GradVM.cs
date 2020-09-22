using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Administrator.Models
{
    public class GradVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obavezno polje")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Obavezno polje")]
        public int DrzavaId { get; set; }
        public string DrzavaNaziv { get; set; }
        public List<SelectListItem> Drzave { get; set; }
    }
}
