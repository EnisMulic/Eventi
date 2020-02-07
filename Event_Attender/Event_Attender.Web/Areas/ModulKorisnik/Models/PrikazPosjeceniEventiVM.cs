using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.ModulKorisnik.Models
{
    public class PrikazPosjeceniEventiVM
    {
        public int page { get; set; }
        public int KupovinaId { get; set; }
        public int EventId { get; set; }
        public string Naziv { get; set; }
        public string Kategorija { get; set; }
        public string ProstorOdrzavanjaGrad { get; set; }
        public string DatumOdrzavanja { get; set; }
        public string VrijemeOdrzavanja { get; set; }
        public string Slika { get; set; }
        public int KorisnikId { get; set; }
        public float UkupnoPlaceno { get; set; }
    }
}
