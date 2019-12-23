using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class OrganizatorVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public int GradId { get; set; }
        public string GradNaziv { get; set; }

        public List<SelectListItem> Gradovi { get; set; }
    }
}
