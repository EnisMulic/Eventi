using Event_Attender.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class IzvodjacVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public TipIzvodjaca TipIzvodjaca { get; set; }
    }
}
