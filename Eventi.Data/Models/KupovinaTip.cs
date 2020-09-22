using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Data.Models
{
    public class KupovinaTip
    {
        public int Id { get; set; }

        public int KupovinaId { get; set; }
        public Kupovina Kupovina { get; set; }

        public TipKarte TipKarte { get; set; }  
        public int BrojKarata { get; set; }  // odredjuje korisnik
        public float Cijena { get; set; } // BrojKarta*ProdajaTip.CijenaTip

        ////ProdajaTip ?  // probati
        public int? ProdajaTipId { get; set; }
        public ProdajaTip ProdajaTip { get; set; }

    }
}
