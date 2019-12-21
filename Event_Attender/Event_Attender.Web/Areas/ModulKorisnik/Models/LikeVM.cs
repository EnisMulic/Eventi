using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.ModulKorisnik.Models
{
    public class LikeVM
    {
        public int KorisnikId{get;set; }
        public int EventId { get; set; }

        public DateTime Datum { get; set; }
    }
}
