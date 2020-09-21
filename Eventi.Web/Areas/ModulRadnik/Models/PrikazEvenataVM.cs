using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.ModulRadnik.Models
{
    public class PrikazEvenataVM
    {
       public int RadnikId { get; set; }
        public List<Rows> eventi { get; set; }
        public class Rows
        {
            public int RadnikId { get; set; }
            public int EventId { get; set; }
            public int RadnikEventId { get; set; }
            public string NazivEventa { get; set; }
            public string DatumOdrzavanja { get; set; }
            public string Vrijeme { get; set; }
            public string Grad { get; set; }
            public string ProstorOdrzavanjaIAdresa { get; set; }
            public float UkupnoZaradaOdEventa { get; set; }
        }
    }
}
