using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class SponzorVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
    }
}
