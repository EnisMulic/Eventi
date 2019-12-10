using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class SponzorDisplayVM
    {
        public List<SponzorVM> Sponzori { get; set; } = new List<SponzorVM>();
        public SponzorVM OnDisplay { get; set; } = new SponzorVM();
    }
}
