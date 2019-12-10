using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class GradDisplayVM
    {
        public List<GradVM> Gradovi { get; set; } = new List<GradVM>();
        public GradVM OnDisplay { get; set; } = new GradVM();
    }
}
