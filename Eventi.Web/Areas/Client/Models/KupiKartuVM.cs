using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.ModulKorisnik.Models
{
    public class KupiKartuVM
    {
        public int EventId { get; set; }
        public int KorisnikId { get; set; }
        public string KorisnikIme { get; set; }
        public string KorisnikPrezime { get; set; }
        public string KorisnikAdresa { get; set; }
        public string KorisnikGrad { get; set; }
        public string KorisnikBrojracun { get; set; }

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
            public bool IsRasprodano { get; set; }
        }
        public int OdabraniTipProdajeId { get; set; }
        public int OdabranBrKarata { get; set; }
        
    }
}
