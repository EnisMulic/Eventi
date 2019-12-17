using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class KorisnikDisplayVM
    {
        public List<KorisnikVM> Korisnici { get; set; } = new List<KorisnikVM>();
        public KorisnikVM OnDisplay { get; set; } = new KorisnikVM();
        public List<GradVM> Gradovi { get; set; } = new List<GradVM>();
    }
}
