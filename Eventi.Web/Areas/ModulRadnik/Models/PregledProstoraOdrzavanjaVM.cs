using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.ModulRadnik.Models
{
    public class PregledProstoraOdrzavanjaVM
    {
        public List<Rows> Redovi { get; set; } = new List<Rows>();

        public class Rows
        {
            public int ProstorOdrzavanjaId { get; set; }
            public string Naziv { get; set; }
            public string Adresa { get; set; }
            public string TipProstora { get; set; }
            public string Grad { get; set; }

        }
    }
}
