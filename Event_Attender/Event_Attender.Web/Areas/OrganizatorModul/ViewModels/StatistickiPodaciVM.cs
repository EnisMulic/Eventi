using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.OrganizatorModul.ViewModels
{
    public class StatistickiPodaciVM
    {

        public List<Rows> Redovi { get; set; } = new List<Rows>();

        public class Rows
        {
            public string NazivEventa { get; set; }
            public int UkupnoBrojProdatihKarata { get; set; }
            public float UkupanPrihodPoEventu { get; set; }
            public string ProsjecnaOcjenaEventa { get; set; }
        }
    }
}
