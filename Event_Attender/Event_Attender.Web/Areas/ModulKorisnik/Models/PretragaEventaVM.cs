using Event_Attender.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.ModulKorisnik.Models
{
    public class PretragaEventaVM
    {
        public List<Rows> Eventi { get; set; }
         public int KorisnikId { get; set; } // ili u akciji getLogiraniUser ?
        
       // bool IsLikean { get; set; }   // ?
        public class Rows
        {
            public int EventId { get; set; }
            public string Naziv { get; set; }
            public string Kategorija { get; set; }
            public string Opis { get; set; }
            public string ProstorOdrzavanjaNaziv { get; set; }
            public string ProstorOdrzavanjaAdresa { get; set; }
            public string ProstorOdrzavanjaGrad { get; set; }
            public string DatumOdrzavanja { get; set; }  // Date?
            public string VrijemeOdrzavanja { get; set; }
            public byte[] Slika { get; set; }

        }

         
    }
}
