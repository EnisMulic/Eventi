using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.ModulRadnik.Models
{
    public class DodajSponzorFormVM
    {
        public string NazivSponzora { get; set; }
        [RegularExpression(@"(0[1-9]{2}[-][0-9]{3}[-][0-9]{3}|0[1-9]{1}0[-][0-9]{3}[-][0-9]{4})")]
        public string TelefonSponzora { get; set; }
        [RegularExpression(@"[a-z0-9]{3,10}[.]{1}[a-z0-9]{3,10}[@]{1}(gmail|hotmail|outlook|yahoo|edu.fit)(.com|.ba)")]
        public string EmailSponzora { get; set; }
    }
}
