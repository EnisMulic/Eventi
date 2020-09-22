using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.ModulRadnik.Models
{
    public class EventDetaljiVM
    {
        public int EventId { get; set; }
        public string NazivEventa { get; set; }
       
        public List<TipProdaje> TipoviProdaje { get; set; }
        public class TipProdaje
        {
            public int ProdajaTipId { get; set; }
            public string TipKarte { get; set; }
            public int UkupnoKarataTip { get; set; }
            public int BrojProdatihKarataTip { get; set; }
            public float CijenaTip { get; set; }
            public int BrojPreostalihKarata { get; set; }
            public float ZaradaOdTipa { get; set; }
        }
    }
}
