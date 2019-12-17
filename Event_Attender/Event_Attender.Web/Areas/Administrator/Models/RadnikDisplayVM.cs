using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class RadnikDisplayVM
    {
        public List<RadnikVM> Radnici { get; set; } = new List<RadnikVM>();
        public RadnikVM OnDisplay { get; set; } = new RadnikVM();
        public List<GradVM> Gradovi { get; set; } = new List<GradVM>();
    }
}
