using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class DrzavaDisplayVM
    {
        public List<DrzavaVM> Drzave { get; set; } = new List<DrzavaVM>();
        public DrzavaVM OnDisplay { get; set; } = new DrzavaVM();
    }
}
