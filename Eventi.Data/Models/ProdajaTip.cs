using System;
using System.Collections.Generic;
using System.Text;

public enum TipKarte {Vip, Parter, Tribina, Obicna}
namespace Eventi.Data.Models
{
    public class ProdajaTip
    {
        public int Id { get; set; }
        public TipKarte TipKarte { get; set; }
        public int UkupnoKarataTip { get; set; }
        public int BrojProdatihKarataTip { get; set; }
        public float CijenaTip { get; set; }
        public bool PostojeSjedista { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

    }
}
