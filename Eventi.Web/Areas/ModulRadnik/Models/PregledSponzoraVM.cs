using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.ModulRadnik.Models
{
    public class PregledSponzoraVM
    {
        public List<Rows> Redovi { get; set; } = new List<Rows>();

        public class Rows
        {
            public int Id { get; set; }
            public string NazivSponzora { get; set; }
            public string TelefonSponzora { get; set; }
            public string EmailSponzora { get; set; }
        }
    }
}
