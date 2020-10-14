using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Client.Models
{
    public class EventKorisnikVM
    {
        public int EventId { get; set; }
        public string Naziv { get; set; }
        public string Kategorija { get; set; }
        public string Opis { get; set; }
        public string ProstorOdrzavanjaNaziv { get; set; }
        public string ProstorOdrzavanjaAdresa { get; set; }
        public string ProstorOdrzavanjaGrad { get; set; }
        public string DatumOdrzavanja { get; set; }  
        public string VrijemeOdrzavanja { get; set; }
        public string Slika { get; set; }
        public int KorisnikId { get; set; }
        public string KorisnikIme { get; set; }
        public string KorisnikPrezime { get; set; }
        public string KorisnikAdresa { get; set; }
        public string KorisnikGrad { get; set; }
        public string KorisnikBrojracun { get; set; }

        public bool IsLikean { get; set; }

       
    }
}
