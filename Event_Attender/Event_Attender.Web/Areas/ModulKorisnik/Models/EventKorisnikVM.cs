using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.ModulKorisnik.Models
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
        public byte[] Slika { get; set; }

        public int KorisnikId { get; set; }
        public string KorisnikIme { get; set; }
        public string KorisnikPrezime { get; set; }
        public string KorisnikAdresa { get; set; }
        public string KorisnikGrad { get; set; }
        public string KorisnikBrojracun { get; set; }

        public bool IsLikean { get; set; }

        public List<TipProdaje> TipoviProdaje { get; set; }
        public class TipProdaje
        {
            public int ProdajaTipId { get; set; }
            public string TipKarte { get; set; }
            public int UkupnoKarataTip { get; set; }
            public int BrojProdatihKarataTip { get; set; }
            public float CijenaTip { get; set; }
            public bool PostojeSjedista { get; set; }
            public int BrojPreostalihKarata { get; set; }
        }

    }
}
