using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class ProstorOdrzavanjaDisplayVM
    {
        public List<ProstorOdrzavanjaVM> Prostori { get; set; } = new List<ProstorOdrzavanjaVM>();
        public ProstorOdrzavanjaVM OnDisplay { get; set; } = new ProstorOdrzavanjaVM();
        public List<GradVM> Gradovi { get; set; } = new List<GradVM>();
    }
}
